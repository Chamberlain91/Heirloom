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
            return new OpenGLWindowGraphics(this, window, vsync);
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
            private readonly Window _window;
            private readonly bool _vsync;

            public OpenGLWindowGraphics(GraphicsAdapter adapter, Window window, bool vsync)
                : base(adapter, window.Multisample)
            {
                _window = window;
                _vsync = vsync;

                // 
                _window.FramebufferResized += _ => SetDefaultSurfaceSize(_window.FramebufferSize);
                SetDefaultSurfaceSize(_window.FramebufferSize);
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
        }
    }
}
