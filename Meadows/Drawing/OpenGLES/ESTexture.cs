using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.OpenGLES
{
    internal sealed class ESTexture : IDisposable
    {
        private readonly bool _isMultisampled;
        private bool _isDisposed = false;

        #region Constructors

        public ESTexture(IntSize size, TextureSizedFormat format = TextureSizedFormat.RGBA8, int samples = 1)
        {
            Size = size;
            _isMultisampled = samples > 1;

            // Log the creation of this texture
            if (_isMultisampled) { Log.Debug($"Creating {format} Texture({size} w/ {samples} samples)"); }
            else { Log.Debug($"Creating {format} Texture ({size})"); }

            Target = _isMultisampled ? TextureTarget.Texture2DMultisample : TextureTarget.Texture2D;

            // 
            Handle = GLES.GenTexture();
            GLES.BindTexture(Target, Handle);
            {
                // Allocate texture memory, possibly multisampled
                if (_isMultisampled) { GLES.TexStorage2DMultisample((TextureImageTarget) Target, samples, format, size.Width, size.Height); }
                else
                {
                    GLES.TexStorage2D((TextureImageTarget) Target, 1, format, size.Width, size.Height);

                    GLES.SetTextureParameter(Target, TextureParameter.MaxLod, 1);

                    // Configure with point filtering and repeating UVs
                    GLES.SetTextureParameter(Target, TextureParameter.MinFilter, (int) TextureMinFilter.NearestMipNearest);
                    GLES.SetTextureParameter(Target, TextureParameter.MagFilter, (int) TextureMagFilter.Nearest);
                    GLES.SetTextureParameter(Target, TextureParameter.WrapS, (int) TextureWrap.Repeat);
                    GLES.SetTextureParameter(Target, TextureParameter.WrapT, (int) TextureWrap.Repeat);
                }
            }
            GLES.BindTexture(Target, 0);
        }

        ~ESTexture()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        internal uint Handle { get; }

        internal TextureTarget Target { get; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        public IntSize Size { get; }

        public uint Version { get; internal set; }

        #endregion

        /// <summary>
        /// Submits new image data to the texture.
        /// </summary>
        public unsafe void Update(int x, int y, Image image)
        {
            if (_isMultisampled)
            {
                throw new InvalidOperationException("Unable to write to multisampled texture from CPU side.");
            }

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
                GLES.TexSubImage2D((TextureImageTarget) Target, 0,
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
                    // nothing
                }

                // Schedule deleting the GL texture resource
                ESGraphicsContext.InvokeOnSomeThread(() =>
                {
                    Log.Debug($"[Dispose] Texture ({Handle})");
                    GLES.DeleteTexture(Handle);
                });

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
