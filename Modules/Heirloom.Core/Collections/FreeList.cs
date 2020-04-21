using System;

namespace Heirloom
{
    /// <summary>
    /// A free list an allocation-centric data structure that allows insertion and 
    /// removal of elements in O(1) time, but does not behave like a typical 
    /// "list" data type.
    /// </summary>
    public sealed class FreeList<T>
    {
        private FreeElement[] _data = Array.Empty<FreeElement>();
        private int _freeIndex;

        #region Constructors

        /// <summary>
        /// Constructs a new free list instance.
        /// </summary>
        public FreeList(int capacity)
        {
            if (capacity <= 0) { throw new ArgumentException("Must be initialized with a capacity greater than zero."); }
            Resize(capacity);
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets an element by index. This should not be used for iteration as the index and underlying array aren't bounded by the element count. <para/>
        /// This index is not validated, you must be responsible.
        /// </summary>
        public T this[int i] => _data[i].Value;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the total number of elements that can be stored in this <see cref="FreeList{T}"/>.
        /// </summary>
        public int Capacity => _data.Length;

        /// <summary>
        /// Gets the number of elements stored in this <see cref="FreeList{T}"/>.
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the free list, invalidating all indices and clearing element data.
        /// </summary>
        public void Clear()
        {
            // Trash all elements ... O(n)
            for (var i = 0; i < Capacity; i++)
            {
                ref var el = ref _data[i];
                el.Value = default;
                el.Next = i + 1;
            }

            _freeIndex = 0;
        }

        /// <summary>
        /// Inserts an element into the free list and returns its index.
        /// </summary>
        public int Insert(T value)
        {
            if (Count == Capacity) { throw new InvalidOperationException("Unable to insert element, reached capacity."); }

            // Set value on free index
            ref var el = ref _data[_freeIndex];
            el.Value = value;

            // Update free index and count
            var index = _freeIndex;
            _freeIndex = el.Next;
            Count++;

            return index;
        }

        /// <summary>
        /// Removes an element from the free list by an index returned by <see cref="Insert(T)"/>. <para/>
        /// This index is not validated, you must be responsible.
        /// </summary>
        public void Remove(int index)
        {
            if (Count <= 0) { throw new InvalidOperationException("Unable to remove element, a invalid usage has been detected."); }

            // Make this node as the next free index
            ref var el = ref _data[index];
            el.Value = default; // clear value
            el.Next = _freeIndex;
            _freeIndex = index;

            Count--;

            // todo: how to reduce allocation if parts are heavily unused?
        }

        /// <summary>
        /// Resize the free list with an increased capacity.
        /// </summary>
        public void Resize(int newCapacity)
        {
            if (newCapacity < Capacity)
            {
                // List was resized to be smaller, and there is no way to know if elements are in the tailing portion
                throw new InvalidOperationException("Resize can only be used to increase capacity.");
            }

            // Same capacity, a no-op
            if (newCapacity == Capacity) { return; }
            else
            {
                var oldCapacity = Capacity;

                // Resize array
                Array.Resize(ref _data, newCapacity);

                // Link new free elements to their neighbor
                for (var i = oldCapacity; i < newCapacity; i++)
                {
                    _data[i].Next = i + 1;
                }
            }
        }

        #endregion

        private struct FreeElement
        {
            public T Value;
            public int Next;
        }
    }
}
