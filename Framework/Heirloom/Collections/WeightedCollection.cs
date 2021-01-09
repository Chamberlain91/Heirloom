using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Range = Heirloom.Mathematics.Range;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a weighted collection (discrete distribution) of items.
    /// </summary>
    public sealed class WeightedCollection<T> : IWeightedCollection<T>
    {
        private readonly Dictionary<T, Item> _items;
        private float _totalWeight;
        private bool _dirty;

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="WeightedCollection{T}"/>.
        /// </summary>
        public WeightedCollection()
        {
            _items = new Dictionary<T, Item>();
        }

        /// <summary>
        /// Constructs a new instance of <see cref="WeightedCollection{T}"/> with the values given.
        /// </summary>
        /// <param name="values">A sequence of item-weight pairs.</param>
        public WeightedCollection(IEnumerable<(T, float)> values) : this()
        {
            // Collapse duplicate keys by summing the weights of the same item.
            values = values.GroupBy(c => c.Item1)
                           .Select(group => group.Aggregate((a, b) => (a.Item1, a.Item2 + b.Item2)));

            // Add each item
            foreach (var (value, weight) in values)
            {
                Add(value, weight);
            }
        }

        #endregion

        /// <summary>
        /// Gets the number of items in this collection.
        /// </summary>
        public int Count => _items.Count;

        #region Mutators (Clear, Add, Update, Remove)

        /// <inheritdoc/>
        public void Clear()
        {
            _items.Clear();
        }

        /// <inheritdoc/>
        public void Add(T value, float weight)
        {
            if (_items.ContainsKey(value))
            {
                throw new ArgumentException($"Unable to add value to {nameof(WeightedCollection<T>)}, item already exists.");
            }
            else
            {
                // Create a new item
                _items[value] = new Item(value, weight);
                _dirty = true;
            }
        }

        /// <inheritdoc/>
        public void Update(T value, float weight)
        {
            if (_items.TryGetValue(value, out var item))
            {
                item.Weight = weight;
                _dirty = true;
            }
            else
            {
                throw new ArgumentException($"Unable to update value in {nameof(WeightedCollection<T>)}, item does not exist.");
            }
        }

        /// <inheritdoc/>
        public bool Remove(T value)
        {
            return _items.Remove(value);
        }

        #endregion

        #region Accessor (Contains, Get, TryGet)

        /// <inheritdoc/>
        public bool Contains(T value)
        {
            return _items.ContainsKey(value);
        }

        /// <inheritdoc/>
        public T GetValue(Random random)
        {
            Allocate(); // Ensure items are 'distributed'

            // Select a random value in the "weight line"
            var selector = random.NextFloat(0F, _totalWeight);

            // Find the item that overlaps that randomized value
            foreach (var item in _items.Values)
            {
                if (item.Range.Contains(selector))
                {
                    return item.Value;
                }
            }

            throw new InvalidOperationException($"FATAL: Somehow unable to select an item.");
        }

        /// <inheritdoc/>
        public float GetWeight(T value)
        {
            if (TryGetWeight(value, out var weight))
            {
                return weight;
            }
            else
            {
                throw new ArgumentException($"Unable to get weight in {nameof(WeightedCollection<T>)}, item does not exist.");
            }
        }

        /// <inheritdoc/>
        public bool TryGetWeight(T value, out float weight)
        {
            if (_items.TryGetValue(value, out var item))
            {
                weight = item.Weight;
                return true;
            }
            else
            {
                weight = default;
                return false;
            }
        }

        #endregion

        private void Allocate()
        {
            if (_dirty)
            {
                _totalWeight = 0F;
                foreach (var item in _items.Values)
                {
                    item.Offset = _totalWeight;
                    _totalWeight += item.Weight;
                }

                _dirty = false;
            }
        }

        private class Item
        {
            public readonly T Value;

            public float Offset;

            public float Weight;

            public Item(T value, float weight)
            {
                Weight = weight;
                Value = value;
            }

            public Range Range => new Range(Offset, Offset + Weight);
        }

        /// <inheritdoc/>
        public IEnumerator<(T, float)> GetEnumerator()
        {
            foreach (var item in _items.Values)
            {
                yield return (item.Value, item.Weight);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
