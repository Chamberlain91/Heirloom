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
        private short[] _samplesBuffer = Array.Empty<short>();
        private readonly IAudioProvider _provider;

        private LinkedListNode<AudioSource> _node;
        private AudioMixer _mixer;

        #region Constructors

        public AudioSource(AudioClip clip)
            : this(clip, AudioMixer.Master)
        { }

        public AudioSource(AudioClip clip, AudioMixer mixer)
            : this(new AudioClipProvider(clip), mixer)
        { }

        public AudioSource(Stream stream)
            : this(stream, AudioMixer.Master)
        { }

        public AudioSource(Stream stream, AudioMixer mixer)
            : this(new AudioStreamProvider(stream), mixer)
        { }

        protected AudioSource(IAudioProvider provider, AudioMixer mixer)
        {
            Effects = new EffectChain();

            _provider = provider;
            _mixer = mixer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets this sources <see cref="EffectChain"/>.
        /// </summary>
        public EffectChain Effects { get; }

        public AudioMixer Mixer
        {
            get => _mixer;

            set
            {

                if (_node != null) { Stop(); }
                _mixer = value;
            }
        }

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
        /// Gets the current playback time (position) in seconds.
        /// </summary>
        /// <seealso cref="Position"/>
        /// <seealso cref="Duration"/>
        /// <seealso cref="Length"/>
        public float Time => Position < 0 ? 0 : Position / (float) (AudioContext.Instance.SampleRate * AudioContext.Instance.Channels);

        /// <summary>
        /// The duration of the audio in seconds. <para/>
        /// May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).
        /// </summary>
        /// <seealso cref="Time"/>
        /// <seealso cref="Position"/>
        /// <seealso cref="Length"/>
        public float Duration => Length == 0 ? 0 : Length / (float) (AudioContext.Instance.SampleRate * AudioContext.Instance.Channels);

        /// <summary>
        /// Gets the current playback position (time) in samples.
        /// </summary>
        /// <seealso cref="Time"/>
        /// <seealso cref="Duration"/>
        /// <seealso cref="Length"/>
        public int Position => _provider.Position;

        /// <summary>
        /// The length of the audio source in PCM frames. <para/>
        /// May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).
        /// </summary>
        /// <seealso cref="Time"/>
        /// <seealso cref="Duration"/>
        /// <seealso cref="Position"/>
        public int Length => _provider.Length;

        /// <summary>
        /// Gets a value that determines if playback has finished.
        /// </summary>
        public bool IsPlaybackFinished => Position >= Length;

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
                _node = Mixer.InsertSource(this);
            }
        }

        /// <summary>
        /// Pause playing audio.
        /// </summary>
        public void Pause()
        {
            if (_node != null)
            {
                Mixer.RemoveSource(_node);
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
            _provider.Seek(offset);
        }

        /// <summary>
        /// Seek playback position to some time in seconds.
        /// </summary>
        public void Seek(float time)
        {
            Seek((int) (time * AudioMixer.SampleRate * AudioMixer.Channels));
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
                var sample = _samplesBuffer[i] * Volume;

                // Balance (Stereo Mixing)
                var pan = (Balance / 2) + 0.5F;
                if (i % AudioMixer.Channels == 0) { sample *= MathF.Sqrt(pan); }
                else { sample *= MathF.Sqrt(1 - pan); }

                // Output
                outputBuffer[i] += (short) sample;
            }

            // Process effects applied directly onto source
            Effects.MixOutput(outputBuffer);
        }

        private unsafe void ReadSamples(int count)
        {
            // Ensure read buffer is large enough
            if (_samplesBuffer.Length < count) { Array.Resize(ref _samplesBuffer, count); }

            // Read samples
            var read = _provider.ReadSamples(_samplesBuffer, 0, count);

            // End of audio detected
            if (read == 0 || read < count)
            {
                // Invoke end of audio event
                OnPlaybackEnded();

                // Don't bother with loop case if we are unable to seek
                if (CanSeek && Looping)
                {
                    // Go to first sample
                    Seek(0);

                    // Did not read enough before reaching end of audio, read the remainder of requested samples.
                    // This hopefully reduces the 'click' you might here on looping audio
                    while (read < count)
                    {
                        // Read the remaining samples into the buffer.
                        var readMore = _provider.ReadSamples(_samplesBuffer, read, count - read);
                        if (readMore == 0) { break; } else { read += readMore; }
                    }
                }
            }
        }

        internal void OnPlaybackEnded()
        {
            // 
            PlaybackEnded?.Invoke();

            // If not looping, remove from mixer by pausing.
            if (!Looping) { Pause(); }
        }
    }
}
