using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a reverb.
    /// </summary>
    /// <remarks> Based on Freeverb </remarks>
    public class ReverbEffect : AudioEffect
    {
        private readonly AllpassFilter[] _allpassFilters;
        private readonly CombFilter[] _combFilters;

        private const float FixedGain = 0.025f;
        private const float ScaleDamp = 0.4f;
        private const float ScaleRoom = 0.30f;
        private const float OffsetRoom = 0.7f;

        private const float InitialRoomSize = 0.5f;
        private const float InitialDamp = 0.5f;

        private float _roomsize;
        private float _damping;

        public ReverbEffect(float roomSize = InitialRoomSize, float damping = InitialDamp)
        {
            const float ConversionSampleRate = 44100F;

            _allpassFilters = new AllpassFilter[4]
            {
                new AllpassFilter(556 / ConversionSampleRate, 0.5F),
                new AllpassFilter(441 / ConversionSampleRate, 0.5F),
                new AllpassFilter(341 / ConversionSampleRate, 0.5F),
                new AllpassFilter(225 / ConversionSampleRate, 0.5F)
            };

            _combFilters = new CombFilter[8]
            {
               new CombFilter(1116 / ConversionSampleRate),
               new CombFilter(1188 / ConversionSampleRate),
               new CombFilter(1277 / ConversionSampleRate),
               new CombFilter(1356 / ConversionSampleRate),
               new CombFilter(1422 / ConversionSampleRate),
               new CombFilter(1491 / ConversionSampleRate),
               new CombFilter(1557 / ConversionSampleRate),
               new CombFilter(1617 / ConversionSampleRate)
            };

            // Set intial parameters
            RoomSize = roomSize;
            Damping = damping;
        }

        /// <summary>
        /// Gets or sets the damping value. Larger values soften the sound earlier.
        /// </summary>
        public float Damping
        {
            get => _damping;

            set
            {
                if (value >= 1.0F) { throw new ArgumentOutOfRangeException(); }
                if (value <= 0.0F) { throw new ArgumentOutOfRangeException(); }

                _damping = value;
                var s = _damping * ScaleDamp;
                foreach (var comb in _combFilters) { comb.Damp = s; }
            }
        }

        /// <summary>
        /// Gets or sets the room size. Larger values mean longer reverb.
        /// </summary>
        public float RoomSize
        {
            get => _roomsize;

            set
            {
                if (value >= 1.0F) { throw new ArgumentOutOfRangeException(); }
                if (value <= 0.0F) { throw new ArgumentOutOfRangeException(); }

                _roomsize = value;

                var feedback = (_roomsize * ScaleRoom) + OffsetRoom;
                foreach (var comb in _combFilters) { comb.Feedback = feedback; }
            }
        }

        protected internal override void MixOutput(Span<float> samples)
        {
            // Its called a Schroeder Reverb
            // Which looks like the paper defines 3 all-pass filters fed into 4 comb filters
            // Freeverb then looks to use 8 comb fed into 4 all pass... so eh?

            // todo: could remove if adjusting the Feedback/Delay paramters did this step
            foreach (var allpass in _allpassFilters) { allpass.Prepare(); }
            foreach (var comb in _combFilters) { comb.Prepare(); }

            // Process current samples
            for (var i = 0; i < samples.Length; i++)
            {
                var sample = samples[i] * FixedGain;

                var outSample = 0F;

                foreach (var comb in _combFilters)
                {
                    outSample += comb.Process(sample);
                }

                // Process all-pass series
                foreach (var allpass in _allpassFilters)
                {
                    outSample = allpass.Process(outSample);
                }

                // Replace sample
                samples[i] = outSample;
            }
        }

        private class AllpassFilter
        {
            private float[] _buffer = Array.Empty<float>();
            private int _bufidx;

            public AllpassFilter(float delay, float feedback)
            {
                Delay = delay;
                Feedback = feedback;
            }

            public float Delay { get; set; }

            public float Feedback { get; set; }

            public void Prepare()
            {
                var size = (int) (Delay * AudioContext.SampleRate);
                if (_buffer.Length < size) { Array.Resize(ref _buffer, size); }
            }

            public float Process(float input)
            {
                var bufout = _buffer[_bufidx];

                var output = -input + bufout;

                _buffer[_bufidx] = input + (bufout * Feedback);

                if (++_bufidx >= _buffer.Length) { _bufidx = 0; }

                return output;
            }
        }

        private class CombFilter
        {
            private float[] _buffer = Array.Empty<float>();
            private float _filterstore;
            private int _bufidx;

            public CombFilter(float delay)
            {
                Delay = delay;
            }

            public float Damp { get; set; } = 0.5F;

            public float Damp2 => 1F - Damp;

            public float Feedback { get; set; }

            public float Delay { get; set; }

            public void Prepare()
            {
                var size = (int) (Delay * AudioContext.SampleRate);
                if (_buffer.Length < size) { Array.Resize(ref _buffer, size); }
            }

            public float Process(float input)
            {
                var output = _buffer[_bufidx];

                _filterstore *= Damp;
                _filterstore += output * Damp2;

                _buffer[_bufidx] = input + (_filterstore * Feedback);

                if (++_bufidx >= _buffer.Length) { _bufidx = 0; }

                return output;
            }
        }
    }
}
