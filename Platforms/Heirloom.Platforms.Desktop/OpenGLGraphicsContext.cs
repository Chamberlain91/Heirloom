using System;

using Heirloom.Drawing;
using Heirloom.Drawing.Backends.OpenGL;
using Heirloom.GLFW;

namespace Heirloom.Platforms.Desktop
{
    internal sealed class OpenGLGraphicsContext : GraphicsContext
    {
        internal OpenGLGraphicsContext()
            : base(GraphicsAPI.OpenGL)
        { }

        internal override RenderContext CreateRenderingContext(Window window)
        {
            return new GlfwRenderContext(window);
        }

        private class GlfwRenderContext : OpenGLRenderContext
        {
            internal GlfwRenderContext(Window window)
            {
                Window = window ?? throw new ArgumentNullException(nameof(window));

                // Set initial size, and whenever the window is resized, also set the default surface size
                SetDefaultSurfaceSize(Window.FramebufferSize);
                Window.Resized += (o, e) => SetDefaultSurfaceSize(Window.FramebufferSize);

                // Begin GL thread
                StartThread();
            }

            public Window Window { get; }

            protected override void PrepareContext()
            {
                Console.WriteLine("Initialize OpenGL Context");

                Glfw.MakeContextCurrent(Window.Native);
                Glfw.SwapInterval(Window.VSync ? 1 : 0);
            }

            protected override void TerminateContext()
            {
                Console.WriteLine("Terminate OpenGL Context");

                // Actually kill window (thus the wgl/egl/glx context too)
                Glfw.DestroyWindow(Window.Native);
            }

            protected override void SetSwapInterval(int interval)
            {
                Invoke(() => Glfw.SwapInterval(interval));
            }

            public override void SwapBuffers()
            {
                // Complete any pending work
                Flush();

                /* 
                 * Conceptually, I should be able to use InvokeLater for every case, as logically
                 * speaking its a queue. I should be able to submit to the queue without waiting
                 * and still have expected ordering. When using InvokeLater in a single window case
                 * an unexpected occasional freeze seems to occur and I am not sure why. The need
                 * for the non-blocking invoke is to avoid waiting for each windows swap to finish
                 * in a multi-windowed application. This causes a significant slow down in a
                 * single threaded application driving the windows as I've been implementing it as
                 * Draw A, B, C, Swap A, B, C.
                 */

                // swap buffers (aka, puts the rendered image on screen)
                if (Instance.Windows.Count > 1) { InvokeLater(() => Glfw.SwapBuffers(Window.Native)); }
                else { Invoke(() => Glfw.SwapBuffers(Window.Native)); }
            }
        }
    }
}
