using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

using Heirloom.Desktop.GLFW;
using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class ESWindowGraphicsBackend : ESGraphicsBackend
    {
        private readonly List<ESGraphicsContext> _graphics = new();

        internal ESGraphicsContext CreateGraphics(Window window, bool vsync)
        {
            var graphics = new ESWindowGraphicsContext(this, window, vsync);
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
                // The application has a global GL context, we will schedule GL work there.
                // This context is not associated to any graphics context, so we should avoid
                // rendering and changing state. Just the allocation and manipulation of objects.
                Application.Invoke(() =>
                {
                    // Make the share context current here
                    Glfw.MakeContextCurrent(Application.ShareContext);

                    // Execute action
                    action();

                    // Release context from thread. We want it not associated with any thread.
                    Glfw.MakeContextCurrent(WindowHandle.None);
                });
            }
        }

        internal sealed class ESWindowGraphicsContext : ESGraphicsContext
        {
            private readonly ESWindowGraphicsBackend _backend;
            private readonly Window _window;
            private readonly bool _vsync;

            public ESWindowGraphicsContext(ESWindowGraphicsBackend backend, Window window, bool vsync)
                : base(backend, window)
            {
                _backend = backend;
                _window = window ?? throw new ArgumentNullException(nameof(window));
                _vsync = vsync;

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

            internal override void SwapBuffers()
            {
                // Swap buffers (on the gl thread)
                Invoke(() => Glfw.SwapBuffers(_window.Handle), false);
            }

            internal override void Dispose(bool disposing)
            {
                _backend.RemoveGraphics(this);
                base.Dispose(disposing);
            }

            private delegate void MinSampleShading(float rate);

            private static T LoadFunction<T>(string name) where T : Delegate
            {
                var addr = Glfw.GetProcAddress(name);
                return addr != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<T>(addr) : null;
            }
        }
    }
}
