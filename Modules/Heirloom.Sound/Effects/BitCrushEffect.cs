using System;

namespace Heirloom.Sound.Effects
{
    /// <summary>
    /// An audio effect to crush bits and help mimic cheap radios and other low quality "retro" or "lo-fi" stuff.
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

            var skip = _downsampling * AudioContext.Channels;

            // For each block
            for (var a = 0; a < samples.Length; a += skip)
            {
                // For each audio channel
                for (var c = 0; c < AudioContext.Channels; c++)
                {
                    var sample = samples[a + c];

                    // Reduce effective integer bit count
                    sample = MathF.Floor(sample * bit) / bit;

                    // 
                    for (var b = 0; b < skip; b += AudioContext.Channels)
                    {
                        if ((a + b + c) >= samples.Length) { continue; }
                        samples[a + b + c] += sample;
                    }
                }
            }
        }
    }
}
