using Heirloom.Math;

namespace StreamingAtlas
{
    public class ShelfPacker<TElement> : RectanglePacker<TElement>
    {
        private int _shelf, _x, _y;

        #region Constructor

        public ShelfPacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public ShelfPacker(IntSize size)
            : base(size)
        { }

        #endregion

        public override void Clear()
        {
            _x = _y = _shelf = 0;
            base.Clear();
        }

        protected override bool Insert(IntSize size, out IntRectangle rectangle)
        {
            // If beyond the right
            if (_x + size.Width >= Size.Width)
            {
                _x = 0;
                _y = _shelf;
            }

            // If beyond the bottom
            if (_y + size.Height >= Size.Height)
            {
                // Cannot fit
                rectangle = default;
                return false;
            }

            // Adjust shelf
            _shelf = Calc.Max(_y + size.Height, _shelf);
            _x += size.Width;

            // Found a location to insert the rectangle
            rectangle = new IntRectangle(_x, _y, size.Width, size.Height);
            return true;
        }
    }
}
