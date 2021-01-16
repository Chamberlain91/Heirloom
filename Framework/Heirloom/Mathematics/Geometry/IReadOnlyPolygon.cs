using System.Collections.Generic;

namespace Heirloom.Mathematics
{
    public interface IReadOnlyPolygon
    {
        float Area { get; }
        Rectangle Bounds { get; }
        Vector Center { get; }
        Vector Centroid { get; }

        IReadOnlyList<IReadOnlyList<Vector>> ConvexPartitions { get; }

        bool IsConvex { get; }

        IReadOnlyList<Vector> Normals { get; }
        IReadOnlyList<Vector> Vertices { get; }

        bool Contains(Vector point);

        Vector GetNearestPoint(Vector point);
        Vector GetSupport(Vector direction);

        bool Overlaps(IShape shape);

        bool Raycast(Ray ray);
        bool Raycast(Ray ray, out RayContact hit);

        IEnumerable<Triangle> Triangulate();
    }
}
