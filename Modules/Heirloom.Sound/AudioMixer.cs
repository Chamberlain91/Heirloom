using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    public class AudioMixer : AudioNode
    {
        private readonly LinkedList<AudioNode> _sources;
        private readonly LinkedList<AudioNode> _sourcesRemove;
        private readonly LinkedList<AudioNode> _sourcesAdd;

        private LinkedListNode<AudioNode> _node;
        private AudioMixer _parent;

        #region Constructors

        public AudioMixer()
            : this(Default)
        { }

        public AudioMixer(AudioMixer parentMixer)
            : this(parentMixer, false)
        { }

        private AudioMixer(AudioMixer parentMixer, bool allowOrphanMixer)
        {
            _sources = new LinkedList<AudioNode>();
            _sourcesRemove = new LinkedList<AudioNode>();
            _sourcesAdd = new LinkedList<AudioNode>();

            // Set parent mixer
            if (!allowOrphanMixer && parentMixer == null) { throw new ArgumentNullException(nameof(parentMixer)); }
            Parent = parentMixer;
        }

        #endregion

        /// <summary>
        /// Gets or sets this mixer objects parent mixer.
        /// </summary>
        public AudioMixer Parent
        {
            get => _parent;

            set
            {
                if (this == Default) { throw new InvalidOperationException("Unable to set the parent of the default (root) mixer."); }

                // If a previous parent exists, remove this mixer from it
                _parent?.RemoveNode(_node);

                // Set new parent and register this mixer as a child
                // TODO: Detect cycles to prevent stack overflow/infinite recursions.
                _parent = value;
                _node = _parent?.AddNode(this);
            }
        }

        protected override void PopulateBuffer(Span<float> output)
        {
            lock (_sources)
            {
                // Process source list mutation
                foreach (var node in _sourcesRemove) { _sources.Remove(node); }
                foreach (var node in _sourcesAdd) { _sources.AddLast(node); }
                _sourcesRemove.Clear();
                _sourcesAdd.Clear();

                // Mix output from audio source
                foreach (var source in _sources)
                {
                    source.MixOutput(output); // additive
                }
            }
        }

        internal LinkedListNode<AudioNode> AddNode(AudioNode source)
        {
            lock (_sources)
            {
                return _sourcesAdd.AddLast(source);
            }
        }

        internal void RemoveNode(LinkedListNode<AudioNode> node)
        {
            lock (_sources)
            {
                _sourcesRemove.AddLast(node);
            }
        }

        #region Static

        private static readonly AudioMixer _default = new AudioMixer(default, true);

        /// <summary>
        /// Gets the default mixer object.
        /// </summary>
        public static AudioMixer Default => _default;

        /// <summary>
        /// Gets the audio sample rate.
        /// </summary>
        public static int SampleRate => AudioContext.Instance.SampleRate;

        /// <summary>
        /// Gets the number of audio channels.
        /// </summary>
        public static int Channels => AudioContext.Instance.Channels;

        #endregion
    }
}
