using System;

namespace Heirloom.Sound
{
    internal sealed class AudioClipProvider : AudioSourceProvider // todo: evaluate name
    {
        public readonly AudioClip Clip;

        private int _cursor = 0;

        public AudioClipProvider(AudioClip clip)
        {
            Clip = clip ?? throw new ArgumentNullException(nameof(clip));
        }

        // a clip can always seek as its raw data in memory
        protected internal override bool CanSeek => true;

        protected internal override uint Length => Clip.Length;

        protected internal override int ReadFrames(short[] samples, int frameOffset, int frameCount)
        {
            // moves value into stereo sample coordinates
            var sampleCount = frameCount * 2;
            var sampleOffset = frameOffset * 2;

            // Compute how much can actually be read
            var remaining = Math.Max(0, Clip.Samples.Length - _cursor);
            var samplesToRead = Math.Min(remaining, sampleCount);

            // Copy from clip array to samples array
            for (var i = 0; i < samplesToRead; i++)
            {
                samples[sampleOffset + i] = Clip.Samples[_cursor + i];
            }

            // Move clip cursor along
            _cursor += samplesToRead;
            return samplesToRead / 2; // move back into frame coordinates
        }

        protected internal override void SeekToFrame(int frameOffset)
        {
            // todo: validate cursor is within a valid range
            _cursor = frameOffset * 2;
        }
    }
}
