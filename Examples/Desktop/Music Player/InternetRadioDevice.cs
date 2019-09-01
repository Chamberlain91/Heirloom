using System.IO;

using Heirloom.Sound;
using Heirloom.Sound.LowLevel;

namespace Heirloom.Examples.MusicPlayer
{
    internal unsafe class InternetRadioDevice : AudioProcessor
    {
        private readonly AudioDecoder _decoder;

        public InternetRadioDevice(Stream stream)
            : base(AudioDeviceMode.Playback)
        {
            // Supports .wav, .ogg, .mp3 and .flac
            _decoder = new AudioDecoder(stream, SampleRate);
        }

        // Called on an miniaudio thread
        protected override unsafe void UpdateAudioStream(short* pOutput, short* pInput, uint frameCount)
        {
            if (_decoder != null)
            {
                var read = _decoder.DecodeFrames(pOutput, frameCount);
                if (read < frameCount) // loop case
                {
                    _decoder.SeekToFrame(0);
                    _decoder.DecodeFrames(pOutput + read, frameCount - read);
                }
            }
        }
    }
}
