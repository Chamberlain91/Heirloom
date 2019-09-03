using System;
using System.Collections.Generic;
using System.IO;

namespace Heirloom.Sound
{
    /// <summary>
    /// An instance of playable audio.
    /// </summary>
    public class AudioSource
    {
        private short[] _samples = new short[0];

        private readonly AudioSourceProvider _provider;
        private LinkedListNode<AudioSource> _node;

        #region Constructors

        public AudioSource(AudioClip clip)
            : this(new AudioClipProvider(clip))
        {
            Clip = clip;
        }

        public AudioSource(Stream stream)
            : this(new AudioStreamProvider(stream))
        { }

        public AudioSource(AudioSourceProvider provider)
        {
            _provider = provider;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the clip used by this audio source if constructed with one, thus this may return null.
        /// </summary>
        public AudioClip Clip { get; }

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
        public bool CanSeek => _provider.CanSeek;

        /// <summary>
        /// The length of the audio source in PCM frames.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        public uint Length => _provider.Length;

        /// <summary>
        /// The duration of the audio in seconds.
        /// May report zero if the length of the source cannot be determined (such as a stream or format limitation).
        /// </summary>
        public float Duration => Length == 0 ? 0 : Length / (float) SampleRate;

        /// <summary>
        /// How many samples are processed per second (ie, 44100 hz).
        /// </summary>
        public uint SampleRate => AudioDevice.Instance.SampleRate;

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
                _node = AudioDevice.Instance.InsertSource(this);
            }
        }

        /// <summary>
        /// Pause playing audio.
        /// </summary>
        public void Pause()
        {
            if (_node != null)
            {
                AudioDevice.Instance.RemoveSource(_node);
                _node = null;
            }
        }

        /// <summary>
        /// Pause playing audio and seeks to the first frame in the audio data.
        /// If seek is not supported, this is equivalent to <see cref="Pause"/>.
        /// </summary>
        public void Stop()
        {
            Pause();
            if (CanSeek) { Seek(0); }
        }

        /// <summary>
        /// Seek to the desired PCM frame.
        /// </summary>
        public void Seek(int offset)
        {
            if (!CanSeek) { throw new InvalidOperationException("Unable to seek this audio source."); }
            _provider.SeekToFrame(offset);
        }

        #endregion

        internal unsafe void ProcessAudioOutput(float[] samples, int frameCount)
        {
            // Ensure samples read buffer is large enough
            var sampleCount = frameCount * AudioDevice.Instance.Channels;
            if (_samples.Length < sampleCount) { Array.Resize(ref _samples, (int) sampleCount); }

            // Read samples
            var framesRead = _provider.ReadFrames(_samples, 0, frameCount);

            // End of audio detected
            if (framesRead == 0 || framesRead < frameCount)
            {
                // Invoke end of audio event
                OnPlaybackEnded();

                // Don't bother with loop case if we are unable to seek
                if (CanSeek && Looping)
                {
                    Seek(0); // Go to first frame

                    // Did not read enough before reaching end of audio, 
                    // read the remainder of requested samples.
                    if (framesRead < frameCount)
                    {
                        // read the remaining samples into the buffer
                        _provider.ReadFrames(_samples, framesRead, frameCount - framesRead);
                    }
                }
            }

            // Mix!
            for (var i = 0u; i < _samples.Length; i++)
            {
                MixSample(ref samples[i], _samples[i], i % AudioDevice.Instance.Channels);
            }
        }

        protected internal virtual void MixSample(ref float existing, float incoming, uint channel)
        {
            // Volume
            double sample = incoming * Volume;

            // Balance / Panning
            var x = (Balance / 2) + 0.5F;
            if (channel == 0) { sample *= Math.Sqrt(x); }
            else { sample *= Math.Sqrt(1 - x); }

            // Output
            existing += (short) sample;
        }

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
    }
}
