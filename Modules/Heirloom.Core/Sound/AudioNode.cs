using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Represents a node in the audio mixing tree.
    /// </summary>
    public abstract class AudioNode
    {
        private float[] _samples = Array.Empty<float>();
        private float _balance = 0F;
        private float _volume = 1F;

        protected AudioNode()
        {
            Effects = new List<AudioEffect>();
        }

        /// <summary>
        /// Gets the list of <see cref="AudioEffect"/> that affect the audio on this node.
        /// </summary>
        public List<AudioEffect> Effects { get; }

        /// <summary>
        /// Gets or sets the volume (gain) of the audio.
        /// </summary>
        public float Volume
        {
            get => _volume;
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException(); }
                _volume = value;
            }
        }

        /// <summary>
        /// Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right )
        /// </summary>
        public float Balance
        {
            get => _balance;
            set
            {
                if (value < -1 || value > 1) { throw new ArgumentOutOfRangeException(); }
                _balance = value;
            }
        }

        internal void MixOutput(Span<float> output)
        {
            // Resize audio buffer
            if (_samples.Length < output.Length) { Array.Resize(ref _samples, output.Length); }
            var samples = new Span<float>(_samples, 0, output.Length); // Get span of exact length

            // Populate audio buffer (ie, read from sources or child groups)
            PopulateBuffer(samples);

            // Compute panning factors
            var pan = (Balance / 2F) + 0.5F;
            var lPanFactor = MathF.Sqrt(1F - pan);
            var rPanFactor = MathF.Sqrt(pan);

            // Apply standard mixing to samples
            for (var i = 0; i < samples.Length; i++)
            {
                ref var sample = ref samples[i];

                // Volume (Gain)
                sample *= Volume;

                // Balance (Stereo Mixing/Panning)
                if (i % AudioAdapter.Channels == 0) { sample *= lPanFactor; }
                else { sample *= rPanFactor; }
            }

            lock (Effects)
            {
                // Process effect chain
                for (var i = 0; i < samples.Length; i++)
                {
                    foreach (var effect in Effects)
                    {
                        samples[i] = effect.Process(samples[i], i % AudioAdapter.Channels);
                    }
                }
            }

            // Append local buffer to output
            for (var i = 0; i < samples.Length; i++)
            {
                output[i] += samples[i];
                samples[i] = 0;
            }
        }

        protected abstract void PopulateBuffer(Span<float> buffer);
    }
}
