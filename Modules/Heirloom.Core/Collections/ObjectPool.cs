using System;
using System.Collections.Generic;

namespace Heirloom.Collections
{
    public class ObjectPool<T> where T : class
    {
        private readonly HashSet<T> _owned = new HashSet<T>();
        private readonly Queue<T> _queue = new Queue<T>();

        protected virtual T CreateItem()
        {
            return Activator.CreateInstance<T>();
        }

        protected virtual void ResetItem(T item)
        {
            // Do Nothing
        }

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
