using System;
using System.Diagnostics.CodeAnalysis;

using Heirloom;

namespace Examples.Depth
{
    public abstract class Entity : IComparable<Entity>
    {
        public int Depth { get; set; }

        public Vector Position { get; set; }

        public GameContext Game { get; internal set; }

        internal abstract void Update(float dt);

        internal abstract void Draw(GraphicsContext gfx, float dt);

        public int CompareTo([AllowNull] Entity other)
        {
            return Depth.CompareTo(other?.Depth);
        }
    }
}
