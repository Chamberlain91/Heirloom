using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    /// <summary>
    /// Implements an object pool to recycle objects and reduce allocatio stress.
    /// </summary>
    /// <typeparam name="T">Some reference type.</typeparam>
    public class ObjectPool<T> where T : class
    {
        private readonly HashSet<T> _owned = new HashSet<T>();
        private readonly Queue<T> _queue = new Queue<T>();

        /// <summary>
        /// Constructs an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>An newly allocated instance of <typeparamref name="T"/>.</returns>
        protected virtual T CreateItem()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// When <see cref="ObjectPool{T}"/> is subclassed, this can be overriden to clear state on recycled objects.
        /// </summary>
        /// <param name="item">A recycled object.</param>
        protected virtual void ResetItem(T item)
        {
            // Do Nothing
        }

        /// <summary>
        /// Requests an object from the <see cref="ObjectPool{T}"/>.
        /// </summary>
        /// <returns>An object owned by this pool.</returns>
        public T Request()
        {
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    // Grab the next item in the pool
                    return _queue.Dequeue();
                }
                else
                {
                    // Construct a new item and return it
                    var item = CreateItem();
                    _owned.Add(item);
                    return item;
                }
            }
        }

        /// <summary>
        /// Recycles an object owned by this pool for layer reuse with <see cref="Request"/>.
        /// </summary>
        /// <param name="item">An object owned by this pool.</param>
        public void Recycle(T item)
        {
            lock (_queue)
            {
                if (_owned.Contains(item))
                {
                    _queue.Enqueue(item);
                    ResetItem(item);
                }
                else if (item is null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
                else
                {
                    throw new InvalidOperationException("Unable to recycle item, object not owned by the pool.");
                }
            }
        }
    }
}
