using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    internal struct GraphicsState : IEquatable<GraphicsState>
    {
        public Surface Surface;

        public Shader Shader;

        public Rectangle Viewport;

        public Matrix Transform;

        public InterpolationMode Interpolation;

        public Blending Blending;

        public Color Color;

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is GraphicsState state && Equals(state);
        }

        public bool Equals(GraphicsState other)
        {
            return Surface == other.Surface &&
                   Interpolation == other.Interpolation &&
                   Blending == other.Blending &&
                   Viewport.Equals(other.Viewport) &&
                   Transform.Equals(other.Transform) &&
                   Color.Equals(other.Color);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surface, Interpolation, Viewport, Transform, Blending, Color);
        }

        public static bool operator ==(GraphicsState left, GraphicsState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GraphicsState left, GraphicsState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
