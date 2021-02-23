using System.Collections.Generic;

using Heirloom.Mathematics;

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

        public ICollection<TElement> Elements => _elements.Keys;

        public IntSize Size { get; }

        #endregion

        public virtual void Clear()
        {
            _elements.Clear();
        }

        public bool Compact()
        {
            // Copy elements
            var elements = new List<KeyValuePair<TElement, IntRectangle>>(_elements);

            // Clear 
            Clear();

            // Insert elements in optimal order
            SortElements(elements);
            foreach (var (element, rect) in elements)
            {
                if (!TryAdd(element, rect.Size))
                {
                    // Was unable to compact
                    RestorePriorState();
                    return false;
                }
            }

            // Compact complete
            return true;

            void RestorePriorState()
            {
                _elements.Clear();
                foreach (var (k, v) in elements)
                {
                    _elements.Add(k, v);
                }
            }
        }

        protected virtual void SortElements(List<KeyValuePair<TElement, IntRectangle>> elements)
        {
            elements.Sort((a, b) =>
            {
                // Sort elements area descending order
                return b.Value.Area.CompareTo(a.Value.Area);
            });
        }

        public bool Contains(TElement element)
        {
            return _elements.ContainsKey(element);
        }

        public bool TryAdd(TElement element, IntSize itemSize)
        {
            if (Contains(element))
            {
                // Already packed.
                return false;
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
