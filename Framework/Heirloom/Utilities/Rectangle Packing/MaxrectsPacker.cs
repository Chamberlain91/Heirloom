using System.Collections.Generic;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom
{
    internal sealed class MaxrectsPacker<TElement> : RectanglePackerImpl<TElement>
    {
        private readonly HashSet<IntRectangle> _freeRects = new HashSet<IntRectangle>();

        #region Constructor

        public MaxrectsPacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public MaxrectsPacker(IntSize size)
            : base(size)
        {
            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            // Purge existing storage
            _freeRects.Clear();

            // Insert initial super rectangle    
            _freeRects.Add(new IntRectangle(IntVector.Zero, Size));
        }

        protected override bool Insert(IntSize itemSize, out IntRectangle itemRect)
        {
            // Try to find suitable rectangle for item
            if (TryGetSuitableFreeRect(itemSize, out var freeRect))
            {
                // Store the element and associate rectangle
                itemRect = new IntRectangle(freeRect.Position, itemSize);

                // Partition and merge the free rects
                Partition(itemRect);

                return true;
            }
            else
            {
                // Unable to fit item into any rectangle
                itemRect = default;
                return false;
            }
        }

        private void Partition(IntRectangle itemRect)
        {
            var _rems = new List<IntRectangle>(128);
            var _adds = new List<IntRectangle>(128);

            // For all known free rects
            foreach (var freeRect in _freeRects)
            {
                // If the free rect overlaps the item rect
                if (freeRect.Overlaps(itemRect))
                {
                    // Generate the new partitions
                    _adds.AddRange(GeneratePartitions(freeRect, itemRect));

                    // Remove the free rect
                    _rems.Add(freeRect);
                }

                // Remove other free rects that fully contained
                foreach (var other in _freeRects)
                {
                    if (freeRect == other) { continue; }
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
