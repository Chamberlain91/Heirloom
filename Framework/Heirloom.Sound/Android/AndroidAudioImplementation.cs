#if ANDROID
using System;
using System.Diagnostics;
using System.IO;

using Android.Media;

using Stream = System.IO.Stream;

namespace Heirloom.Sound.Android
{
    internal sealed partial class AndroidAudioImplementation : AudioImplementation
    {
        private bool _isDisposed;

        public AndroidAudioImplementation()
        {
            var thread = new System.Threading.Thread(AudioThread) { IsBackground = true };
            thread.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }
        }

        private void AudioThread()
        {
            // global::Android.OS.Process.SetThreadPriority(ThreadPriority.Audio);

            Log.Warning("[Audio Thread] Begin");

            // Create streaming audio track to act as our output sink
            var track = CreateAudioTrack();
            track.Play();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var samples = new short[track.BufferSizeInFrames];
            var time = 0;

            var elapsedError = 0;
            while (!_isDisposed)
            {
                var elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000F;
                var elapsedSamples = elapsedError + (int) (elapsedSeconds * AudioBackend.SampleRate * AudioBackend.Channels);

                if (elapsedSamples >= samples.Length)
                {
                    elapsedError = elapsedSamples - samples.Length;
                    stopwatch.Restart();

                    // Gather samples for output
                    AudioBackend.GatherOutputSamples(samples);

                    // Submit samples to stream
                    track.Write(samples, 0, samples.Length);
                    time += samples.Length;
                }

                // is this important?
                System.Threading.Thread.Yield();
            }

            track.Dispose();
        }

        private static AudioTrack CreateAudioTrack()
        {
            var formatBuilder = new AudioFormat.Builder();
            formatBuilder.SetChannelMask(ChannelOut.Stereo);
            formatBuilder.SetEncoding(Encoding.Pcm16bit);
            formatBuilder.SetSampleRate(44100);

            var trackBuilder = new AudioTrack.Builder();
            trackBuilder.SetAudioFormat(formatBuilder.Build());
            trackBuilder.SetPerformanceMode(AudioTrackPerformanceMode.LowLatency);
            trackBuilder.SetTransferMode(AudioTrackMode.Stream);

            var track = trackBuilder.Build();
            return track;
        }

        #region Create Decoders

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

        #endregion 
    }
}
#endif
