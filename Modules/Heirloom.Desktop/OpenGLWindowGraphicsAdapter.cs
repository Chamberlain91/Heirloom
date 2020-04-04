using System;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLWindowGraphicsAdapter : OpenGLGraphicsAdapter, IWindowGraphicsFactory
    {
        public Graphics CreateGraphics(Window window, bool vsync)
        {
            return new OpenGLWindowGraphics(window, vsync);
        }

        #region Invoke

        protected override T Invoke<T>(Func<T> function)
        {
            return Application.Invoke(() =>
            {
                // Make the share context current here
                // todo: Possibly correct for this, this feels terrible...
                Glfw.MakeContextCurrent(Application.ShareContext);

                // Execute function and keep return value
                var returnValue = function();

                // Release context from thread. We want it not associated with any thread. On a AMD Vega
                // platform this caused the main rendering loop to halt (resource blocking?).
                Glfw.MakeContextCurrent(WindowHandle.None);

                return returnValue;
            });
        }

        protected override void Invoke(Action function)
        {
            Application.Invoke(() =>
            {
                // Make the share context current here
                // todo: Possibly correct for this, this feels terrible...
                Glfw.MakeContextCurrent(Application.ShareContext);

                // Execute function and keep return value
                function();

                // Release context from thread. We want it not associated with any thread. On a AMD Vega
                // platform this caused the main rendering loop to halt (resource blocking?).
                Glfw.MakeContextCurrent(WindowHandle.None);
            });
        }

        #endregion

        private sealed class OpenGLWindowGraphics : OpenGLGraphics
        {
            private readonly Window _window;
            private readonly bool _vsync;

            public OpenGLWindowGraphics(Window window, bool vsync)
                : base(window.Multisample)
            {
                _window = window ?? throw new ArgumentNullException(nameof(window));
                _vsync = vsync;

                // Set initial default surface size
                DefaultSurface.SetSize(_window.FramebufferSize);

                // Whenever the window framebuffer is resized, also update the
                // viewport and default surface.
                _window.FramebufferResized += _ =>
                {
                    DefaultSurface.SetSize(_window.FramebufferSize);
                    if (Surface != null) { ComputeViewportRect(); }
                };

                // Run OpenGL Consumer Thread
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
            }

            protected override void SwapBuffers()
            {
                // Swap buffers (on the gl thread)
                Invoke(() => Glfw.SwapBuffers(_window.Handle), false);
            }

            protected override void Dispose(bool disposeManaged)
            {
                // Dispose base class
                base.Dispose(disposeManaged);
            }
        }
    }
}
