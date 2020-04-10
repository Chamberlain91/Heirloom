using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect that implements a Schroeder reverb.
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

        public override float Process(float sample, int channel)
        {
            // Process current samples 
            sample *= FixedGain;

            var output = 0F;

            // Process comb filters (in parallel)
            foreach (var comb in _combFilters)
            {
                output += comb.Process(sample, channel);
            }

            // Process allpass filters (in series)
            foreach (var allpass in _allpassFilters)
            {
                output = allpass.Process(output, channel);
            }

            // Replace sample
            return output;
        }

        private class AllpassFilter : AudioEffect
        {
            private float[] _buffer = Array.Empty<float>();
            private int _bufferIndex;

            private float _delay;

            public AllpassFilter(float delay, float feedback)
            {
                Feedback = feedback;
                Delay = delay;
            }

            public float Feedback { get; set; }

            public float Delay
            {
                get => _delay;

                set
                {
                    _delay = value;

                    // Adjust buffer size to fit delay size
                    var size = (int) (_delay * AudioContext.SampleRate * AudioContext.Channels);
                    Array.Resize(ref _buffer, size);
                }
            }

            public override float Process(float input, int channel)
            {
                var bufferSample = _buffer[_bufferIndex];

                // Process allpass filter
                _buffer[_bufferIndex] = input + (bufferSample * Feedback);

                // Wrap around buffer
                if (++_bufferIndex >= _buffer.Length) { _bufferIndex = 0; }

                return bufferSample - input;
            }
        }

        private class CombFilter : AudioEffect
        {
            private float[] _buffer = Array.Empty<float>();
            private int _bufferIndex;

            private readonly float[] _feedback;
            private float _delay;

            public CombFilter(float delay)
            {
                _feedback = new float[AudioContext.Channels];
                Delay = delay;
            }

            public float Damp { get; set; } = 0.5F;

            private float Damp2 => 1F - Damp;

            public float Feedback { get; set; }

            public float Delay
            {
                get => _delay;

                set
                {
                    _delay = value;

                    // Adjust buffer size to fit delay size
                    var size = (int) (_delay * AudioContext.SampleRate * AudioContext.Channels);
                    Array.Resize(ref _buffer, size);
                }
            }

            public override float Process(float input, int channel)
            {
                var output = _buffer[_bufferIndex];

                _feedback[channel] *= Damp;
                _feedback[channel] += output * Damp2;

                _buffer[_bufferIndex] = input + (_feedback[channel] * Feedback);

                if (++_bufferIndex >= _buffer.Length) { _bufferIndex = 0; }

                return output;
            }
        }
    }
}
