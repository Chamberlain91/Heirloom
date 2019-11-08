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
        bool ContainsPoint(in Vector point);

        /// <summary>
        /// Determines if this shape overlaps the specified shape.
        /// </summary>
        bool Overlaps(IShape shape);

        ///// <summary>
        ///// Performs a raycast against this shape.
        ///// </summary>
        //bool Raycast(in Ray ray, out float distance);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray, out Contact contact);

        /// <summary>
        /// Performs a raycast against this shape.
        /// </summary>
        bool Raycast(in Ray ray);

        ///// <summary>
        ///// Project this shape along the specified axis.
        ///// </summary>
        //Range Project(in Vector axis);
    }
}
