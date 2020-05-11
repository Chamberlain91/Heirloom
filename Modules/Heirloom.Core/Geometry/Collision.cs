using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Heirloom.Collections;

using static Heirloom.Calc;
using static Heirloom.Vector;

namespace Heirloom.Geometry
{
    /// <summary>
    /// Collision detection routines.
    /// </summary>
    public static class Collision
    // Code here has been refactored is ultimately has just been ported to Heirloom
    // from Randy Gaul's "Impulse Engine" (http://RandyGaul.net)
    // The zlib license requires "state changes and keep the notice",
    // hopefully this counts as that notice.
    {
        private static readonly ObjectPool<Polygon> _polygonPool = new ObjectPool<Polygon>();

        /// <summary>
        /// Perform collision detection between two shapes.
        /// Both shapes are assumed to already be in the same space.
        /// </summary>
        /// <remarks>
        /// Note: <see cref="Polygon"/> are assumed to be convex.
        /// </remarks>
        /// <param name="s1">The first shape.</param>
        /// <param name="s2">The second shape.</param>
        /// <param name="result">This value is populated with collision data upon a successful collision.</param>
        /// <returns>True, if a collision was detected.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Collide(in IShape s1, in IShape s2, out CollisionData result)
        {
            switch (s1)
            {
                case Circle c1:
                {
                    switch (s2)
                    {
                        case Circle c2:
                            return Collide(c1, c2, out result);

                        case Polygon p2:
                            return Collide(c1, p2, out result);

                        case Rectangle r2:
                        {
                            // Temporarily convert rectangle into polygon
                            var p = GetTempPolygon(in r2);
                            var status = Collide(c1, p, out result);
                            _polygonPool.Recycle(p);
                            return status;
                        }

                        case Triangle t2:
                        {
                            // Temporarily convert triangle into polygon
                            var p = GetTempPolygon(in t2);
                            var status = Collide(c1, p, out result);
                            _polygonPool.Recycle(p);
                            return status;
                        }
                    }

                    break;
                }

                case Polygon p1:
                {
                    switch (s2)
                    {
                        case Circle c2:
                            return Collide(p1, c2, out result);

                        case Polygon p2:
                            return Collide(p1, p2, out result);

                        case Rectangle r2:
                        {
                            // Temporarily convert rectangle into polygon
                            var p = GetTempPolygon(in r2);
                            var status = Collide(p1, p, out result);
                            _polygonPool.Recycle(p);
                            return status;
                        }

                        case Triangle t2:
                        {
                            // Temporarily convert triangle into polygon
                            var p = GetTempPolygon(in t2);
                            var status = Collide(p1, p, out result);
                            _polygonPool.Recycle(p);
                            return status;
                        }
                    }

                    break;
                }

                case Rectangle r1:
                {
                    // Temporarily convert rectangle into polygon
                    var p = GetTempPolygon(in r1);
                    var status = Collide(p, s2, out result);
                    _polygonPool.Recycle(p);
                    return status;
                }

                case Triangle t1:
                {
                    // Temporarily convert triangle into polygon and recurse
                    var p = GetTempPolygon(in t1);
                    var status = Collide(p, s2, out result);
                    _polygonPool.Recycle(p);
                    return status;
                }
            }

            // 
            throw new InvalidOperationException($"Unable to determine collision between {s1.GetType().Name} and {s2.GetType().Name}.");
        }

        /// <summary>
        /// Perform collision detection between two circles.
        /// Both shapes are assumed to already be in the same space.
        /// </summary>
        /// <param name="c1">The first shape.</param>
        /// <param name="c2">The second shape.</param>
        /// <param name="result">This value is populated with collision data upon a successful collision.</param>
        /// <returns>True, if a collision was detected.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Collide(in Circle c1, in Circle c2, out CollisionData result)
        {
            result = CircleToCircle(in c1, in c2);
            return result.Count > 0;
        }

        /// <summary>
        /// Perform collision detection between a circle and a convex polygon.
        /// Both shapes are assumed to already be in the same space.
        /// </summary>
        /// <param name="c1">The first shape.</param>
        /// <param name="p2">The second shape.</param>
        /// <param name="result">This value is populated with collision data upon a successful collision.</param>
        /// <returns>True, if a collision was detected.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Collide(in Circle c1, Polygon p2, out CollisionData result)
        {
            result = CircleToPolygon(in c1, p2);
            return result.Count > 0;
        }

        /// <summary>
        /// Perform collision detection between a convex polygon and a circle.
        /// Both shapes are assumed to already be in the same space.
        /// </summary>
        /// <param name="p1">The first shape.</param>
        /// <param name="c2">The second shape.</param>
        /// <param name="result">This value is populated with collision data upon a successful collision.</param>
        /// <returns>True, if a collision was detected.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Collide(in Polygon p1, Circle c2, out CollisionData result)
        {
            result = PolygonToCircle(p1, in c2);
            return result.Count > 0;
        }

        /// <summary>
        /// Perform collision detection between two convex polygons.
        /// Both shapes are assumed to already be in the same space.
        /// </summary>
        /// <param name="p1">The first shape.</param>
        /// <param name="p2">The second shape.</param>
        /// <param name="result">This value is populated with collision data upon a successful collision.</param>
        /// <returns>True, if a collision was detected.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Collide(in Polygon p1, Polygon p2, out CollisionData result)
        {
            result = PolygonToPolygon(p1, p2);
            return result.Count > 0;
        }

        #region Get Temporary Polygon

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Polygon GetTempPolygon(in Triangle tri)
        {
            var polygon = _polygonPool.Request();
            polygon.Clear();
            polygon.Add(tri.A);
            polygon.Add(tri.B);
            polygon.Add(tri.C);
            return polygon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Polygon GetTempPolygon(in Rectangle rect)
        {
            var polygon = _polygonPool.Request();
            polygon.Clear();
            polygon.Add(rect.TopLeft);
            polygon.Add(rect.TopRight);
            polygon.Add(rect.BottomRight);
            polygon.Add(rect.BottomLeft);
            return polygon;
        }

        #endregion

        #region Collision Implementation

        internal static CollisionData CircleToCircle(in Circle a, in Circle b)
        {
            var m = default(CollisionData);

            // Calculate translational vector, which is normal
            var normal = b.Position - a.Position;

            var dist_sqr = normal.LengthSquared;
            var radius = a.Radius + b.Radius;

            // Not in contact
            if (dist_sqr >= radius * radius)
            {
                return m;
            }

            var distance = Sqrt(dist_sqr);

            if (distance == 0.0f)
            {
                m.Penetration = a.Radius;
                m.Normal = Right;
                m.AddContact(a.Position);
            }
            else
            {
                m.Penetration = radius - distance;
                m.Normal = normal / distance; // Faster than using Normalized since we already performed sqrt
                m.AddContact((m.Normal * a.Radius) + a.Position);
            }

            return m;
        }

        internal static CollisionData CircleToPolygon(in Circle a, Polygon b)
        {
            var m = default(CollisionData);

            // Transform circle center to Polygon model space
            var center = a.Position;

            // Find edge with minimum penetration
            // Exact concept as using support points in Polygon vs Polygon
            var separation = float.MinValue;
            var faceNormal = 0;
            for (var i = 0; i < b.Vertices.Count; ++i)
            {
                var s = Dot(b.Normals[i], center - b.Vertices[i]);

                if (s > a.Radius)
                {
                    return m;
                }

                if (s > separation)
                {
                    separation = s;
                    faceNormal = i;
                }
            }

            // Grab face's vertices
            var v1 = b.Vertices[faceNormal];
            var i2 = faceNormal + 1 < b.Vertices.Count ? faceNormal + 1 : 0;
            var v2 = b.Vertices[i2];

            // Check to see if center is within polygon
            if (separation < Epsilon)
            {
                m.Normal = -b.Normals[faceNormal];
                m.AddContact((m.Normal * a.Radius) + a.Position);
                m.Penetration = a.Radius;
                return m;
            }

            // Determine which voronoi region of the edge center of circle lies within
            var dot1 = Dot(center - v1, v2 - v1);
            var dot2 = Dot(center - v2, v1 - v2);
            m.Penetration = a.Radius - separation;

            // Closest to v1
            if (dot1 <= 0.0f)
            {
                if (DistanceSquared(center, v1) > a.Radius * a.Radius)
                {
                    return m;
                }

                var n = v1 - center;

                n.Normalize();
                m.Normal = n;
                m.AddContact(v1);
            }

            // Closest to v2
            else if (dot2 <= 0.0f)
            {
                if (DistanceSquared(center, v2) > a.Radius * a.Radius)
                {
                    return m;
                }

                m.AddContact(v2);
                m.Normal = Normalize(v2 - center);
            }
            // Closest to face
            else
            {
                var n = b.Normals[faceNormal];
                if (Dot(center - v1, n) > a.Radius)
                {
                    return m;
                }

                m.Normal = -n;
                m.AddContact((m.Normal * a.Radius) + a.Position);
            }

            return m;
        }

        internal static CollisionData PolygonToCircle(Polygon a, in Circle b)
        {
            var m = CircleToPolygon(in b, a);
            m.Normal = -m.Normal;
            return m;
        }

        internal static CollisionData PolygonToPolygon(Polygon a, Polygon b)
        {
            var m = default(CollisionData);

            // Check for a separating axis with A's face planes
            var penetrationA = FindAxisLeastPenetration(out var faceA, a, b);
            if (penetrationA >= 0.0f)
            {
                return m;
            }

            // Check for a separating axis with B's face planes
            var penetrationB = FindAxisLeastPenetration(out var faceB, b, a);
            if (penetrationB >= 0.0f)
            {
                return m;
            }

            int referenceIndex;
            bool flip; // Always point from a to b

            Polygon refPoly; // Reference
            Polygon IncPoly; // Incident

            // Determine which shape contains reference face
            if (penetrationA > penetrationB)
            {
                refPoly = a;
                IncPoly = b;
                referenceIndex = faceA;
                flip = false;
            }
            else
            {
                refPoly = b;
                IncPoly = a;
                referenceIndex = faceB;
                flip = true;
            }

            // World space incident face
            Span<Vector> incidentFace = stackalloc Vector[2];
            FindIncidentFace(incidentFace, refPoly, IncPoly, referenceIndex);

            //        y
            //        ^  .n       ^
            //      +---c ------posPlane--
            //  x < | i |\
            //      +---+ c-----negPlane--
            //             \       v
            //              r
            //
            //  r : reference face
            //  i : incident poly
            //  c : clipped point
            //  n : incident normal

            // Setup reference face vertices
            var v1 = refPoly.Vertices[referenceIndex];
            referenceIndex = referenceIndex + 1 == refPoly.Vertices.Count ? 0 : referenceIndex + 1;
            var v2 = refPoly.Vertices[referenceIndex];

            // Calculate reference face side normal in world space
            var sidePlaneNormal = Normalize(v2 - v1);

            // Orthogonalize
            var refFaceNormal = new Vector(sidePlaneNormal.Y, -sidePlaneNormal.X);

            // ax + by = c
            // c is distance from origin
            var refC = Dot(refFaceNormal, v1);
            var negSide = -Dot(sidePlaneNormal, v1);
            var posSide = Dot(sidePlaneNormal, v2);

            // Clip incident face to reference face side planes
            if (Clip(-sidePlaneNormal, negSide, incidentFace) < 2)
            {
                return m; // Due to floating point error, possible to not have required points
            }

            if (Clip(sidePlaneNormal, posSide, incidentFace) < 2)
            {
                return m; // Due to floating point error, possible to not have required points
            }

            // Flip
            m.Normal = flip ? -refFaceNormal : refFaceNormal;

            // Keep points behind reference face
            var cp = 0; // clipped points behind reference face
            var separation = Dot(refFaceNormal, incidentFace[0]) - refC;

            if (separation <= 0.0f)
            {
                m.AddContact(incidentFace[0]);
                m.Penetration = -separation;
                ++cp;
            }
            else
            {
                m.Penetration = 0;
            }

            separation = Dot(refFaceNormal, incidentFace[1]) - refC;
            if (separation <= 0.0f)
            {
                m.AddContact(incidentFace[1]);
                m.Penetration += -separation;
                ++cp;

                // Average penetration
                m.Penetration /= cp;
            }

            return m;
        }

        private static float FindAxisLeastPenetration(out int faceIndex, Polygon a, Polygon b)
        {
            var bestDistance = float.MinValue;
            var bestIndex = -1;

            for (var i = 0; i < a.Vertices.Count; i++)
            {
                // Retrieve a face normal from A
                var n = a.Normals[i];

                // Retrieve support point from B along -n
                var s = GetSupport(b, -n);

                // Compute penetration distance
                var d = Dot(n, s - a.Vertices[i]);

                // Store greatest distance
                if (d > bestDistance)
                {
                    bestDistance = d;
                    bestIndex = i;
                }
            }

            faceIndex = bestIndex;
            return bestDistance;
        }

        // The extreme point along a direction within a polygon
        private static Vector GetSupport(Polygon p, Vector dir)
        {
            var bestProjection = float.MinValue;
            Vector bestVertex = default;

            for (var i = 0; i < p.Vertices.Count; ++i)
            {
                var v = p.Vertices[i];
                var projection = Dot(v, dir);

                if (projection > bestProjection)
                {
                    bestVertex = v;
                    bestProjection = projection;
                }
            }

            return bestVertex;
        }

        private static void FindIncidentFace(Span<Vector> v, Polygon refPoly, Polygon incPoly, int referenceIndex)
        {
            var referenceNormal = refPoly.Normals[referenceIndex];

            // Find most anti-normal face on incident polygon
            var incidentFace = 0;
            var minDot = float.MaxValue;
            for (var i = 0; i < incPoly.Vertices.Count; ++i)
            {
                var dot = Dot(referenceNormal, incPoly.Normals[i]);
                if (dot < minDot)
                {
                    minDot = dot;
                    incidentFace = i;
                }
            }

            // Assign face vertices for incidentFace
            v[0] = incPoly.Vertices[incidentFace];
            incidentFace = incidentFace + 1 >= incPoly.Vertices.Count ? 0 : incidentFace + 1;
            v[1] = incPoly.Vertices[incidentFace];
        }

        private static int Clip(Vector n, float c, Span<Vector> face)
        {
            var sp = 0;
            Span<Vector> @out = stackalloc Vector[2] { face[0], face[1] };

            // Retrieve distances from each endpoint to the line
            // d = ax + by - c
            var d1 = Dot(n, face[0]) - c;
            var d2 = Dot(n, face[1]) - c;

            // If negative (behind plane) clip
            if (d1 <= 0.0f) { @out[sp++] = face[0]; }
            if (d2 <= 0.0f) { @out[sp++] = face[1]; }

            // If the points are on different sides of the plane
            if (d1 * d2 < 0.0f) // less than to ignore -0.0f
            {
                // Push interesection point
                var alpha = d1 / (d1 - d2);
                @out[sp] = face[0] + alpha * (face[1] - face[0]);
                ++sp;
            }

            // Assign our new converted values
            face[0] = @out[0];
            face[1] = @out[1];

            Debug.Assert(sp != 3);

            return sp;
        }

        #endregion
    }
}
