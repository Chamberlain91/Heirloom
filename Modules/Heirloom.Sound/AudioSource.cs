using System;
using System.Collections.Generic;
using System.IO;

namespace Heirloom.Sound
{
    /// <summary>
    /// An instance of playable audio.
    /// </summary>
    public abstract class AudioSource
    {
        private short[] _samplesReadBuffer = Array.Empty<short>();

        private LinkedListNode<AudioSource> _node;

        #region Properties

        /// <summary>
        /// Should this clip loop when finished playing?
        /// </summary>
        public bool Looping { get; set; } = false;

        /// <summary>
        /// In the range of 0.0 to 1.0 the volume of this source.
        /// </summary>
        public float Volume { get; set; } = 1F;

        /// <summary>
        /// In the range of -1.0 to +1.0 the stereo balance of the audio playback.
        /// </summary>
        public float Balance { get; set; } = 0F;

        /// <summary>
        /// Is it possible seek through this sources audio data to change playback position.
        /// </summary>
        public abstract bool CanSeek { get; }

        /// <summary>
        /// The duration of the audio in seconds.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        public float Duration => Length == 0 ? 0 : Length / (float) (AudioContext.Instance.SampleRate * AudioContext.Instance.Channels);

        /// <summary>
        /// The length of the audio source in PCM frames.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        public abstract int Length { get; }

        #endregion

        #region Events

        /// <summary>
        /// An event invoked when this source reaches the end of playable audio.
        /// </summary>
        public event Action PlaybackEnded; // TODO: Better name...

        #endregion

        #region Audio Playback Control

        /// <summary>
        /// Begin playing audio.
        /// </summary>
        public void Play()
        {
            if (_node == null)
            {
                _node = AudioMixer.Master.InsertSource(this);
            }
        }

        /// <summary>
        /// Pause playing audio.
        /// </summary>
        public void Pause()
        {
            if (_node != null)
            {
                AudioMixer.Master.RemoveSource(_node);
                _node = null;
            }
        }

        /// <summary>
        /// Pause playing audio and seeks to the beginning in the audio data.
        /// If seek is not supported, this is equivalent to <see cref="Pause"/>.
        /// </summary>
        public void Stop()
        {
            Pause();
            if (CanSeek) { Seek(0); }
        }

        /// <summary>
        /// Seek playback position to some time in samples.
        /// </summary>
        public void Seek(int offset)
        {
            if (!CanSeek) { throw new InvalidOperationException("Unable to seek this audio source."); }
            SeekInternal(offset);
        }

        /// <summary>
        /// Seek playback position to some time in seconds.
        /// </summary>
        public void Seek(float time)
        {
            Seek((int) (time * AudioContext.Instance.SampleRate * AudioContext.Instance.Channels));
        }

        #endregion

        internal unsafe void MixOutput(Span<float> outputBuffer)
        {
            var count = outputBuffer.Length;

            ReadSamples(count);

            // Process samples and write to output
            for (var i = 0; i < count; i++)
            {
                // Volume (Gain)
                var sample = _samplesReadBuffer[i] * Volume;

                // Balance (Stereo Mixing)
                var pan = (Balance / 2) + 0.5F;
                if (i % AudioMixer.Channels == 0) { sample *= MathF.Sqrt(pan); }
                else { sample *= MathF.Sqrt(1 - pan); }

                // Output
                outputBuffer[i] += (short) sample;
            }
        }

        private unsafe void ReadSamples(int count)
        {
            // Ensure read buffer is large enough
            if (_samplesReadBuffer.Length < count) { Array.Resize(ref _samplesReadBuffer, count); }

            // Read samples
            var read = ReadSamples(_samplesReadBuffer, 0, count);

            // End of audio detected
            if (read == 0 || read < count)
            {
                // Invoke end of audio event
                OnPlaybackEnded();

                // Don't bother with loop case if we are unable to seek
                if (CanSeek && Looping)
                {
                    Seek(0); // Go to first sample

                    // Did not read enough before reaching end of audio, read the remainder of requested samples.
                    // This hopefully reduces the 'click' you might here on looping audio
                    while (read < count)
                    {
                        // Read the remaining samples into the buffer.
                        var readMore = ReadSamples(_samplesReadBuffer, read, count - read);
                        if (readMore == 0) { break; } else { read += readMore; }
                    }
                }
            }
        }

        protected internal abstract int ReadSamples(short[] samples, int offset, int count);

        protected abstract void SeekInternal(int offset);

        internal void OnPlaybackEnded()
        {
            // 
            PlaybackEnded?.Invoke();

            // If not looping, remove from audio device
            // by pausing.
            if (!Looping)
            {
                Pause();
            }
        }

        public static AudioSource FromClip(AudioClip clip)
        {
            return new AudioClipSource(clip);
        }

        public static AudioSource FromStream(Stream stream)
        {
            return new AudioStreamSource(stream);
        }
    }
}
