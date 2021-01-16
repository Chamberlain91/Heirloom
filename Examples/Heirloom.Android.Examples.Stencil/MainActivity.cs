using Android.App;
using Android.Content.PM;
using Android.OS;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Mathematics;
using Heirloom.Sound;
using Heirloom.Utilities;

using Image = Heirloom.Drawing.Image;

namespace Heirloom.Android.Examples.Stencil
{
    [Activity(Label = "Stencil Example",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public sealed class MainActivity : GraphicsActivity
    {
        private float _time;

        public static readonly Image Image = new Image("zelda.jpg");

        public readonly GameLoop Loop;

        public MainActivity()
        {
            Loop = new GameLoop(Update);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var source = new AudioSource(Files.OpenStream("bensound-dubstep.ogg"));
            source.Play();
        }

        protected override void GraphicsResume()
        {
            Loop.Start();
        }

        protected override void GraphicsPause()
        {
            Loop.Stop();
        }

        public void Update(float dt)
        {
            // Enable the performance overlay
            Graphics.Performance.ShowOverlay = true;

            // Advance time
            _time += dt;

            // Render stencil example
            RenderStencilTest(Graphics, Calc.Sin(_time) * 15);

            // Present graphics to screen
            Graphics.Screen.Refresh();
        }

        private static void RenderStencilTest(GraphicsContext gfx, float angle)
        {
            gfx.ResetState();
            gfx.Clear(Color.Yellow * Color.DarkGray);

            // Draw base image (darkened)
            gfx.Color = Color.Gray;
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
