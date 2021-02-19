using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Minimal
{
    public sealed class Program : GameWrapper
    {
        private float _time;

        public Program()
            : base(CreateWindowGraphics())
        {
            // Enable performance overlay
            Graphics.Performance.ShowOverlay = true;
        }

        protected override void Update(float dt)
        {
            _time += dt;

            // Clear the orange
            Graphics.ResetState();
            Graphics.Clear(Color.Orange);

            // Draw time text
            Graphics.Color = Color.Black;
            Graphics.DrawText($"Time since launching the application:\n{Time.GetEnglishTime(_time)}", (Vector) Graphics.Surface.Size / 2F,
                Font.SansSerifBold, 48, TextAlign.Center | TextAlign.Middle);

            // Update the screen
            Graphics.Screen.Refresh();
        }

        public static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            // Create the window
            var window = new Window("Heirloom Minimal Example");
            window.Maximize();

            return window.Graphics;
        }
    }
}
