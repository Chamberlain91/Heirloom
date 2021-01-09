using Heirloom;

using static Heirloom.Calc;

namespace Examples.Physics
{
    public sealed class CircleCollider : Collider
    {
        private readonly Circle _circle;

        internal CircleCollider(Circle circle)
        {
            _circle = circle;
        }

        internal override IShape Shape => _circle;

        internal override void ComputeMassData(RigidBody body)
        {
            body.Mass = Pi * _circle.Radius * _circle.Radius * body.Density;
            body.InverseMass = body.IsStatic ? 0F : 1F / body.Mass;
            body.Inertia = body.Mass * _circle.Radius * _circle.Radius;
            body.InverseIntertia = body.IsStatic ? 0F : 1F / body.Inertia;
        }

        internal override void Draw(GraphicsContext gfx)
        {
            gfx.PushState();
            {
                gfx.Color = Color;
                gfx.GlobalTransform = Matrix;
                gfx.DrawCircleOutline(in _circle);
            }
            gfx.PopState();
        }
    }
}
