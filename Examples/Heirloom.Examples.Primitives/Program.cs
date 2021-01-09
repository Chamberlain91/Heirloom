using System;
using System.Collections.Generic;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Examples.Primitives
{
    public class Program : Application
    {
        public Window Window { get; }

        public GraphicsContext Graphics => Window.Graphics;

        public float Time;

        private readonly List<Example> _examples;

        public Program()
        {
            // Create window
            Window = new Window("Heirloom - Primitives Demo", (1280, 720), MultisampleQuality.Medium) { IsResizable = false };
            Window.Position = (IntVector) (Display.Primary.Size - Window.Size) / 2; // Center on display
            Window.Graphics.Performance.ShowOverlay = true;

            // Create examples
            _examples = new List<Example>()
            {
                new CircleExample(),
                new TriangleExample(),
                new RectangleExample(),
                new PentagonExample(),
                new PolygonExample(),
            };

            const int Padding = 12;

            // Layout examples, grid layout "as square as possible"
            var nCols = Calc.Ceil(Calc.Sqrt(_examples.Count));
            var nRows = Calc.Ceil(_examples.Count / (float) nCols);
            var w = (Window.Surface.Width - ((nCols + 1) * Padding)) / nCols;
            var h = (Window.Surface.Height - ((nRows + 1) * Padding)) / nRows;
            for (var i = 0; i < _examples.Count; i++)
            {
                var x = i % nCols * (w + Padding);
                var y = i / nCols * (h + Padding);

                // Set bounds
                _examples[i].Bounds = new Rectangle(Padding + x, Padding + y, w, h);

                // Compute color
                var hue = i / (float) _examples.Count * 360F;
                _examples[i].Color = Color.FromHSV(hue, 0.7F, 0.9F);
            }

            GameLoop.StartNew(Update);
        }

        private void Update(float dt)
        {
            Time += dt;

            // 
            Graphics.Clear(Color.Gray);

            // Draw examples
            foreach (var example in _examples)
            {
                Graphics.Color = Color.LightGray;
                Graphics.DrawRect(example.Bounds);

                Graphics.Color = Color.Black;
                Graphics.DrawRectOutline(example.Bounds, 4);

                example.Update(Graphics, Time + example.Phase);
            }

            // 
            Window.Refresh();
        }

        private static void Main(string[] args)
        {
            Run<Program>();
        }

        private abstract class Example
        {
            public Rectangle Bounds { get; set; }

            public Color Color { get; set; }

            public float Phase { get; } = Calc.Random.NextFloat(0, Calc.TwoPi);

            internal protected abstract void Update(GraphicsContext gfx, float time);
        }

        private sealed class TriangleExample : Example
        {
            protected internal override void Update(GraphicsContext gfx, float time)
            {
                var center = Bounds.Center;
                var radius = Calc.Min(Bounds.Width, Bounds.Height) / 2F - 8;

                var t = Calc.TwoPi / 3F;
                var a = center + Vector.FromAngle(time + (t * 0)) * radius;
                var b = center + Vector.FromAngle(time + (t * 1)) * radius;
                var c = center + Vector.FromAngle(time + (t * 2)) * radius;

                gfx.Color = Color;
                gfx.DrawTriangle(a, b, c);

                gfx.Color = Color.Black;
                gfx.DrawTriangleOutline(a, b, c, 6);
            }
        }

        private sealed class CircleExample : Example
        {
            protected internal override void Update(GraphicsContext gfx, float time)
            {
                var center = Bounds.Center;
                var radius = Calc.Min(Bounds.Width, Bounds.Height) / 2F - 8;

                gfx.Color = Color;
                gfx.DrawCircle(center, radius);

                gfx.Color = Color.Black;
                gfx.DrawCircleOutline(center, radius, 6);
            }
        }

        private sealed class RectangleExample : Example
        {
            protected internal override void Update(GraphicsContext gfx, float time)
            {
                var center = Bounds.Center;
                var radius = Calc.Min(Bounds.Width, Bounds.Height) / Calc.Sqrt2 - 8;

                gfx.PushState();
                {
                    gfx.Transform = Matrix.CreateTransform(center, time, 1F);

                    var rectangle = new Rectangle(-radius / 2, -radius / 2, radius, radius);

                    gfx.Color = Color;
                    gfx.DrawRect(rectangle);

                    gfx.Color = Color.Black;
                    gfx.DrawRectOutline(rectangle, 6);
                }
                gfx.PopState();
            }
        }

        private sealed class PentagonExample : Example
        {
            protected internal override void Update(GraphicsContext gfx, float time)
            {
                var center = Bounds.Center;
                var radius = Calc.Min(Bounds.Width, Bounds.Height) / 2F - 8;

                gfx.PushState();
                {
                    gfx.Transform = Matrix.CreateTransform(center, time, 1F);

                    gfx.Color = Color;
                    gfx.DrawRegularPolygon(Vector.Zero, radius, 5);

                    gfx.Color = Color.Black;
                    gfx.DrawRegularPolygonOutline(Vector.Zero, radius, 5, 6);
                }
                gfx.PopState();
            }
        }

        private sealed class PolygonExample : Example
        {
            protected internal override void Update(GraphicsContext gfx, float time)
            {
                var center = Bounds.Center;
                var radius = Calc.Min(Bounds.Width, Bounds.Height) / 2F - 8;

                var polygon = new Polygon(GeometryTools.GenerateStar(radius));

                gfx.PushState();
                {
                    gfx.Transform = Matrix.CreateTransform(center, time, 1F);

                    gfx.Color = Color;
                    gfx.DrawPolygon(polygon);

                    gfx.Color = Color.Black;
                    gfx.DrawPolygonOutline(polygon, 6);
                }
                gfx.PopState();
            }
        }
    }
}
