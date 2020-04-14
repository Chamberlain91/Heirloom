using System;
using System.Diagnostics.CodeAnalysis;

namespace Examples.Depth
{
    public sealed class EffectLayer : IComparable<EffectLayer>
    {
        public readonly int Depth;
         
        public readonly SurfaceEffect Effect;
         
        public EffectLayer(int depth, SurfaceEffect effect)
        {
            Effect = effect ?? throw new ArgumentNullException(nameof(effect)); 
            Depth = depth;
        }

        public int CompareTo([AllowNull] EffectLayer other)
        {
            return Depth.CompareTo(other?.Depth);
        }
    }
}
