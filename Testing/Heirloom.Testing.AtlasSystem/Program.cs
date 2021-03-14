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
        public int Total;

        private static readonly Experiment[] _experiments = new[]
        {
            GenerateTileset(),
            GenerateRandomSmall(),
            GenerateRandomLarge(),
            GenerateRandomAll(),
            GenerateSynthetic()
        };

        private readonly Stopwatch _sw = new Stopwatch();

        public Program() : base(CreateWindowGraphics())
        {
            Calc.SetRandomSeed(0);

            var sw = new Stopwatch();

            const int Iterations = 10;

            var wasted = new float[Iterations];
            var time = new float[Iterations];

            // 
            foreach (var experiment in _experiments)
            {
                Log.Info($"{experiment.Name} ({experiment.Items.Count()})");

                foreach (var sort in new[] { false, true })
                {
                    for (var i = 0; i < Iterations; i++)
                    {
                        // Prepare packer
                        ClearPacker(experiment, sort);
                        sw.Restart();

                        // Insert every element
                        var used = InsertAllElements();

                        // Record initial time
                        time[i] = (float) sw.Elapsed.TotalMilliseconds;

                        // Compute wasted space
                        wasted[i] = GetWastedSpace(used);
                    }

                    if (sort) { Log.Info($"    Sorted"); }
                    else { Log.Info($"    Unsorted"); }

                    Log.Info($"        Waste: {Statistics.Compute(wasted):N2} %");
                    Log.Info($"        Time:  {Statistics.Compute(time):N2} ms");
                }

                Log.Info(string.Empty);
            }

            // Reset packer
            ClearPacker(GenerateSynthetic(), true);

            float GetWastedSpace(float used)
            {
                // Compute tighter bounds
                var tightBounds = IntRectangle.Zero;
                tightBounds.Height = Packer.Elements.Select(Packer.GetRectangle).Max(r => r.Bottom);
                tightBounds.Width = Packer.Size.Width;

                return (1F - (used / tightBounds.Area)) * 100;
            }
        }

        private static Experiment GenerateTileset()
        {
            var size = new IntSize(Image.MaxImageDimension, Image.MaxImageDimension);

            const int SizeMin = 16;
            const int SizeMax = 16;

            var area = 0;
            var maxArea = size.Area * 1.0;
            var items = new List<IntSize>();
            while (area < maxArea)
            {
                var w = Calc.Random.Next(SizeMin, SizeMax);
                var h = Calc.Random.Next(SizeMin, SizeMax);
                if (area + (w * h) <= maxArea)
                {
                    items.Add(new IntSize(w, h));
                }

                area += w * h;
            }

            return new Experiment("Perfect 16x16", size, items);
        }

        private static Experiment GenerateRandomSmall()
        {
            var size = new IntSize(Image.MaxImageDimension, Image.MaxImageDimension);

            const int SizeMin = 16;
            const int SizeMax = 32;

            var area = 0;
            var maxArea = size.Area * 0.75;
            var items = new List<IntSize>();
            while (area < maxArea)
            {
                var w = Calc.Random.Next(SizeMin, SizeMax);
                var h = Calc.Random.Next(SizeMin, SizeMax);
                if (area + (w * h) <= maxArea)
                {
                    items.Add(new IntSize(w, h));
                }

                area += w * h;
            }

            return new Experiment($"Random Items ({SizeMin} to {SizeMax})", size, items);
        }

        private static Experiment GenerateRandomLarge()
        {
            var size = new IntSize(Image.MaxImageDimension, Image.MaxImageDimension);

            const int SizeMin = 128;
            const int SizeMax = 512;

            var area = 0;
            var maxArea = size.Area * 0.75;
            var items = new List<IntSize>();
            while (area < maxArea)
            {
                var w = Calc.Random.Next(SizeMin, SizeMax);
                var h = Calc.Random.Next(SizeMin, SizeMax);
                if (area + (w * h) <= maxArea)
                {
                    items.Add(new IntSize(w, h));
                }

                area += w * h;
            }

            return new Experiment($"Random Items ({SizeMin} to {SizeMax})", size, items);
        }

        private static Experiment GenerateRandomAll()
        {
            var size = new IntSize(Image.MaxImageDimension, Image.MaxImageDimension);

            const int SizeMin = 16;
            const int SizeMax = 256;

            var area = 0;
            var maxArea = size.Area * 0.75;
            var items = new List<IntSize>();
            while (area < maxArea)
            {
                var w = Calc.Random.Next(SizeMin, SizeMax);
                var h = Calc.Random.Next(SizeMin, SizeMax);
                if (area + (w * h) <= maxArea)
                {
                    items.Add(new IntSize(w, h));
                }

                area += w * h;
            }

            return new Experiment($"Random Items ({SizeMin} to {SizeMax})", size, items);
        }

        private static Experiment GenerateSynthetic()
        {
            var size = new IntSize(Image.MaxImageDimension, Image.MaxImageDimension);

            var area = 0;
            var maxArea = size.Area * 0.75;
            var items = new List<IntSize>();
            foreach (var item in GenerateItems())
            {
                if (area + item.Area < maxArea)
                {
                    items.Add(item);
                }

                area += item.Area;

                if (area >= maxArea)
                {
                    break;
                }
            }

            return new Experiment("Synthetic", size, items);

            static IEnumerable<IntSize> GenerateItems()
            {
                while (true)
                {
                    if (Calc.Random.Chance(0.02F))
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

        private void ClearPacker(Experiment experiment, bool sort)
        {
            // Generate new boxes
            Boxes = experiment.Items.ToList();
            Total = Boxes.Count;

            if (sort)
            {
                // Optimize insertion order
                // Note: Reversed 'a' and 'b' here because we use the list like a queue from the end
                Boxes.Sort((a, b) => Compare(b, a, experiment.Size.Width));
            }

            // Create packer
            Packer = new RectanglePacker<int>(experiment.Size);

            static int Compare(IntSize a, IntSize b, int packerWidth)
            {
                var costA = (a.Height * packerWidth) + b.Width;
                var costB = (b.Height * packerWidth) + b.Width;
                return costB.CompareTo(costA);
            }
        }

        private float InsertAllElements()
        {
            var used = 0;
            while (Boxes.Count > 0)
            {
                var c = InsertElement();
                if (c == 0) { break; }
                used += c;
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
                Log.Error($"Unable to fit box. ({name} / {Total})");
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
                if (Boxes.Count == 0) { ClearPacker(GenerateSynthetic(), true); }

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

    internal readonly struct Experiment
    {
        public readonly IntSize Size;

        public readonly IEnumerable<IntSize> Items;

        public readonly string Name;

        public Experiment(string name, IntSize size, IEnumerable<IntSize> items)
        {
            Size = size;
            Items = items;
            Name = name;
        }
    }
}
