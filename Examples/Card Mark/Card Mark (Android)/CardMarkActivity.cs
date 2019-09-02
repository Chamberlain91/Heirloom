using Android.App;
using Android.Content.PM;
using Android.OS;

using Heirloom.Drawing;
using Heirloom.Platforms.Android;

namespace Heirloom.Examples.CardMark
{
    [Activity(
        Immersive = true,
        Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard,
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        MainLauncher = true)]
    public class CardMarkActivity : GameActivity
    {
        public CardMarkApplication CardMark;

        protected override void OnCreate(Bundle bundle)
        {
            CardMark = new CardMarkApplication(30, 5, 5000);
            base.OnCreate(bundle);
        }

        protected override void Update(float delta)
        {
            CardMark.Update(delta);
        }

        protected override void Render(RenderContext ctx, float delta)
        {
            CardMark.Render(ctx, delta);
        }
    }
}
