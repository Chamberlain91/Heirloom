using System;
using System.Collections.Generic;

namespace Meadows
{
    public sealed class RecyclePool<T>
    {
        private readonly Queue<T> _unclaimed = new Queue<T>();
        private readonly Func<T> _spawn;

        public RecyclePool()
            : this(Activator.CreateInstance<T>)
        { }

        public RecyclePool(Func<T> spawn)
        {
            _spawn = spawn ?? throw new ArgumentNullException(nameof(spawn));
        }

        public T Request()
        {
            if (_unclaimed.Count > 0) { return _unclaimed.Dequeue(); }
            else { return _spawn(); }
        }

        public void Recycle(T item)
        {
            _unclaimed.Enqueue(item);
        }
    }
}
