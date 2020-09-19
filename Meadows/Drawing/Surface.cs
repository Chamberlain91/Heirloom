using Meadows.Mathematics;

using System;

namespace Meadows.Drawing
{
    public abstract class Surface : Texture, IDrawCommands
    {
        public MultisampleQuality Multisample { get; init; }

        public abstract void Clear(Color color);

        public abstract void SetViewport(Rectangle rectangle);

        public abstract void ClearMask();

        public abstract void BeginMask();

        public abstract void EndMask();

        public abstract void Draw<X>(Vertex[] vertices, in Matrix matrix, in Material<X> material) where X : unmanaged;

        public abstract void Draw<X>(Vertex[] vertices, in ReadOnlySpan<Matrix> matrices, in Material<X> material) where X : unmanaged;

        public abstract void Draw(DrawBuffer buffer);
    }
}
