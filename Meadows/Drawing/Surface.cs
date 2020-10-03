using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public abstract class Surface : Texture, IDrawContext
    {
        public Screen Screen { get; protected set; }

        public MultisampleQuality Multisample { get; init; }

        public abstract BlendingMode Blending { get; set; }

        public abstract Shader Shader { get; set; }

        public abstract void SetViewport(Rectangle rectangle);

        public abstract void SetCamera(Vector center, float scale = 1F, float rotation = 0F);

        public abstract void Clear(Color color);

        #region Stencil

        public abstract void ClearMask();

        public abstract void BeginMask();

        public abstract void EndMask();

        #endregion

        #region Draw

        public abstract void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in ReadOnlySpan<Matrix> matrices);

        #endregion
    }
}
