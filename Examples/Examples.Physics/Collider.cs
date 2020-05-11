using System;

using Heirloom;
using Heirloom.Geometry;

namespace Examples.Physics
{
    public abstract class Collider
    {
        protected readonly Color Color = GenerateColor();

        private static Color GenerateColor()
        {
            var h = Calc.Random.Next(0, 360);
            return Color.FromHSV(h, 0.66F, 0.8F);
        }

        internal Collider()
        {
            // Internal here to prevent inheritance outside assembly
        }

        internal abstract IShape WorldShape { get; }

        internal abstract void ComputeMassData(RigidBody body);

        internal abstract void UpdateWorldShape(RigidBody body);

        internal abstract void Draw(GraphicsContext gfx);
    }
}
