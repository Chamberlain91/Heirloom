using System;

using Heirloom;

using static Heirloom.Vector;

namespace Examples.Physics
{
    public sealed class PolygonCollider : Collider
    {
        private readonly Polygon _localPolygon;
        private readonly Polygon _worldPolygon;

        #region Constructors

        internal PolygonCollider(Triangle triangle)
            : this(new Polygon())
        {
            UpdatePolygon(_localPolygon, triangle, Matrix.Identity);
        }

        internal PolygonCollider(Rectangle rectangle)
            : this(new Polygon())
        {
            UpdatePolygon(_localPolygon, rectangle, Matrix.Identity);
        }

        internal PolygonCollider(Polygon polygon)
        {
            // Set template
            _localPolygon = polygon ?? throw new ArgumentNullException(nameof(polygon));
            if (_localPolygon.Vertices.Count > 0 && !_localPolygon.IsConvex)
            {
                throw new ArgumentException($"Collider polygon must be convex.", nameof(polygon));
            }

            _worldPolygon = new Polygon();
        }

        #endregion

        #region Properties

        internal override IShape WorldShape => _worldPolygon;

        #endregion

        internal override void ComputeMassData(RigidBody body)
        {
            var I = 0F;
            for (var i1 = 0; i1 < _localPolygon.Vertices.Count; i1++)
            {
                var p1 = _localPolygon.Vertices[i1];
                var p2 = _localPolygon.Vertices[(i1 + 1 < _localPolygon.Vertices.Count) ? i1 + 1 : 0];
                I += 1 / 12F * Cross(p1, p2) * (Dot(p1, p1) + Dot(p2, p2) + Dot(p1, p2));
            }

            body.Mass = body.Density * _localPolygon.Area;
            body.InverseMass = (body.Mass > 0) ? 1.0f / body.Mass : 0.0f;
            body.Inertia = I * body.Density;
            body.InverseIntertia = (body.Inertia > 0) ? 1.0f / body.Inertia : 0.0f;
        }

        internal override void UpdateWorldShape(RigidBody body)
        {
            var matrix = Matrix.CreateTransform(body.Position, body.Rotation, 1F);
            UpdatePolygon(_worldPolygon, _localPolygon, matrix);
        }

        internal override void Draw(GraphicsContext gfx)
        {
            gfx.Color = Color;
            gfx.DrawPolygonOutline(_worldPolygon);
        }

        #region Update Polygon

        private static void UpdatePolygon(Polygon polygon, Polygon templatePolygon, in Matrix matrix)
        {
            polygon.Clear();
            foreach (var v in templatePolygon.Vertices)
            {
                polygon.Add(matrix * v);
            }
        }

        private static void UpdatePolygon(Polygon polygon, Triangle templateTriangle, in Matrix matrix)
        {
            polygon.Clear();
            polygon.Add(matrix * templateTriangle.A);
            polygon.Add(matrix * templateTriangle.B);
            polygon.Add(matrix * templateTriangle.C);
        }

        private static void UpdatePolygon(Polygon polygon, Rectangle templateRectangle, in Matrix matrix)
        {
            polygon.Clear();
            polygon.Add(matrix * templateRectangle.TopLeft);
            polygon.Add(matrix * templateRectangle.TopRight);
            polygon.Add(matrix * templateRectangle.BottomRight);
            polygon.Add(matrix * templateRectangle.BottomLeft);
        }

        #endregion
    }
}
