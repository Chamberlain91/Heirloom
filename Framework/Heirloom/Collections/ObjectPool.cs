using System;
using System.Collections.Generic;

using Heirloom.Utilities;

namespace Heirloom.Collections
{
    /// <summary>
    /// Implements an object pool to recycle objects and reduce allocation stress.
    /// </summary>
    /// <typeparam name="T">Some reference type.</typeparam>
    public sealed class ObjectPool<T> where T : class
    {
        private readonly HashSet<T> _owned = new HashSet<T>();
        private readonly Queue<T> _queue = new Queue<T>();

        private static readonly ObjectPoolStrategy<T> _strategy;

        static ObjectPool()
        {
            // Find object pool strategies
            var types = new List<Type>(ReflectionHelper.GetSubclassTypes<ObjectPoolStrategy<T>>());
            if (types.Count > 1)
            {
                Log.Warning($"WARNING: More than one implementation of {nameof(ObjectPoolStrategy<T>)}.");
            }

            // Creat instance of strategy
            if (types.Count > 0) { _strategy = Activator.CreateInstance(types[0]) as ObjectPoolStrategy<T>; }
            else { _strategy = new DefaultStrategy(); }
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
                    var item = _strategy.Create();
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
                    _strategy.Clear(item);
                    _queue.Enqueue(item);
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

        private sealed class DefaultStrategy : ObjectPoolStrategy<T>
        {
            protected internal override T Create()
            {
                return Activator.CreateInstance<T>();
            }

            protected internal override void Clear(T obj)
            {
                // Does Nothing
            }
        }
    }
}
