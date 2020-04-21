namespace Heirloom
{
    public sealed class ShelfPacker<TElement> : RectanglePacker<TElement>
    {
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
            // Start clean
            Clear();
        }

        #endregion

        public override void Clear()
        {
            base.Clear();

            // Reset shelf
            _x = _y = _next = 0;
        }

        protected override bool Insert(IntSize size, out IntRectangle rect)
        {
            // If too large to ever fit in the packer
            if (size.Width > Size.Width || size.Height > Size.Height)
            {
                rect = default;
                return false;
            }

            // Compute right edge
            var right = _x + size.Width;

            // Will the insert exceed the right boundary?
            if (right >= Size.Width)
            {
                // Perform a "newline"
                _y = _next;
                _x = 0;
            }

            // Compute bottom edge, pushing next shelf
            var bottom = _y + size.Height;
            if (bottom > _next) { _next = bottom; }

            // Will the image exceed bottom boundary?
            if (bottom > Size.Height)
            {
                // Item could not fit.
                rect = default;
                return false;
            }

            // Compute packing rectangle
            rect = new IntRectangle(new IntVector(_x, _y), size);
            _x += size.Width; // Advance cursor
            return true;
        }
    }
}
