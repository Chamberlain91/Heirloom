using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Provides a container for creating and reusing objects.
    /// </summary>
    public sealed class ObjectPool<T> where T : class
    {
        private readonly Func<T> _createObject;
        private readonly Action<T> _resetObject;
        private readonly Queue<T> _pool;

        #region Properties

        /// <summary>
        /// Number of objects expected to be outside the pool.
        /// </summary>
        public int OutsideCount { get; private set; }

        /// <summary>
        /// The number of objects inside the pool that have been recycled.
        /// </summary>
        public int InsideCount => _pool.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="ObjectPool{T}"/> using default constructors via <see cref="Activator.CreateInstance{T}"/>. <para/>
        /// Objects will not have their state reset when recycled.
        /// </summary>
        public ObjectPool()
            : this(Activator.CreateInstance<T>)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="ObjectPool{T}"/>.
        /// </summary>
        /// <param name="createObject">Function that instantiates a new object.</param>
        /// <param name="resetObject">(Optional) Function that resets the state of a recycled object.</param>
        public ObjectPool(Func<T> createObject, Action<T> resetObject = null)
        {
            // 
            _createObject = createObject ?? throw new ArgumentNullException(nameof(createObject));
            _resetObject = resetObject;

            // 
            _pool = new Queue<T>();
        }

        #endregion

        /// <summary>
        /// Requests the next available object. Will try to use a recycled object, otherwise returns a freshly instantiated object.
        /// </summary>
        public T Request()
        {
            T obj;
            if (_pool.Count > 0)
            {
                // Get object from pool
                obj = _pool.Dequeue();

                // Recycle object (reset state)
                _resetObject?.Invoke(obj);
            }
            else
            {
                // Allocate a new object
                obj = _createObject();
            }

            OutsideCount++;
            return obj;
        }

        /// <summary>
        /// Places the objects into this pool for reuse w/ <see cref="Request"/>.
        /// </summary>
        public void Recycle(T obj)
        {
            _pool.Enqueue(obj);
            OutsideCount--;
        }
    }
}
