using System.Runtime.CompilerServices;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal class ESAtlasSimple : ESAtlas
    {
        private readonly ConditionalWeakTable<Texture, ESTexture> _textures;

        public ESAtlasSimple(ESGraphicsContext context)
            : base(context)
        {
            _textures = new ConditionalWeakTable<Texture, ESTexture>();
        }

        public override bool Submit(Image image, out ESTexture atlasTexture, out Rectangle atlasRect)
        {
            // Try to get known framebuffer instance. Framebuffers are "container objects" and
            // need to be uniquely created for each graphics context.
            if (!_textures.TryGetValue(image, out var texture))
            {
                texture = Context.Backend.GetNativeObject<ESTexture>(image);
                _textures.Add(image, texture);
            }

            // Image and texture are out of sync, write image to texture
            if (image.Version != texture.Version)
            {
                Context.Invoke(() =>
                {
                    texture.Bind();
                    texture.Update(0, 0, image);
                    texture.GenerateMips();
                });

                texture.Version = image.Version;
            }

            // Emit
            atlasRect = Rectangle.One;
            atlasTexture = texture;
            return true;
        }

        public override void Commit()
        {
            // Nothing to commit since we aren't really managing an atlas
        }

        public override void Evict()
        {
            // Won't be called since the atlas doesn't actually exist
        }
    }
}
