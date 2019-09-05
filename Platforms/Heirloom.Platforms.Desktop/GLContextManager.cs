using System;

using Heirloom.Drawing;
using Heirloom.Drawing.Backends.OpenGL;
using Heirloom.GLFW;
using Heirloom.OpenGLES;

namespace Heirloom.Platforms.Desktop
{
    internal sealed class GLContextManager : ContextManager
    {
        internal GLContextManager()
            : base(GraphicsAPI.OpenGL)
        { }

        internal override RenderContext CreateRenderContext(Window window)
        {
            return new GLRenderContext(window);
        }

        protected override void Configure()
        {
            Glfw.WindowHint(Glfw.CLIENT_API, Glfw.OPENGL_API);
            Glfw.WindowHint(Glfw.OPENGL_PROFILE, Glfw.OPENGL_CORE_PROFILE);
            Glfw.WindowHint(Glfw.OPENGL_FORWARD_COMPAT, Glfw.True);
            Glfw.WindowHint(Glfw.CONTEXT_VERSION_MAJOR, 3);
            Glfw.WindowHint(Glfw.CONTEXT_VERSION_MINOR, 2);
        }

        protected override unsafe void LoadFunctions()
        {
            Glfw.MakeContextCurrent(ShareContext);
            Glfw.SwapInterval(0); // disable-vsync

            // Load OpenGL Functions
            if (!GL.HasLoadedFunctions)
            {
                GL.LoadFunctions(Glfw.GetProcAddress);
            }
        }
    }

    internal class GLRenderContext : OpenGLRenderContext
    {
        internal GLRenderContext(Window window)
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
            if (Window.Windows.Count > 1) { InvokeLater(() => Glfw.SwapBuffers(Window.Native)); }
            else { Invoke(() => Glfw.SwapBuffers(Window.Native)); }
        }
    }
}
