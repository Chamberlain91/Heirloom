using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLWindowGraphicsAdapter : OpenGLGraphicsAdapter, IWindowGraphicsFactory
    {
        public OpenGLWindowGraphicsAdapter()
        {
            Graphics = new List<OpenGLWindowGraphics>();
        }

        private List<OpenGLWindowGraphics> Graphics { get; }

        public Graphics CreateGraphics(Window window, bool vsync)
        {
            return new OpenGLWindowGraphics(this, window, vsync);
        }

        #region Invoke

        protected override T Invoke<T>(Func<T> function)
        {
            // If a graphics context is known, use that thread.
            if (Graphics.Count > 0) { return Graphics[0].Invoke(function); }
            // Otherwise, temporarily bind the shared context on the window thread.
            else
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
        }

        protected override void Invoke(Action function)
        {
            // If a graphics context is known, use that thread.
            if (Graphics.Count > 0) { Graphics[0].Invoke(function); }
            // Otherwise, temporarily bind the shared context on the window thread.
            else
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
        }

        #endregion

        private sealed class OpenGLWindowGraphics : OpenGLGraphics
        {
            private readonly OpenGLWindowGraphicsAdapter _adapter;
            private readonly Window _window;
            private readonly bool _vsync;

            public OpenGLWindowGraphics(OpenGLWindowGraphicsAdapter adapter, Window window, bool vsync)
                : base(adapter, window.Multisample)
            {
                _adapter = adapter;
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

                // Append graphics context tracking list
                lock (_adapter.Graphics) { _adapter.Graphics.Add(this); }
            }

            protected override void SwapBuffers()
            {
                // Swap buffers (on the gl thread)
                base.Invoke(() => Glfw.SwapBuffers(_window.Handle), false);
            }

            #region Invoke

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal new void Invoke(Action action, bool blocking = true)
            {
                base.Invoke(action, blocking);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal new T Invoke<T>(Func<T> action)
            {
                return base.Invoke(action);
            }

            #endregion

            protected override void Dispose(bool disposeManaged)
            {
                // Remove from context tracking list
                lock (_adapter.Graphics) { _adapter.Graphics.Remove(this); }

                // Dispose base class
                base.Dispose(disposeManaged);
            }
        }
    }
}
