using System;
using System.Runtime.CompilerServices;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;

using Heirloom.Drawing;
using Heirloom.Drawing.Backends.OpenGL;
using Heirloom.Math;
using Heirloom.OpenGLES;
using Heirloom.OpenGLES.Platform;

namespace Heirloom.Platforms.Android
{
    /// <summary>
    /// A <see cref="SurfaceView"/>
    /// </summary>
    public class HeirloomSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        private readonly GLRenderContext _renderContext;
        private bool _canRender;

        public HeirloomSurfaceView(Activity activity)
            : this(activity, AndroidHelper.ComputeAutomaticResolution(activity))
        { }

        public HeirloomSurfaceView(Context context, IntSize resolution)
            : base(context)
        {
            // 
            Holder.AddCallback(this);
            Holder.SetFixedSize(resolution.Width, resolution.Height);
            Holder.SetFormat(Format.Rgb888);

            Console.WriteLine($"[EGL] Creating OpenGL ES 3.0 Context");

            // Find best configuration for 24 bit color no depth
            var config = Egl.ChooseConfig(new EglConfigAttributes
            {
                RedBits = 8,
                GreenBits = 8,
                BlueBits = 8,
                AlphaBits = 0,
                DepthBits = 0,
                StencilBits = 0,
                Samples = 0
            });

            // Create OpenGL ES 3.0 Context & Surface 
            EglContext = Egl.CreateContext(config);

            // Create render context, and set to initial size
            _renderContext = new GLRenderContext(this);
            _renderContext.SetDefaultSurfaceSize(resolution);
            _renderContext.StartThread();
        }

        public RenderContext RenderContext => _renderContext;

        internal EglContext EglContext { get; private set; }

        internal EglSurface EglSurface { get; private set; }

        public bool CanRender
        {
            get => _canRender;

            private set
            {
                if (_canRender != value)
                {
                    _canRender = value;
                    RenderingAvailable?.Invoke(_canRender);
                }
            }
        }

        public event CanRenderChanged RenderingAvailable;

        public event Action Resized;

        public delegate void CanRenderChanged(bool available);

        void ISurfaceHolderCallback.SurfaceCreated(ISurfaceHolder holder)
        {
            Console.WriteLine($"[Holder] Surface Created");

            // 
            Console.WriteLine($"[EGL] Creating Surface");
            EglSurface = Egl.CreateWindowSurface(EglContext, AndroidWindow.GetWindowHandle(holder));

            // 
            _renderContext.MakeCurrent();
            CanRender = true;
        }

        void ISurfaceHolderCallback.SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
        {
            Console.WriteLine($"[Holder] Surface Changed ({width} by {height})");

            // Inform context window surface changed size
            _renderContext.SetDefaultSurfaceSize(new IntSize(width, height));

            // Resize event
            Resized?.Invoke();
        }

        void ISurfaceHolderCallback.SurfaceDestroyed(ISurfaceHolder holder)
        {
            Console.WriteLine($"[Holder] Surface Destroyed");

            if (EglSurface != null)
            {
                // Release window handle (???)
                AndroidWindow.ReleaseWindowHandle(EglSurface.Handle);

                // Dispose surface
                EglSurface.Dispose();
                EglSurface = null;
            }

            // Surface was destroyed, so unable to render
            CanRender = false;
        }

        private sealed class GLRenderContext : OpenGLRenderContext
        {
            public HeirloomSurfaceView SurfaceView { get; }

            internal GLRenderContext(HeirloomSurfaceView surfaceView)
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
                    // Make current (with vsync forced on)
                    Egl.MakeCurrent(SurfaceView.EglSurface, SurfaceView.EglContext);
                    SurfaceView.EglSurface?.Display.SetSwapInterval(1);

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
}
