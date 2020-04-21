using System;
using System.Collections.Generic;

namespace Heirloom
{
    internal abstract class RectanglePackerImpl<TElement> : IRectanglePacker<TElement>
    {
        private readonly Dictionary<TElement, IntRectangle> _elements;

        #region Constructors

        protected RectanglePackerImpl(IntSize size)
        {
            _elements = new Dictionary<TElement, IntRectangle>();

            Size = size;
        }

        #endregion

        #region Properties

        public IEnumerable<TElement> Elements => _elements.Keys;

        public IntSize Size { get; }

        #endregion

        public virtual void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(TElement element)
        {
            return _elements.ContainsKey(element);
        }

        public bool Add(TElement element, IntSize itemSize)
        {
            if (Contains(element))
            {
                throw new InvalidOperationException("Unable to pack rectangle element, already contained by packer.");
            }
            else if (Insert(itemSize, out var rectangle))
            {
                // Rectangle was inserted, store element.
                _elements[element] = rectangle;
                return true;
            }
            else
            {
                // Failed to insert
                return false;
            }
        }

        public bool TryGetRectangle(TElement element, out IntRectangle rectangle)
        {
            return _elements.TryGetValue(element, out rectangle);
        }

        public IntRectangle GetRectangle(TElement element)
        {
            if (TryGetRectangle(element, out var rectangle))
            {
                return rectangle;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        protected abstract bool Insert(IntSize size, out IntRectangle rectangle);
    }
}
