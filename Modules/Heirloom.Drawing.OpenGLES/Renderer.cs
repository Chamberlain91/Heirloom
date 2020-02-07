using System;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class Renderer
    {
        protected Renderer(OpenGLGraphics graphics)
        {
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        protected OpenGLGraphics Graphics { get; }

        public abstract bool IsDirty { get; }

        public abstract void Submit(Mesh mesh, in Rectangle atlasRect, in Matrix transform, in Color color);

        public abstract void FlushBatch();
    }
}
