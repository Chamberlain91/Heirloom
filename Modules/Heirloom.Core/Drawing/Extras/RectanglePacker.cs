using System;
using System.Collections.Generic;

namespace Heirloom
{
    public class RectanglePacker<T> : IRectanglePacker<T>
    {
        private readonly RectanglePackerImpl<T> _impl;

        public RectanglePacker(IntSize size, PackingAlgorithm quality = PackingAlgorithm.Maxrects)
            : this(size.Width, size.Height, quality)
        { }

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

        public IEnumerable<T> Elements => ((IRectanglePacker<T>) _impl).Elements;

        public IntSize Size => ((IRectanglePacker<T>) _impl).Size;

        public bool Add(T element, IntSize itemSize)
        {
            return ((IRectanglePacker<T>) _impl).Add(element, itemSize);
        }

        public void Clear()
        {
            ((IRectanglePacker<T>) _impl).Clear();
        }

        public bool Contains(T element)
        {
            return ((IRectanglePacker<T>) _impl).Contains(element);
        }

        public IntRectangle GetRectangle(T element)
        {
            return ((IRectanglePacker<T>) _impl).GetRectangle(element);
        }

        public bool TryGetRectangle(T element, out IntRectangle rectangle)
        {
            return ((IRectanglePacker<T>) _impl).TryGetRectangle(element, out rectangle);
        }
    }
}
