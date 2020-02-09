namespace Heirloom.Math
{
    /// <summary>
    /// Represents the general interface of a shape and common operators each shape should support.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets the bounding rectangle of this shape.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Gets the area of this shape.
        /// </summary>
        float Area { get; }

        /// <summary>
        /// Gets the nearest point on the shape to the specified point.
        /// </summary>
        Vector GetClosestPoint(in Vector point);

        /// <summary>
        /// Determines if this shape contains the specified point.
        /// </summary>
        bool Contains(in Vector point);

        /// <summary>
        /// Determines if this shape overlaps the specified shape.
        /// </summary>
        bool Overlaps(IShape shape);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray, out RayContact contact);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray);

        /// <summary>
        /// Project this shape onto the specified axis.
        /// </summary>
        Range Project(in Vector axis);
    }
}
