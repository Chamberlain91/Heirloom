using Android.App;
using Android.Content.PM;
using Android.OS;

using Heirloom.Android;
using Heirloom.Drawing;

namespace Benchmark
{
    [Activity(
           Immersive = true,
           Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
           ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard,
           ScreenOrientation = ScreenOrientation.SensorPortrait,
           MainLauncher = true)]
    public class MainActivity : GameActivity
    {
        public BenchmarkApp App;

        protected override void OnCreate(Bundle bundle)
        {
            // Display FPS
            ShowFPSOverlay = true;

            // Create app instance
            App = new BenchmarkApp(30);

            base.OnCreate(bundle);
        }

        protected override void Update(float dt)
        {
            App.Update(dt);
        }

        protected override void Render(RenderContext ctx, float dt)
        {
            App.Render(ctx, dt);
        }
    }
}
