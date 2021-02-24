using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom
{
    internal sealed class SkylinePacker<TElement> : RectanglePackerImpl<TElement>
    {
        private readonly LinkedList<Strip> _strips;

        #region Constructor

        public SkylinePacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public SkylinePacker(IntSize size)
            : base(size)
        {
            _strips = new LinkedList<Strip>();

            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            // Clear strips
            _strips.Clear();
            _strips.AddFirst(new Strip(0, 0, Size.Width));
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
            var minNode = default(LinkedListNode<Strip>);

            rect = default;

            // Find minimal cost insertion
            // for (var i = 0; i < _strips.Count; i++)
            var node = _strips.First;
            while (node != null)
            {
                // Try to fit the strip
                if (CheckFit(node, size, out var cost) && cost < minCost)
                {
                    var position = node.Value.Position;
                    rect = new IntRectangle(position, size);
                    minNode = node;
                    minCost = cost;
                }

                // Advance to next node
                node = node.Next;
            }

            // If we found some heuristic insertion
            if (minCost < int.MaxValue)
            {
                // Insert at best matching location
                InsertStrip(minNode, new Strip(rect.X, rect.Y + size.Height, size.Width));
                return true;
            }

            // No acceptable insertion, try to place on "shelf"
            if (CheckFitShelf(size, out var shelf))
            {
                // Create new shelf (complete horizontal layer)
                InsertStrip(_strips.First, new Strip(0, shelf + size.Height, size.Width));

                // Return success
                rect = new IntRectangle((0, shelf), size);
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

        private bool CheckFit(LinkedListNode<Strip> node, IntSize size, out int cost)
        {
            var root = node.Value;

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
                // Skip the first node, we know it does not fit from the check above.
                node = node.Next;

                // We must do a more sophisticated check because the item will exceed the initial strip.
                // We then must check subsequent strips in order.
                while (node != null) // O(n)
                {
                    // var strip = _strips[i];
                    var strip = node.Value;

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

                    // Advance to next node
                    node = node.Next;
                }

                // Was not rejected!
                return true;
            }
        }

        private void InsertStrip(LinkedListNode<Strip> node, Strip insert)
        {
            // Get right edge of insertion range
            var r = insert.X + insert.Width;

            // Find overlapping strips and split
            _strips.AddBefore(node, insert);

            while (node != null)
            {
                var strip = node.Value;
                var next = node.Next;

                // Strip is beyond edge of insertion range, we can exit the loop now.
                if (strip.X > r) { break; }

                // Determine if the strip overlaps the strip we are inserting
                if (strip.CheckOverlap(insert.X, insert.Width, out var overlap))
                {
                    // If overlaping, clip and insert
                    if (overlap == OverlapType.Overlaps)
                    {
                        var l = insert.X + insert.Width;
                        var w = strip.Width - l + strip.X;
                        // If strip is too small, don't add it.
                        // This is an empiracal value but it seeminly does the best to improve
                        // both packing quality as well as improving performance.
                        // todo: might be unecessary if contiguous strips can be merged
                        if (w > 2) // todo: configurable threshold?
                        {
                            // Add strip
                            _strips.AddBefore(node, new Strip(l, strip.Y, w));
                        }
                    }

                    // Remove strip
                    _strips.Remove(node);
                }

                // Advance node
                node = next;
            }
        }

        public struct Strip
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
        }

        public enum OverlapType
        {
            None,
            Contained,
            Overlaps
        }
    }
}
