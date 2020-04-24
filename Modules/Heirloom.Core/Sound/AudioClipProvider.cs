using System;

namespace Heirloom
{
    internal sealed class AudioClipProvider : IAudioProvider
    {
        public AudioClipProvider(AudioClip clip)
        {
            Clip = clip ?? throw new ArgumentNullException(nameof(clip));
        }

        public AudioClip Clip { get; }

        public int Position { get; private set; }

        public int Length => Clip.Length;

        public bool CanSeek => true;

        public int ReadSamples(short[] samples, int offset, int count)
        {
            // Compute how much can actually be read
            var remaining = Length - Position;
            var samplesToRead = Math.Min(remaining, count);

            // Copy from clip array to samples array
            for (var i = 0; i < samplesToRead; i++)
            {
                samples[offset + i] = Clip[Position + i];
            }

            // Move clip cursor along
            Position += samplesToRead;
            return samplesToRead;
        }

        public void Seek(int frameOffset)
        {
            // todo: validate cursor is within a valid range
            Position = frameOffset;
        }
    }
}
