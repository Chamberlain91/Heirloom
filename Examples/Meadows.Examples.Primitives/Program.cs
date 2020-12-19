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

        public Bezier Curve;

        public Program()
        {
            // Create window
            Window = new Window("Heirloom - Primitives Demo", (512, 512), MultisampleQuality.None);
            Window.Maximize();

            // Track user input from this window
            Input.SetInputSource(Window);

            // Random curve
            Curve = new Bezier();

            var phase = Calc.Random.NextFloat(0, Calc.TwoPi);

            var h0 = Vector.Zero;
            for (var s = 0; s <= 6; s++)
            {
                var a0 = ((s + 0) / 6F * Calc.Pi) + phase;
                var a1 = ((s + 1) / 6F * Calc.Pi) + phase;

                var p0 = Vector.FromAngle(a0) * 400F;

                if (s == 0) { h0 = Calc.Random.NextVectorDisk() * 200; }
                var h1 = Calc.Random.NextVectorDisk() * 200;
                Curve.Add(p0, h0, h1);
                h0 = -h1;
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
            Graphics.Clear(Color.DarkGray);

            // 
            DrawCurve();

            // 
            Window.Refresh();
        }

        private void DrawCurve()
        {
            // Draw Curve
            Graphics.Color = Color.Pink;
            Graphics.Transform = Matrix.CreateTranslation(Input.MousePosition);
            Graphics.DrawCurve(Curve, 3F);

            // Draw Curve "Handles"
            for (var c = 0; c < Curve.Count - 1; c++)
            {
                var p0 = Curve.GetPoint(c + 0);
                var p1 = Curve.GetPoint(c + 0) + Curve.GetInHandle(c);
                var p2 = Curve.GetPoint(c + 1) + Curve.GetOutHandle(c);
                var p3 = Curve.GetPoint(c + 1);

                Graphics.Color = (c % 2) == 1 ? Color.White : Color.Gray;
                Graphics.DrawDottedLine(p0, p1);
                Graphics.DrawDottedLine(p2, p3);
            }
        }

        private static void Main(string[] args)
        {
            Run<Program>();
        }
    }
}
