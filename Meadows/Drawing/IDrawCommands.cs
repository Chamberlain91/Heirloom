using Meadows.Mathematics;

using System;

namespace Meadows.Drawing
{
    public interface IDrawCommands
    {
        // clear color buffer
        void Clear(Color color);

        // single mesh
        void Draw<X>(Vertex[] vertices, in Matrix matrix, in Material<X> material) where X : unmanaged;

        // instanced mesh
        void Draw<X>(Vertex[] vertices, in ReadOnlySpan<Matrix> matrices, in Material<X> material) where X : unmanaged;

        // clear stencil buffer
        void ClearMask();

        // colmask=0, stencil replace ref+1
        void BeginMask();

        // colmask=all, stencil equal ref 
        void EndMask();
    }
}
