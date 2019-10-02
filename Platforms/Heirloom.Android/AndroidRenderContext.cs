using System;
using System.Runtime.CompilerServices;

using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.EGL;
using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Android
{
    internal sealed class AndroidRenderContext : OpenGLRenderContext
    {
        public HeirloomSurfaceView SurfaceView { get; }

        internal AndroidRenderContext(HeirloomSurfaceView surfaceView, MultisampleQuality multisample)
            : base(multisample)
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
                SurfaceView.EglSurface?.Display.SetSwapInterval(1);

                // Load GL Functions
                if (!GL.HasLoadedFunctions)
                {
                    GL.LoadFunctions(Egl.GetProcAddress);
                }
            });
        }

        protected override void SwapBuffers()
        {
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
