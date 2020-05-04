using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Heirloom.Collections
{
    /// <summary>
    /// Represents a heap data structure.
    /// Allows the insertion and removal of items by priority.
    /// </summary>
    /// <typeparam name="T">Type of the elements.</typeparam>
    /// 
    /// <remarks>
    /// The heap always acts like a min-heap but inverts the result of comparison for max heaps.
    /// </remarks>
    public class Heap<T> : IHeap<T>
    {
        private readonly Dictionary<T, Node> _lookup;
        private Node[] _items;

        private int _version = 0;

        #region Constructors

        /// <summary>
        /// Constructs a new heap that optionally sorts by maximum or minimum comparisons.
        /// </summary>
        /// <param name="type">Should the priority item be the comparably largest?</param>
        public Heap(HeapType type = HeapType.Min)
            : this(Comparer<T>.Default, type)
        { }

        /// <summary>
        /// Constructs a new heap that optionally sorts by maximum or minimum comparisons with a custom comparison function.
        /// </summary>
        /// <param name="comparison">Some custom comparison function.</param>
        /// <param name="maximize">Should the priority item be the comparably largest?</param>
        public Heap(Comparison<T> comparison, HeapType type = HeapType.Min)
            : this(Comparer<T>.Create(comparison), type)
        { }

        /// <summary>
        /// Constructs a new heap that optionally sorts by maximum or minimum comparisons with an instance of a custom comparer.
        /// </summary>
        /// <param name="comparer">Some instance of a comparer.</param>
        /// <param name="maximize">Should the priority item be the comparably largest?</param>
        public Heap(Comparer<T> comparer, HeapType type = HeapType.Min)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

            // 
            Type = type;
            Count = 0;

            _lookup = new Dictionary<T, Node>(16);
            _items = new Node[16];
        }

        // Clone
        private Heap(Heap<T> heap)
        {
            Comparer = heap.Comparer;
            Count = heap.Count;
            Type = heap.Type;

            // Clone the lookup
            _lookup = new Dictionary<T, Node>();
            foreach (var kv in heap._lookup)
            {
                _lookup[kv.Key] = kv.Value;
            }

            // Clone items
            _items = new Node[heap._items.Length];
            for (var i = 0; i < _items.Length; i++)
            {
                if (heap._items[i] != null)
                {
                    _items[i] = new Node(heap._items[i]);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The comparer used by this heap.
        /// </summary>
        public Comparer<T> Comparer { get; }

        /// <summary>
        /// Gets the number of elements contained in the heap.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Which kind of heap is this?
        /// </summary>
        public HeapType Type { get; }

        #endregion

        #region Compare Items

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Compare(T a, T b)
        {
            var cmp = Comparer.Compare(a, b);
            return Type == HeapType.Max ? (cmp *= -1) : cmp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Compare(int a, int b)
        {
            return Compare(Get(a), Get(b));
        }

        #endregion

        #region Node Access

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasLeft(int node) { return GetLeftIndex(node) <= Count; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasRight(int node) { return GetRightIndex(node) <= Count; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasParent(int node) { return node > 1; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetLeftIndex(int node) { return node * 2; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetRightIndex(int node) { return node * 2 + 1; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetParentIndex(int node) { return node / 2; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref T Get(int node) { return ref _items[node].Value; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref Node GetNode(int node) { return ref _items[node]; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Node GetNode(T node) { return _lookup[node]; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref T GetLeft(int node) { return ref Get(GetLeftIndex(node)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref T GetRight(int node) { return ref Get(GetRightIndex(node)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref T GetParent(int node) { return ref Get(GetParentIndex(node)); }

        #endregion

        #region Bubble / Swap / Resize

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BubbleUp(int index)
        {
            if (index == 0) { throw new ArgumentException("Unable to bubble up from index 0, this is an invalid node number."); }

            // Swap with parent until parent is no longer larger (to satisfy the min-property)
            while (HasParent(index) && Compare(GetParentIndex(index), index) > 0)
            {
                var parent = GetParentIndex(index);
                Swap(index, parent);
                index = parent;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BubbleDown(int index)
        {
            if (index == 0) { throw new ArgumentException("Unable to bubble down from index 0, this is an invalid node number."); }

            // While we are capable of traversing down
            while (HasLeft(index))
            {
                // Get the left child as our child index
                var childIndex = GetLeftIndex(index);

                // Check if the right child is smaller than our left child, use it instead.
                if (HasRight(index) && Compare(GetRight(index), GetLeft(index)) < 0)
                {
                    childIndex = GetRightIndex(index);
                }

                // If the minimum child is larger, then the min-property holds.
                if (Compare(childIndex, index) >= 0)
                {
                    break;
                }

                //
                Swap(index, childIndex);

                // Now move cursor to child
                index = childIndex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(int a, int b)
        {
            var _temp = _items[a];

            // Swap values
            _items[a] = _items[b];
            _items[b] = _temp;

            // Swap indices
            _items[a].Index = a;
            _items[b].Index = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckResize()
        {
            var capacity = _items.Length - 1;

            // Do we need more space?
            if (Count >= capacity)
            {
                Array.Resize(ref _items, 1 + capacity * 2);
            }

            // Wasting space
            if (capacity > 16 && Count < capacity / 2)
            {
                Array.Resize(ref _items, 1 + capacity / 2);
            }
        }

        #endregion

        /// <summary>
        /// Adds an item to the heap.
        /// </summary>
        public bool Add(T item)
        {
            if (item == null) { throw new ArgumentNullException(nameof(item)); }
            else
            {
                // Only add to structure if it doesn't exist already
                if (_lookup.ContainsKey(item) == false)
                {
                    // Make sure we have space
                    CheckResize();

                    // We have another item, increment 
                    // The heap exploits a 2*i relation to the array, so it counts from 1.
                    // This means the last valid item in the array is at _items[Count].
                    Count++;

                    var node = new Node(item, Count);

                    // Insert item into last leaf (bottom of tree)
                    _items[Count] = node;
                    _lookup[item] = node;

                    // Propagate item item up (swap w/ parent)
                    BubbleUp(Count);

                    _version++;

                    // Item was new
                    return true;
                }
                else
                {
                    // Item was not new
                    return false;
                }
            }
        }

        /// <summary>
        /// Adds multiple items to the heap.
        /// </summary>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) { throw new ArgumentNullException(nameof(items)); }
            else
            {
                // Add each item individually
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Gets the next item in the heap to be removed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Peek()
        {
            if (Count == 0) { throw new InvalidOperationException($"Unable to peek, {nameof(Heap<T>)} is empty."); }
            return Get(1);
        }

        /// <summary>
        /// Removes and returns the next priority item in the heap.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Remove()
        {
            // If heap is empty, throw a fit
            if (Count == 0)
            {
                throw new InvalidOperationException($"Unable to remove priority item, {nameof(Heap<T>)} is empty.");
            }
            // Otherwise, remove and return priority item
            else
            {
                // Get the top of the heap
                var result = Peek();

                // Swap top item with the last leaf
                Swap(1, Count);

                // Remove the item (now at the end of storage)
                _items[Count--] = default;
                _lookup.Remove(result);

                // We may need to resize to save space
                CheckResize();

                // Bubble the leaf down until heap is stable
                BubbleDown(1);

                _version++;

                // Return item
                return result;
            }
        }

        /// <summary>
        /// Removes a specific item from the heap.
        /// </summary>
        public bool Remove(T item)
        {
            if (Contains(item))
            {
                // Get nodes
                var a = GetNode(item);  // 'item'
                var b = GetNode(Count); // 'leaf'

                // Swap leaf and item
                Swap(a.Index, b.Index);

                // Bubble the leaf down until heap is stable
                BubbleDown(b.Index);

                // Remove the item (now at the end of storage)
                _items[Count--] = default;
                _lookup.Remove(item);

                // We may need to resize to save space
                CheckResize();

                _version++;

                // Yes, item was removed
                return true;
            }
            else
            {
                // No, item was not found
                return false;
            }
        }

        /// <summary>
        /// Alerts the heap to update the position the element within the heap.
        /// </summary>
        public void Update(T item)
        {
            if (Contains(item))
            {
                // Get nodes
                var a = GetNode(item);  // 'item'
                var b = GetNode(Count); // 'leaf'

                // Swap leaf and item
                Swap(a.Index, b.Index);

                // Correct heap properties
                BubbleUp(a.Index);   // Bubble item up until heap is stable
                BubbleDown(b.Index); // Bubble leaf down until heap is stable

                _version++;
            }
            else
            {
                throw new InvalidOperationException($"Unable to update, item was not contained by this heap.");
            }
        }

        /// <summary>
        /// Determines whether the <see cref="Heap{T}"/> contains the specified item.
        /// </summary>
        /// <remarks>
        /// This lookup is implemented in <c>O(1)</c> time
        /// </remarks>
        /// <param name="item">Some item.</param>
        /// <returns>Returns true if contained.</returns>
        public bool Contains(T item)
        {
            return _lookup.ContainsKey(item);
        }

        /// <summary>
        /// Clones the heap, and returns an array of the elements in priority ordering.
        /// </summary>
        public T[] ToArray()
        {
            var heap = new Heap<T>(this); // Clone

            var items = new T[heap.Count];
            var index = 0;

            // Remove each item from the heap
            while (heap.Count > 0)
            {
                items[index++] = heap.Remove();
            }

            return items;
        }

        /// <summary>
        /// Enumerates the values in the heap (in no particular order)
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new HeapEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public int Index;
            public T Value;

            public Node(Node other)
            {
                Value = other.Value;
                Index = other.Index;
            }

            public Node(T value, int index)
            {
                Value = value;
                Index = index;
            }
        }

        private class HeapEnumerator : IEnumerator<T>
        {
            private readonly Heap<T> _heap;
            private int _position;
            private int _version;

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public HeapEnumerator(Heap<T> heap)
            {
                _heap = heap ?? throw new ArgumentNullException(nameof(heap));
                _version = _heap._version;
                _position = 0;
            }

            public bool MoveNext()
            {
                if (_version != _heap._version)
                {
                    throw new InvalidOperationException("Enumeration failed. Heap was modified.");
                }

                // 
                if (_position >= _heap.Count) { return false; }
                else
                {
                    Current = _heap.Get(_position + 1);
                    _position++;

                    return true;
                }
            }

            public void Reset()
            {
                _position = 0;
                _version = _heap._version;
                Current = default;
            }

            public void Dispose()
            {
                // 
            }
        }
    }
}
