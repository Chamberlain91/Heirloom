using Heirloom.Math;

namespace Heirloom.Collections.Spatial
{
    /// <summary>
    /// Represents an infinite sparse grid.
    /// </summary>
    public interface ISparseGrid<T> : IGrid<T>
    {
        bool TryGetValue(IntVector co, out T value);

        bool ContainsAt(IntVector co);

        bool Remove(IntVector co);
    }
}
