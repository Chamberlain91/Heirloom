using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Benchmark
{
    public class BenchmarkApp : RenderLoop
    {
        private const float CapacityInterval = 0.33F;
        private const float SearchInterval = CapacityInterval * 4;

        private readonly IReadOnlyList<Benchmark> _benchmarks;
        private int _benchmarkIndex = 0;

        private bool _isComplete = false;

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
                new Benchmark(targetFPS, "Large", 1 / 1F, 0, surface, _rabbitImages),
                new Benchmark(targetFPS, "Medium", 1 / 3F,0, surface, _rabbitImages),
                new Benchmark(targetFPS, "Small", 1 / 9F, 0, surface, _rabbitImages),
                new Benchmark(targetFPS, "Tiny", 1 / 27F, 0, surface, _rabbitImages),
                new Benchmark(targetFPS, "Casino", 1F, 1F, surface, _casinoImages)
            };
        }

        protected override void Update(Graphics ctx, float delta)
        {
            // If the current benchmark is complete
            if (_benchmarks[_benchmarkIndex].Phase == Phase.Complete)
            {
                // Can we move to the next benchmark?
                if ((_benchmarkIndex + 1) < _benchmarks.Count) { _benchmarkIndex++; }
                else
                {
                    _isComplete = true;

                    // Complete!
                    // todo: write to "benchmark_results.txt"
                }
            }

            // Update the current benchmark
            _benchmarks[_benchmarkIndex].Update(delta);

            ctx.Clear(_backgroundColor);

            if (_isComplete)
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
                var resolutionInfo = $"{ctx.Surface.Width}x{ctx.Surface.Height}";
                var overallInfo = CreateTableRow("Overall", $"{score,0:N0}");

                // 
                DrawStateText(ctx, $"Heirloom Benchmark\n{resolutionInfo}\n\n{benchmarkInfo}\n{overallInfo}");
            }
            else
            {
                // Draw current benchmark
                _benchmarks[_benchmarkIndex].Render(ctx, delta);

                // Draw evaluation progress
                var overallProgress = _benchmarks.Average(x => x.Progress);
                var overallInfo = CreateTableRow("Evaluating", $"{overallProgress,3:N0}%");
                DrawStateText(ctx, $"Heirloom Benchmark\n{overallInfo}");
            }
        }

        private static string CreateTableRow(string name, string info, int width = 24)
        {
            name = $"{name}:";
            var spac = new string(' ', Calc.Max(1, width - name.Length - info.Length));
            return $"{name}{spac}{info}";
        }

        private void DrawStateText(Graphics ctx, string text)
        {
            var size = TextRenderer.Measure(text, Font.Default, 32);
            var rect = new Rectangle((ctx.Surface.Width - size.Width) / 2F, (ctx.Surface.Height - size.Height) / 2F, size.Width, size.Height);

            ctx.Color = Color.Gray;
            ctx.DrawRect(Rectangle.Inflate(rect, 10));

            ctx.Color = Color.LightGray;
            ctx.DrawRect(Rectangle.Inflate(rect, 8));

            ctx.Color = _pomegranate;
            ctx.DrawText(text, rect.Position + (size.Width / 2, 0), Font.Default, 32, TextAlign.Center);
        }

        internal enum Phase
        {
            Before,
            Capacity,
            Search,
            Complete
        }

        public sealed class Benchmark
        {
            private readonly Random _random = new Random(0);

            private readonly Heartbeat _heartbeat;
            private readonly int _targetFPS;

            private IntRange _searchDomain;
            private int _searchDomainThreshold;
            private int _searchDomainCapacity;
            private int _searchStep = 1;

            // Quarter million cards *should* always be a grotesque amount?
            private readonly Particle[] _particles = new Particle[750000];

            private float _frameElapsed = 0;
            private int _frameCount = 0;

            public IReadOnlyList<Image> Images { get; }

            public float Scale { get; }

            public float Rotation { get; }

            public string Name { get; }

            public Benchmark(int targetFPS, string name, float scale, float rotation, Surface surface, IEnumerable<Image> images)
            {
                Name = name;

                Rotation = rotation;
                Scale = scale;

                // Load Images
                Images = images.ToList();

                // 
                _targetFPS = targetFPS;

                _searchDomainCapacity = 500;
                _searchDomainThreshold = 1;

                // Create particles
                for (var i = 0; i < _particles.Length; i++)
                {
                    _particles[i] = CreateParticle(i, surface, scale);
                }

                // 
                _heartbeat = new Heartbeat(CardSearchUpdate, 1F);
            }

            internal int Count { get; private set; }

            internal Phase Phase { get; private set; } = Phase.Before;

            internal bool IsEvaluating => Phase == Phase.Capacity || Phase == Phase.Search;

            internal int Progress // 0 to 100
            {
                get
                {
                    if (Phase == Phase.Complete) { return 100; }
                    if (Phase != Phase.Search) { return 0; }

                    var stepsDeep = Calc.Ceil(Calc.Log(_searchDomainCapacity, 2));
                    var stepsThreshold = Calc.Floor(Calc.Log(_searchDomainThreshold / 2, 2));
                    var spread = stepsDeep - stepsThreshold;

                    return Calc.Floor(_searchStep / (float) spread * 100F);
                }
            }

            public void CardSearchUpdate()
            {
                var fps = _frameCount / _frameElapsed;
                _frameElapsed -= _heartbeat.Interval;
                _frameCount = 0;

                // 
                switch (Phase)
                {
                    case Phase.Capacity:
                        FindCapacity(fps);
                        break;

                    case Phase.Search:
                        Search(fps);
                        break;
                }
            }

            private void FindCapacity(float fps)
            {
                // Have we tanked the framerate yet?
                if (fps < _targetFPS * (3F / 4F))
                {
                    // Yes, suboptimal frame rate 
                    _heartbeat.Interval = SearchInterval;
                    Phase = Phase.Search;

                    // 
                    _searchDomainThreshold = Calc.Clamp(Calc.Ceil(_searchDomainCapacity * 0.01F), 2, 250);
                    _searchDomainCapacity = Count;

                    // Construct search domain
                    _searchDomain = new IntRange(0, _searchDomainCapacity);
                    Count = _searchDomainCapacity / 2;
                }
                else
                {
                    // No, increase particle count
                    Count = _searchDomainCapacity;
                    _searchDomainCapacity *= 2;

                    // Have we exceeded the particle buffer (how...?)
                    if (Count >= _particles.Length)
                    {
                        // Topped out, jump straight to complete!
                        _searchDomainCapacity = _particles.Length;
                        Count = _searchDomainCapacity;

                        // 
                        Phase = Phase.Complete;
                    }
                }
            }

            private void Search(float fps)
            {
                // Are we within target fps tolerance?
                if (_searchDomain.Size < _searchDomainThreshold)
                {
                    // Found our desired score
                    Phase = Phase.Complete;
                }
                else
                {
                    if (fps >= _targetFPS)
                    {
                        // Increase Cards
                        _searchDomain = new IntRange(Count, _searchDomain.Max);
                        Count = _searchDomain.Average;
                    }
                    else
                    {
                        // Reduce Cards
                        _searchDomain = new IntRange(_searchDomain.Min, Count);
                        Count = _searchDomain.Average;
                    }

                    // 
                    _searchStep++;
                }
            }

            internal void Update(float delta)
            {
                // If complete, nothing to do!
                if (Phase == Phase.Complete) { return; }

                // If in the before phase, immediately become active!
                if (Phase == Phase.Before)
                {
                    _heartbeat.Interval = CapacityInterval;
                    Count = _searchDomainCapacity;
                    Phase = Phase.Capacity;
                }

                // 
                _heartbeat.Update(delta);
            }

            internal void Render(Graphics ctx, float delta)
            {
                // 
                if (Phase != Phase.Complete)
                {
                    _frameElapsed += delta;
                    _frameCount++;
                }

                // 
                var scale = Matrix.CreateScale(Scale);
                var bounds = new Rectangle(Vector.Zero, ctx.Surface.Size);

                // Draw Particles
                for (var i = 0; i < Count; i++)
                {
                    var particle = _particles[i];

                    // Update
                    particle.Update(in bounds, in delta, Scale);
                    particle.Rotation = Rotation * Calc.Sin(i + particle.Time);

                    // Draw
                    ctx.DrawImage(particle.Image, particle.Transform * scale);
                }
            }

            private Particle CreateParticle(int index, Surface surface, float scale)
            {
                // Create card
                var image = Images[index % Images.Count];
                var card = new Particle(image);

                // Randomize position
                card.Position.X = _random.NextFloat(0, surface.Width - image.Width * scale);
                card.Position.Y = _random.NextFloat(0, surface.Height - image.Height * scale);

                // Randomize velocity
                card.Velocity.X = _random.NextFloat(-1F, +1F);
                card.Velocity.Y = _random.NextFloat(-1F, +1F);
                card.Velocity = card.Velocity.Normalized * 200F;

                // 
                return card;
            }
        }

        private static IEnumerable<Image> WrapAtlas(Image[] images)
        {
            Image.CreateAtlas(images);
            return images;
        }

        private readonly Color _backgroundColor = Color.Parse("2C3E50");
        private readonly Color _pomegranate = Color.Parse("C0392B");

        private static readonly IEnumerable<Image> _rabbitImages = WrapAtlas(new[]
        {
            new Image(Files.OpenStream("files/rabbits/rabbit1.png")),
            new Image(Files.OpenStream("files/rabbits/rabbit2.png")),
            new Image(Files.OpenStream("files/rabbits/rabbit3.png")),
            new Image(Files.OpenStream("files/rabbits/rabbit4.png")),
            new Image(Files.OpenStream("files/rabbits/rabbit5.png")),
            new Image(Files.OpenStream("files/rabbits/rabbit6.png")),
        });

        private static readonly IEnumerable<Image> _casinoImages = WrapAtlas(new[]
        {
            new Image(Files.OpenStream("files/casino/dieRed1.png")),
            new Image(Files.OpenStream("files/casino/dieRed2.png")),
            new Image(Files.OpenStream("files/casino/dieRed3.png")),
            new Image(Files.OpenStream("files/casino/dieRed4.png")),
            new Image(Files.OpenStream("files/casino/dieRed5.png")),
            new Image(Files.OpenStream("files/casino/dieRed6.png")),

            new Image(Files.OpenStream("files/casino/chipBlackWhite_border.png")),
            new Image(Files.OpenStream("files/casino/chipBlueWhite_border.png")),
            new Image(Files.OpenStream("files/casino/chipGreenWhite_border.png")),
            new Image(Files.OpenStream("files/casino/chipRedWhite_border.png")),
            new Image(Files.OpenStream("files/casino/chipWhiteBlue_border.png")),

            new Image(Files.OpenStream("files/casino/pieceBlack_border01.png")),
            new Image(Files.OpenStream("files/casino/pieceBlack_border10.png")),
            new Image(Files.OpenStream("files/casino/pieceBlack_border17.png")),

            new Image(Files.OpenStream("files/casino/pieceBlue_border02.png")),
            new Image(Files.OpenStream("files/casino/pieceBlue_border11.png")),
            new Image(Files.OpenStream("files/casino/pieceBlue_border18.png")),

            new Image(Files.OpenStream("files/casino/pieceRed_border01.png")),
            new Image(Files.OpenStream("files/casino/pieceRed_border10.png")),
            new Image(Files.OpenStream("files/casino/pieceRed_border17.png")),

            new Image(Files.OpenStream("files/casino/pieceGreen_border01.png")),
            new Image(Files.OpenStream("files/casino/pieceGreen_border10.png")),
            new Image(Files.OpenStream("files/casino/pieceGreen_border17.png")),

            new Image(Files.OpenStream("files/casino/pieceWhite_border01.png")),
            new Image(Files.OpenStream("files/casino/pieceWhite_border10.png")),
            new Image(Files.OpenStream("files/casino/pieceWhite_border17.png")),

            new Image(Files.OpenStream("files/casino/cardDiamondsA.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds2.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds3.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds4.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds5.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds6.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds7.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds8.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds9.png")),
            new Image(Files.OpenStream("files/casino/cardDiamonds10.png")),
            new Image(Files.OpenStream("files/casino/cardDiamondsJ.png")),
            new Image(Files.OpenStream("files/casino/cardDiamondsQ.png")),
            new Image(Files.OpenStream("files/casino/cardDiamondsK.png")),

            new Image(Files.OpenStream("files/casino/cardHeartsA.png")),
            new Image(Files.OpenStream("files/casino/cardHearts2.png")),
            new Image(Files.OpenStream("files/casino/cardHearts3.png")),
            new Image(Files.OpenStream("files/casino/cardHearts4.png")),
            new Image(Files.OpenStream("files/casino/cardHearts5.png")),
            new Image(Files.OpenStream("files/casino/cardHearts6.png")),
            new Image(Files.OpenStream("files/casino/cardHearts7.png")),
            new Image(Files.OpenStream("files/casino/cardHearts8.png")),
            new Image(Files.OpenStream("files/casino/cardHearts9.png")),
            new Image(Files.OpenStream("files/casino/cardHearts10.png")),
            new Image(Files.OpenStream("files/casino/cardHeartsJ.png")),
            new Image(Files.OpenStream("files/casino/cardHeartsQ.png")),
            new Image(Files.OpenStream("files/casino/cardHeartsK.png")),

            new Image(Files.OpenStream("files/casino/cardSpadesA.png")),
            new Image(Files.OpenStream("files/casino/cardSpades2.png")),
            new Image(Files.OpenStream("files/casino/cardSpades3.png")),
            new Image(Files.OpenStream("files/casino/cardSpades4.png")),
            new Image(Files.OpenStream("files/casino/cardSpades5.png")),
            new Image(Files.OpenStream("files/casino/cardSpades6.png")),
            new Image(Files.OpenStream("files/casino/cardSpades7.png")),
            new Image(Files.OpenStream("files/casino/cardSpades8.png")),
            new Image(Files.OpenStream("files/casino/cardSpades9.png")),
            new Image(Files.OpenStream("files/casino/cardSpades10.png")),
            new Image(Files.OpenStream("files/casino/cardSpadesJ.png")),
            new Image(Files.OpenStream("files/casino/cardSpadesQ.png")),
            new Image(Files.OpenStream("files/casino/cardSpadesK.png")),

            new Image(Files.OpenStream("files/casino/cardClubsA.png")),
            new Image(Files.OpenStream("files/casino/cardClubs2.png")),
            new Image(Files.OpenStream("files/casino/cardClubs3.png")),
            new Image(Files.OpenStream("files/casino/cardClubs4.png")),
            new Image(Files.OpenStream("files/casino/cardClubs5.png")),
            new Image(Files.OpenStream("files/casino/cardClubs6.png")),
            new Image(Files.OpenStream("files/casino/cardClubs7.png")),
            new Image(Files.OpenStream("files/casino/cardClubs8.png")),
            new Image(Files.OpenStream("files/casino/cardClubs9.png")),
            new Image(Files.OpenStream("files/casino/cardClubs10.png")),
            new Image(Files.OpenStream("files/casino/cardClubsJ.png")),
            new Image(Files.OpenStream("files/casino/cardClubsQ.png")),
            new Image(Files.OpenStream("files/casino/cardClubsK.png")),
        });
    }
}
