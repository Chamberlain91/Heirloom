using System;
using System.Collections.Generic;
using System.IO;

using Heirloom.Sound.LowLevel;

namespace Heirloom.Sound
{
    /// <summary>
    /// An object to contain (and decode) audio data into raw PCM samples.
    /// </summary>
    public sealed class AudioClip
    {
        internal readonly short[] Samples;

        #region Constructors

        /// <summary>
        /// Constructs a new audio clip from the given stream, fully decoding all samples.
        /// </summary>
        /// <param name="device">The audio device to use parameters during the decode.</param>
        /// <param name="stream">A stream to a file of a supported format.</param>
        public AudioClip(Stream stream)
            : this(DecodeFiniteStream(stream))
        { }

        /// <summary>
        /// Constructs a new audio clip from the given in-memory file, fully decoding all samples.
        /// </summary>
        /// <param name="device">The audio device to use parameters during the decode.</param>
        /// <param name="stream">An in-memory copy of a file of a supported format.</param>
        public AudioClip(byte[] file)
            : this(new MemoryStream(file))
        { }

        /// <summary>
        /// Constructs a new audio clip from existing samples decoded or generated elsewhere.
        /// The samples must be interleved to the number of channels in the device and at the sample rate of the device.
        /// </summary>
        /// <param name="device">The audio device to use parameters during the decode.</param>
        /// <param name="samples">Raw PCM samples interleved.</param>
        public AudioClip(short[] samples)
        {
            Samples = samples;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The device used in constructing this audio clip.
        /// </summary>
        internal AudioDevice Device => AudioDevice.Instance;

        /// <summary>
        /// The number of samples processed per second.
        /// </summary>
        public uint SampleRate => Device.SampleRate;

        /// <summary>
        /// The length of this clip in samples.
        /// </summary>
        public uint Length => (uint) Samples.Length;

        #endregion

        /// <summary>
        /// Gets a single sample of the raw audio data, audio is encoded as interleved stereo audio.
        /// </summary>
        public short GetSample(int offset)
        {
            return Samples[offset];
        }

        private static short[] DecodeFiniteStream(Stream stream)
        {
            using (var decoder = new AudioDecoder(stream, AudioDevice.Instance.SampleRate))
            {
                // Does the decoder report audio length?
                if (decoder.Length > 0)
                {
                    // Compute how many samples are needed
                    var samples = new short[decoder.Length * decoder.Channels];

                    // Read all samples from decoder in one go
                    var read = decoder.DecodeFrames(samples, 0, decoder.Length);
                    if (read != decoder.Length)
                    {
                        throw new InvalidOperationException($"Error when decoding, read {read} frames but expected {decoder.Length}.");
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

                    const ulong FRAME_BLOCK_SIZE = 22050;

                    ulong readFrames, totalFrames = 0;
                    var blocks = new List<short[]>();

                    do
                    {
                        // Allocate next block
                        var block = new short[FRAME_BLOCK_SIZE * AudioDevice.Instance.Channels];

                        // Read samples into block
                        readFrames = decoder.DecodeFrames(block, 0, FRAME_BLOCK_SIZE);
                        if (readFrames > 0)
                        {
                            // accumulate total frame count
                            totalFrames += readFrames;

                            // Append block
                            blocks.Add(block);
                        }

                    } while (readFrames == FRAME_BLOCK_SIZE);

                    // Concatinate blocks into total length array
                    var samples = new short[totalFrames * AudioDevice.Instance.Channels];

                    var i = 0;
                    foreach (var block in blocks)
                    {
                        // Each block is the same size
                        var blockSize = block.Length;

                        // On the final block, we have to compute how many
                        // sample are actually occupying the block via totalFrames
                        if (i == (blocks.Count - 1))
                        {
                            blockSize = (int) (totalFrames * AudioDevice.Instance.Channels) % block.Length;
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
}
