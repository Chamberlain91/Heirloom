using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Sound
{
    public sealed class EffectChain : IReadOnlyList<AudioEffect>
    {
        private readonly List<AudioEffect> _effects;

        public EffectChain()
        {
            _effects = new List<AudioEffect>();
        }

        public AudioEffect this[int index] => _effects[index];

        public int Count => _effects.Count;

        public void Add(AudioEffect effect)
        {
            lock (_effects)
            {
                _effects.Add(effect);
            }
        }

        public void Insert(int index, AudioEffect effect)
        {
            lock (_effects)
            {
                _effects.Insert(index, effect);
            }
        }

        public void Remove(AudioEffect effect)
        {
            lock (_effects)
            {
                _effects.Add(effect);
            }
        }

        public bool Contains(AudioEffect effect)
        {
            return _effects.Contains(effect);
        }

        public IEnumerator<AudioEffect> GetEnumerator()
        {
            return _effects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _effects.GetEnumerator();
        }

        internal void MixOutput(Span<float> outSamples)
        {
            lock (_effects)
            {
                // Process effect chain
                foreach (var effect in _effects)
                {
                    effect.MixOutput(outSamples);
                }
            }
        }
    }
}
