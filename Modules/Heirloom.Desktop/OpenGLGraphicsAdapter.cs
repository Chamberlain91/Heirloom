using System;
using System.Threading;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.OpenGLES;

namespace Heirloom.Desktop
{
    internal sealed class OpenGLGraphicsAdapter : WindowGraphicsAdapter
    {
        internal override Graphics CreateGraphics(Window window)
        {
            return new OpenGLWindowGraphics(this, window);
        }

        protected override GraphicsCapabilities QueryCapabilities()
        {
            return new GraphicsCapabilities(
                maxSupportedTextures: GL.GetInteger(GetParameter.MaxTextureImageUnits),
                isMobilePlatform: false);
        }

        protected override object CompileShader(string vert, string frag)
        {
            Console.WriteLine("OPENGL COMPILE SHADER NOW PLZ");
            throw new NotImplementedException();
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
