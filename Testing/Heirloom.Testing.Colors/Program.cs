using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Testing.Colors
{
    public sealed class Program : GameWrapper
    {
        private const int ImageWidth = 500;
        private const int ImageHeight = 50;

        public readonly Image GradientLAB;
        public readonly Image GradientRGB;

        public Program()
            : base(CreateWindowGraphics())
        {
            // Create images
            GradientLAB = new Image(ImageWidth, ImageHeight);
            GradientRGB = new Image(ImageWidth, ImageHeight);

            // Populate images with gradients
            PopulateGradient(Color.Blue, Color.White);
        }

        public void PopulateGradient(Color a, Color b)
        {
            var gradientLAB = new Gradient(GradientMode.LAB);
            var gradientRGB = new Gradient(GradientMode.RGB);

            gradientLAB.Add(0F, a);
            gradientLAB.Add(1F, b);

            gradientRGB.Add(0F, a);
            gradientRGB.Add(1F, b);

            // 
            foreach (var co in Rasterizer.Rectangle(GradientLAB.Size))
            {
                var time = co.X / (float) GradientLAB.Width;

                GradientLAB[co] = gradientLAB.Evaluate(time);
                GradientRGB[co] = gradientRGB.Evaluate(time);
            }
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Color.Gray);
            Graphics.ResetState();


            // Draw gradient image
            var x = (Graphics.Surface.Width - GradientLAB.Width) / 2;
            var y = (Graphics.Surface.Height - (32 + GradientLAB.Height * 2)) / 2;

            Graphics.Color = Color.DarkGray;
            Graphics.DrawText("LAB", (x, y), Font.Default, 16);

            Graphics.Color = Color.White;
            Graphics.DrawImage(GradientLAB, Matrix.CreateTranslation(x, y + 16));

            Graphics.Color = Color.DarkGray;
            Graphics.DrawText("RGB", (x, y + 16 + GradientLAB.Height), Font.Default, 16);

            Graphics.Color = Color.White;
            Graphics.DrawImage(GradientRGB, Matrix.CreateTranslation(x, y + 32 + GradientLAB.Height));

            // 
            Graphics.Screen.Refresh();
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Heirloom - Color Testing")
            {
                Size = (16 + ImageWidth, 16 + (ImageHeight + 16) * 2),
                IsResizable = false
            };

            // Center on primary display
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }
    }
}
