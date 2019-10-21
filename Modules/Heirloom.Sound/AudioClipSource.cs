using System;

namespace Heirloom.Sound
{
    internal sealed class AudioClipSource : AudioSource
    {
        public readonly AudioClip Clip;

        private int _cursor = 0;

        public AudioClipSource(AudioClip clip)
        {
            Clip = clip ?? throw new ArgumentNullException(nameof(clip));
        }

        public override int Length => Clip.Length;

        public override bool CanSeek => true;

        protected internal override int ReadSamples(short[] samples, int offset, int count)
        {
            // Compute how much can actually be read
            var remaining = Length - _cursor;
            var samplesToRead = Math.Min(remaining, count);

            // Copy from clip array to samples array
            for (var i = 0; i < samplesToRead; i++)
            {
                samples[offset + i] = Clip[_cursor + i];
            }

            // Move clip cursor along
            _cursor += samplesToRead;
            return samplesToRead;
        }

        protected override void SeekInternal(int frameOffset)
        {
            // todo: validate cursor is within a valid range
            _cursor = frameOffset;
        }
    }
}
