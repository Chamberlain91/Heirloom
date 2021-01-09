namespace Meadows.Mathematics
{
    /// <summary>
    /// Represents the general interface of a shape.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets the bounding rectangle of this shape.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Gets the center (or an approximated center) of this shape.
        /// </summary>
        Vector Center { get; }

        /// <summary>
        /// Gets the area of this shape.
        /// </summary>
        float Area { get; }

        /// <summary>
        /// Gets a value that determines if this shape is convex.
        /// </summary>
        bool IsConvex { get; }

        /// <summary>
        /// Gets the nearest point on the shape boundary to the specified point.
        /// </summary>
        Vector GetNearestPoint(Vector point);

        /// <summary>
        /// Gets the support point (ie, deepest) in the specified direction.
        /// </summary>
        Vector GetSupport(Vector direction);

        /// <summary>
        /// Determines if this shape contains the specified point.
        /// </summary>
        bool Contains(Vector point);

        /// <summary>
        /// Determines if this shape overlaps another shape.
        /// </summary>
        bool Overlaps(IShape shape);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(Ray ray, out RayContact contact);
    }
}
