using System;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class Texture : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public Texture(IntSize size)
        {
            Size = size;

            Log.Debug($"Creating Texture ({size})");

            // 
            Handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            {
                // Allocate texture memory (no mipmaps)
                GL.TexStorage2D(TextureImageTarget.Texture2D, 1, TextureSizedFormat.RGBA8, size.Width, size.Height);
                GL.SetTextureParameter(TextureTarget.Texture2D, TextureParameter.MaxLod, 1);

                // Configure with bilinear filtering and repeating UVs
                GL.SetTextureParameter(TextureTarget.Texture2D, TextureParameter.MinFilter, (int) TextureMinFilter.NearestMipLinear);
                GL.SetTextureParameter(TextureTarget.Texture2D, TextureParameter.MagFilter, (int) TextureMagFilter.Linear);
                GL.SetTextureParameter(TextureTarget.Texture2D, TextureParameter.WrapS, (int) TextureWrap.Repeat);
                GL.SetTextureParameter(TextureTarget.Texture2D, TextureParameter.WrapT, (int) TextureWrap.Repeat);
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        ~Texture()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        internal uint Handle { get; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        public IntSize Size { get; }

        public uint Version { get; internal set; }

        #endregion

        public unsafe void Update(int x, int y, Image image)
        {
            // Validate we were provide a non-null image.
            if (image == null) { throw new ArgumentNullException(nameof(image)); }

            // Ensure the image and position provided will fit within texture.
            if (x < 0 || y < 0 || (x + image.Width) > Width || (y + image.Height) > Height)
            {
                throw new ArgumentException($"Texture update does not fit within texture dimensions", nameof(image));
            }

            // Copy pixels to texture
            fixed (ColorBytes* ptr = image.Pixels)
            {
                GL.TexSubImage2D(TextureImageTarget.Texture2D, 0,
                    x, y, image.Width, image.Height, // Sub Region
                    TexturePixelFormat.RGBA, TexturePixelType.UnsignedByte,
                    (IntPtr) ptr);
            }
        }

        #region Dispose

        private void Dispose(bool disposeManaged)
        {
            if (!_isDisposed)
            {
                if (disposeManaged)
                {
                    // TODO: dispose managed objects.
                }

                // Schedule deleting the GL texture resource
                OpenGLGraphicsAdapter.Schedule(() => GL.DeleteTexture(Handle));

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
