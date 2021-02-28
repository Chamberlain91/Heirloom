using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Testing.AtlasSystem
{
    public sealed class Program : GameWrapper
    {
        public RectanglePacker<int> Packer;
        public List<IntSize> Boxes;

        private readonly Stopwatch _sw = new Stopwatch();

        public Program() : base(CreateWindowGraphics())
        {
            Calc.SetRandomSeed(0);

            var sw = new Stopwatch();

            var times = new List<(int x, float y)>();

            var inc = 200;
            for (var c = inc; c < 15000; c += inc)
            {
                const int Iterations = 100;
                inc = inc * 3 / 2;

                var wasted = new float[Iterations];
                var time = new float[Iterations];

                for (var i = 0; i < Iterations; i++)
                {
                    ClearPacker(c);
                    sw.Restart();

                    // Insert every element
                    var used = InsertAllElements();

                    // Record initial time
                    time[i] = (float) sw.Elapsed.TotalMilliseconds;

                    // Compute wasted space
                    wasted[i] = GetWastedSpace(used);
                }

                // Store mean time
                times.Add((c / 100, time.Average()));

                Log.Info($"Size: {c}");
                Log.Info($"  Waste: {Statistics.Compute(wasted)} %");
                Log.Info($"  Time:  {Statistics.Compute(time)} ms");
            }

            Log.Info(string.Join(",", times.Select(c => $"({c.x},{c.y})")));

            // Reset packer
            ClearPacker(1000);

            float GetWastedSpace(float used)
            {
                // Compute tighter bounds
                var tightBounds = IntRectangle.Zero;
                tightBounds.Height = Packer.Elements.Select(Packer.GetRectangle).Max(r => r.Bottom);
                tightBounds.Width = Packer.Size.Width;

                return (1F - (used / tightBounds.Area)) * 100;
            }
        }

        private void ClearPacker(int c)
        {
            // Generate new boxes
            Boxes = Generate(c).ToList();

            // Find maximal box height and total area
            var maxHeight = Boxes.Max(b => b.Height);
            var sumArea = Boxes.Sum(i => i.Area);

            // Compute packer container size
            var boxAreaW = (int) Calc.Sqrt(sumArea);
            var boxAreaH = maxHeight * Boxes.Count;

            // Create packer
            Packer = new RectanglePacker<int>(boxAreaW, boxAreaH);

            // Optimize insertion order
            // Note: Reversed 'a' and 'b' here because we use the list like a queue from the end
            Boxes.Sort((a, b) => Compare(b, a, Packer.Size.Width));

            static IEnumerable<IntSize> Generate(int c)
            {
                for (var i = 0; i < c; i++)
                {
                    if (Calc.Random.Chance(0.01F))
                    {
                        var w = (int) Calc.Random.NextFloat(200, 400);
                        var h = (int) Calc.Random.NextFloat(200, 400);

                        yield return new IntSize(w, h);
                    }
                    else
                    {
                        var w = (int) Calc.Random.NextFloat(8, 32);
                        var h = (int) Calc.Random.NextFloat(8, 32);

                        yield return new IntSize(w, h);
                    }
                }
            }
        }

        public static int Compare(IntSize a, IntSize b, int packerWidth)
        {
            var costA = (a.Height * packerWidth) + b.Width;
            var costB = (b.Height * packerWidth) + b.Width;
            return costB.CompareTo(costA);
        }

        private float InsertAllElements()
        {
            var used = 0;
            while (Boxes.Count > 0)
            {
                used += InsertElement();
            }

            return used;
        }

        private int InsertElement()
        {
            var size = Boxes[Boxes.Count - 1];
            Boxes.RemoveAt(Boxes.Count - 1);

            var name = Boxes.Count;

            if (!Packer.TryAdd(name, size))
            {
                Log.Error($"Unable to fit box #{name}");
                return 0;
            }
            else
            {
                return size.Area;
            }
        }

        protected override void Update(float dt)
        {
            Graphics.ResetState();
            Graphics.Clear(Color.DarkGray);
            Graphics.Transform = Matrix.CreateTranslation(32, 32);

            // Draw tight atlas frame
            if (Packer.Elements.Count > 0)
            {
                // Compute tighter bounds
                var tightBounds = IntRectangle.Zero;
                tightBounds.Height = Packer.Elements.Select(Packer.GetRectangle).Max(r => r.Bottom);
                tightBounds.Width = Packer.Size.Width;

                Graphics.Color = Color.Orange;
                Graphics.DrawRect(tightBounds);
            }

            // Draw packed images
            foreach (var image in Packer.Elements)
            {
                var rect = Packer.GetRectangle(image);

                Graphics.Color = Color.DarkGray * Color.DarkGray;
                Graphics.DrawRect(rect);

                Graphics.Color = Color.Gray;
                Graphics.DrawRectOutline(rect);

                if (rect.Width > 12 && rect.Height > 12)
                {
                    Graphics.DrawText($"{rect.Height}", rect.Center, Font.Default, 16, TextAlign.Center | TextAlign.Middle);
                }
            }

            Graphics.Color = Color.White;
            Graphics.Transform = Matrix.Identity;
            Graphics.DrawText($"Remaining: {Boxes.Count}", (Graphics.Surface.Width - 10, 10), Font.Default, 16, TextAlign.Right);

            Graphics.Screen.Refresh();

            var shift = Input.IsKeyDown(Key.LeftShift);
            if (Input.IsKeyPressed(Key.Space, true))
            {
                if (Boxes.Count == 0) { ClearPacker(1000); }

                if (!shift)
                {
                    // Insert one element
                    InsertElement();
                }
                else
                {
                    // Insert all elements
                    InsertAllElements();
                }
            }

            if (Input.IsKeyPressed(Key.C, true))
            {
                _sw.Restart();
                Packer.Compact();
                _sw.Stop();

                Log.Info($"Compact Time:  {Time.GetEnglishTime((float) _sw.Elapsed.TotalSeconds)}");
            }
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Heirloom Atlas Experiment");
            window.Maximize();

            return window.Graphics;
        }

        private static void Main(string[] args)
        {
            Application.Run<Program>();
        }
    }
}
