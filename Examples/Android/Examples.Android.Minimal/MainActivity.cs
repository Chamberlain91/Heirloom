using Android.App;
using Android.Content.PM;
using Android.OS;

using Heirloom.Android;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Android.Minimal
{
    [Activity(
           Immersive = true,
           Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
           ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard,
           ScreenOrientation = ScreenOrientation.SensorLandscape,
           MainLauncher = true)]
    public class MainActivity : GameActivity
    {
        const string Text = "Hello Android!";

        protected override void OnCreate(Bundle bundle)
        {
            ShowFPSOverlay = true;
            base.OnCreate(bundle);
        }

        protected override void Update(RenderContext ctx, float dt)
        {
            // Draw hello world text
            ctx.Clear(Color.DarkGray);

            // todo: feature, should be able to vertical align without this.
            var align = new Vector(0, Font.Default.MeasureText(Text, 48).Height / 2F);
            ctx.DrawText(Text, -align + ((Vector) ctx.Surface.Size) * 0.5F, Font.Default, 48, TextAlign.Center);
        }
    }
}
