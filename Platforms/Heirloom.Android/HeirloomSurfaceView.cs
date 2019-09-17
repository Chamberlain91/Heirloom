using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;

using Heirloom.Drawing;
using Heirloom.EGL;
using Heirloom.Math;

namespace Heirloom.Android
{
    public class HeirloomSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        private readonly AndroidRenderContext _renderContext;
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
            _renderContext = new AndroidRenderContext(this);
            _renderContext.SetDefaultSurfaceSize(resolution);
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

        public void Resume()
        {
            SetImmersiveFullscreenFlags();
            _renderContext?.StartThread();
        }

        public void Pause()
        {
            SetImmersiveFullscreenFlags();
            //_renderContext?.StopThread();
        }

        private void SetImmersiveFullscreenFlags()
        {
            SystemUiVisibility = (StatusBarVisibility) (SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky | SystemUiFlags.Fullscreen);
        }

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
    }
}
