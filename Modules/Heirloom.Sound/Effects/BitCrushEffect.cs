using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect to crush the sound to mimic cheap radios other low quality or "retro" stuff.
    /// </summary>
    public class BitCrushEffect : AudioEffect
    {
        private int _bits;
        private int _downsampling;

        public BitCrushEffect(int bits = 6, int downSampling = 8)
        {
            Bits = bits;
            DownSampling = downSampling;
        }

        public int Bits
        {
            get => _bits;
            set
            {
                if (value < 1 || value > 16) { throw new ArgumentOutOfRangeException(); }
                _bits = value;
            }
        }

        public int DownSampling
        {
            get => _downsampling;
            set
            {
                if (value < 1) { throw new ArgumentOutOfRangeException(); }
                _downsampling = value;
            }
        }

        protected internal override void MixOutput(Span<float> samples)
        {
            var bit = 1 << _bits;
            var inc = 1 + (_downsampling - 1) * 4;

            for (var i = 0; i < samples.Length; i += inc)
            {
                var sample = 0F;

                // Compute interpolated/downsampled sample
                for (var j = 0; j < inc; j++)
                {
                    if ((i + j) < samples.Length) { sample += samples[i + j]; }
                    else { break; }
                }

                sample /= inc; // average
                samples[i] += MathF.Floor(sample * bit) / bit;
            }
        }
    }
}
