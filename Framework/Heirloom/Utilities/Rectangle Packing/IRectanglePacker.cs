using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom
{
    /// <summary>
    /// An interface for packing rectangles into a larger container rectangle.
    /// </summary>
    /// <category>Utility</category>
    internal interface IRectanglePacker<TElement>
    {
        /// <summary>
        /// Gets the elements packed into this collection.
        /// </summary>
        ICollection<TElement> Elements { get; }

        /// <summary>
        /// Gets the size of the container rectangle.
        /// </summary>
        IntSize Size { get; }

        /// <summary>
        /// Attempts to add an element to the collection.
        /// </summary>
        /// <param name="element">The element to pack.</param>
        /// <param name="itemSize">The size of the element.</param>
        /// <returns>Will return false if the item was not packed successfully or already existed, otherwise true.</returns>
        bool TryAdd(TElement element, IntSize itemSize);

        /// <summary>
        /// Removes all packed elements from this collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines if this collection contains the specified element.
        /// </summary>
        bool Contains(TElement element);

        /// <summary>
        /// Gets the packed rectangle of the specified element.
        /// </summary>
        /// <param name="element">Some element contained by this collection.</param>
        /// <returns>The rectangle of the packed element.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when requesting an element that is not contained by this collection.</exception>
        IntRectangle GetRectangle(TElement element);

        /// <summary>
        /// Attempts to get the packed rectangle of the specified element.
        /// </summary>
        /// <param name="element">Some element potentially contained by this collection.</param>
        /// <param name="rectangle">Outputs the rectangle of the packed element, if call returns true.</param>
        /// <returns>True if the element was contained by this collection.</returns>
        bool TryGetRectangle(TElement element, out IntRectangle rectangle);
    }
}
