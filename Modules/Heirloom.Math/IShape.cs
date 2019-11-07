namespace Heirloom.Math
{
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
        /// Determines is this shape overlaps anothe shape.
        /// </summary>
        bool Overlaps(IShape shape);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray, out Contact contact);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray);
    }
}
