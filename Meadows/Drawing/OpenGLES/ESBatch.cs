using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.OpenGLES
{
    internal abstract class ESBatch
    {
        protected ESBatch(ESGraphicsContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ESGraphicsContext Context { get; }

        public abstract bool IsDirty { get; }

        public abstract void Clear(Color color);

        public abstract bool Submit(Mesh mesh, Rectangle uvRect, Matrix matrix, Color color);

        public abstract void Commit();
    }
}
