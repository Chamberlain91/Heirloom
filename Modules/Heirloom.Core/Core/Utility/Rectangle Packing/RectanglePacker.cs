using System;
using System.Collections.Generic;

namespace Heirloom
{
    /// <summary>
    /// A utility object for packing rectangles into a larger container rectangle.
    /// </summary>
    /// <remarks>
    /// Useful for creating things like sprite sheets, font atlases, etc.
    /// </remarks>
    /// <category>Utility</category>
    public sealed class RectanglePacker<T> : IRectanglePacker<T>
    {
        private readonly RectanglePackerImpl<T> _impl;

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="RectanglePacker{T}"/>.
        /// </summary>
        /// <param name="size">The size of the container rectangle.</param>
        /// <param name="quality">The packing algorithm to use.</param>
        public RectanglePacker(IntSize size, PackingAlgorithm quality = PackingAlgorithm.Maxrects)
            : this(size.Width, size.Height, quality)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="RectanglePacker{T}"/>.
        /// </summary>
        /// <param name="width">The width of the container rectangle.</param>
        /// <param name="height">The height of the container rectangle.</param>
        /// <param name="quality">The packing algorithm to use.</param>
        public RectanglePacker(int width, int height, PackingAlgorithm quality = PackingAlgorithm.Maxrects)
        {
            _impl = quality switch
            {
                PackingAlgorithm.Maxrects => new SkylinePacker<T>(width, height),
                PackingAlgorithm.Skyline => new SkylinePacker<T>(width, height),
                PackingAlgorithm.Shelf => new ShelfPacker<T>(width, height),
                _ => throw new ArgumentException("Incorrect packing algorithm specified.", nameof(quality)),
            };
        }

        #endregion

        /// <inheritdoc/>
        public IEnumerable<T> Elements => ((IRectanglePacker<T>) _impl).Elements;

        /// <inheritdoc/>
        public IntSize Size => ((IRectanglePacker<T>) _impl).Size;

        /// <inheritdoc/>
        public bool TryAdd(T element, IntSize itemSize)
        {
            return ((IRectanglePacker<T>) _impl).TryAdd(element, itemSize);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            ((IRectanglePacker<T>) _impl).Clear();
        }

        /// <inheritdoc/>
        public bool Contains(T element)
        {
            return ((IRectanglePacker<T>) _impl).Contains(element);
        }

        /// <inheritdoc/>
        public IntRectangle GetRectangle(T element)
        {
            return ((IRectanglePacker<T>) _impl).GetRectangle(element);
        }

        /// <inheritdoc/>
        public bool TryGetRectangle(T element, out IntRectangle rectangle)
        {
            return ((IRectanglePacker<T>) _impl).TryGetRectangle(element, out rectangle);
        }
    }
}
