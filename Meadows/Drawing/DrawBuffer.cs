using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract class DrawBuffer : IDrawContext
    {
        public abstract InterpolationMode Interpolation { get; set; }

        public abstract BlendingMode Blending { get; set; }

        public abstract Shader Shader { get; set; }

        public abstract void Reset();

        #region Stencil

        public abstract void ClearMask();

        public abstract void BeginMask();

        public abstract void EndMask();

        #endregion

        #region Draw

        public abstract void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in ReadOnlySpan<Matrix> matrices);

        #endregion

        // Optimize will, merge sequences of triangles with equivalent state.
        // Think about a tilemap being generated with a buffer, drawing one tile at a time.
        // Every tile with the same material, but different transforms. Optimize could bake the tranforms into the triangles,
        // and merge into one draw call. Allowing speedy reuse of the draw buffer with a more computationally expensive preprocessing step.
        public abstract void Optimize();
    }
}
