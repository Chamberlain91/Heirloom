namespace Heirloom.Sound
{
    /// <summary>
    /// Provides an <see cref="AudioSource"/> with samples.
    /// </summary>
    public abstract unsafe class AudioProvider
    {
        protected AudioProvider()
        {
            // Does nothing
        }

        #region Properties

        /// <summary>
        /// Can this provider seek through time?
        /// </summary>
        protected internal abstract bool CanSeek { get; }

        /// <summary>
        /// The length of the audio source in samples.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        protected internal abstract int Length { get; }

        #endregion

        protected internal abstract int ReadSamples(short[] samples, int offset, int count);

        protected internal abstract void Seek(int offset);
    }
}
