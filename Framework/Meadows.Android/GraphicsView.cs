using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Opengl;
using Android.Runtime;
using Android.Util;
using Android.Views;

using Meadows.Android.EGL;
using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.Android
{
    internal sealed class GraphicsView : SurfaceView, IScreen, ISurfaceHolderCallback
    {
        private readonly MultisampleQuality _multisample;
        private readonly bool _vsync;

        internal EglSurface EglSurface;

        public GraphicsContext Graphics { get; private set; }

        internal GraphicsView(Activity activity, IntSize resolution, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
           : base(activity)
        {
            _multisample = multisample;
            _vsync = vsync;

            Holder.AddCallback(this);
            Holder.SetFixedSize(resolution.Width, resolution.Height);
            Holder.SetFormat(Format.Rgb888);
        }

        public event Action GraphicsInitialized;

        public event Action GraphicsDisposed;

        //private void SetCompleteFullscreen()
        //{
        //    // note: this deprecation is for Android R
        //    SystemUiVisibility = (StatusBarVisibility) (SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky | SystemUiFlags.Fullscreen);
        //}

        void ISurfaceHolderCallback.SurfaceCreated(ISurfaceHolder holder)
        {
            Console.WriteLine($"[Holder] Surface Created");

            if (GraphicsBackend.Current is ESAndroidGraphicsBackend esBackend)
            {
                Console.WriteLine($"[EGL] Creating Surface");

                // Create EGL Resources
                EglSurface = Egl.CreateWindowSurface(esBackend.EglContext, AndroidWindow.GetWindowHandle(holder));

                // Create default surface
                Surface = new Drawing.Surface(_multisample, SurfaceFormat.UnsignedByte, this);

                // Create the graphics context
                Graphics = esBackend.CreateGraphics(this, _vsync);

                // Notify graphics are available
                GraphicsInitialized?.Invoke();
                // CanRender = true;
            }
            else
            {
                throw new InvalidOperationException("Unable to create graphics context, unknown backend.");
            }

            //SetCompleteFullscreen();
        }

        void ISurfaceHolderCallback.SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
        {
            Console.WriteLine($"[Holder] Surface Changed ({width} by {height})");

            if (Graphics != null)
            {
                // Adjust surface size and invoke event
                var size = new IntSize(width, height);
                Console.WriteLine($"[Graphics] Surface Resized to {size}.");
                Graphics.Surface.SetSize(size);
                SurfaceResized?.Invoke(this, size);
            }

            //SetCompleteFullscreen();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            // The view has resized. This will likely be a different size.
            Resized?.Invoke(this, new IntSize(w, h));
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

            // notify graphics is no longer available
            GraphicsDisposed?.Invoke();
            // CanRender = false;
        }

        public IntSize Size
        {
            get => new IntSize(Width, Height);
            set => throw new NotImplementedException("Unable to change size of android view.");
        }

        public Drawing.Surface Surface { get; private set; }

        public event Action<IScreen, IntSize> Resized;

        public event Action<IScreen, IntSize> SurfaceResized;

        public void Refresh()
        {
            // todo: poll input

            Graphics.CompleteFrame();
        }
    }
}
