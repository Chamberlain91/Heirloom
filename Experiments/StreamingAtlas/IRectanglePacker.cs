using System.Collections.Generic;

using Heirloom.Math;

namespace StreamingAtlas
{
    public interface IRectanglePacker<TElement>
    {
        IEnumerable<TElement> Elements { get; }
        IntSize Size { get; }

        void Clear();

        bool Contains(TElement element);

        bool Add(TElement element, IntSize itemSize);

        bool TryGetRectangle(TElement key, out IntRectangle rectangle);
        IntRectangle GetRectangle(TElement key);
    }
}
