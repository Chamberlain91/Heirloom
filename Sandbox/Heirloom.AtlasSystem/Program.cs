using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.AtlasSystem
{
    public sealed class Program : GameWrapper
    {
        public RectanglePacker<int> Packer;
        public Queue<IntSize> Boxes;

        private readonly Stopwatch _sw = new Stopwatch();

        public Program() : base(CreateWindowGraphics())
        {
            Calc.SetRandomSeed(0);

            var wasted = new float[500];
            for (var i = 0; i < wasted.Length; i++) { wasted[i] = GenerateAndPack() * 100; }
            Log.Info($"Packing Waste: {Statistics.Compute(wasted)}");

            // Reset packer
            ClearPacker();
        }

        private void ClearPacker()
        {
            // Generate new boxes
            var elements = Generate().ToArray();
            elements.Shuffle(Calc.Random);

            // Find maximal box height and total area
            var maxHeight = elements.Max(b => b.Height);
            var sumArea = elements.Sum(i => i.Area);

            // Sort elements favoring similar heights before different widths
            Boxes = new Queue<IntSize>(elements);

            // Compute packer container size
            var boxAreaW = (int) Calc.Sqrt(sumArea);
            var boxAreaH = maxHeight * Boxes.Count;

            // Create packer
            Packer = new RectanglePacker<int>(boxAreaW, boxAreaH);

            static IEnumerable<IntSize> Generate()
            {
                for (var r = 0; r < 3; r++)
                {
                    // Some fake text, 16pt font
                    for (var i = 0; i < 256; i++)
                    {
                        var w = (int) Calc.Random.NextFloat(10, 18);
                        var h = (int) Calc.Random.NextFloat(14, 18);

                        yield return new IntSize(w, h);
                    }

                    // Some fake sprite anims
                    for (var i = 0; i < 100; i++) { yield return new IntSize(24, 32); }
                    for (var i = 0; i < 50; i++) { yield return new IntSize(64, 72); }

                    // Some background assets
                    yield return new IntSize(300, 250);
                    yield return new IntSize(250, 250);
                    yield return new IntSize(100, 250);

                    // Some skybox
                    yield return new IntSize(640, 400);
                }
            }
        }

        private float GenerateAndPack()
        {
            ClearPacker();

            // Insert every element
            var used = InsertAllElements();

            // Compact atlas
            Packer.Compact();

            // Compute tighter bounds
            var tightBounds = IntRectangle.Zero;
            tightBounds.Height = Packer.Elements.Select(Packer.GetRectangle).Max(r => r.Bottom);
            tightBounds.Width = Packer.Size.Width;

            // Return wasted space
            return 1F - (used / tightBounds.Area);
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
            var size = Boxes.Dequeue();
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
                if (Boxes.Count == 0) { ClearPacker(); }

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

                Log.Info($"Compact Time: {Time.GetEnglishTime((float) _sw.Elapsed.TotalSeconds)}");
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
