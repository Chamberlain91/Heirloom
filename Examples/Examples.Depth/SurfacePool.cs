using System;
using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Examples.Depth
{
    public static class SurfacePool
    {
        private static readonly Dictionary<(MultisampleQuality, IntSize), Queue<Surface>> _pools;
        private static readonly HashSet<Surface> _owned;

        static SurfacePool()
        {
            _pools = new Dictionary<(MultisampleQuality, IntSize), Queue<Surface>>();
            _owned = new HashSet<Surface>();
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

        public static Surface Request(int width, int height, MultisampleQuality multisample = MultisampleQuality.None)
        {
            return Request(new IntSize(width, height), multisample);
        }

        public static Surface Request(IntSize size, MultisampleQuality multisample = MultisampleQuality.None)
        {
            // Get the associated pool the specified multisample level
            var pool = GetPool(size, multisample);
            if (pool.Count > 0)
            {
                // Return an existing surface
                return pool.Dequeue();
            }
            else
            {
                // Construct a new surface
                var surface = new Surface(size, multisample);
                _owned.Add(surface);
                return surface;
            }
        }

        public static void Recycle(Surface surface)
        {
            if (_owned.Contains(surface))
            {
                // Place surface
                var pool = GetPool(surface.Size, surface.Multisample);
                pool.Enqueue(surface);

                // todo: Heuristic for discarding excessive surfaces
            }
            else
            {
                throw new ArgumentException($"Surface was not owned by the surface pool.");
            }
        }
    }
}
