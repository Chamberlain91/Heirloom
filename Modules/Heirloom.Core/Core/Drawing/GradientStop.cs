using System;

namespace Heirloom
{
    /// <summary>
    /// Represents a specific gradient stop (color key).
    /// </summary>
    public readonly struct GradientStop
    {
        /// <summary>
        /// The normalized time of this stop.
        /// </summary>
        public readonly float Time;

        /// <summary>
        /// The color of this stop.
        /// </summary>
        public readonly Color Color;

        internal GradientStop(float time, Color color)
        {
            Time = time;
            Color = color;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is GradientStop other &&
                   Time == other.Time &&
                   Color.Equals(other.Color);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Time, Color);
        }

        /// <inheritdoc/>
        public void Deconstruct(out float time, out Color color)
        {
            time = Time;
            color = Color;
        }
    }
}
