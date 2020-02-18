using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class SimpleAtlasTechnique : AtlasTechnique
    {
        internal override void ApplyTextures()
        {
            // Update texture
            if (_textureBindDirty)
            {
                GL.ActiveTexture(0);
                GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                _textureBindDirty = false;
            }
        }

        internal override bool Submit(ImageSource image, out Rectangle uvRect)
        {
            if (_imageSource != imageSource)
            {
                _imageSource = imageSource;

                // Request texture information for the given input image
                GetTextureInformation(imageSource, out var texture, out _uvRect);

                // Inconsistent texture, flush and update state
                if (_texture != texture)
                {
                    // Complete pending work
                    Flush();

                    // Mark that we need to update the texture binding
                    _textureBindDirty = true;

                    // Store new texture reference
                    _texture = texture;
                }
            }
        }
    }
}
