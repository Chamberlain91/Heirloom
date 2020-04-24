using System;
using System.IO;

namespace Heirloom
{
    /// <summary>
    /// An instance of playable audio.
    /// </summary>
    public sealed class AudioSource : AudioNode
    {
        [ThreadStatic] private static short[] _samplesBuffer = Array.Empty<short>();
        private readonly IAudioProvider _provider;

        private bool _isActive;
        private AudioGroup _group;

        #region Constructors

        /// <summary>
        /// Create an audio source for the given clip in the default audio group (ie, <see cref="AudioGroup.Default"/>).
        /// </summary>
        public AudioSource(AudioClip clip)
            : this(clip, AudioGroup.Default)
        { }

        /// <summary>
        /// Create an audio source for the given clip in the specified audio group.
        /// </summary> 
        public AudioSource(AudioClip clip, AudioGroup group)
            : this(new AudioClipProvider(clip), group)
        { }

        /// <summary>
        /// Create an audio source for the given stream in the default audio group (ie, <see cref="AudioGroup.Default"/>).
        /// </summary> 
        public AudioSource(Stream stream)
            : this(stream, AudioGroup.Default)
        { }

        /// <summary>
        /// Create an audio source for the given stream in the specified audio group.
        /// </summary> 
        public AudioSource(Stream stream, AudioGroup group)
            : this(new AudioStreamProvider(stream), group)
        { }

        private AudioSource(IAudioProvider provider, AudioGroup group)
        {
            _provider = provider;
            _group = group;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets which audio group this source is part of (default is <see cref="AudioGroup.Default"/>).
        /// </summary>
        public AudioGroup Group
        {
            get => _group;

            set
            {
                if (_isActive) { Stop(); }
                _group = value;
            }
        }

        /// <summary>
        /// Should this clip loop when finished playing?
        /// </summary>
        public bool IsLooping { get; set; } = false;

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
        public float Time => Position < 0 ? 0 : Position / (float) (AudioAdapter.SampleRate * AudioAdapter.Channels);

        /// <summary>
        /// The duration of the audio in seconds. <para/>
        /// May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).
        /// </summary>
        /// <seealso cref="Time"/>
        /// <seealso cref="Position"/>
        /// <seealso cref="Length"/>
        public float Duration => Length == 0 ? 0 : Length / (float) (AudioAdapter.SampleRate * AudioAdapter.Channels);

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
        public event Action PlaybackEnded;

        #endregion

        #region Audio Playback Control

        /// <summary>
        /// Begin playing audio.
        /// </summary>
        public void Play()
        {
            if (_isActive == false)
            {
                Group.AddNode(this);
                _isActive = true;
            }
        }

        /// <summary>
        /// Pause playing audio.
        /// </summary>
        public void Pause()
        {
            if (_isActive)
            {
                Group.RemoveNode(this);
                _isActive = false;
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
            Seek((int) (time * AudioAdapter.SampleRate * AudioAdapter.Channels));
        }

        #endregion

        protected override void PopulateBuffer(Span<float> output)
        {
            // Ensure read buffer is large enough
            if (_samplesBuffer.Length < output.Length)
            {
                Array.Resize(ref _samplesBuffer, output.Length);
            }

            // Read samples
            var read = _provider.ReadSamples(_samplesBuffer, 0, output.Length);

            // End of audio detected
            if (read == 0 || read < output.Length)
            {
                // Invoke end of audio event
                OnPlaybackEnded();

                // Don't bother with loop case if we are unable to seek
                if (CanSeek && IsLooping)
                {
                    // Go to first sample
                    Seek(0);

                    // Did not read enough before reaching end of audio, read the remainder of requested samples.
                    // This hopefully reduces the 'click' you might here on looping audio
                    while (read < output.Length)
                    {
                        // Read the remaining samples into the buffer.
                        var readMore = _provider.ReadSamples(_samplesBuffer, read, output.Length - read);
                        if (readMore == 0) { break; } else { read += readMore; }
                    }
                }
            }

            const float NORMALIZE_SHORT = 1F / short.MaxValue;

            // Write read samplss to output
            for (var i = 0; i < output.Length; i++)
            {
                output[i] += _samplesBuffer[i] * NORMALIZE_SHORT;
            }
        }

        internal void OnPlaybackEnded()
        {
            // 
            PlaybackEnded?.Invoke();

            // If not looping, remove from active list by pausing.
            if (!IsLooping) { Pause(); }
        }
    }
}
