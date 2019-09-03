using System;
using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Returned from a Asesprite loader</remarks>
    public class SpriteCollection : IReadOnlyDictionary<string, Sprite>
    {
        private readonly Dictionary<string, Sprite> _sprites;

        public SpriteCollection()
        {
            _sprites = new Dictionary<string, Sprite>();
        }

        public Sprite this[string key] => _sprites[key];

        public int Count => _sprites.Count;

        public IEnumerable<string> Names => _sprites.Keys;

        public IEnumerable<Sprite> Sprites => _sprites.Values;

        public void Add(string name, Sprite sprite)
        {
            if (Contains(name)) { throw new ArgumentException($"Duplicate key, sprite '{name}' already exists the {nameof(SpriteCollection)}."); }
            else
            {
                _sprites[name] = sprite;
            }
        }

        public Sprite Get(string name)
        {
            if (!TryGet(name, out var sprite))
            {
                throw new KeyNotFoundException($"Unable to find sprite named '{name}' in the {nameof(SpriteCollection)}");
            }

            return sprite;
        }

        public bool TryGet(string name, out Sprite sprite)
        {
            return _sprites.TryGetValue(name, out sprite);
        }

        public bool Remove(string name)
        {
            return _sprites.Remove(name);
        }

        public bool Contains(string name)
        {
            return _sprites.ContainsKey(name);
        }

        #region IReadOnlyDictionary

        bool IReadOnlyDictionary<string, Sprite>.ContainsKey(string key)
        {
            return _sprites.ContainsKey(key);
        }

        bool IReadOnlyDictionary<string, Sprite>.TryGetValue(string key, out Sprite value)
        {
            return _sprites.TryGetValue(key, out value);
        }

        IEnumerable<string> IReadOnlyDictionary<string, Sprite>.Keys => _sprites.Keys;

        IEnumerable<Sprite> IReadOnlyDictionary<string, Sprite>.Values => _sprites.Values;

        IEnumerator<KeyValuePair<string, Sprite>> IEnumerable<KeyValuePair<string, Sprite>>.GetEnumerator()
        {
            return _sprites.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sprites.GetEnumerator();
        }

        #endregion
    }
}
