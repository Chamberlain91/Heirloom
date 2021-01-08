using Android.App;
using Android.Content.PM;

using Meadows.Android;

namespace Meadows.Benchmark.Android
{
    [Activity(Label = "Benchmark",
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public class MainActivity : GraphicsActivity
    {
        public BenchmarkApp App;

        protected override void GetUserGraphicsConfiguration(ref GraphicsConfiguration config)
        {
            config.VSync = false;
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
