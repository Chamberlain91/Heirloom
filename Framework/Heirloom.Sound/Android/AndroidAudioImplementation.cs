using System;
using System.IO;

namespace Heirloom.Sound.Android
{
#if ANDROID
    internal sealed partial class AndroidAudioImplementation : AudioImplementation
    {
        internal override IAudioDecoder CreateDecoder(Stream stream)
        {
            var data = stream.ReadAllBytes();
            if (TryCreateMp3Decoder(data, out var decoder)) { return decoder; }
            else
            if (TryCreateOggDecoder(data, out decoder)) { return decoder; }
            else
            {
                throw new NotImplementedException("Unable to create decoder for audio stream");
            }
        }

        internal override AudioDevice GetDefaultPlaybackDevice()
        {
            throw new NotImplementedException();
        }

        internal override AudioDevice GetDefaultCaptureDevice()
        {
            throw new NotImplementedException();
        }

        internal override AudioDevice[] GetPlaybackDevices()
        {
            throw new NotImplementedException();
        }

        internal override AudioDevice[] GetCaptureDevices()
        {
            throw new NotImplementedException();
        }

        internal override void UsePlaybackDevice(AudioDevice device)
        {
            throw new NotImplementedException();
        }

        internal override void UseCaptureDevice(AudioDevice device)
        {
            throw new NotImplementedException();
        }

        private static bool TryCreateMp3Decoder(byte[] data, out IAudioDecoder decoder)
        {
            try
            {
                decoder = new Mp3Decoder(data);
                return true;
            }
            catch (InvalidOperationException)
            {
                decoder = default;
                return false;
            }
        }

        private static bool TryCreateOggDecoder(byte[] data, out IAudioDecoder decoder)
        {
            try
            {
                decoder = new OggDecoder(data);
                return true;
            }
            catch (InvalidOperationException)
            {
                decoder = default;
                return false;
            }
        }
    }
#endif
}
