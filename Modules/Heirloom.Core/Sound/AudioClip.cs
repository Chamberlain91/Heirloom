using System;
using System.Collections.Generic;
using System.IO;

namespace Heirloom.Sound
{
    /// <summary>
    /// An object to contain (and decode) audio data into raw samples.
    /// </summary>
    public sealed class AudioClip
    {
        private readonly short[] _samples;

        #region Constructors

        /// <summary>
        /// Constructs a new audio clip from the given stream, fully decoding all samples.
        /// </summary>
        /// <param name="stream">A stream to a file of a supported format.</param>
        public AudioClip(Stream stream)
            : this(DecodeFiniteStream(stream))
        { }

        /// <summary>
        /// Constructs a new audio clip from the given in-memory file, fully decoding all samples.
        /// </summary>
        /// <param name="file">The raw bytes of a audio file, as if it were on disk.</param>
        public AudioClip(byte[] file)
            : this(new MemoryStream(file))
        { }

        /// <summary>
        /// Constructs a new audio clip from existing audio samples.
        /// The samples must be interleved to the expected number of channels in the device and at the sample rate of the device.
        /// </summary>
        /// <param name="samples">Raw PCM samples interleved.</param>
        public AudioClip(short[] samples)
        {
            _samples = samples;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a specific sample.
        /// </summary>
        public short this[int index] => _samples[index];

        /// <summary>
        /// Gets the duration of the clip in seconds.
        /// </summary>
        public float Duration => Length / (float) (AudioAdapter.SampleRate * AudioAdapter.Channels);

        /// <summary>
        /// Gets the length of the clip in samples.
        /// </summary>
        public int Length => _samples.Length;

        #endregion

        private static short[] DecodeFiniteStream(Stream stream)
        {
            using var decoder = AudioAdapter.Instance.CreateDecoder(stream);

            // Does the decoder report audio length?
            if (decoder.Length > 0)
            {
                var samples = new short[decoder.Length];

                // Read all samples from decoder in one go
                var read = decoder.Decode(new Span<short>(samples, 0, decoder.Length));
                if (read != decoder.Length)
                {
                    throw new InvalidOperationException($"Error when decoding, read {read} samples but expected {decoder.Length}.");
                }

                return samples;
            }
            else
            {
                // 
                // The size of the content is unknown, as long as its not infinite this will
                // attempt to decode the content by reading a finite block of samples one at
                // a time until exhausted.
                // 
                // How am I to guard against an infinite stream and warn the user?
                // 
                //   Probably detect something considered an absurd amount of time/samples
                //   and terminate reading and throw exception with relevant exception?
                // 

                const int BLOCK_SIZE = 22050;

                long count, total = 0;
                var blocks = new List<short[]>();

                do
                {
                    // Allocate next block
                    var block = new short[BLOCK_SIZE * AudioAdapter.Channels];

                    // Read samples into block
                    count = decoder.Decode(new Span<short>(block, 0, BLOCK_SIZE));
                    if (count > 0)
                    {
                        // Far too much data, probably was a infinite stream (ie, internet radio)
                        if (total >= (int.MaxValue - count))
                        {
                            // Reaching this exception would require over 13 hours of audio at 44.1K sample rate
                            throw new InvalidOperationException($"Error when decoding, too many samples! Stream may be infinite length.");
                        }

                        // accumulate total number of samples read
                        total += count;

                        // Append block
                        blocks.Add(block);
                    }

                } while (count == BLOCK_SIZE);

                // Concatinate blocks into total length array
                var samples = new short[total * AudioAdapter.Channels];

                var i = 0;
                foreach (var block in blocks)
                {
                    // Each block is the same size
                    var blockSize = block.Length;

                    // On the final block, we have to compute how many
                    // sample are actually occupying the block via total
                    if (i == (blocks.Count - 1))
                    {
                        blockSize = (int) (total * AudioAdapter.Channels) % block.Length;
                    }

                    // Copy block into final samples array
                    Array.Copy(block, 0, samples, i * block.Length, blockSize);
                    i++;
                }

                return samples;
            }
        }
    }
}
