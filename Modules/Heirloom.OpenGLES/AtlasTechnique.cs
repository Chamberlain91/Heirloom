using System;

namespace Heirloom.OpenGLES
{
    internal abstract class AtlasTechnique
    {
        internal readonly OpenGLGraphics Graphics;

        protected AtlasTechnique(OpenGLGraphics graphics)
        {
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        internal abstract bool Submit(Image image, out Texture texture, out Rectangle uvRect);

        internal abstract void CommitChanges();
        internal abstract void Evict();
    }
}
