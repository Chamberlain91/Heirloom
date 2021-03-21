using Android.App;
using Android.OS;
using Android.Views;

using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Sound;

namespace Heirloom.Android
{
    public abstract class GraphicsActivity : Activity
    {
        private GraphicsBackend _backend;
        private GraphicsView _view;

        public GraphicsContext Graphics => _view.Graphics;

        public bool CanRender { get; private set; }

        protected struct GraphicsConfiguration
        {
            public IntSize Resolution;
            public MultisampleQuality Multisample;
            public bool VSync;
        }

        protected virtual void GetUserGraphicsConfiguration(ref GraphicsConfiguration config)
        {
            // Nothing by default
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Make activity fullscreen
            AndroidHelper.SetWindowFullscreen(this);

            // Get the user graphics config
            var config = new GraphicsConfiguration
            {
                Resolution = AndroidHelper.ComputeAutomaticResolution(this),
                Multisample = MultisampleQuality.None,
                VSync = true
            };

            GetUserGraphicsConfiguration(ref config);

            // Initialize ES Graphics Backend
            _backend = new ESAndroidGraphicsBackend(config.Multisample);

            // Create graphics view
            _view = new GraphicsView(this, config.Resolution, config.Multisample, config.VSync);
            SetContentView(_view);

            _view.GraphicsEnabled += () =>
            {
                AudioBackend.Resume();
                GraphicsResume();

                CanRender = true;
            };

            _view.GraphicsDisable += () =>
            {
                CanRender = false;

                GraphicsPause();
                AudioBackend.Pause();
            };
        }

        protected abstract void GraphicsResume();

        protected abstract void GraphicsPause();

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Dispose of the graphics backend
            // TODO: How/where to properly dispose of the graphics backend...?  
            _backend.Dispose();
            _backend = null;
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            if (hasFocus)
            {
                SetImmersiveFullscreen();
            }
        }

        private void SetImmersiveFullscreen()
        {
            // thanks to:
            // https://forums.xamarin.com/discussion/99255/android-immersive-mode

            var uiOptions = (int) Window.DecorView.SystemUiVisibility;

            uiOptions |= (int) SystemUiFlags.LayoutStable;
            uiOptions |= (int) SystemUiFlags.LayoutHideNavigation;
            uiOptions |= (int) SystemUiFlags.LayoutFullscreen;

            uiOptions |= (int) SystemUiFlags.LowProfile;
            uiOptions |= (int) SystemUiFlags.HideNavigation;
            uiOptions |= (int) SystemUiFlags.Fullscreen;

            uiOptions |= (int) SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility) uiOptions;
        }
    }
}
