using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace StreamingAtlas
{
    public class BinPacker<TElement>
    {
        private readonly Dictionary<TElement, IntRectangle> _elements = new Dictionary<TElement, IntRectangle>();
        private readonly HashSet<IntRectangle> _freeRects = new HashSet<IntRectangle>();
        private readonly IntRectangle _bounds;

        [ThreadStatic] private static readonly List<IntRectangle> _rems = new List<IntRectangle>(128);
        [ThreadStatic] private static readonly List<IntRectangle> _adds = new List<IntRectangle>(128);

        #region Constructor

        public BinPacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public BinPacker(IntSize size)
        {
            // Store master bounds
            _bounds = new IntRectangle(IntVector.Zero, size);

            // Start clean
            Clear();
        }

        #endregion

        public IEnumerable<TElement> Elements => _elements.Keys;

        public void Clear()
        {
            // Purge existing storage
            _freeRects.Clear();
            _elements.Clear();

            // Insert initial super rectangle    
            _freeRects.Add(_bounds);
        }

        public bool Add(TElement element, IntSize itemSize, out IntRectangle packedRect)
        {
            if (Add(element, itemSize))
            {
                packedRect = GetRectangle(element);
                return true;
            }
            else
            {
                packedRect = default;
                return false;
            }
        }

        public bool Add(TElement element, IntSize itemSize)
        {
            // Try to find suitable rectangle for item
            if (TryGetSuitableFreeRect(itemSize, out var freeRect))
            {
                // Store the element and associate rectangle
                var itemRect = new IntRectangle(freeRect.Position, itemSize);
                _elements[element] = itemRect;

                // Partition and merge the free rects
                Partition(itemRect);

                return true;
            }
            else
            {
                // Unable to fit item into any rectangle
                return false;
            }
        }

        public bool TryGetRectangle(TElement key, out IntRectangle rectangle)
        {
            return _elements.TryGetValue(key, out rectangle);
        }

        public IntRectangle GetRectangle(TElement key)
        {
            if (TryGetRectangle(key, out var rectangle))
            {
                return rectangle;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public bool Repack()
        {
            // Clone current configuration
            var oldElements = _elements.Select(e => (e.Key, Rect: e.Value)).ToArray();
            var oldFreeRects = _freeRects.ToArray();

            // Purge configuration
            Clear();

            // Insert elements
            foreach (var (element, rect) in oldElements.OrderByDescending(e => e.Rect.Width + e.Rect.Height))
            {
                if (!Add(element, rect.Size))
                {
                    // Restore previous configuration
                    foreach (var (oldElement, oldRect) in oldElements) { _elements[oldElement] = oldRect; }
                    foreach (var oldRect in oldFreeRects) { _freeRects.Add(oldRect); }

                    // Repack was unsuccessful
                    return false;
                }
            }

            // Repack was successful
            return true;
        }

        private void Partition(IntRectangle itemRect)
        {
            _rems.Clear();
            _adds.Clear();

            // For all known free rects
            foreach (var freeRect in _freeRects)
            {
                // If the free rect overlaps the item rect
                if (freeRect.Overlaps(itemRect))
                {
                    // Remove the free rect
                    _rems.Add(freeRect);

                    // Generate the new partitions
                    _adds.AddRange(GeneratePartitions(freeRect, itemRect));
                }

                // Remove other free rects that fully contained
                foreach (var other in _freeRects)
                {
                    if (freeRect == other) { continue; }

                    // If the current free rect contains the other rect
                    if (freeRect.Contains(other)) { _rems.Add(other); }
                }
            }

            // Perform mutations to rectangle set
            foreach (var rect in _rems) { _freeRects.Remove(rect); }
            foreach (var rect in _adds) { _freeRects.Add(rect); }
        }

        private static IEnumerable<IntRectangle> GeneratePartitions(IntRectangle freeRect, IntRectangle itemRect)
        {
            var ht = itemRect.Top - freeRect.Top;
            if (ht > 0) { yield return new IntRectangle(freeRect.X, freeRect.Y, freeRect.Width, ht); }

            var hb = freeRect.Bottom - itemRect.Bottom;
            if (hb > 0) { yield return new IntRectangle(freeRect.X, itemRect.Bottom, freeRect.Width, hb); }

            var wr = freeRect.Right - itemRect.Right;
            if (wr > 0) { yield return new IntRectangle(itemRect.Right, freeRect.Top, wr, freeRect.Height); }

            var wl = itemRect.Left - freeRect.Left;
            if (wl > 0) { yield return new IntRectangle(freeRect.Left, freeRect.Top, wl, freeRect.Height); }
        }

        private bool TryGetSuitableFreeRect(IntSize itemSize, out IntRectangle freeRectangle)
        {
            freeRectangle = IntRectangle.Infinite;

            var found = false;

            // 
            foreach (var freeRect in _freeRects.OrderBy(r => r.Min.X + r.Min.Y))
            {
                // If the item can fit in here...
                if (freeRect.Width >= itemSize.Width && freeRect.Height >= itemSize.Height)
                {
                    // Select the free rect that causes minimal wasted area
                    if (minimizeArea(freeRectangle, freeRect, itemSize))
                    {
                        freeRectangle = freeRect;
                        found = true;
                    }
                }
            }

            // Return if we found a suitable rectangle
            return found;

            static bool minimizeArea(IntRectangle a, IntRectangle b, IntSize item)
            {
                var x0 = a.Width - item.Width;
                var y0 = a.Height - item.Height;
                var x1 = b.Width - item.Width;
                var y1 = b.Height - item.Height;

                return (x0 + y0) <= (x1 + y1);
            }
        }
    }
}
