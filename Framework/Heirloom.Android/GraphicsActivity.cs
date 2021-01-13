using Android.App;
using Android.OS;
using Android.Views;

using Heirloom.Drawing;
using Heirloom.Hardware;
using Heirloom.Mathematics;

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
            public IntSize? Resolution;
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

            // Detect CPU Information
            SystemInformation.CpuInfo = DetectCPUInfo();

            // Get the user graphics config
            var config = new GraphicsConfiguration
            {
                Resolution = null,
                Multisample = MultisampleQuality.None,
                VSync = true
            };

            GetUserGraphicsConfiguration(ref config);

            // Initialize ES Graphics Backend
            _backend = new ESAndroidGraphicsBackend(config.Multisample);

            // Get the backbuffer resolution, by default
            var resolution = config.Resolution ?? AndroidHelper.ComputeAutomaticResolution(this);

            // Create graphics view
            _view = new GraphicsView(this, resolution, config.Multisample, config.VSync);
            SetContentView(_view);

            _view.GraphicsEnabled += () =>
            {
                SystemInformation.GpuInfo = DetectGPUInfo();

                GraphicsResume();
                CanRender = true;
            };

            _view.GraphicsDisable += () =>
            {
                CanRender = false;
                GraphicsPause();
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

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        { 
            base.OnPause();
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus)
            {
                HideSoftwareMenuBars();
            }
        }

        private void HideSoftwareMenuBars()
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

        private GpuInfo DetectGPUInfo()
        {
            // Query GPU info
            return GraphicsBackend.Current?.GpuInfo ?? GpuInfo.Unknown;
        }

        private CpuInfo DetectCPUInfo()
        {
            // todo: detect CPU info
            return CpuInfo.Unknown;
        }
    }
}
