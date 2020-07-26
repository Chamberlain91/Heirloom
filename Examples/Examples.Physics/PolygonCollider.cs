using System;

using Heirloom;

using static Heirloom.Vector;

namespace Examples.Physics
{
    public sealed class PolygonCollider : Collider
    {
        private readonly Polygon _polygon;

        #region Constructors

        internal PolygonCollider(Triangle triangle)
            : this(triangle.ToPolygon())
        { }

        internal PolygonCollider(Rectangle rectangle)
            : this(rectangle.ToPolygon())
        { }

        internal PolygonCollider(Polygon polygon)
        {
            // Set template
            _polygon = polygon ?? throw new ArgumentNullException(nameof(polygon));
            if (_polygon.Vertices.Count > 0 && !_polygon.IsConvex)
            {
                throw new ArgumentException($"Collider polygon must be convex.", nameof(polygon));
            }
        }

        #endregion

        #region Properties

        internal override IShape Shape => _polygon;

        #endregion

        internal override void ComputeMassData(RigidBody body)
        {
            var I = 0F;
            for (var i1 = 0; i1 < _polygon.Vertices.Count; i1++)
            {
                var p1 = _polygon.Vertices[i1];
                var p2 = _polygon.Vertices[(i1 + 1 < _polygon.Vertices.Count) ? i1 + 1 : 0];
                I += 1 / 12F * Cross(p1, p2) * (Dot(p1, p1) + Dot(p2, p2) + Dot(p1, p2));
            }

            body.Mass = body.Density * _polygon.Area;
            body.InverseMass = (body.Mass > 0) ? 1.0f / body.Mass : 0.0f;
            body.Inertia = I * body.Density;
            body.InverseIntertia = (body.Inertia > 0) ? 1.0f / body.Inertia : 0.0f;
        }

        internal override void Draw(GraphicsContext gfx)
        {
            gfx.PushState();
            {
                gfx.Color = Color;
                gfx.GlobalTransform = Matrix;
                gfx.DrawPolygonOutline(_polygon);
            }
            gfx.PopState();
        }
    }
}
