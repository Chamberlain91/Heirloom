using Android.App;
using Android.Content.PM;

using Heirloom.Android;

namespace Heirloom.Examples.FingerPaint
{
    [Activity(Label = "Finger Paint",
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public sealed class MainActivity : GraphicsActivity
    {
        public FingerPaintApp App;

        protected override void GraphicsResume()
        {
            if (App == null) { App = new FingerPaintApp(Graphics); }
            App.Loop.Start();
        }

        protected override void GraphicsPause()
        {
            App.Loop.Stop();
        }
    }
}
