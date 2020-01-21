using System;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLWindowGraphicsAdapter : OpenGLGraphicsAdapter, IWindowGraphicsFactory
    {
        public Graphics CreateGraphics(Window window)
        {
            return new OpenGLWindowGraphics(this, window);
        }

        protected override T InvokeOnGLThread<T>(Func<T> function)
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

        protected override void InvokeOnGLThread(Action function)
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

        private sealed class OpenGLWindowGraphics : OpenGLGraphics
        {
            public OpenGLWindowGraphics(GraphicsAdapter graphicsAdapter, Window window)
                : base(graphicsAdapter, window.Multisample)
            {
                Window = window;

                // Set initial size, and whenever the window is resized, also set the default surface size
                Window.FramebufferResized += _ => SetDefaultSurfaceSize(Window.FramebufferSize);
                SetDefaultSurfaceSize(Window.FramebufferSize);
            }

            public Window Window { get; }

            protected override void PrepareContext()
            {
                // Wait for window to be set to help avoid the race condition, since the context thread is a different thread.
                SpinWait.SpinUntil(() => Window != null);

                // Make context current on context thread
                Glfw.MakeContextCurrent(Window.WindowHandle);
                Glfw.SetSwapInterval(Window.VSync ? 1 : 0);
            }

            protected override void SwapBuffers()
            {
                Invoke(() => Glfw.SwapBuffers(Window.WindowHandle), false);
            }
        }
    }
}
