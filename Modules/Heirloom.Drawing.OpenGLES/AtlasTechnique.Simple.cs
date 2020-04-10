using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class SimpleAtlasTechnique : AtlasTechnique
    {
        private readonly ConditionalWeakTable<Image, Texture> _textures;

        public SimpleAtlasTechnique(OpenGLGraphics graphics)
            : base(graphics)
        {
            _textures = new ConditionalWeakTable<Image, Texture>();
        }

        internal override void Evict()
        {
            _textures.Clear();
        }

        internal override bool Submit(Image image, out Texture texture, out Rectangle uvRect)
        {
            // Try to get known framebuffer instance. Framebuffers are "container objects" and
            // need to be uniquely created for each graphics context.
            if (!_textures.TryGetValue(image, out var texture_))
            {
                // Was not known, we will now create it 
                texture_ = Graphics.Invoke(() => new Texture(image.Size));

                // Store texture
                _textures.Add(image, texture_);
            }

            // Image and texture are out of sync, write image to texture
            if (image.Version != texture_.Version)
            {
                Graphics.Invoke(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, texture_.Handle);
                    texture_.Update(0, 0, image);
                });

                texture_.Version = image.Version;
            }

            // Emit
            uvRect = (0, 0, 1, 1);
            texture = texture_;
            return true;
        }

        internal override void CommitChanges()
        {
            // Nothing to do since it already forcefully happens above
        }
    }
}
