using Android.App;
using Android.Content.PM;

using Heirloom.Android;

namespace Heirloom.Benchmark.Android
{
    [Activity(Label = "Benchmark",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public class MainActivity : GraphicsActivity
    {
        public BenchmarkApp App;

        protected override void GetUserGraphicsConfiguration(ref GraphicsConfiguration config)
        {
            config.VSync = false;
            config.Resolution = (1280, 720);
        }

        protected override void GraphicsResume()
        {
            if (App == null) { App = new BenchmarkApp(Graphics); }
            App.Loop.Start();
        }

        protected override void GraphicsPause()
        {
            App.Loop.Stop();
        }
    }
}
