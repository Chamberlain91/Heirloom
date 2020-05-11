using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Physics
{
    public static class Collisions
    // ref: https://www.randygaul.net/2013/07/16/separating-axis-test-and-support-points-in-2d/
    // ref: https://github.com/RandyGaul/cute_headers cute_c2.h
    {
        /**
         * The fundamental checks are:
         *  - circ to poly
         *  - poly to poly
         *  
         * Triangles and rectangles are also convex polygons.
         */

        #region Collision (Rectangle to Rectangle)

        public static bool CheckCollision(this in Rectangle a, in Rectangle b, out Manifold manifold)
        {
            var polyA = a.GetTempPolygon(0);
            var polyB = b.GetTempPolygon(1);

            return CheckCollision(polyA, polyB, out manifold);
        }

        #endregion

        #region Collision (Rectangle to Circle, Implementation)

        public static bool CheckCollision(this in Rectangle a, in Circle b, out Manifold manifold)
        {
            var poly = a.GetTempPolygon(0);
            return CheckCollision(b, poly, out manifold);
        }

        #endregion

        #region Collision (Rectangle to Polygon, Implementation)

        public static bool CheckCollision(this in Rectangle a, IReadOnlyList<Vector> b, out Manifold manifold)
        {
            var poly = a.GetTempPolygon(0);
            return CheckCollision(poly, b, out manifold);
        }

        #endregion

        #region Collision (Circle to Rectangle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckCollision(this in Circle a, in Rectangle b, out Manifold manifold)
        {
            var s = CheckCollision(b, a, out manifold);
            manifold.Normal *= -1;
            return s;
        }

        #endregion

        #region Collision (Circle to Circle, Implementation)

        public static bool CheckCollision(this in Circle a, in Circle b, out Manifold manifold)
        {
            manifold = default;
            manifold.Count = 0;

            var d = b.Position - a.Position;
            var d2 = Vector.Dot(d, d);
            var r = a.Radius + b.Radius;

            if (d2 < r * r)
            {
                var l = Calc.Sqrt(d2);
                var n = Calc.NearZero(l) ? Vector.Up : d * (1.0F / l);
                var c = b.Position - (n * b.Radius);

                manifold.Count = 1;
                manifold.SetContact(0, c, l - r);
                manifold.Normal = n;
            }

            return manifold.Count > 0;
        }

        #endregion

        #region Collision (Circle to Polygon, Implementation)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Overlaps(this in Circle a, IReadOnlyList<Vector> b)
        {
            return FindSeparation(b, a, out _) < 0;
        }

        public static bool CheckCollision(this in Circle a, IReadOnlyList<Vector> b, out Manifold manifold)
        {
            manifold = default;

            var sep = FindSeparation(b, a, out var pC);
            if (sep < 0)
            {
                // Contact normal
                var cN = Vector.Normalize(pC - a.Position);
                if (PolygonTools.ContainsPoint(b, a.Position)) { cN *= -1F; }

                // Contact point
                var cC = a.Position - (cN * a.Radius);

                manifold.SetContact(0, cC, sep);
                manifold.Normal = cN;
                manifold.Count = 1;
            }

            // 
            return manifold.Count > 0;
        }

        #endregion

        #region Collision (Polygon to Rectangle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckCollision(this IReadOnlyList<Vector> a, in Rectangle b, out Manifold manifold)
        {
            var s = CheckCollision(b, a, out manifold);
            manifold.Normal *= -1;
            return s;
        }

        #endregion

        #region Collision (Polygon to Circle)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckCollision(this IReadOnlyList<Vector> a, in Circle b, out Manifold manifold)
        {
            var hit = CheckCollision(b, a, out manifold);
            manifold.Normal *= -1;
            return hit;
        }

        #endregion

        #region Collision (Polygon to Polygon)

        /// <summary>
        /// Checks for a collision (overlap) of a convex polygon to another convex polygon.
        /// </summary>
        public static bool Overlaps(this IReadOnlyList<Vector> a, IReadOnlyList<Vector> b)
        {
            // If a positive separation is found, the polygons are separated
            if (FindSeparation(a, b, out _) > 0.0F) { return false; }
            if (FindSeparation(b, a, out _) > 0.0F) { return false; }

            return true;
        }

        /// <summary>
        /// Checks for a collision (overlap) of a convex polygon to another convex polygon and computes the contact points.
        /// </summary>
        public static unsafe bool CheckCollision(this IReadOnlyList<Vector> a, IReadOnlyList<Vector> b, out Manifold manifold)
        {
            // Find separation of A w/ respect to B and vice versa
            var sepA = FindSeparation(a, b, out var edgeA);
            var sepB = FindSeparation(b, a, out var edgeB);

            const float kRelTol = 0.95f, kAbsTol = 0.01f;

            // r - reference, i - incident
            var rInfo = b;
            var iInfo = a;
            var rEdge = edgeB;
            var flip = true;

            if (sepA * kRelTol > sepB + kAbsTol)
            {
                rInfo = a;
                iInfo = b;
                rEdge = edgeA;
                flip = false;
            }

            unsafe
            {
                var incident = stackalloc Vector[2];
                IncidentEdge(incident, iInfo, rInfo, rEdge);

                // 
                if (!SidePlanes(incident, in rInfo, rEdge, out var rh))
                {
                    manifold = default;
                    return false;
                }

                // 
                KeepDeep(incident, in rh, out manifold);

                // 
                if (flip)
                {
                    manifold.Normal = -manifold.Normal;
                }

                return manifold.Count > 0;
            }
        }

        #endregion

        #region Collision Utility (Poly to Circle)

        private static float FindSeparation(IReadOnlyList<Vector> a, Circle circle, out Vector edgePoint)
        {
            var bestSeparation = float.MaxValue;
            var bestPoint = default(Vector);

            // For each edge on the polygon
            for (var i = 0; i < a.Count; i++)
            {
                // Get polygon vert/norm in world space
                var p0 = a[i];
                var p1 = a[i + 1 == a.Count ? 0 : i + 1];

                var o = circle.Position - p0;
                var e = p1 - p0;

                // 
                var s = Vector.Dot(o, e);
                var t = Calc.Clamp(s / Vector.Dot(e, e), 0F, 1F);

                var point = p0 + (e * t); // point on edge nearest to circle center

                // 
                var separation = Vector.Distance(point, circle.Position) - circle.Radius;
                if (separation < bestSeparation)
                {
                    bestSeparation = separation;
                    bestPoint = point;
                }
            }

            // 
            edgePoint = bestPoint;
            return bestSeparation;
        }

        #endregion

        #region Collision Utility (Polygon to Polygon)

        private static float FindSeparation(IReadOnlyList<Vector> a, IReadOnlyList<Vector> b, out int edgeIndex)
        // ref: c2CheckFaces
        {
            var sep = -float.MaxValue;
            var index = ~0;

            for (var i = 0; i < a.Count; ++i)
            {
                var h = Halfspace.PlaneAt(a, i);
                var s = b[Support(b, -h.Normal)];

                var d = Halfspace.Distance(h, s);
                if (d > sep)
                {
                    sep = d;
                    index = i;
                }
            }

            edgeIndex = index;
            return sep;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Support(IReadOnlyList<Vector> polygon, Vector direction)
        {
            var imax = 0;
            var dmax = Vector.Dot(polygon[0], direction);

            // 
            for (var i = 1; i < polygon.Count; i++)
            {
                // 
                var d = Vector.Dot(polygon[i], direction);

                // 
                if (d > dmax)
                {
                    imax = i;
                    dmax = d;
                }
            }

            return imax;
        }

        private static unsafe void IncidentEdge(Vector* incident, IReadOnlyList<Vector> iPoly, IReadOnlyList<Vector> rPoly, int rEdge)
        {
            var iNormal = PolygonTools.GetScaledNormal(rPoly, rEdge);

            var index = ~0;
            var min_dot = float.MaxValue;
            for (var i = 0; i < iPoly.Count; i++)
            {
                var dot = Vector.Dot(iNormal, PolygonTools.GetScaledNormal(iPoly, i));

                if (dot < min_dot)
                {
                    min_dot = dot;
                    index = i;
                }
            }

            // 
            incident[0] = iPoly[index + 0];
            incident[1] = iPoly[index + 1 == iPoly.Count ? 0 : index + 1];
        }

        // clip a segment to the "side planes" of another segment.
        // side planes are planes orthogonal to a segment and attached to the
        // endpoints of the segment
        private static unsafe bool SidePlanes(Vector* seg, in IReadOnlyList<Vector> r, int e, out Halfspace h)
        {
            // end point of edge `e`
            var ra = r[e + 0];
            var rb = r[e + 1 == r.Count ? 0 : e + 1];

            var n = Vector.Normalize(rb - ra);
            var left = new Halfspace { Normal = -n, D = Vector.Dot(-n, ra) };
            var right = new Halfspace { Normal = n, D = Vector.Dot(n, rb) };

            h = default;

            if (Clip(seg, left) < 2) { return false; }
            if (Clip(seg, right) < 2) { return false; }

            h.Normal = n.Perpendicular;
            h.D = Vector.Dot(h.Normal, ra);
            return true;
        }

        // clip a segment to a plane
        private static unsafe int Clip(Vector* seg, Halfspace h)
        {
            var @out = stackalloc Vector[2];

            var sp = 0;
            float d0, d1;

            if ((d0 = Halfspace.Distance(h, seg[0])) < 0) { @out[sp++] = seg[0]; }
            if ((d1 = Halfspace.Distance(h, seg[1])) < 0) { @out[sp++] = seg[1]; }

            if (d0 == 0 && d1 == 0)
            {
                @out[sp++] = seg[0];
                @out[sp++] = seg[1];
            }
            else if (d0 * d1 <= 0)
            {
                @out[sp++] = Halfspace.Intersect(seg[0], seg[1], d0, d1);
            }

            seg[0] = @out[0];
            seg[1] = @out[1];
            return sp;
        }

        private static unsafe void KeepDeep(Vector* seg, in Halfspace h, out Manifold manifold)
        {
            manifold = default;

            var count = 0;
            for (var i = 0; i < 2; ++i)
            {
                var p = seg[i];
                var d = Halfspace.Distance(h, p);

                if (d <= 0)
                {
                    manifold.SetContact(count, p, d);
                    count++;
                }
            }

            manifold.Normal = h.Normal;
            manifold.Count = count;
        }

        private struct Halfspace
        {
            public Vector Normal;

            public float D;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Halfspace h, Vector p)
            {
                return Vector.Dot(h.Normal, p) - h.D;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector Intersect(Vector a, Vector b, float da, float db)
            {
                // return c2Add(a, c2Mulvs(c2Sub(b, a), (da / (da - db))));
                return a + ((b - a) * (da / (da - db)));
            }

            public static Halfspace PlaneAt(IReadOnlyList<Vector> p, int i)
            {
                Halfspace h;
                h.Normal = PolygonTools.GetNormal(p, i);
                h.D = Vector.Dot(h.Normal, p[i]);
                return h;
            }
        }

        #endregion 
    }
}
