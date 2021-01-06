using Android.App;
using Android.Content.PM;

using Meadows.Android;
using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Heirloom.Android.Examples.Stencil
{
    [Activity(Label = "Stencil Example",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public sealed class MainActivity : GraphicsActivity
    {
        public readonly GameLoop Loop;

        public readonly Image Image = new Image("zelda.jpg");

        private float _time;

        public MainActivity()
        {
            Loop = new GameLoop(Update);
        }

        protected override void GraphicsInitialized()
        {
            Meadows.Log.Warning("GRAPHICS INITIALIZED");
        }

        protected override void GraphicsDisposed()
        {
            Meadows.Log.Warning("GRAPHICS DISPOSED");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Loop.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Loop.Stop();
        }

        public void Update(float dt)
        {
            if (Graphics == null) { return; }

            Graphics.Performance.ShowOverlay = true;

            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);

            //
            _time += dt;
            RenderStencilTest(Graphics, 4 + Calc.Sin(_time) * 6);

            // Present graphics to screen
            Graphics.Screen.Refresh();
        }

        private void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.DarkGray;
            DrawBackgroundImages(gfx, Image, angle);

            // Draw a stencil mask
            gfx.BeginStencil();
            gfx.PushState();
            {
                var center = (Vector) gfx.Surface.Size / 2F;
                gfx.Transform = CreateRotationCenter(angle / 2F * Calc.ToRadians, center);
                gfx.DrawText("Princess Zelda", center, Font.Default, 200, TextAlign.Center | TextAlign.Middle);
            }
            gfx.PopState();
            gfx.EndStencil();

            // White for full brightness
            gfx.Color = Color.White;

            // Draw image (uses above stencil)
            DrawBackgroundImages(gfx, Image, angle);

            // Clear the stencil, back to regular drawing.
            gfx.ClearStencil();

            // Draw regular text again
            gfx.DrawText($"Heirloom 2D Graphics", (gfx.Surface.Width - 8, 8), Font.Default, 16, TextAlign.Top | TextAlign.Right);
        }

        private static void DrawBackgroundImages(GraphicsContext gfx, Image image, float angle)
        {
            var center = (IntVector) (image.Size / 2F);

            var tranformA = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - (image.Size * 10)) / 2) * CreateRotationCenter(angle / 10F * Calc.ToRadians, center);
            gfx.DrawImage(image, tranformA * Matrix.CreateScale(10));

            var transformB = Matrix.CreateTranslation((Vector) (gfx.Surface.Size - image.Size) / 2) * CreateRotationCenter(angle * Calc.ToRadians, center);
            gfx.DrawImage(image, transformB);
        }

        private static Matrix CreateRotationCenter(float angle, Vector center)
        {
            return Matrix.CreateTranslation(center)
                 * Matrix.CreateRotation(angle)
                 * Matrix.CreateTranslation(-center);
        }
    }
}
