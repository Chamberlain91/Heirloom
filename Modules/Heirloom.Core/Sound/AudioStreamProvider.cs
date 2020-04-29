using System.IO;

namespace Heirloom.Sound
{
    internal sealed class AudioStreamProvider : IAudioProvider
    {
        private readonly AudioDecoder _decoder;
        private readonly Stream _stream;

        public AudioStreamProvider(Stream stream)
        {
            _decoder = AudioAdapter.Instance.CreateDecoder(stream);
            _stream = stream;
        }

        public int Length => _decoder.Length;

        public bool CanSeek => _stream.CanSeek;

        public int Position { get; private set; }

        public int ReadSamples(short[] samples, int offset, int count)
        {
            count = _decoder.Decode(samples, offset, count);
            Position += count;
            return count;
        }

        public void Seek(int offset)
        {
            _decoder.Seek(offset);
            Position = offset;
        }
    }
}
