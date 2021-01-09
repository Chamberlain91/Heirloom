using System;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESTexture : IDisposable
    {
        private bool _isDisposed = false;

        #region Constructors

        public ESTexture(IntSize size, TextureSizedFormat format = TextureSizedFormat.RGBA8, int samples = 1)
        {
            Size = size;
            // Format = format;
            Samples = samples;

            Target = IsMultisampled ? TextureTarget.Texture2DMultisample : TextureTarget.Texture2D;

            // 
            Handle = GLES.GenTexture();
            GLES.BindTexture(Target, Handle);
            {
                // Allocate texture memory, possibly multisampled
                if (IsMultisampled) { GLES.TexStorage2DMultisample((TextureImageTarget) Target, samples, format, size.Width, size.Height); }
                else
                {
                    // Compute the max mip-map depth
                    var maxLod = 1 + (int) Calc.Log(Calc.Min(size.Width, size.Height), 2);
                    GLES.TexStorage2D((TextureImageTarget) Target, maxLod, format, size.Width, size.Height);
                    GLES.SetTextureParameter(Target, TextureParameter.MaxLod, maxLod);

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

        public int Samples { get; }

        public uint Version { get; internal set; }

        public bool IsMultisampled => Samples > 1;

        #endregion

        /// <summary>
        /// Submits new image data to the texture.
        /// </summary>
        public unsafe void Update(int x, int y, Image image)
        {
            if (IsMultisampled)
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

        public void GenerateMips()
        {
            GLES.GenerateMipmap(Target);
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
                ESGraphicsBackend.Current.Invoke(() => GLES.DeleteTexture(Handle));

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
