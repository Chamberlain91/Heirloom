using System.Collections.Generic;

using Heirloom.Math;

namespace StreamingAtlas
{
    /// <summary>
    /// The interface for a rectangle packing utility.
    /// </summary>
    public interface IRectanglePacker<TElement>
    {
        /// <summary>
        /// The elements associated (keys) with each packed rectangle.
        /// </summary>
        IEnumerable<TElement> Elements { get; }

        /// <summary>
        /// The size of the packing bounds.
        /// </summary>
        IntSize Size { get; }

        /// <summary>
        /// Clears all elements.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets a value that determines if the element is contained in this packing.
        /// </summary>
        /// <returns></returns>
        bool Contains(TElement element);

        /// <summary>
        /// Attempts to pack an element into this packing.
        /// </summary>
        /// <param name="element">Some element.</param>
        /// <param name="itemSize">Size of the rectangular element to insert.</param>
        /// <returns>True if the element was able to be inserted.</returns>
        bool Add(TElement element, IntSize itemSize);

        /// <summary>
        /// Attempts to retreive the rectangle associated with the element.
        /// </summary>
        bool TryGetRectangle(TElement element, out IntRectangle rectangle);

        /// <summary>
        /// Gets the rectangle associated with the element.
        /// </summary>
        IntRectangle GetRectangle(TElement element);
    }
}
