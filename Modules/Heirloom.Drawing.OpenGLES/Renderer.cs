using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class Renderer
    {
        private ImageSource _imageSource;

        private bool _updateTextureBind;
        private Texture _texture;

        public Renderer(OpenGLGraphics graphics)
        {
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            BatchingTechnique = new HybridBatchingTechnique();
        }

        private OpenGLGraphics Graphics { get; }

        private BatchingTechnique BatchingTechnique { get; }

        public bool IsDirty => BatchingTechnique.IsDirty;

        public Rectangle UVRect { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
        {
            // Configure to use image (texture)
            UseImage(image);

            // Submit to batch
            while (!BatchingTechnique.Submit(mesh, UVRect, in transform, in color))
            {
                Graphics.Flush();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush()
        {
            // Update texture
            if (_updateTextureBind)
            {
                GL.ActiveTexture(0);
                GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                _updateTextureBind = false;
            }

            // Draw batched geometry
            BatchingTechnique.DrawBatch();
            Graphics.MarkSurfaceDirty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UseImage(ImageSource imageSource)
        {
            if (_imageSource != imageSource)
            {
                _imageSource = imageSource;

                Texture texture;
                if (imageSource is Image image) // From Image Atlas
                {
                    // todo: Replace with request from atlas
                    texture = ResourceManager.GetTexture(Graphics, image);
                    UVRect = image.UVRect;
                }
                else if (imageSource is Surface surface) // From Surface
                {
                    // todo: Request framebuffer via Graphics?
                    var framebuffer = ResourceManager.GetFramebuffer(Graphics, surface);
                    texture = framebuffer.Texture;
                    UVRect = (0, 0, 1, -1);
                }
                else
                {
                    throw new InvalidOperationException("Image source was not a valid type");
                }

                // Inconsistent texture, flush and update state
                if (_texture != texture)
                {
                    // Complete pending work
                    Graphics.Flush();

                    // Mark that we need to update the texture binding
                    _updateTextureBind = true;

                    // Store new texture reference
                    _texture = texture;
                }
            }
        }
    }
}
