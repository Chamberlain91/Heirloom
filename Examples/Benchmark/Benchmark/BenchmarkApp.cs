using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;

namespace Benchmark
{
    public class BenchmarkApp
    {
        private readonly IReadOnlyList<Benchmark> _benchmarks;
        private int _index = 0;

        private readonly int _targetFPS;

        public BenchmarkApp(int targetFPS, int samplePeriod, int increment)
        {
            //  
            _targetFPS = targetFPS;

            // 
            _benchmarks = new Benchmark[]
            {
                new Benchmark(targetFPS, increment, samplePeriod, "Rabbits (LG)", 1 / 1F,  _rabbitImages),
                new Benchmark(targetFPS, increment, samplePeriod, "Rabbits (MD)", 1 / 3F,  _rabbitImages),
                new Benchmark(targetFPS, increment, samplePeriod, "Rabbits (SM)", 1 / 9F,  _rabbitImages),
                new Benchmark(targetFPS, increment, samplePeriod, "Rabbits (XS)", 1 / 27F, _rabbitImages),
                new Benchmark(targetFPS, increment, samplePeriod, "Casino", 1F,  _casinoImages)
            };
        }

        public void Update(float delta)
        {
            // If the current benchmark is complete
            if (_benchmarks[_index].Phase == Phase.Complete)
            {
                // Can we move to the next benchmark?
                if ((_index + 1) < _benchmarks.Count) { _index++; }
                else
                {
                    // Complete!
                    // todo: write to "benchmark_results.txt"
                }
            }

            // Update the current benchmark
            _benchmarks[_index].Update(delta);
        }

        public void Render(RenderContext ctx, float delta)
        {
            // 
            ctx.Clear(Colors.FlatUI.MidnightBlue);

            // 
            _benchmarks[_index].Render(ctx, delta);

            // 
            var statusText = "";
            var average = 0;

            foreach (var benchmark in _benchmarks)
            {
                // Get name
                var name = benchmark.Name;

                // Append status text
                var label = $"{name}: {benchmark.Count}";
                if (benchmark.IsEvaluating) { label = $"> {label} <"; }
                statusText += $"{label}\n";

                // Append to average scoring
                average += benchmark.Count;
            }

            var resolutionInfo = $"{ctx.Surface.Width}x{ctx.Surface.Height} at {_targetFPS}HZ";
            average /= _benchmarks.Count;

            DrawStateText(ctx, $"{resolutionInfo}\nOverall: {average}\n\n" + statusText);
        }

        private void DrawStateText(RenderContext ctx, string text)
        {
            var size = Font.Default.MeasureText(text, 32);
            var rect = new Rectangle((ctx.Surface.Width - size.Width) / 2F, (ctx.Surface.Height - size.Height) / 2F, size.Width, size.Height);

            ctx.Color = Colors.FlatUI.Concrete;
            ctx.DrawRect(rect.Inflate(10));

            ctx.Color = Colors.FlatUI.Clouds;
            ctx.DrawRect(rect.Inflate(8));

            ctx.Color = Colors.FlatUI.Pomegranate;
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
            private readonly int _increment;
            private readonly int _targetFPS;
            private readonly int _samplePeriod;
            private int _capacity;

            private IntRange _searchDomain;

            // Quarter million cards *should* always be a grotesque amount?
            private readonly Particle[] _particles = new Particle[250000];

            private float _frameElapsed = 0;
            private int _frameCount = 0;

            public IReadOnlyList<Image> Images { get; }

            public float Scale { get; }

            public string Name { get; }

            public Benchmark(int targetFPS, int increment, int samplePeriod, string name, float scale, IEnumerable<Image> images)
            {
                Name = name;
                Scale = scale;

                // Load Images
                Images = images.ToList();

                // 
                _samplePeriod = samplePeriod;
                _targetFPS = targetFPS;
                _increment = increment;
                _capacity = 0;

                // Create particles
                for (var i = 0; i < _particles.Length; i++)
                {
                    _particles[i] = CreateParticle(i);
                }

                // 
                _heartbeat = new Heartbeat(CardSearchUpdate, _samplePeriod);
            }

            internal int Count { get; private set; }

            internal Phase Phase { get; private set; } = Phase.Before;

            internal bool IsEvaluating => Phase == Phase.Capacity || Phase == Phase.Search;

            public void CardSearchUpdate()
            {
                var fps = _frameCount / _frameElapsed;
                _frameElapsed -= _samplePeriod;
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
                if (fps < _targetFPS * (2F / 3F))
                {
                    // Yes, suboptimal frame rate
                    Console.WriteLine("Change Phase: Search");
                    Phase = Phase.Search;
                    _capacity = Count;

                    // Construct search domain
                    _searchDomain = new IntRange(0, _capacity);
                    Count = _capacity / 2;
                }
                else
                {
                    // No, increase particle count
                    Count += _increment;

                    // Have we exceeded the particle buffer (how...?)
                    if (Count >= _particles.Length)
                    {
                        // Topped out, jump straight to complete!
                        _capacity = _particles.Length;
                        Count = _capacity;

                        Console.WriteLine("Change Phase: Complete");
                        Phase = Phase.Complete;
                    }
                }
            }

            private void Search(float fps)
            {
                // Are we within target fps tolerance?
                if (_searchDomain.Size < 75)
                {
                    // Found our desired score
                    Console.WriteLine("Change Phase: Complete");
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
                }
            }

            internal void Update(float delta)
            {
                // If complete, nothing to do!
                if (Phase == Phase.Complete) { return; }

                // If in the before phase, immediately become active!
                if (Phase == Phase.Before)
                {
                    Phase = Phase.Capacity;
                    Count = _increment;
                }

                // 
                _heartbeat.Update(delta);
            }

            internal void Render(RenderContext ctx, float delta)
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

                    // Draw
                    ctx.DrawImage(particle.Image, particle.Transform * scale);
                }
            }

            private Particle CreateParticle(int index)
            {
                // Create card
                var image = Images[index % Images.Count];
                var card = new Particle(image);

                // Randomize position
                card.Position.X = _random.NextFloat(0, 200);
                card.Position.Y = _random.NextFloat(0, 200);

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
