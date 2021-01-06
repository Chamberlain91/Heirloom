using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

using Meadows.Android.EGL;
using Meadows.Drawing;
using Meadows.Drawing.OpenGLES;
using Meadows.Mathematics;

namespace Meadows.Android
{
    internal sealed class ESAndroidGraphicsBackend : ESGraphicsBackend
    {
        internal readonly EglContext EglContext;

        private readonly List<ESGraphicsContext> _graphics = new List<ESGraphicsContext>();

        public ESAndroidGraphicsBackend(MultisampleQuality multisample)
        {
            Log.Info($"[EGL] Creating OpenGL ES 3.0 Context");

            // Find best configuration for 24 bit with 8 bit stencil
            var configs = Egl.ChooseConfigs(new EglConfigAttributes
            {
                RedBits = 8,
                GreenBits = 8,
                BlueBits = 8,
                AlphaBits = 0,
                DepthBits = 0,
                StencilBits = 8,
                Samples = 0
            });

            var numSamples = (int) multisample;
            foreach (var config in configs.OrderByDescending(c => c.Attributes.Samples))
            {
                // This configuration is the best matching configuration.
                // It has the most samples
                if (config.Attributes.Samples <= numSamples)
                {
                    // Create OpenGL ES 3.0 Context
                    EglContext = Egl.CreateContext(config);
                    break;
                }
            }
        }

        internal ESGraphicsContext CreateGraphics(GraphicsView view, bool vsync)
        {
            var graphics = new ESSurfaceGraphicsContext(this, view, vsync);
            lock (_graphics) { _graphics.Add(graphics); }
            return graphics;
        }

        internal void RemoveGraphics(ESGraphicsContext graphics)
        {
            lock (_graphics)
            {
                _graphics.Remove(graphics);
            }
        }

        protected internal override void Invoke(Action action)
        {
            // TODO: Properly ensure we are unable to invoke when the application is terminating
            //if (Current?.IsDisposed ?? false)
            //{
            //    Log.Error("Unable to schedule action on GL thread. Backend has been disposed.");
            //}

            // Does the current thread happen to be a graphics thread?
            if (ESGraphicsContext.IsGraphicsThread)
            {
                action();
            }
            else
            {
                // Find a graphics thread
                lock (_graphics)
                {
                    // Try to invoke on an existing graphics thread
                    foreach (var context in _graphics)
                    {
                        // If graphics context is still initialized...
                        if (context.IsInitialized)
                        {
                            // Invoke action on that thread and exit
                            context.Invoke(action);
                            return;
                        }
                    }
                }

                // 
                throw new NotImplementedException("Unable to invoke without graphics thread");
            }
        }

        internal sealed class ESSurfaceGraphicsContext : ESGraphicsContext
        {
            private readonly ESAndroidGraphicsBackend _backend;
            private readonly GraphicsView _view;
            private readonly bool _vsync;

            public ESSurfaceGraphicsContext(ESAndroidGraphicsBackend backend, GraphicsView view, bool vsync)
                : base(backend, view)
            {
                _backend = backend ?? throw new ArgumentNullException(nameof(backend));
                _view = view ?? throw new ArgumentNullException(nameof(view));

                // 
                _vsync = vsync;
            }

            protected override void MakeCurrent()
            {
                // Load ES functions
                GLES.LoadFunctions(Egl.GetProcAddress);

                // Waits until surface reference is known
                Log.Warning("Waiting For EGL Surface");
                SpinWait.SpinUntil(() => _view.EglSurface != null);
                Log.Warning("Acquired EGL Surface");

                // Makes context current on calling thread
                Egl.MakeCurrent(_view.EglSurface, _backend.EglContext);

                // Configure swap interval
                _view.EglSurface?.Display.SetSwapInterval(_vsync ? 1 : 0);

                // Ensure backend is properly initialized (ie, compile default shaders, etc)
                InitializeBackend();

                // Keep minimum allowable multisample level. We have to set this here since the capabilities aren't detected until
                // the backend is initialized. On desktop, the backend is initialized promptly so this deferred assignment does not occur.
                Screen.Surface.Multisample = Calc.Min(GraphicsBackend.Current.Capabilities.MaxSupportedMultisample, Screen.Surface.Multisample);

                // Enable quality MSAA
                if (Screen.Surface.Multisample > MultisampleQuality.None)
                {
                    // Load 'glMinSampleShading', this is a GL4 function that lets the user control
                    // the behaviour of multisampling. This could be cached... but this way makes
                    // it pretty seamless to include.
                    var glMinSampleShader = LoadFunction<MinSampleShading>("glMinSampleShading");

                    // Enable multisampling explicity. From what I've read this is
                    // should already be enabled but I thought it good to enable it myself.
                    GLES.Enable((EnableCap) 0x809D); // GL_MULTISAMPLE

                    // If we are running a version that can support sample shading
                    // enable it and set the rate to 100%. This allows multisampling
                    // to affect every pixel instead of just the geometry edges.
                    if (glMinSampleShader != null)
                    {
                        GLES.Enable((EnableCap) 0x8C36); // GL_SAMPLE_SHADING
                        glMinSampleShader(1F);
                    }
                }
            }

            protected override void SwapBuffers()
            {
                // Swap buffers (on the gl thread)
                Invoke(blocking: false, action: () =>
                {
                    if (_view.EglSurface == null) { Log.Warning("Failed to swap buffers! (no surface)"); }
                    else if (_view.EglSurface.SwapBuffers() == false) { Log.Warning("Failed to swap buffers!"); }
                });
            }

            protected override void Dispose(bool disposing)
            {
                _backend.RemoveGraphics(this);
                base.Dispose(disposing);
            }

            private delegate void MinSampleShading(float rate);

            private static T LoadFunction<T>(string name) where T : Delegate
            {
                var addr = Egl.GetProcAddress(name);
                return addr != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<T>(addr) : null;
            }
        }
    }
}
