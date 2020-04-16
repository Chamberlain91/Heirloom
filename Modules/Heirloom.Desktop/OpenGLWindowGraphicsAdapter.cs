using System;
using System.Runtime.InteropServices;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLWindowGraphicsAdapter : OpenGLGraphicsAdapter, IWindowGraphicsFactory
    {
        public Graphics CreateGraphics(Window window, MultisampleQuality multisample, bool vsync)
        {
            return new OpenGLWindowGraphics(window, multisample, vsync);
        }

        #region Invoke

        protected internal override T Invoke<T>(Func<T> function)
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

        protected internal override void Invoke(Action function)
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

            public OpenGLWindowGraphics(Window window, MultisampleQuality multisample, bool vsync)
                : base(multisample)
            {
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
                // Dispose base class
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
