using Meadows.Mathematics;

using System;

namespace Meadows.Drawing
{
    public abstract class DrawBuffer : IDrawCommands
    {
        public abstract void Clear(Color color);

        public abstract void Draw<X>(Vertex[] vertices, in Matrix matrix, in Material<X> material) where X : unmanaged;

        public abstract void Draw<X>(Vertex[] vertices, in ReadOnlySpan<Matrix> matrices, in Material<X> material) where X : unmanaged;

        public abstract void ClearMask();

        public abstract void BeginMask();

        public abstract void EndMask();

        // Optimize will, merge sequences of triangles with equivalent state.
        // Think about a tilemap being generated with a buffer, drawing one tile at a time.
        // Every tile with the same material, but different transforms. Optimize could bake the tranforms into the triangles,
        // and merge into one draw call. Allowing speedy reuse of the draw buffer with a more computationally expensive preprocessing step.
        public abstract void Optimize();

        // ...?
        public abstract void ClearQueue();
    }
}
