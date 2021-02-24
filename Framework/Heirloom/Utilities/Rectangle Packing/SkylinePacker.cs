using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom
{
    internal sealed class SkylinePacker<TElement> : RectanglePackerImpl<TElement>
    {
        private readonly List<Strip> _strips;

        #region Constructor

        public SkylinePacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public SkylinePacker(IntSize size)
            : base(size)
        {
            _strips = new List<Strip>();

            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            // Clear strips
            _strips.Clear();
            _strips.Add(new Strip(0, 0, Size.Width));
        }

        protected override void OptimizeElementOrder(List<KeyValuePair<TElement, IntRectangle>> elements)
        {
            elements.Sort((a, b) => Compare(a.Value, b.Value, Size.Width));
        }

        public static int Compare(IntRectangle a, IntRectangle b, int packerWidth)
        {
            var costA = (a.Height * packerWidth) + b.Width;
            var costB = (b.Height * packerWidth) + b.Width;
            return costB.CompareTo(costA);
        }

        protected override bool Insert(IntSize size, out IntRectangle rect)
        {
            var minCost = int.MaxValue;
            rect = default;

            // Find minimal cost insertion
            for (var i = 0; i < _strips.Count; i++)
            {
                // Try to fit the strip
                if (CheckFit(i, size, out var cost) && cost < minCost)
                {
                    var position = _strips[i].Position;
                    rect = new IntRectangle(position, size);
                    minCost = cost;
                }
            }

            // If we found some heuristic insertion
            if (minCost < int.MaxValue)
            {
                // Insert at best matching location
                InsertStrip(new Strip(rect.X, rect.Y + size.Height, size.Width));
                return true;
            }

            // No acceptable insertion, try to place on "shelf"
            if (CheckFitShelf(size, out var shelf))
            {
                rect = new IntRectangle((0, shelf), size);
                InsertStrip(new Strip(0, shelf + size.Height, size.Width));
                return true;
            }

            // Unable to find a place to insert
            return false;
        }

        private bool CheckFitShelf(IntSize size, out int shelf)
        {
            shelf = _strips.Where(s => s.X < size.Width).Max(s => s.Y);
            return Size.Height - shelf >= size.Height;
        }

        private bool CheckFit(int index, IntSize size, out int cost)
        {
            var root = _strips[index];

            // Get min position
            var x = root.X;
            var y = root.Y;

            // Get max position
            var r = x + size.Width;
            var b = y + size.Height;

            // Start with zero wasted space, but penalize elevation.
            cost = y * 200;

            // Horizontal Bounds Check
            if (r > Size.Width) { return false; }

            // Vertical Bounds Check
            if (b > Size.Height) { return false; }

            // If the item can fit entirely in the intial strip, accept immediately.
            if (root.Width >= size.Width) { return true; }
            else
            {
                // We must do a more sophisticated check because the item will exceed the initial strip.
                // We then must check subsequent strips in order.
                for (var i = index + 1; i < _strips.Count; i++) // O(n)
                {
                    var strip = _strips[i];

                    // Strip is beyond edge of insertion range, we can exit the loop now.
                    if (strip.X > r) { break; }

                    // Strip is at a higher elevation, so the item will hit that wall.
                    if (strip.Y > y) { return false; }
                    else
                    {
                        // Strip is below, we will waste some pixel space if we insert here.
                        // We know the strip overlaps due to the edge check above.
                        strip.CheckOverlap(x, size.Width, out var overlap);

                        // Compute wasted height.
                        var trashHeight = y - strip.Y;

                        // Strip overlap on an edge, so we must compute the split point.
                        // We also know the strips are x-sorted, so we can do this easily.
                        if (overlap == OverlapType.Overlaps)
                        {
                            cost += (r - strip.X) * trashHeight;
                        }
                        // Strip is entirely contained inside the insertion bounds.
                        else if (overlap == OverlapType.Contained)
                        {
                            cost += strip.Width * trashHeight;
                        }
                    }
                }

                // Was not rejected!
                return true;
            }
        }

        private int FindFirstStrip(int x)
        {
            var min = 0;
            var max = _strips.Count - 1;

            while (min <= max)
            {
                var mid = (min + max) / 2;
                var cmp = x.CompareTo(_strips[mid].X);

                // Exact match
                if (cmp == 0) { return mid; }
                else
                // Strip is greater than input
                if (cmp == 1)
                {
                    min = mid + 1;
                }
                // Strip is lesser than input
                else
                {
                    max = mid - 1;
                }
            }

            return min;
        }

        private void InsertStrip(Strip insert) // O(n*log(n))
        {
            // Get right edge of insertion range
            var r = insert.X + insert.Width;

            var rem = 0;

            // Find overlapping strips and split
            var start = FindFirstStrip(insert.X); // O(log(n))

            for (var i = start; i < _strips.Count; i++)
            {
                var strip = _strips[i - rem];

                // Strip is beyond edge of insertion range, we can exit the loop now.
                if (strip.X > r) { break; }

                // Determine if the strip overlaps the strip we are inserting
                if (strip.CheckOverlap(insert, out var overlap))
                {
                    // Remove strip
                    _strips.RemoveAt(i - rem);
                    rem++;

                    // If overlaping, clip and insert
                    if (overlap == OverlapType.Overlaps)
                    {
                        var l = insert.X + insert.Width;
                        var w = strip.Width - l + strip.X;
                        // If strip is too small, don't add it.
                        // This seeminly improves quality slighly as well as improving performance.
                        // todo: might be unecessary if contiguous strips can be merged
                        if (w > 2) // todo: configurable threshold?
                        {
                            // Add strip
                            _strips.Insert(++i - rem, new Strip(l, strip.Y, w));
                        }
                    }
                }
            }

            // 
            _strips.Insert(start, insert);
        }

        public struct Strip : IComparable<Strip>
        {
            public int X;

            public int Y;

            public int Width;

            public Strip(int x, int y, int width)
            {
                if (width < 0) { throw new ArgumentException("Encountered negative strip width"); }

                X = x;
                Y = y;
                Width = width;
            }

            public IntVector Position => new IntVector(X, Y);

            public bool CheckOverlap(Strip strip, out OverlapType type)
            {
                return CheckOverlap(strip.X, strip.Width, out type);
            }

            public bool CheckOverlap(int x, int width, out OverlapType type)
            {
                var aL = X;
                var aR = X + Width;

                var bL = x;
                var bR = x + width;

                // Assumes by default no overlap (disjoint)
                type = OverlapType.None;

                // A inside B
                if ((aL >= bL && aR <= bR) || (aL < bL && aR > bR))
                {
                    // This strip is full contained by the other strip (or equal)
                    type = OverlapType.Contained;
                }
                else
                if ((aL <= bL && aR > bL) || (aL < bR && aR >= bR))
                {
                    // A overlaps B only on one edge
                    type = OverlapType.Overlaps;
                }

                // Return true if any overlap of any kind occurs.
                return type != OverlapType.None;
            }

            public int CompareTo(Strip other)
            {
                // order is increasing x-coordinates
                return X.CompareTo(other.X);
            }
        }

        public enum OverlapType
        {
            None,
            Contained,
            Overlaps
        }
    }
}
