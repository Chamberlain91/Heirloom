
using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    public interface IFiniteGrid<T> : IGrid<T>
    {
        int Height { get; }

        int Width { get; }

        bool IsValidCoordinate(in IntVector co);
    }
}
