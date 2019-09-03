using System.IO;

using Heirloom.Sound.LowLevel;

namespace Heirloom.Sound
{
    internal sealed class AudioStreamProvider : AudioSourceProvider
    {
        private readonly AudioDecoder _decoder;
        private readonly Stream _stream;

        public AudioStreamProvider(Stream stream)
        {
            _decoder = new AudioDecoder(stream, SampleRate);
            _stream = stream;
        }

        protected internal override bool CanSeek => _stream.CanSeek;

        protected internal override uint Length => (uint) _decoder.Length;

        protected internal override int ReadFrames(short[] samples, int offset, int count)
        {
            // decoder works in pcm frame coordinates
            return (int) _decoder.DecodeFrames(samples, (ulong) offset, (ulong) count);
        }

        protected internal override void SeekToFrame(int offset)
        {
            _decoder.SeekToFrame((ulong) offset);
        }
    }
}
