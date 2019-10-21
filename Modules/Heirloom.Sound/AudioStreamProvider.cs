using System.IO;

namespace Heirloom.Sound
{
    internal sealed class AudioStreamProvider : AudioProvider
    {
        private readonly AudioDecoder _decoder;
        private readonly Stream _stream;

        public AudioStreamProvider(Stream stream)
        {
            _decoder = new AudioDecoder(stream);
            _stream = stream;
        }

        protected internal override bool CanSeek => _stream.CanSeek;

        protected internal override int Length => _decoder.Length;

        protected internal override int ReadSamples(short[] samples, int offset, int count)
        {
            return _decoder.Decode(samples, offset, count);
        }

        protected internal override void Seek(int offset)
        {
            _decoder.Seek(offset);
        }
    }
}
