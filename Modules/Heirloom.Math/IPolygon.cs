using System.Collections.Generic;

namespace Heirloom.Math
{
    public interface IPolygon : IReadOnlyList<Vector>
    {
        Vector Center { get; }

        Vector Centroid { get; }

        Rectangle Bounds { get; }

        float Area { get; }
    }
}
