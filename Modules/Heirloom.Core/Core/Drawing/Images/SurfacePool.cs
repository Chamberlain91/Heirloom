using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// Provides a mechanism for requesting temporary surfaces and recycling them for reuse later.
    /// </summary>
    /// <category>Drawing</category>
    public static class SurfacePool
    // todo: Heuristic for discarding excessive surfaces?
    //       Find surfaces two "generations" old...?
    //       Perhaps replace the queue with a stack?
    {
        private static readonly Dictionary<(MultisampleQuality, IntSize), Queue<Surface>> _pools;
        private static readonly HashSet<Surface> _owned;

        static SurfacePool()
        {
            _pools = new Dictionary<(MultisampleQuality, IntSize), Queue<Surface>>();
            _owned = new HashSet<Surface>();
        }

        /// <summary>
        /// Requests a temporary surface.
        /// </summary>
        /// <param name="width">The width of the surface.</param>
        /// <param name="height">The height of the surface.</param>
        /// <param name="multisample">The multisample quality of the surface.</param>
        /// <returns>A surface owned by this pool.</returns>
        public static Surface Request(int width, int height, MultisampleQuality multisample = MultisampleQuality.None)
        {
            return Request(new IntSize(width, height), multisample);
        }

        /// <summary>
        /// Requests a temporary surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="multisample">The multisample quality of the surface.</param>
        /// <returns>A surface owned by this pool.</returns>
        public static Surface Request(IntSize size, MultisampleQuality multisample = MultisampleQuality.None)
        {
            // Get the associated pool the specified multisample level
            var pool = GetPool(size, multisample);

            lock (pool)
            {
                if (pool.Count > 0)
                {
                    // Return an existing surface
                    return pool.Dequeue();
                }
            }

            // The pool did not return a surface...
            lock (_owned)
            {
                // Construct a new surface and return it.
                var surface = new Surface(size, multisample);
                _owned.Add(surface);
                return surface;
            }
        }

        /// <summary>
        /// Recycle a surface back into the pool for reuse. It is assumed the surface is no longer used after this call.
        /// </summary>
        /// <param name="surface">Some surface owned by this pool.</param>
        public static void Recycle(Surface surface)
        {
            lock (_owned)
            {
                if (_owned.Contains(surface))
                {
                    // Place surface
                    var pool = GetPool(surface.Size, surface.Multisample);
                    lock (pool)
                    {
                        pool.Enqueue(surface);
                    }
                }
                else
                {
                    throw new ArgumentException($"Surface was not owned by the surface pool.");
                }
            }
        }

        /// <summary>
        /// Removes surfaces currently existing in the pool.
        /// </summary>
        public static void Clean()
        {
            // For each pool
            foreach (var (_, pool) in _pools)
            {
                // Remove ownership
                foreach (var p in pool)
                {
                    _owned.Remove(p);
                }

                // Clear pool
                pool.Clear();
            }
        }

        private static Queue<Surface> GetPool(IntSize size, MultisampleQuality multisample)
        {
            var key = (multisample, size);
            if (_pools.TryGetValue(key, out var pool))
            {
                // Return already known pool
                return pool;
            }
            else
            {
                // Construct pool
                _pools[key] = new Queue<Surface>();
                return _pools[key];
            }
        }
    }
}
