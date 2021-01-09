using System;

namespace Heirloom.Sound
{
    /// <summary>
    /// An interface representing an audio decoder.
    /// </summary>
    public interface IAudioDecoder : IDisposable
    {
        /// <summary>
        /// Gets tha value that determines if this decoder been disposed.
        /// Once disposed, this instance is useless.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets the length of the pcm frames known to the decoder.
        /// May be zero if audio is a stream or simply unknown to the audio format.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Seeks the decoder to a desired sample position from the beginning of the audio data.
        /// </summary>
        /// <param name="offset">Some offset in samples.</param>
        /// <returns>True, if seeking is possible and was successful.</returns>
        bool Seek(int offset);

        /// <summary>
        /// Decodes the next block of samples, writing samples into <paramref name="samples"/>.
        /// </summary>
        /// <param name="samples">The buffer to write decoded samples into.</param>
        /// <returns>The actual number of samples decoded, will be less than or equal to the length <paramref name="samples"/>.</returns>
        int Decode(Span<short> samples);
    }
}
