namespace Heirloom.Sound
{
    /// <summary>
    /// Provides an <see cref="AudioSource"/> with samples when played.
    /// </summary>
    public abstract unsafe class AudioSourceProvider
    {
        protected AudioSourceProvider()
        {
            // Does nothing
        }

        #region Properties

        /// <summary>
        /// The number of samples processed per second.
        /// </summary>
        public uint SampleRate => AudioDevice.Instance.SampleRate;

        /// <summary>
        /// The number of audio channels (interleved).
        /// </summary>
        public uint Channels => AudioDevice.Instance.Channels;

        /// <summary>
        /// Can this provided seek through time?
        /// </summary>
        protected internal abstract bool CanSeek { get; }

        /// <summary>
        /// The length of the audio source in PCM frames.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        protected internal abstract uint Length { get; }

        #endregion

        internal protected abstract int ReadFrames(short[] samples, int frameOffset, int frameCount);

        protected internal abstract void SeekToFrame(int offset);
    }
}
