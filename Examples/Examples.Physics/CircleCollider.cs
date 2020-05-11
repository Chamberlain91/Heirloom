using Heirloom;
using Heirloom.Geometry;

using static Heirloom.Calc;

namespace Examples.Physics
{
    public sealed class CircleCollider : Collider
    {
        private readonly Circle _circle;
        private Circle _worldCircle;

        internal CircleCollider(Circle circle)
        {
            _circle = circle;
        }

        internal override IShape WorldShape => _worldCircle;

        internal override void ComputeMassData(RigidBody body)
        {
            body.Mass = Pi * _circle.Radius * _circle.Radius * body.Density;
            body.InverseMass = body.IsStatic ? 0F : 1F / body.Mass;
            body.Inertia = body.Mass * _circle.Radius * _circle.Radius;
            body.InverseIntertia = body.IsStatic ? 0F : 1F / body.Inertia;
        }

        internal override void UpdateWorldShape(RigidBody body)
        {
            _worldCircle = new Circle(body.Position + _circle.Position, _circle.Radius);
        }

        internal override void Draw(GraphicsContext gfx)
        {
            gfx.Color = Color;
            gfx.DrawCircle(_worldCircle);
        }
    }
}
