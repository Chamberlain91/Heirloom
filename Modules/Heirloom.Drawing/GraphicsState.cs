using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public struct GraphicsState : IEquatable<GraphicsState>
    {
        public Surface Surface;

        public Rectangle Viewport;

        public Matrix Transform;

        public Blending Blending;

        public Color Color;

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is GraphicsState state && Equals(state);
        }

        public bool Equals(GraphicsState other)
        {
            return EqualityComparer<Surface>.Default.Equals(Surface, other.Surface) &&
                   Viewport.Equals(other.Viewport) &&
                   EqualityComparer<Matrix>.Default.Equals(Transform, other.Transform) &&
                   Blending == other.Blending &&
                   Color.Equals(other.Color);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surface, Viewport, Transform, Blending, Color);
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
