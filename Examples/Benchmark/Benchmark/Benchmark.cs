using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Benchmark
{
    public sealed class Benchmark
    {
        public const float CapacityInterval = 0.33F;
        public const float SearchInterval = CapacityInterval * 4;

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

        internal BenchmarkPhase Phase { get; private set; } = BenchmarkPhase.Before;

        internal bool IsEvaluating => Phase == BenchmarkPhase.Capacity || Phase == BenchmarkPhase.Search;

        internal int Progress // 0 to 100
        {
            get
            {
                if (Phase == BenchmarkPhase.Complete) { return 100; }
                if (Phase != BenchmarkPhase.Search) { return 0; }

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
                case BenchmarkPhase.Capacity:
                    FindCapacity(fps);
                    break;

                case BenchmarkPhase.Search:
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
                Phase = BenchmarkPhase.Search;

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
                    Phase = BenchmarkPhase.Complete;
                }
            }
        }

        private void Search(float fps)
        {
            // Are we within target fps tolerance?
            if (_searchDomain.Size < _searchDomainThreshold)
            {
                // Found our desired score
                Phase = BenchmarkPhase.Complete;
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
            if (Phase == BenchmarkPhase.Complete) { return; }

            // If in the before phase, immediately become active!
            if (Phase == BenchmarkPhase.Before)
            {
                _heartbeat.Interval = CapacityInterval;
                Count = _searchDomainCapacity;
                Phase = BenchmarkPhase.Capacity;
            }

            // 
            _heartbeat.Update(delta);
        }

        internal void Render(Graphics ctx, float delta)
        {
            // 
            if (Phase != BenchmarkPhase.Complete)
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

}
