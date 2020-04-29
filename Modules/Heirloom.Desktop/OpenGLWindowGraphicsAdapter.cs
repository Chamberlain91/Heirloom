using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

using Heirloom.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLWindowGraphicsAdapter : OpenGLGraphicsAdapter, IWindowGraphicsFactory
    {
        private readonly List<OpenGLGraphics> _graphics = new List<OpenGLGraphics>();

        public Graphics CreateGraphics(Window window, Surface surface, bool vsync)
        {
            var graphics = new OpenGLWindowGraphics(this, window, surface, vsync);
            lock (_graphics) { _graphics.Add(graphics); }
            return graphics;
        }

        private void RemoveGraphics(OpenGLWindowGraphics graphics)
        {
            lock (_graphics)
            {
                _graphics.Remove(graphics);
            }
        }

        #region Invoke

        protected internal override T Invoke<T>(Func<T> function)
        {
            var val = default(T);
            Invoke(() =>
            {
                val = function();
                return;
            });
            return val;
        }

        protected internal override void Invoke(Action function)
        {
            if (Adapter?.IsDisposed ?? false)
            {
                Log.Error("Unable to schedule action on GL thread. Adapter has been disposed.");
            }

            lock (_graphics)
            {
                // Try to invoke on an existing graphics thread
                foreach (var gfx in _graphics)
                {
                    // If graphics context is still initialized...
                    if (gfx.IsInitialized)
                    {
                        // Invoke action on that thread and exit
                        gfx.Invoke(function);
                        return;
                    }
                }
            }

            // Was unable to invoke on a graphics thread, so we will temporarily make the window
            // events thread a graphics thread to invoke the function.
            Application.Invoke(() =>
            {
                // Make the share context current here
                Glfw.MakeContextCurrent(Application.ShareContext);

                // Execute function and keep return value
                function();

                // Release context from thread. We want it not associated with any thread.
                Glfw.MakeContextCurrent(WindowHandle.None);
            });
        }

        #endregion

        private sealed class OpenGLWindowGraphics : OpenGLGraphics
        {
            private readonly OpenGLWindowGraphicsAdapter _adapter;
            private readonly Window _window;
            private readonly bool _vsync;

            public OpenGLWindowGraphics(OpenGLWindowGraphicsAdapter adapter, Window window, Surface surface, bool vsync)
                : base(surface)
            {
                _adapter = adapter;
                _window = window ?? throw new ArgumentNullException(nameof(window));
                _vsync = vsync;

                // Set initial default surface size
                DefaultSurface.SetSize(_window.FramebufferSize);

                // Whenever the window framebuffer is resized, also update the
                // viewport and default surface. If the window is first created,
                // we use this event as a "window is ready" event to launch the
                // OpenGL thread.
                _window.FramebufferResized += _ =>
                {
                    DefaultSurface.SetSize(_window.FramebufferSize);
                };

                // Run OpenGL thread
                StartThread();
            }

            protected override void MakeCurrent()
            {
                // Waits until window reference is known
                SpinWait.SpinUntil(() => _window != null);

                // Makes context current on calling thread
                Glfw.MakeContextCurrent(_window.Handle);

                // Configure swap interval
                Glfw.SetSwapInterval(_vsync ? 1 : 0);

                // Enable quality MSAA
                if (DefaultSurface.Multisample > MultisampleQuality.None)
                {
                    // Load glMinSampleShading, this could be cached... but this way makes it pretty
                    // seamless to include.
                    var glMinSampleShader = LoadFunction<MinSampleShading>("glMinSampleShading");

                    // Enable multisampling explicity. From what I've read this is
                    // should already be enabled but I thought it good to enable it myself.
                    GL.Enable((EnableCap) 0x809D); // GL_MULTISAMPLE

                    // If we are running a version that can support sample shading
                    // enable it and set the rate to 100%. This allows multisampling
                    // to affect every pixel instead of just the geometry edges.
                    if (glMinSampleShader != null)
                    {
                        GL.Enable((EnableCap) 0x8C36); // GL_SAMPLE_SHADING
                        glMinSampleShader(1F);
                    }
                }
            }

            protected override void SwapBuffers()
            {
                // Swap buffers (on the gl thread)
                Invoke(() => Glfw.SwapBuffers(_window.Handle), false);
            }

            protected override void Dispose(bool disposeManaged)
            {
                _adapter.RemoveGraphics(this);
                base.Dispose(disposeManaged);
            }

            private delegate void MinSampleShading(float rate);

            private static T LoadFunction<T>(string name) where T : Delegate
            {
                var ptr = Glfw.GetProcAddress(name);
                return ptr != null ? Marshal.GetDelegateForFunctionPointer<T>(ptr)
                                   : null;
            }
        }
    }
}
