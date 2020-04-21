using System;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    internal struct GraphicsState : IEquatable<GraphicsState>
    {
        public Surface Surface;

        public Shader Shader;

        public IntRectangle Viewport;

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
                   Shader == other.Shader &&
                   Viewport == other.Viewport &&
                   Transform == other.Transform &&
                   Interpolation == other.Interpolation &&
                   Blending == other.Blending &&
                   Color == other.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surface, Shader, Viewport, Transform, Interpolation, Blending, Color);
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
