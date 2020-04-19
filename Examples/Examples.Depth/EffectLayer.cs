using System;
using System.Diagnostics.CodeAnalysis;

using Heirloom.Drawing;

namespace Examples.Depth
{
    public sealed class EffectLayer : IComparable<EffectLayer>
    {
        public readonly int Depth;

        public readonly SurfaceEffect Effect;

        public readonly int Downscale;

        public EffectLayer(int depth, SurfaceEffect effect, int downscale = 1)
        {
            if (downscale <= 0) { throw new ArgumentException("Downscale must be larger than zero."); }
            Effect = effect ?? throw new ArgumentNullException(nameof(effect));
            Downscale = downscale;
            Depth = depth;
        }

        public int CompareTo([AllowNull] EffectLayer other)
        {
            return Depth.CompareTo(other?.Depth);
        }
    }
}
