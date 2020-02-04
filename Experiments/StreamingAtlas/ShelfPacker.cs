using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Math;

namespace StreamingAtlas
{
    public class ShelfPacker<TElement> : IRectanglePacker<TElement>
    {
        private readonly Dictionary<TElement, IntRectangle> _elements = new Dictionary<TElement, IntRectangle>();
        private readonly IntRectangle _bounds;

        private int _shelf, _x, _y;

        [ThreadStatic] private static readonly List<IntRectangle> _rems = new List<IntRectangle>(128);
        [ThreadStatic] private static readonly List<IntRectangle> _adds = new List<IntRectangle>(128);

        #region Constructor

        public ShelfPacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public ShelfPacker(IntSize size)
        {
            // Store master bounds
            _bounds = new IntRectangle(IntVector.Zero, size);

            // Start clean
            Clear();
        }

        #endregion

        public IEnumerable<TElement> Elements => _elements.Keys;

        public IntSize Size => _bounds.Size;

        public void Clear()
        {
            _x = _y = _shelf = 0;
            _elements.Clear();
        }

        /// <summary>
        /// Inserts an element into the packing.
        /// </summary>
        public bool Add(TElement element, IntSize itemSize)
        {
            // If beyond the right
            if (_x + itemSize.Width >= _bounds.Width)
            {
                _x = 0;
                _y = _shelf;
            }

            // If beyond the bottom
            if (_y + itemSize.Height >= _bounds.Height)
            {
                // Cannot fit
                return false;
            }

            // Adjust shelf
            _shelf = Calc.Max(_y + itemSize.Height, _shelf);

            // Insert rectangle
            var rect = new IntRectangle(_x, _y, itemSize.Width, itemSize.Height);
            _elements[element] = rect;

            _x += itemSize.Width;

            return true;
        }

        public bool Contains(TElement element)
        {
            return _elements.ContainsKey(element);
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
    }
}
