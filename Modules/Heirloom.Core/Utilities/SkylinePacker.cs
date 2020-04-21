using System;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    public sealed class SkylinePacker<TElement> : RectanglePacker<TElement>
    {
        public List<Strip> Strips;

        #region Constructor

        public SkylinePacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public SkylinePacker(IntSize size)
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

        protected override bool Insert(IntSize size, out IntRectangle rect)
        {
            // Find first acceptable insertion
            for (var i = 0; i < Strips.Count; i++)
            {
                // Try to fit compact
                if (CheckFit(i, in size))
                {
                    // Anchor image to 
                    rect = new IntRectangle(Strips[i].Position, size);
                    InsertStrip(new Strip(rect.X, rect.Y + size.Height, size.Width));
                    return true;
                }
            }

            // No acceptable insertion, try to place on "shelf"
            if (CheckFitShelf(size, out var shelf))
            {
                rect = new IntRectangle((0, shelf), size);
                InsertStrip(new Strip(0, shelf + size.Height, size.Width));
                return true;
            }

            // Unable to find a place to insert
            rect = default;
            return false;
        }

        private bool CheckFitShelf(IntSize size, out int shelf)
        {
            shelf = Strips.Where(s => s.X < size.Width).Max(s => s.Y);
            return Size.Height - shelf >= size.Height;
        }

        private bool CheckFit(int index, in IntSize size)
        {
            var root = Strips[index];

            // Get insert position
            var x = root.X;
            var y = root.Y;

            // Get insert right edge
            var r = x + size.Width;
            var b = y + size.Height;

            // Horizontal Bounds Check
            if (r > Size.Width) { return false; }

            // Vertical Bounds Check
            if (b > Size.Height) { return false; }

            // Can fit in the input strip, accept.
            if (root.Width >= size.Width) { return true; }
            else
            {
                // We must do a more sophisticated check. We already know the strip at index
                // can't fit it, so we know we exceed that strip. We then must check the strips
                // in order.
                for (var i = index + 1; i < Strips.Count; i++)
                {
                    var strip = Strips[i];

                    // Strip does not overlap insert region
                    if (strip.X > r) { continue; }
                    if (strip.X + strip.Width < x) { continue; }

                    // Strip higher, so we will hit its wall.
                    if (strip.Y > y) { return false; }
                }

                // Was not rejected!
                return true;
            }
        }

        private void InsertStrip(in Strip insert)
        {
            var adds = new List<Strip> { insert };
            var rems = new List<Strip>();

            // Find overlapping strips and decimate
            for (var i = 0; i < Strips.Count; i++)
            {
                var current = Strips[i];

                if (current.CheckOverlap(insert, out var overlap))
                {
                    // Remove strip
                    rems.Add(current);

                    // If overlaping, clip and insert
                    if (overlap == OverlapType.Overlaps)
                    {
                        var l = insert.X + insert.Width;
                        var w = current.Width - l + current.X;
                        if (w > 2) { adds.Add(new Strip(l, current.Y, w)); }
                    }
                }
            }

            // Mutate strip list
            foreach (var rem in rems) { Strips.Remove(rem); }
            Strips.AddRange(adds);

            // Sort all strips 
            Strips.Sort(StripSort);
        }

        private int StripSort(Strip a, Strip b)
        {
            // Sort by height, and strips on the same level, keep left most
            var key = a.Y.CompareTo(b.Y);
            return key == 0 ? a.X.CompareTo(b.X) : key;
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

            public bool CheckOverlap(in Strip strip, out OverlapType type)
            {
                var aL = X;
                var aR = X + Width;

                var bL = strip.X;
                var bR = strip.X + strip.Width;

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
