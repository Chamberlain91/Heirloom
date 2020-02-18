using System;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class AtlasTechnique
    {
        internal readonly OpenGLGraphics Graphics;

        protected AtlasTechnique(OpenGLGraphics graphics)
        {
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        internal abstract void GetTextureInformation(Image image, out Texture texture, out Rectangle uvRect);
    }
}
