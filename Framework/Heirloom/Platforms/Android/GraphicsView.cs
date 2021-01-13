#if ANDROID
using System;

using Android.App;
using Android.Graphics;
using Android.Views;

using Heirloom.Android.EGL;
using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.Mathematics;

namespace Heirloom.Android
{
    internal sealed class GraphicsView : SurfaceView, IScreen, ISurfaceHolderCallback
    {
        private readonly ESGraphicsContext _esGraphics;

        internal EglSurface EglSurface;

        public GraphicsContext Graphics => _esGraphics;

        internal GraphicsView(Activity activity, IntSize resolution, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
           : base(activity)
        {
            // Configure holder resolution
            Holder.AddCallback(this);
            Holder.SetFixedSize(resolution.Width, resolution.Height);
            Holder.SetFormat(Format.Rgb888);

            // Construct the default surface and context
            if (GraphicsBackend.Current is ESAndroidGraphicsBackend esBackend)
            {
                // Create default surface
                Surface = new Drawing.Surface(multisample, SurfaceFormat.UnsignedByte, this);

                // Create the graphics context
                _esGraphics = esBackend.CreateGraphics(this, vsync);
            }
            else
            {
                throw new InvalidOperationException("Unable to create graphics context, unknown backend.");
            }
        }

        public event Action GraphicsEnabled;

        public event Action GraphicsDisable;

        void ISurfaceHolderCallback.SurfaceCreated(ISurfaceHolder holder)
        {
            Log.Debug($"[Holder] Surface Created");

            if (GraphicsBackend.Current is ESAndroidGraphicsBackend esBackend)
            {
                Log.Debug($"[EGL] Creating Surface");

                // Create window surface via handle, this informs android to increment the window reference counter.
                EglSurface = Egl.CreateWindowSurface(esBackend.EglContext, AndroidWindow.GetWindowHandle(holder));

                // Launch GL thread, EGL surface has been created.
                if (_esGraphics.IsThreadRunning == false) { _esGraphics.StartThread(); }

                // Notify that we need to rebind the context to the thread. The surface has changed.
                _esGraphics.NotifyMakeCurrent();

                // Notify graphics are available
                GraphicsEnabled?.Invoke();
            }
            else
            {
                throw new InvalidOperationException("Unable to create EGL surface, unknown backend.");
            }
        }

        void ISurfaceHolderCallback.SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
        {
            Log.Debug($"[Holder] Surface Changed ({width} by {height})");

            // Adjust surface size and invoke event
            var size = new IntSize(width, height);
            Graphics.Surface.SetSize(size);
            SurfaceResized?.Invoke(this, size);

            Log.Debug($"[Graphics] Surface Resized to {size}.");
        }

        void ISurfaceHolderCallback.SurfaceDestroyed(ISurfaceHolder holder)
        {
            Log.Debug($"[Holder] Surface Destroyed");

            if (EglSurface != null)
            {
                Log.Debug($"[EGL] Release Surface");

                // Release window handle, this informs android to decrement the window reference counter.
                AndroidWindow.ReleaseWindowHandle(EglSurface.Handle);

                // Dispose surface
                EglSurface.Dispose();
                EglSurface = null;
            }

            // Notify graphics are not available.
            GraphicsDisable?.Invoke();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            Log.Debug($"[Holder] Screen Resize ({w} by {h})");

            // The view has resized. This will likely be a different size than the surface.
            Resized?.Invoke(this, new IntSize(w, h));
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
#endif
