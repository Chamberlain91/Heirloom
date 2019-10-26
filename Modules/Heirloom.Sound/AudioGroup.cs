using System;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    /// <summary>
    /// An <see cref="AudioNode"/> to mix and apply effects a group of other nodes.
    /// </summary>
    public class AudioGroup : AudioNode
    {
        private readonly LinkedList<AudioNode> _sources;
        private readonly LinkedList<LinkedListNode<AudioNode>> _sourcesRemove;
        private readonly LinkedList<LinkedListNode<AudioNode>> _sourcesAdd;

        private LinkedListNode<AudioNode> _node;
        private AudioGroup _parent;

        #region Constructors

        public AudioGroup()
            : this(Default)
        { }

        public AudioGroup(AudioGroup parentMixer)
            : this(parentMixer, false)
        { }

        private AudioGroup(AudioGroup parentMixer, bool allowOrphanMixer)
        {
            _sources = new LinkedList<AudioNode>();
            _sourcesRemove = new LinkedList<LinkedListNode<AudioNode>>();
            _sourcesAdd = new LinkedList<LinkedListNode<AudioNode>>();

            // Set parent mixer
            if (!allowOrphanMixer && parentMixer == null) { throw new ArgumentNullException(nameof(parentMixer)); }
            Parent = parentMixer;
        }

        #endregion

        /// <summary>
        /// Gets or sets this mixer objects parent mixer.
        /// </summary>
        public AudioGroup Parent
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
                var listNode = new LinkedListNode<AudioNode>(source);
                _sourcesAdd.AddLast(listNode);
                return listNode;
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

        /// <summary>
        /// Gets the default audio group (ie, the speakers, headphones, etc).
        /// </summary>
        public static AudioGroup Default { get; } = new AudioGroup(default, true);

        #endregion
    }
}
