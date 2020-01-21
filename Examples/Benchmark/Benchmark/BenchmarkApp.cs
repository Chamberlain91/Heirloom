using System.Collections.Generic;
using System.IO;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Benchmark
{
    public class BenchmarkApp : RenderLoop
    {
        private readonly IReadOnlyList<Benchmark> _benchmarks;
        private int _benchmarkIndex = 0;

        private bool _isComplete = false;
        private bool _hasWritten = false;

        private readonly int _targetFPS;

        public BenchmarkApp(int targetFPS, Graphics ctx)
            : base(ctx)
        {
            //  
            _targetFPS = targetFPS;

            var surface = ctx.DefaultSurface;

            // 
            _benchmarks = new Benchmark[]
            {
                new Benchmark(targetFPS, "Rabbits (Large)", 1F, 0, surface, _rabbits),
                new Benchmark(targetFPS, "Rabbits (Small)", 0.5F, 0, surface, _rabbits),
                new Benchmark(targetFPS, "Rabbits (Tiny)", 0.33F, 0, surface, _rabbits),
                new Benchmark(targetFPS, "Adventure", 1F, 0, surface, _adventure),
                new Benchmark(targetFPS, "Casino", 1F, 2F, surface, _casino),
                new Benchmark(targetFPS, "Emotes", 1F, 0, surface, _emotes),
            };
        }

        protected override void Update(Graphics ctx, float delta)
        {
            // If the current benchmark is complete
            if (_benchmarks[_benchmarkIndex].Phase == BenchmarkPhase.Complete)
            {
                // Can we move to the next benchmark?
                if ((_benchmarkIndex + 1) < _benchmarks.Count) { _benchmarkIndex++; }
                else
                {
                    // Complete!
                    _isComplete = true;

                    if (_hasWritten == false)
                    {
                        _hasWritten = true;

                        using var fs = new FileStream("benchmark_results.txt", FileMode.Create);
                        using var wr = new StreamWriter(fs);

                        // Write results to file
                        wr.WriteLine(GetResultsText(ctx));
                    }
                }
            }

            // Update the current benchmark
            _benchmarks[_benchmarkIndex].Update(delta);

            ctx.Clear(_backgroundColor);

            if (_isComplete)
            {
                // Draws the results
                DrawStateText(ctx, GetResultsText(ctx));
            }
            else
            {
                // Draw current benchmark
                var benchmark = _benchmarks[_benchmarkIndex];
                benchmark.Render(ctx, delta);

                // Draw evaluation progress
                var overallProgress = (float) (_benchmarks.Average(x => x.Progress) / 100.0);
                var overallInfo = CreateProgressBar(overallProgress); // CreateTableRow("Evaluating", $"{overallProgress,3:N0}%");
                DrawStateText(ctx, $"Heirloom Benchmark\n{overallInfo}\n\"{benchmark.Name}\"");
            }
        }

        private string CreateProgressBar(float progress, int width = 24)
        {
            var n = (int) (progress * (width - 2));
            var p = new string('=', n);
            var q = new string('-', width - n - 2);
            return $"[{p}{q}]";
        }

        private string GetResultsText(Graphics ctx)
        {
            // 
            var benchmarkInfo = "";
            var overallScore = 0;

            foreach (var benchmark in _benchmarks)
            {
                // Append information about each benchmark
                benchmarkInfo += CreateTableRow(benchmark.Name, $"{benchmark.Count,0:N0}");
                benchmarkInfo += "\n";

                // Add to average score
                overallScore += benchmark.Count;
            }

            // Finish computing score
            overallScore /= _benchmarks.Count;

            // Computes 'framerate normalized score' ... not sure if this a good metric
            var score = overallScore * (_targetFPS / 60.0);

            // 
            var resolutionInfo = $"{GraphicsAdapter.Capabilities.AdapterName}\n";
            resolutionInfo += $"{ctx.Surface.Width}x{ctx.Surface.Height}";

            var overallInfo = CreateTableRow("Overall", $"{score,0:N0}");

            // 
            var resultsText = $"Heirloom Benchmark\n{resolutionInfo}\n\n{benchmarkInfo}\n{overallInfo}";
            return resultsText;
        }

        private static string CreateTableRow(string name, string info, int width = 24)
        {
            name = $"{name}:";
            var spac = new string(' ', Calc.Max(1, width - name.Length - info.Length));
            return $"{name}{spac}{info}";
        }

        private void DrawStateText(Graphics ctx, string text)
        {
            var size = TextLayout.Measure(text, Font.Default, 32);
            var rect = new Rectangle((ctx.Surface.Width - size.Width) / 2F, (ctx.Surface.Height - size.Height) / 2F, size.Width, size.Height);

            ctx.Color = Color.Gray;
            ctx.DrawRect(Rectangle.Inflate(rect, 10));

            ctx.Color = Color.LightGray;
            ctx.DrawRect(Rectangle.Inflate(rect, 8));

            ctx.Color = _pomegranate;
            ctx.DrawText(text, rect.Position + (size.Width / 2, 0), Font.Default, 32, TextAlign.Center);
        }

        private readonly Color _backgroundColor = Color.Parse("2C3E50");
        private readonly Color _pomegranate = Color.Parse("C0392B");

        private static readonly IEnumerable<Image> _emotes = LoadImages("files.emotes", false);
        private static readonly IEnumerable<Image> _rabbits = LoadImages("files.rabbits", false);
        private static readonly IEnumerable<Image> _adventure = LoadImages("files.adventure", false);
        private static readonly IEnumerable<Image> _casino = LoadImages("files.casino", true);

        private static IEnumerable<Image> LoadImages(string prefix, bool center)
        {
            var images = Files.GetEmbeddedFiles()
                              .Where(ef => ef.Identifiers.Any(i => i.StartsWith(prefix)))
                              .Select(ef => ef.Identifiers.First())
                              .Where(HasImageExtension)
                              .Select(p => new Image(p))
                              .ToArray();

            // Something went wrong couldn't find files
            if (images.Length == 0)
            {
                throw new FileNotFoundException($"Discovery of embedded with prefix '{prefix}' failed.");
            }

            if (center)
            {
                // Set the origin of each images to its center
                foreach (var image in images)
                {
                    image.Origin = (Vector) image.Size / 2F;
                }
            }

            Image.CreateAtlas(images);
            return images;
        }

        private static bool HasImageExtension(string p)
        {
            var ext = Path.GetExtension(p);
            return ext == ".png" || ext == ".jpg";
        }
    }
}
