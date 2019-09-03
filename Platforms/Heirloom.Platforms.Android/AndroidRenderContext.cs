using System;
using System.Runtime.CompilerServices;

using Heirloom.Drawing.Backends.OpenGL;
using Heirloom.Math;
using Heirloom.OpenGLES;
using Heirloom.OpenGLES.Platform;

namespace Heirloom.Platforms.Android
{
    internal sealed class AndroidRenderContext : OpenGLRenderContext
    {
        public HeirloomSurfaceView SurfaceView { get; }

        internal AndroidRenderContext(HeirloomSurfaceView surfaceView)
        {
            SurfaceView = surfaceView;
        }

        protected override void PrepareContext()
        {
            MakeCurrent();
        }

        public void MakeCurrent()
        {
            Invoke(() =>
            {
                // Make current
                Egl.MakeCurrent(SurfaceView.EglSurface, SurfaceView.EglContext);
                if (SurfaceView.EglSurface != null) { SetSwapInterval(1); }

                // Load GL Functions
                if (!GL.HasLoadedFunctions)
                {
                    GL.LoadFunctions(Egl.GetProcAddress);
                }
            });
        }

        protected override void TerminateContext()
        {
            // todo: what to do here?!
            Console.WriteLine("Terminate Context");
        }

        protected override void SetSwapInterval(int interval)
        {
            Invoke(() => SurfaceView.EglSurface.Display.SetSwapInterval(interval));
        }

        public override void SwapBuffers()
        {
            // Finish any pending work
            Flush();

            // Display onto the screen
            Invoke(() =>
            {
                if (!SurfaceView.EglSurface?.SwapBuffers() ?? false)
                {
                    Console.WriteLine("Failed To Swap Buffers!");
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new void SetDefaultSurfaceSize(IntSize size) { base.SetDefaultSurfaceSize(size); }
    }
}
