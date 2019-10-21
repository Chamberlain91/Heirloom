using System;

namespace Heirloom.Sound
{
    public class AudioBuffer
    {
        private float[] _samples;
        private int _front;

        public AudioBuffer(int capacity)
        {
            _samples = new float[capacity];
            Count = 0;
        }

        /// <summary>
        /// Gets the count of recorded samples.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the total number of samples able to be recorded.
        /// </summary>
        public int Capacity => _samples.Length;

        /// <summary>
        /// Resize the buffer.
        /// </summary>
        public void Resize(int newCapacity)
        {
            if (newCapacity > Capacity)
            {
                Array.Resize(ref _samples, newCapacity);
            }
        }

        /// <summary>
        /// Record a section of audio.
        /// </summary>
        public void Record(ReadOnlySpan<float> samples)
        {
            foreach (var sample in samples)
            {
                Record(sample);
            }
        }

        /// <summary>
        /// Record a single sample of audio.
        /// </summary>
        public void Record(float sample)
        {
            if (Count < _samples.Length) { Count++; }
            _samples[_front] = sample;

            // 
            _front++;
            if (_front >= _samples.Length)
            {
                _front = 0;
            }
        }

        /// <summary>
        /// Gets a recorded audio sample.
        /// </summary>
        public float GetSample(int i)
        {
            // Compute sample position in recording
            i = _front - Count + i;
            if (i < 0) { i += _samples.Length; }

            // If index is now out of range, throw a fit
            if (i < 0) { throw new ArgumentOutOfRangeException(); }
            if (i >= _samples.Length) { throw new ArgumentOutOfRangeException(); }

            // Return requested example
            return _samples[i];
        }
    }
}
