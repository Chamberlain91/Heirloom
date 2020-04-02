using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES.Utilities
{
    public class ShelfPacker<TElement> : RectanglePacker<TElement>
    {
        private readonly IntRectangle _bounds;

        private int _x;
        private int _next;
        private int _y;

        #region Constructor

        public ShelfPacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public ShelfPacker(IntSize size)
            : base(size)
        {
            // Store master bounds
            _bounds = new IntRectangle(IntVector.Zero, size);

            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            // Reset shelf
            _y = _next = 0;
            _x = 0;
        }

        protected override bool Insert(IntSize itemSize, out IntRectangle itemRect)
        {
            // If too large to ever fit in the packer
            if (itemSize.Width > _bounds.Width || itemSize.Height > _bounds.Height)
            {
                itemRect = default;
                return false;
            }

            // Compute right edge
            var right = _x + itemSize.Width;

            // Will the insert exceed the right boundary?
            if (right >= _bounds.Width)
            {
                // Perform a "newline"
                _y = _next;
                _x = 0;
            }

            // Compute bottom, pushing next shelf
            var bottom = _y + itemSize.Height;
            if (bottom > _next) { _next = bottom; }

            // Will the image exceed bottom boundary?
            if (bottom > _bounds.Height)
            {
                // Item could not fit.
                itemRect = default;
                return false;
            }

            // Compute packing rectangle
            itemRect = new IntRectangle(new IntVector(_x, _y), itemSize);

            // Advance cursor 
            _x += itemSize.Width;

            return true;
        }
    }
}
