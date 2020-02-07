using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal abstract class Renderer
    {
        private ImageSource _imageSource;

        private bool _updateTextureBind;
        private Texture _texture;

        protected Renderer(OpenGLGraphics graphics)
        {
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        protected OpenGLGraphics Graphics { get; }

        public abstract bool IsDirty { get; }

        protected Rectangle UVRect { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Submit(ImageSource image, Mesh mesh, in Matrix transform, in Color color)
        {
            // 
            UseImage(image);

            // 
            Submit(mesh, in transform, in color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush()
        {
            if (_updateTextureBind)
            {
                GL.ActiveTexture(0);
                GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);

                _updateTextureBind = false;
            }

            // 
            Draw();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Submit(Mesh mesh, in Matrix transform, in Color color);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Draw();

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
