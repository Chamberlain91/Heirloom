using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class BatchingTechnique
    {
        public abstract bool IsDirty { get; }

        public int BatchCount { get; protected set; }

        public int DrawCount { get; protected set; }

        public int TriCount { get; protected set; }

        internal virtual void ResetCounts()
        {
            BatchCount = 0;
            DrawCount = 0;
            TriCount = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal abstract bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal abstract void DrawBatch();
    }
}
