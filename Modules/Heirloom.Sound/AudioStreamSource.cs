using System.IO;

namespace Heirloom.Sound
{
    internal sealed class AudioStreamSource : AudioSource
    {
        private readonly AudioDecoder _decoder;
        private readonly Stream _stream;

        public AudioStreamSource(Stream stream)
        {
            _decoder = new AudioDecoder(stream);
            _stream = stream;
        }

        public override int Length => _decoder.Length;

        public override bool CanSeek => _stream.CanSeek;

        protected internal override int ReadSamples(short[] samples, int offset, int count)
        {
            return _decoder.Decode(samples, offset, count);
        }

        protected override void SeekInternal(int offset)
        {
            _decoder.Seek(offset);
        }
    }
}
