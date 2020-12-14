using System.Collections.Generic;

using Meadows.Desktop;
using Meadows.Drawing;
using Meadows.Mathematics;
using Meadows.Utilities;

namespace Meadows.Examples.Primitives
{
    public class Program : Application
    {
        public Window Window { get; }

        public GraphicsContext Graphics => Window.Graphics;

        public float Time;

        public Curve[] Curves;
        public Color[] Colors;

        public Program()
        {
            // Create window
            Window = new Window("Heirloom - Primitives Demo", (512, 512), MultisampleQuality.Medium);
            Window.Maximize();

            Curves = new Curve[20];
            Colors = new Color[Curves.Length];
            for (var i = 0; i < Curves.Length; i++)
            {
                // Randomize color
                var fade = Calc.Lerp(0.8F, 0.9F, i / (float) Curves.Length);
                Colors[i] = Calc.Random.NextColorHue(0.5F, fade);

                // Random curve
                var curve = Curves[i] = new Curve();

                var h0 = Calc.Random.NextVectorDisk(100);
                for (var l = 0; l < 8; l++)
                {
                    var h1 = Calc.Random.NextVectorDisk(100);
                    curve.Add(Calc.Random.NextVectorDisk(500), h0, h1);
                    h0 = -h1;
                }
            }

            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            Time += dt;

            // Configure camera
            // todo: automatically resize viewport if set to auto?
            Graphics.SetRenderTarget(Window.Surface);
            Graphics.SetCamera(Vector.Zero);
            Graphics.Clear(Color.White);

            // 
            for (var i = 0; i < Curves.Length; i++)
            {
                Graphics.TransformMatrix = Matrix.Identity;
                Graphics.Color = Colors[i];
                Graphics.DrawCurve(Curves[i], 3F);
            }

            // 
            Window.Refresh();
        }

        private static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
