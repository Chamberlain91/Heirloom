using System;
using System.Collections.Generic;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Contains methods for computing the collision of shapes.
    /// </summary>
    public static class Collision
    {
        private static readonly float _edgeThreshold = 0.0001F;
        private static readonly int _maxIterations = 24;

        [ThreadStatic] private static ShapeProxy _shapeA;
        [ThreadStatic] private static ShapeProxy _shapeB;

        [ThreadStatic] private static List<Support> _gjkPolygon;
        [ThreadStatic] private static Vector _direction;

        private enum GJKIterationResult { Failure, Success, Incomplete };

        #region Check Collision

        /// <summary>
        /// Performs an overlap test of two shapes.
        /// </summary>
        /// <param name="shapeA">The first shape.</param>
        /// <param name="shapeB">The second shape.</param>
        /// <returns>A boolean value that determines if the shapes are overlapping. </returns>
        public static bool CheckOverlap(IShape shapeA, IShape shapeB)
        {
            return CheckOverlap(shapeA, in Matrix.Identity, shapeB, in Matrix.Identity);
        }

        /// <summary>
        /// Performs an overlap test of two shapes.
        /// </summary>
        /// <param name="shapeA">The first shape.</param>
        /// <param name="matrixA">The transform of the first shape.</param>
        /// <param name="shapeB">The second shape.</param>
        /// <param name="matrixB">The transform of the second shape.</param>
        /// <returns>A boolean value that determines if the shapes are overlapping. </returns>
        public static bool CheckOverlap(IShape shapeA, in Matrix matrixA, IShape shapeB, in Matrix matrixB)
        {
            if (shapeA is null) { throw new ArgumentNullException(nameof(shapeA)); }
            if (shapeB is null) { throw new ArgumentNullException(nameof(shapeB)); }

            // Avoid check against self
            if (shapeA == shapeB) { return false; }

            // If both shapes are convex, we can use GJK
            if (shapeA.IsConvex && shapeB.IsConvex)
            {
                // Get shape proxy
                var a = new ShapeProxy(shapeA, in matrixA);
                var b = new ShapeProxy(shapeB, in matrixB);

                // Dispatch to GJK
                return EvaluateOverlap(in a, in b);
            }
            else if (shapeB.IsConvex)
            {
                // CASE: Shape A must be concave

                // Polygon vs Shape
                if (shapeA is Polygon polygonA)
                {
                    var b = new ShapeProxy(shapeB, in matrixB);

                    foreach (var convex in polygonA.ConvexPartitions)
                    {
                        var a = new ShapeProxy(convex, in matrixA);

                        if (EvaluateOverlap(in a, in b))
                        {
                            return true;
                        }
                    }

                    // No overlap detected
                    return false;
                }
            }
            else if (shapeA.IsConvex)
            {
                // CASE: Shape B must be concave

                // Shape vs Polygon
                if (shapeB is Polygon polygonB)
                {
                    var a = new ShapeProxy(shapeA, in matrixA);

                    foreach (var convex in polygonB.ConvexPartitions)
                    {
                        var b = new ShapeProxy(convex, in matrixB);

                        if (EvaluateOverlap(in a, in b))
                        {
                            return true;
                        }
                    }

                    // No overlap detected
                    return false;
                }
            }
            else
            {
                // CASE: Both shapes must concave

                // Polygon vs Polygon
                if (shapeA is Polygon polygonA && shapeB is Polygon polygonB)
                {
                    foreach (var convexA in polygonA.ConvexPartitions)
                    {
                        var a = new ShapeProxy(convexA, in matrixA);

                        foreach (var convexB in polygonB.ConvexPartitions)
                        {
                            var b = new ShapeProxy(convexB, in matrixB);

                            if (EvaluateOverlap(in a, in b))
                            {
                                return true;
                            }
                        }

                    }

                    // No overlap detected
                    return false;
                }
            }

            // Should not get here unless the user implemented IShape themselves.
            throw new InvalidOperationException("Unable to perform collision check with custom non-convex shapes.");
        }

        #endregion

        #region Check Collision (w/ Contact)

        /// <summary>
        /// Performs the static collision of two convex shapes.
        /// </summary>
        /// <param name="shapeA">The first shape.</param>
        /// <param name="shapeB">The second shape.</param>
        /// <param name="contact">
        /// The collision results, viewed from the perspective of <paramref name="shapeA"/>.
        /// Only valid if a collision has occurred.
        /// </param>
        /// <returns>A boolean value that determines if a collision has occurred. </returns>
        public static bool CheckCollision(IShape shapeA, IShape shapeB, out Contact contact)
        {
            return CheckCollision(shapeA, in Matrix.Identity, shapeB, in Matrix.Identity, out contact);
        }

        /// <summary>
        /// Performs the static collision of two convex shapes.
        /// </summary>
        /// <param name="shapeA">The first shape.</param>
        /// <param name="matrixA">The transform of the first shape.</param>
        /// <param name="shapeB">The second shape.</param>
        /// <param name="matrixB">The transform of the second shape.</param>
        /// <param name="contact">
        /// The collision results, viewed from the perspective of <paramref name="shapeA"/>.
        /// Only valid if a collision has occurred.
        /// </param>
        /// <returns>A boolean value that determines if a collision has occurred. </returns>
        public static bool CheckCollision(IShape shapeA, in Matrix matrixA, IShape shapeB, in Matrix matrixB, out Contact contact)
        {
            if (shapeA is null) { throw new ArgumentNullException(nameof(shapeA)); }
            if (shapeB is null) { throw new ArgumentNullException(nameof(shapeB)); }

            if (shapeA.IsConvex && shapeB.IsConvex)
            {
                // Both shapes are convex, we can proceed with GJK
                if (CheckOverlap(shapeA, in matrixA, shapeB, in matrixB))
                {
                    // A collision has occured, we now proceed with EPA
                    contact = ComputePenetration();
                    return true;
                }
                else
                {
                    // No collision, return default
                    contact = default;
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to compute collisions w/ contacts with non-convex shapes.");
            }
        }

        #endregion

        private readonly struct ShapeProxy
        {
            public readonly Func<Vector, Vector> GetSupport;

            public readonly Vector Center;

            #region Constructors

            public ShapeProxy(IShape shape, in Matrix matrix)
            {
                if (shape is null) { throw new ArgumentNullException(nameof(shape)); }

                if (!shape.IsConvex) { throw new ArgumentException("Shape must not be non-convex."); }

                GetSupport = LocalToWorld(shape.GetSupport, matrix);
                Center = LocalToWorld(shape.Center, matrix);
            }

            public ShapeProxy(IEnumerable<Vector> polygon, in Matrix matrix)
            {
                if (polygon is null) { throw new ArgumentNullException(nameof(polygon)); }

                GetSupport = LocalToWorld(d => PolygonTools.GetSupport(polygon, d), matrix);
                Center = LocalToWorld(polygon.Average(), matrix);
            }

            #endregion

            #region Affine Transform Wrapper (Static)

            private static Func<Vector, Vector> LocalToWorld(Func<Vector, Vector> getSupport, Matrix matrix)
            {
                if (!IsIdentity(in matrix))
                {
                    // Compute the transpose of the linear portion of the affine matrix (inverse 2x2)
                    var mTranspose = matrix;
                    Calc.Swap(ref mTranspose.M1, ref mTranspose.M3);

                    return direction =>
                    {
                        // Transform the direction vector into 'local space'
                        Matrix.MultiplyVector(in mTranspose, direction, ref direction);

                        // Get the local space support and transform back to 'world space'
                        return Matrix.Multiply(in matrix, getSupport(direction));
                    };
                }
                else
                {
                    // Identity, return incoming support function
                    return getSupport;
                }
            }

            private static Vector LocalToWorld(Vector center, Matrix matrix)
            {
                return Matrix.Multiply(in matrix, in center);
            }

            private static bool IsIdentity(in Matrix matrix)
            {
                var dia = matrix.M0 + matrix.M4; // diagonal sum to 2
                var zer = matrix.M1 + matrix.M2 + matrix.M3 + matrix.M5; // remaining sum to 0
                return Calc.NearEquals(dia, 2) && Calc.NearZero(zer);
            }

            #endregion
        }

        #region GJK/EPA Implementation

        // GJK
        private static bool EvaluateOverlap(in ShapeProxy shapeA, in ShapeProxy shapeB)
        {
            _shapeA = shapeA;
            _shapeB = shapeB;

            // Reset (or create) simplex/polygon
            if (_gjkPolygon == null) { _gjkPolygon = new List<Support>(); }
            _gjkPolygon.Clear();

            // Perform the GJK algorithm
            var result = GJKIterationResult.Incomplete;
            while (result == GJKIterationResult.Incomplete)
            {
                result = GJKIteration();
            }

            // Return success if an intersection was indeed found
            return result == GJKIterationResult.Success;

            // GJK
            static GJKIterationResult GJKIteration()
            {
                switch (_gjkPolygon.Count)
                {
                    case 0:
                    {
                        _direction = _shapeB.Center - _shapeA.Center;
                    }
                    break;

                    case 1:
                    {
                        _direction = -_direction;
                    }
                    break;

                    case 2:
                    {
                        var b = _gjkPolygon[1];
                        var c = _gjkPolygon[0];

                        // line cb is the line formed by the first two vertices
                        var cb = b.Minkowski - c.Minkowski;

                        // calculate a direction perpendicular to line cb in the direction of the origin
                        _direction = GetPerpendicular(cb, c.Minkowski);
                    }
                    break;

                    case 3:
                    {
                        // calculate if the simplex contains the origin
                        var a = _gjkPolygon[2];
                        var b = _gjkPolygon[1];
                        var c = _gjkPolygon[0];

                        var ab = b.Minkowski - a.Minkowski; // v2 to v1
                        var ac = c.Minkowski - a.Minkowski; // v2 to v0

                        var abPerp = GetPerpendicular(ab, ac);
                        var acPerp = GetPerpendicular(ac, ab);

                        // the origin is outside line ab
                        if (Vector.Dot(abPerp, a.Minkowski) < -Calc.Epsilon)
                        {
                            // get rid of c and add a new support in the direction of abPerp
                            _gjkPolygon.RemoveAt(0);
                            _direction = abPerp;
                        }
                        else
                        // the origin is outside line ac
                        if (Vector.Dot(acPerp, a.Minkowski) < -Calc.Epsilon)
                        {
                            // get rid of b and add a new support in the direction of acPerp
                            _gjkPolygon.RemoveAt(1);
                            _direction = acPerp;
                        }
                        else
                        {
                            // the origin is inside both ab and ac,
                            // so it must be inside the triangle!
                            return GJKIterationResult.Success;
                        }
                    }
                    break;

                    default:
                        throw new InvalidOperationException("Can\'t have simplex with ${vertices.length} verts!");
                }

                // 
                return AddSupportToSimplex(in _direction);
            }

            static GJKIterationResult AddSupportToSimplex(in Vector dir)
            {
                // Compute the next support point to use when evolving the simplex
                var minkowskiSupport = GetMinkowskiSupport(in dir);
                _gjkPolygon.Add(minkowskiSupport);

                // todo: explain this one to yourself...(the support is on the correct side of the origin?)
                return (Vector.Dot(in dir, in minkowskiSupport.Minkowski) > 0) ? GJKIterationResult.Incomplete
                                                                               : GJKIterationResult.Failure;
            }
        }

        // EPA
        private static Contact ComputePenetration()
        {
            // Gets the winding of the triangle simplex
            var e0 = _gjkPolygon[1].Minkowski - _gjkPolygon[0].Minkowski;
            var e1 = _gjkPolygon[2].Minkowski - _gjkPolygon[0].Minkowski;

            // Ensure simplex winding is ... anti-clockwise?
            if (Vector.Cross(e0, e1) < 0) { Calc.Swap(_gjkPolygon, 1, 2); }

            // Penetration information
            var penetration = Vector.Zero;
            var penetrationEdge = 0;

            var minDistance = float.PositiveInfinity;

            // Iteratively find closest edge
            for (var i = 0; i < _maxIterations; i++)
            {
                var edge = FindClosestEdge();

                // Get the support in the direction of the edge
                var support = GetMinkowskiSupport(edge.Normal);
                var supportDistance = Vector.Dot(support.Minkowski, edge.Normal);

                // Keep smallest penetration vector
                if (supportDistance < minDistance)
                {
                    // Store contact information
                    penetration = edge.Normal * supportDistance;
                    penetrationEdge = edge.Index;

                    minDistance = supportDistance;
                }

                // If the distance to the support is equivalent to the distance to the nearest edge.
                if (Calc.NearEquals(supportDistance, edge.Distance, _edgeThreshold))
                {
                    // We have found edge nearest edge of the minkowski difference. edge!
                    // We can now exit the loop.
                    break;
                }
                else
                {
                    _gjkPolygon.Insert(edge.Index, support);
                }
            }

            // Get contact edge supports
            var supportA = _gjkPolygon[Calc.Wrap(penetrationEdge - 1, _gjkPolygon.Count)];
            var supportB = _gjkPolygon[penetrationEdge];

            // Compute the contact points in world space
            var t = Vector.Project(supportA.Minkowski, supportB.Minkowski, penetration);
            var a = Vector.Lerp(supportA.ShapeA, supportB.ShapeA, t);
            // var b = Vector.Lerp(supportA.ShapeB, supportB.ShapeB, t);

            return new Contact(a, penetration);

            static EdgeInfo FindClosestEdge()
            {
                var edgeDistance = float.PositiveInfinity;
                var edgeNormal = Vector.Zero;
                var edgeIndex = 0;

                // For each vertex (ie, for each edge of the polygon)
                for (var i = 0; i < _gjkPolygon.Count; i++)
                {
                    // Get the far index of the edge
                    var j = (i + 1) % _gjkPolygon.Count;

                    // Computes the direction along the edge
                    var e0 = _gjkPolygon[j].Minkowski - _gjkPolygon[i].Minkowski;

                    // If the edge is non-zero in side (ie, points did not overlap)
                    if (e0.LengthSquared > Calc.Epsilon)
                    {
                        // Compute the edge normal (compensate for simplex winding order)
                        var normal = Vector.Normalize(e0.Perpendicular);

                        // Calculate the distance of the edge from the origin
                        var distance = Vector.Dot(normal, _gjkPolygon[i].Minkowski);
                        if (distance < edgeDistance)
                        {
                            edgeDistance = distance;
                            edgeNormal = normal;
                            edgeIndex = j;
                        }
                    }
                }

                return new EdgeInfo(edgeDistance, edgeNormal, edgeIndex);
            }
        }

        private static Support GetMinkowskiSupport(in Vector direction)
        {
            var a = _shapeA.GetSupport(direction);
            var b = _shapeB.GetSupport(-direction);
            return new Support(a, b);
        }

        private static Vector GetPerpendicular(Vector edge, Vector target)
        {
            var perp = edge.Perpendicular;
            return Vector.Dot(perp, target) < 0 ? perp : -perp;
        }

        private readonly struct Support
        {
            public readonly Vector ShapeA;

            public readonly Vector ShapeB;

            public readonly Vector Minkowski;

            public Support(Vector a, Vector b)
            {
                ShapeA = a;
                ShapeB = b;

                Minkowski = a - b;
            }
        }

        private readonly struct EdgeInfo
        {
            public readonly float Distance;

            public readonly Vector Normal;

            public readonly int Index;

            public EdgeInfo(float distance, Vector normal, int index)
            {
                Distance = distance;
                Normal = normal;
                Index = index;
            }
        }

        #endregion
    }
}
