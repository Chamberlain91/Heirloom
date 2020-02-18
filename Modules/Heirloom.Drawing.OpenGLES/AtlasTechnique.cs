using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    public abstract class AtlasTechnique
    {
        internal abstract bool Submit(ImageSource image, out Rectangle uvRect);

        internal abstract void ApplyTextures();
    }
}
