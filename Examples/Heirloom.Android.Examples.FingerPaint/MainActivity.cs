using Android.App;
using Android.Content.PM;

using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Android.Examples.FingerPaint
{
    [Activity(Label = "Finger Paint Example",
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation,
        MainLauncher = true)]
    public sealed class MainActivity : GraphicsActivity
    {
        public readonly GameLoop Loop;

        private Surface _canvas;

        public MainActivity()
        {
            Loop = new GameLoop(Update);
        }

        protected override void GraphicsResume()
        {
            // One time setup
            if (_canvas == null)
            {
                _canvas = new Surface(Graphics.Surface.Size, MultisampleQuality.Beautiful);
            }

            Loop.Start();
        }

        protected override void GraphicsPause()
        {
            Loop.Stop();
        }

        public void Update(float dt)
        {
            Graphics.ResetState(); // todo: bug, should not be needed?

            // Draw lines for the motion of each touch point to the canvas.
            Graphics.SetSurface(_canvas);
            for (var i = 0; i < Input.TouchCount; i++)
            {
                var touch = Input.GetTouch(i);

                Graphics.Color = Color.FromHSV(i * 30, 0.8F, 0.7F);
                Graphics.DrawLine(touch.Position, touch.Position - touch.Delta, 10);
            }

            // Present graphics to screen
            Graphics.SetSurface(Graphics.Screen.Surface);
            Graphics.Clear(Color.DarkGray);

            Graphics.Color = Color.White;
            Graphics.DrawImage(_canvas, Matrix.Identity);

            Graphics.Screen.Refresh();
        }
    }
}
