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

        [ThreadStatic] private static IShape _shapeA;
        [ThreadStatic] private static IShape _shapeB;

        [ThreadStatic] private static List<Support> _gjkPolygon;
        [ThreadStatic] private static Vector _direction;

        private enum GJKIterationResult { Failure, Success, Incomplete };

        #region Check Collision

        /// <summary>
        /// Performs an overlap test of two shapes.
        /// </summary>
        /// <param name="shapeA">The first shape.</param>
        /// <param name="shapeB">The secodn shape.</param>
        /// <returns>True if the shapes are overlapping.</returns>
        public static bool CheckCollision(IShape shapeA, IShape shapeB)
        {
            // Store the collision shapes
            _shapeA = shapeA ?? throw new ArgumentNullException(nameof(shapeA));
            _shapeB = shapeB ?? throw new ArgumentNullException(nameof(shapeB));

            // If both shapes are convex, we can use GJK
            if (shapeA.IsConvex && shapeB.IsConvex)
            {
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
            }
            else
            {
                // Dispatch to a non-convex collision method
                return CheckCollisionNonGJK(shapeA, shapeB);
            }

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

        private static bool CheckCollisionNonGJK(IShape shapeA, IShape shapeB)
        {
            // This function should only be called when shapeA and/or shapeB is non-convex.
            // If both are convex, it should have been handled by the GJK implementation.

            // todo: defer/refactor instance overlap methods to alternative static methods

            if (_shapeB.IsConvex)
            {
                // Shape A must be concave
                if (_shapeA is Polygon polygonA)
                {
                    // Polygon vs Shape
                    return polygonA.Overlaps(shapeB);
                }
            }
            else
            if (_shapeA.IsConvex)
            {
                // Shape B must be concave
                if (_shapeB is Polygon polygonB)
                {
                    // Shape vs Polygon
                    return polygonB.Overlaps(shapeA);
                }
            }
            else
            {
                // Both shapes must concave
                if (_shapeA is Polygon polygonA && _shapeB is Polygon polygonB)
                {
                    // Polygon vs Polygon
                    return polygonA.Overlaps(polygonB);
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
        /// <param name="shapeB">The secodn shape.</param>
        /// <param name="contact">
        /// The collision results, viewed from the perspective of <paramref name="shapeA"/>.
        /// Only valid if a collision has occurred.
        /// </param>
        /// <returns>True if the shapes are overlapping.</returns>
        public static bool CheckCollision(IShape shapeA, IShape shapeB, out Contact contact)
        {
            if (shapeA is null) { throw new ArgumentNullException(nameof(shapeA)); }
            if (shapeB is null) { throw new ArgumentNullException(nameof(shapeB)); }

            if (shapeA.IsConvex && shapeB.IsConvex)
            {
                // Both shapes are convex, we can proceed with GJK
                if (CheckCollision(shapeA, shapeB))
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

            // EPA
            static Contact ComputePenetration()
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
            }

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

        #endregion

        #region GJK/EPA Utilities

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
