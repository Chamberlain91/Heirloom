using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class BatchingTechnique
    {
        public abstract bool IsDirty { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal abstract bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal abstract void DrawBatch();
    }
}
