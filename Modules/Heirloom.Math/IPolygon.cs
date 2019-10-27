using System.Collections.Generic;

namespace Heirloom.Math
{
    public interface IPolygon : IShape, IReadOnlyList<Vector>
    {
        Vector Center { get; }

        Vector Centroid { get; }
    }
}
