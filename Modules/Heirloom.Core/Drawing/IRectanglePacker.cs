using System.Collections.Generic;

namespace Heirloom
{
    internal interface IRectanglePacker<TElement>
    {
        IEnumerable<TElement> Elements { get; }
        IntSize Size { get; }

        bool Add(TElement element, IntSize itemSize);
        void Clear();
        bool Contains(TElement element);
        IntRectangle GetRectangle(TElement element);
        bool TryGetRectangle(TElement element, out IntRectangle rectangle);
    }
}
