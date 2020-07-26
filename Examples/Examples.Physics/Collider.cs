using Heirloom;

namespace Examples.Physics
{
    public abstract class Collider
    {
        protected readonly Color Color = GenerateColor();

        private RigidBody _body;

        private static Color GenerateColor()
        {
            var h = Calc.Random.Next(0, 360);
            return Color.FromHSV(h, 0.66F, 0.8F);
        }

        internal Collider()
        {
            // Internal here to prevent inheritance outside assembly
        }

        internal void RegisterBody(RigidBody body)
        {
            _body = body;
        }

        internal Matrix Matrix => Matrix.CreateTransform(_body.Position, _body.Rotation, _body.Scale);

        internal abstract IShape Shape { get; }

        internal abstract void ComputeMassData(RigidBody body);

        internal abstract void Draw(GraphicsContext gfx);
    }
}
