using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom
{
    internal sealed class SkylinePacker2<TElement> : RectanglePackerImpl<TElement>
    {
        public List<Strip> Strips;

        #region Constructor

        public SkylinePacker2(int width, int height)
            : this(new IntSize(width, height))
        { }

        public SkylinePacker2(IntSize size)
            : base(size)
        {
            Strips = new List<Strip>();

            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            Strips.Clear();
            Strips.Add(new Strip(0, 0, Size.Width));
        }

        protected override void SortElements(List<KeyValuePair<TElement, IntRectangle>> elements)
        {
            elements.Sort((a, b) =>
            {
                var costA = (a.Value.Height * Size.Width) + b.Value.Width;
                var costB = (b.Value.Height * Size.Width) + b.Value.Width;
                return costB.CompareTo(costA);
            });
        }

        protected override bool Insert(IntSize size, out IntRectangle rect)
        {
            var minCost = int.MaxValue;
            rect = default;

            // Find minimal cost insertion
            for (var i = 0; i < Strips.Count; i++)
            {
                // Try to fit the strip
                if (CheckFit(i, size, out var cost) && cost < minCost)
                {
                    var position = Strips[i].Position;
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
            shelf = Strips.Where(s => s.X < size.Width).Max(s => s.Y);
            return Size.Height - shelf >= size.Height;
        }

        private bool CheckFit(int index, IntSize size, out int cost)
        {
            var root = Strips[index];

            // Get min position
            var x = root.X;
            var y = root.Y;

            // Get max position
            var r = x + size.Width;
            var b = y + size.Height;

            // Start with zero wasted space, but penalize elevation.
            cost = y * 50;

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
                for (var i = index + 1; i < Strips.Count; i++)
                {
                    var strip = Strips[i];

                    // Strip is after the edges of the insertion bounds
                    if (strip.X > r) { break; }
                    if (strip.X + strip.Width < x) { continue; }

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

        private void InsertStrip(Strip insert)
        {
            var adds = new List<Strip> { insert };
            var rems = new List<Strip>();

            // Find overlapping strips and split
            foreach (var current in Strips)
            {
                if (current.CheckOverlap(insert, out var overlap))
                {
                    // Remove strip
                    rems.Add(current);

                    // If overlaping, clip and insert
                    if (overlap == OverlapType.Overlaps)
                    {
                        var l = insert.X + insert.Width;
                        var w = current.Width - l + current.X;
                        // If strip is too small, don't add it
                        // todo: unecessary if strip merging can be done?
                        if (w > 2) // todo: configurable threshold?
                        {
                            adds.Add(new Strip(l, current.Y, w));
                        }
                    }
                }
            }

            // Mutate strip list
            foreach (var rem in rems) { Strips.Remove(rem); }
            Strips.AddRange(adds);

            // Sort all strips 
            Strips.Sort();
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
