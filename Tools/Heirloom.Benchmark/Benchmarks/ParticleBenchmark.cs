using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Benchmark
{
    public abstract class ParticleBenchmark : Benchmark
    {
        private const int MaxParticleCount = 1000000; // million
        private const int MinParticleCount = 128;

        private const float NominalEvaluationDuration = 4F;

        private const int FramerateTarget = 60;

        // -------------------------------------------------------------------

        public Particle[] Particles;

        public Image[] Images;

        private EvaluationPhase _evaluationPhase;
        private float _evaluationTime;

        private IntRange _particleRange;
        private float _particleCountTolerance;
        private int _particleCount;

        private float _fpsAccum;
        private int _fpsCount;

        private int _particleCapacity;
        private int _particleLowerCapacity;
        private int _currentStep;

        private readonly int _initialParticleCount;

        protected ParticleBenchmark(string name, int initialParticleCount, IEnumerable<Image> images)
            : base(name)
        {
            _initialParticleCount = initialParticleCount;

            // 
            Images = images.ToArray();

            // Allocate max particle count
            Particles = new Particle[MaxParticleCount];
            for (var i = 0; i < Particles.Length; i++)
            {
                Particles[i] = new Particle(Calc.Random.Choose(Images));
            }

            Units = "images";
        }

        public override void Initialize(in Rectangle bounds)
        {
            // Randomize particles
            for (var i = 0; i < Particles.Length; i++)
            {
                Particles[i].Randomize(in bounds);
            }

            // Clear progress and score
            Progress = 0;
            Score = 0;

            // Reset values
            _evaluationPhase = EvaluationPhase.FindUpperLimit;
            _evaluationTime = GetNominalEvaluationDuration();

            _particleRange = IntRange.Zero;
            _particleCount = _initialParticleCount;

            _currentStep = 0;

            // Start with FPS way to high to compensate for NET to warm up
            _fpsAccum = ushort.MaxValue;
            _fpsCount = 0;
        }

        private float GetNominalEvaluationDuration()
        {
            if (_evaluationPhase == EvaluationPhase.FindUpperLimit) { return NominalEvaluationDuration / 4F; }
            else { return NominalEvaluationDuration; }
        }

        protected override void Update(GraphicsContext gfx, in Rectangle bounds, float dt)
        {
            _evaluationTime -= dt;

            // Have we elapsed enough evaluation time?
            if (_evaluationTime <= 0)
            {
                // Reset evaluation time
                _evaluationTime = GetNominalEvaluationDuration();

                // Compute average FPS during evaluation
                var fps = _fpsAccum / _fpsCount;

                // Reset FPS accumulator
                _fpsAccum = 0;
                _fpsCount = 0;

                if (!IsComplete)
                {
                    // If finding upper limit
                    if (_evaluationPhase == EvaluationPhase.FindUpperLimit)
                    {
                        bool converge;
                        if (fps > FramerateTarget)
                        {
                            // Console.WriteLine($"{_particleCount} ({fps:N2}fps)");

                            // Increase (double)
                            _particleCount *= 2;

                            // 
                            if (_particleCount > MaxParticleCount)
                            {
                                // Hit the ceiling, clamp and print warning
                                Console.WriteLine("Warn: Hit the particle ceiling.");
                                _particleCount = MaxParticleCount;
                                converge = true;
                            }
                            else
                            {
                                // Keep finding upper limit
                                converge = false;
                            }
                        }
                        else
                        {
                            // FPS fell below target
                            converge = true;
                        }

                        if (converge)
                        {
                            // Goto Balance Phase
                            _evaluationPhase = EvaluationPhase.ConvergeTarget;

                            // Compute particle search range
                            _particleCapacity = Calc.Min(_particleCount, MaxParticleCount);

                            var log = (int) Calc.Log(_particleCapacity, 2);
                            var minCapacity = (int) Calc.Pow(2, Calc.Max(2, log - 2));

                            _particleLowerCapacity = minCapacity;

                            _particleRange = new IntRange(_particleLowerCapacity, _particleCapacity);
                            _particleCount = _particleRange.Average;

                            _particleCountTolerance = (int) ((_particleCapacity - _particleLowerCapacity) * 0.05F);
                        }
                    }
                    else
                    {
                        // Check completion conditions
                        var satisfyParticleTolerance = _particleRange.Size < _particleCountTolerance;

                        // Compute progress
                        var steps = Calc.Log((_particleCapacity - _particleLowerCapacity) / _particleCountTolerance, 2);
                        Progress = Calc.Min(_currentStep / steps, 1F);

                        // Console.WriteLine($"{fps:N2}fps w/ {_particleCount} -> {_currentStep} / {steps} | {_particleRange} [{_particleCountTolerance}]");

                        // If all conditions are satisfied, we are complete
                        if (satisfyParticleTolerance)
                        {
                            Score = _particleCount;

                            IsComplete = true;
                            Progress = 1F;
                        }
                        // else, find balancing point
                        else
                        {
                            // Higher, increase lower bound
                            if (fps > FramerateTarget) { _particleRange.Min = _particleCount; }
                            // Lower, decrease upper bound
                            else { _particleRange.Max = _particleCount; }

                            // Clamp particle range
                            if (_particleRange.Min < MinParticleCount) { _particleRange.Min = MinParticleCount; }
                            if (_particleRange.Max > MaxParticleCount) { _particleRange.Max = MaxParticleCount; }
                            _particleCount = _particleRange.Average;
                        }

                        // 
                        _currentStep++;
                    }
                }
            }

            // Accumulate FPS metric
            _fpsAccum += gfx.CurrentFPS;
            _fpsCount++;

            UpdateParticles(in bounds, in dt);
            DrawParticles(gfx);
        }

        private void UpdateParticles(in Rectangle bounds, in float dt)
        {
            for (var i = 0; i < _particleCount; i++)
            {
                var particle = Particles[i];

                // Update particle
                particle.Update(in bounds, in dt);
            }
        }

        private void DrawParticles(GraphicsContext gfx)
        {
            for (var i = 0; i < _particleCount; i++)
            {
                var particle = Particles[i];

                // Draw particle
                gfx.DrawImage(particle.Image, Matrix.CreateTranslation(particle.Position));
            }
        }

        private enum EvaluationPhase
        {
            FindUpperLimit,
            ConvergeTarget
        }
    }
}
