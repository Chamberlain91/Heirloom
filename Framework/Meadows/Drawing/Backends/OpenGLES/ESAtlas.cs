using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.OpenGLES
{
    internal abstract class ESAtlas
    {
        protected ESAtlas(ESGraphicsContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ESGraphicsContext Context { get; }

        public abstract bool Submit(Image image, out ESTexture atlasTexture, out Rectangle atlasRect);

        public abstract void Commit();

        public abstract void Evict();
    }
}
