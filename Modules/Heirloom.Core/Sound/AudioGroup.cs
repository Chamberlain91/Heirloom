using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    /// <summary>
    /// An <see cref="AudioNode"/> to mix and apply effects to a group of other nodes.
    /// </summary>
    public class AudioGroup : AudioNode
    {
        private readonly HashSet<AudioNode> _sources;
        private readonly HashSet<AudioNode> _sourcesRemove;
        private readonly HashSet<AudioNode> _sourcesAdd;

        private AudioGroup _parent;

        #region Constructors

        /// <summary>
        /// Construct a new audio node that is connected to default audio group (ie, <see cref="AudioGroup.Default"/>).
        /// </summary>
        public AudioGroup()
            : this(Default)
        { }

        /// <summary>
        /// Construct a new audio group that is connected to the specified parent group.
        /// </summary>
        /// <param name="parentGroup">The parent audio group.</param>
        public AudioGroup(AudioGroup parentGroup)
            : this(parentGroup, false)
        { }

        private AudioGroup(AudioGroup parentGroup, bool allowOrphan)
        {
            _sources = new HashSet<AudioNode>();
            _sourcesRemove = new HashSet<AudioNode>();
            _sourcesAdd = new HashSet<AudioNode>();

            // Set parent audio group
            if (!allowOrphan && parentGroup == null) { throw new ArgumentNullException(nameof(parentGroup)); }
            Parent = parentGroup;
        }

        #endregion

        /// <summary>
        /// Gets or sets the parent audio group.
        /// </summary>
        public AudioGroup Parent
        {
            get => _parent;

            set
            {
                if (this == Default) { throw new InvalidOperationException("Unable to set the parent of the default (root) audio group."); }

                // If a previous parent exists, remove ourselves from it
                _parent?.RemoveNode(this);

                // Set new parent and register ourselves as a child
                // TODO: Detect cycles to prevent stack overflow/infinite recursions.
                _parent = value;
                _parent?.AddNode(this);
            }
        }

        internal IEnumerable<AudioNode> Children => _sources.Concat(_sourcesAdd);

        protected override void PopulateBuffer(Span<float> output)
        {
            lock (_sources)
            {
                // Process source list mutation
                foreach (var node in _sourcesRemove) { _sources.Remove(node); }
                foreach (var node in _sourcesAdd) { _sources.Add(node); }
                _sourcesRemove.Clear();
                _sourcesAdd.Clear();

                // Mix output from audio source
                foreach (var source in _sources)
                {
                    source.MixOutput(output); // additive
                }
            }
        }

        internal void AddNode(AudioNode source)
        {
            lock (_sources)
            {
                // Schedule node for addition
                _sourcesRemove.Add(source);
                _sourcesAdd.Add(source);

                // Ensure the audio graph is a DAG
                if (!IsMixerGraphAcyclic())
                {
                    throw new InvalidOperationException("Audio graph must be a DAG. Adding this node will introduce a cycle.");
                }
            }
        }

        internal void RemoveNode(AudioNode node)
        {
            lock (_sources)
            {
                // Schedule node for removal
                _sourcesRemove.Add(node);
                _sourcesAdd.Remove(node);
            }
        }

        private bool IsMixerGraphAcyclic()
        {
            const int STATUS_NEW = 0;
            const int STATUS_ACTIVE = 1;
            const int STATUS_FINISHED = 2;

            var statusTable = new Dictionary<AudioNode, int>();

            // For each unvisited node
            foreach (var node in Children)
            {
                if (GetStatus(node) == STATUS_NEW)
                {
                    // If subgraph is not a DAG, then entire graph is not a DAG
                    if (!IsMixerGraphAcyclic(node)) { return false; }
                }
            }

            return true;

            int GetStatus(AudioNode node)
            {
                if (statusTable.TryGetValue(node, out var status)) { return status; }
                return statusTable[node] = STATUS_NEW;
            }

            bool IsMixerGraphAcyclic(AudioNode node)
            {
                statusTable[node] = STATUS_ACTIVE;

                // Node was a group, we need to check its children.
                if (node is AudioGroup group)
                {
                    foreach (var child in group.Children)
                    {
                        // If child is active, we have detected a cycle (thus, not a DAG).
                        if (GetStatus(child) == STATUS_ACTIVE) { return false; }
                        // If the child is new (unvisited) we need to recurse and keep checking
                        else if (GetStatus(child) == STATUS_NEW)
                        {
                            // If subgraph is not a DAG, then the graph is not a DAG
                            if (!IsMixerGraphAcyclic(child)) { return false; }
                        }
                    }
                }

                // Was a source node or was determined to be cycle-free
                statusTable[node] = STATUS_FINISHED;
                return true;
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
