using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Heirloom.Mathematics
{
    /// <summary>
    /// Represents a circle via center position and radius.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Circle : IShape, IEquatable<Circle>
    {
        /// <summary>
        /// The center position of the circle.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public float Radius;

        #region Constructors

        /// <summary>
        /// Constructs a new instance of <see cref="Circle"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the center of the circle.</param>
        /// <param name="y">The y-coordiante of the center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(float x, float y, float radius)
            : this((x, y), radius)
        { }

        /// <summary>
        /// Constructs a new instance of <see cref="Circle"/>.
        /// </summary>
        /// <param name="position">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        #endregion

        #region Properties

        Vector IShape.Center => Position;

        // A circle is always convex
        bool IShape.IsConvex => true;

        /// <summary>
        /// Gets the area of the circle.
        /// </summary>
        public float Area => Calc.Pi * (Radius * Radius);

        /// <summary>
        /// Gets the bounding rectangle of this circle.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                // todo: optimize to reduce copies
                var min = Position - (Vector.One * Radius);
                var max = Position + (Vector.One * Radius);
                return new Rectangle(min, max);
            }
        }

        #endregion

        /// <summary>
        /// Sets the components of this circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        #region Nearest Point / Support

        /// <summary>
        /// Gets the nearest point on the circle to the specified point.
        /// </summary>
        public Vector GetNearestPoint(Vector point)
        {
            return GetSupport(point - Position);
        }

        /// <inheritdoc/>
        public Vector GetSupport(Vector dir)
        {
            return Position + (Radius * Vector.Normalize(dir));
        }

        #endregion

        #region Contains (Point, Circle)

        /// <summary>
        /// Determines if the specified point is contained by the circle.
        /// </summary>
        public bool Contains(Vector point)
        {
            return Vector.DistanceSquared(Position, point) < (Radius * Radius);
        }

        /// <summary>
        /// Determines if this circle contains another circle.
        /// </summary>
        public bool Contains(Circle circle)
        {
            var d = Vector.DistanceSquared(Position, circle.Position);
            return Radius > (d + circle.Radius);
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Determines if this circle overlaps another shape.
        /// </summary>
        public bool Overlaps(IShape shape)
        {
            return Collision.CheckOverlap(this, shape);
        }

        #endregion

        #region Raycast

        /// <summary>
        /// Peforms a raycast onto this circle, returning true upon intersection.
        /// </summary>
        /// <param name="ray">Some ray.</param>
        /// <param name="contact">Ray intersection information.</param>
        /// <returns></returns>
        public bool Raycast(Ray ray, out RayContact contact)
        {
            var v = ray.Origin - Position;
            var c = Vector.Dot(v, v) - (Radius * Radius);
            var b = Vector.Dot(v, ray.Direction);
            var disc = (b * b) - c;

            // ...? Ray origin inside circle?
            if (disc < 0)
            {
                contact = default;
                return false;
            }

            // Compute `time`
            var t = -b - Calc.Sqrt(disc);

            // Positive `time` (forward on line)
            if (t >= 0)
            {
                var impact = ray.Origin + (ray.Direction * t);
                var normal = Vector.Normalize(impact - Position);
                contact = new RayContact(impact, normal, t);
                return true;
            }
            // Negative `time` (behind on line)
            else
            {
                // Do nothing, but felt important to comment in case
                // I feel inspired to create a line shape as well
            }

            contact = default;
            return false;
        }

        #endregion

        #region Inflate

        /// <summary>
        /// Adjusts the circle radius by <paramref name="factor"/>.
        /// </summary>
        public void Inflate(float factor)
        {
            this = Inflate(this, factor);
        }

        /// <summary>
        /// Creates a new circle with the radius extended (or shrunk) by <paramref name="factor"/>.
        /// </summary>
        public static Circle Inflate(Circle circle, float factor)
        {
            circle.Radius += factor;
            return circle;
        }

        #endregion

        /// <summary>
        /// Creates an approximate polygon representation from this circle.
        /// </summary>
        /// <param name="error">An error factor for approximating a circle.</param>
        public Polygon ToPolygon(float error = 1F)
        {
            // Approximates a circle with a regular polygon
            return new Polygon(GeometryTools.GetVertices(this, error));
        }

        #region Compute Bounding Circle

        /// <summary>
        /// Computes the tightest fitting bounding circle.
        /// </summary>
        /// <param name="points">Some points.</param>
        /// <returns>The tightest fitting circle.</returns>
        public static Circle FromPoints(IEnumerable<Vector> points)
        {
            var boundary = new List<Vector>(); // todo: may be able to make static, changes thread safety
            return WelzlRecursion(points.ToHashSet(), boundary);

            static Circle WelzlRecursion(HashSet<Vector> points, List<Vector> boundary)
            {
                if (points.Count == 0 || boundary.Count == 3)
                {
                    return ComputeCircleDirectly(boundary);
                }
                else
                {
                    // todo: A faster / GC-safe way OR is this actually fine?
                    var p = points.Skip(Calc.Random.Next(0, points.Count)).First();

                    // ...?
                    points.Remove(p);

                    // ...?
                    var disk = WelzlRecursion(points, boundary);
                    if (!disk.Contains(p)) // ...?
                    {
                        // ...?
                        boundary.Add(p);
                        disk = WelzlRecursion(points, boundary);

                        // Restore state of set R, since we are using recursion
                        // This allows a single instance to be used at all steps
                        boundary.Remove(p);
                    }

                    // Restore state of set P, since we are using recursion
                    // This allows a single instance to be used at all steps
                    points.Add(p);

                    return disk;
                }
            }

            static Circle ComputeCircleDirectly(List<Vector> points)
            {
                switch (points.Count)
                {
                    case 3:
                        return FitCircle(points[0], points[1], points[2]);

                    case 2:
                        var midpoint = (points[0] + points[1]) / 2F;
                        return new Circle(midpoint, Vector.Distance(midpoint, points[0]));

                    case 1:
                        return new Circle(points[0], 0F);

                    case 0:
                        return new Circle(Vector.Zero, 0F);
                }

                throw new InvalidOperationException("Unable to compute minimal circle.");
            }
        }

        /// <summary>
        /// Computes the circumcircle to fit triangle.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Circle FitCircle(Vector a, Vector b, Vector c)
        {
            var abm = (a + b) / 2F;
            var acm = (a + c) / 2F;
            var abp = (b - a).Perpendicular;
            var acp = (c - a).Perpendicular;

            var m0a = abm;
            var m0b = abm + abp;

            var m1a = acm;
            var m1b = acm + acp;

            if (LineSegment.Intersects(m0a, m0b, m1a, m1b, out Vector pt, clampSegment: false))
            {
                return new Circle(pt, Vector.Distance(pt, a));
            }
            else
            {
                // Was unable to compute intersection. This is most likely because the line segments were
                // colinear. So we will just approximate the fit with a midpoint and radius.

                // todo: Min and Max with 3+ via params
                var min = Vector.Min(a, Vector.Min(b, c));
                var max = Vector.Max(a, Vector.Max(b, c));
                var off = (max - min) / 2F;

                // Midpoint + radius
                return new Circle(min + off, off.Length);
            }
        }

        #endregion

        #region Deconstruct

        /// <summary>
        /// Deconstructs the circle into constituient parts.
        /// </summary>
        /// <param name="center">The circle position.</param>
        /// <param name="radius">The circle radius.</param>
        public void Deconstruct(out Vector center, out float radius)
        {
            center = Position;
            radius = Radius;
        }

        #endregion

        #region Comparison Operators

        /// <summary>
        /// Compares two instances of <see cref="Circle"/> for equality.
        /// </summary>
        public static bool operator ==(Circle left, Circle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances of <see cref="Circle"/> for inequality.
        /// </summary>
        public static bool operator !=(Circle left, Circle right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares this <see cref="Circle"/> for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Circle circle
                && Equals(circle);
        }

        /// <summary>
        /// Compares this <see cref="Circle"/> for equality with another <see cref="Circle"/>.
        /// </summary>
        public bool Equals(Circle other)
        {
            return Position.Equals(other.Position)
                && Calc.NearEquals(Radius, other.Radius);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Circle"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Radius);
        }

        #endregion

        /// <summary>
        /// Gets the string representation of this <see cref="Circle"/>.
        /// </summary>
        public override string ToString()
        {
            return $"(Circle, {Position}, {Radius})";
        }
    }
}
