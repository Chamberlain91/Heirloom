using System;
using System.Buffers;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Heirloom.IO;

using StbImageSharp;

using static StbImageWriteSharp.StbImageWrite;

namespace Heirloom
{
    /// <summary>
    /// Represents an image as a grid of <see cref="ColorBytes"/>.
    /// </summary>
    public sealed class Image : ImageSource
    {
        /// <summary>
        /// The max allowable image size for any dimension.
        /// </summary>
        public const int MaxImageDimension = 8192;

        /// <summary>
        /// The underlying pixel data.
        /// </summary>
        internal readonly ColorBytes[] Pixels;

        private readonly IntSize _size;

        #region Constants

        /// <summary>
        /// A small solid white image.
        /// </summary>
        internal static Image Default = CreateColor(1, 1, Color.White);

        #endregion

        #region Constructors

        /// <summary>
        /// Loads an image by a file path resolved by <see cref="Files.OpenStream(string)"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if an error was encountered when loading the image.</exception>
        public Image(string path)
            : this(Files.OpenStream(path))
        { }

        /// <summary>
        /// Loads an image from a stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if an error was encountered when loading the image.</exception>
        public Image(Stream stream)
            : this(stream.ReadAllBytes())
        { }

        /// <summary>
        /// Loads an image directly from a block of bytes.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if an error was encountered when loading the image.</exception>
        public unsafe Image(byte[] file)
        {
            // Decode from file to raw RGBA bytes
            var result = ImageResult.FromMemory(file, ColorComponents.RedGreenBlueAlpha);

            var width = result.Width;
            var height = result.Height;

            // Ensure image size is acceptable and no error while loading occurred
            ValidateImageSize(in width, in height);

            // Allocate pixels
            Pixels = new ColorBytes[width * height];
            _size = new IntSize(width, height);

            // Copy grabbed pixels to image
            fixed (byte* ptr = result.Data)
            {
                Copy((ColorBytes*) ptr, width, (0, 0, width, height), this, IntVector.Zero);
            }
        }

        /// <summary>
        /// Constructs a new blank image of the specified size.
        /// </summary>
        /// <param name="size">The size of the image in pixels.</param>
        /// <exception cref="ArgumentException">Thrown when any dimension is negative or exceeds <see cref="MaxImageDimension"/>.</exception>
        public Image(IntSize size)
            : this(size.Width, size.Height)
        { }

        /// <summary>
        /// Constructs a new blank image of the specified size.
        /// </summary>
        /// <param name="width">The width of the image in pixels.</param>
        /// <param name="height">The height of the image in pixels.</param>
        /// <exception cref="ArgumentException">Thrown when any dimension is negative or exceeds <see cref="MaxImageDimension"/>.</exception>
        public Image(int width, int height)
        {
            ValidateImageSize(in width, in height);

            // Allocate pixels
            Pixels = new ColorBytes[width * height];
            _size = new IntSize(width, height);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets the color a pixel in this image.
        /// </summary>
        public ColorBytes this[IntVector co]
        {
            get => GetPixel(co);
            set => SetPixel(co, value);
        }

        /// <summary>
        /// Gets or sets the color a pixel in this image.
        /// </summary>
        public ColorBytes this[int x, int y]
        {
            get => GetPixel(x, y);
            set => SetPixel(x, y, value);
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override IntSize Size
        {
            get => _size;
            protected set => throw new NotSupportedException();
        }

        #endregion

        private void ValidateImageSize(in int width, in int height)
        {
            if (width > MaxImageDimension || height > MaxImageDimension)
            {
                var size = new IntSize(width, height);
                throw new ArgumentException($"Image dimensions must be less than or equal to {MaxImageDimension} (was {size}).");
            }
        }

        #region Get or Set Pixels

        /// <summary>
        /// Gets the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <returns>The color value at the specified coordinate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the requested coordinate is outsid the image.</exception>
        public ColorBytes GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width) { throw new ArgumentOutOfRangeException(nameof(x)); }
            if (y < 0 || y >= Height) { throw new ArgumentOutOfRangeException(nameof(y)); }

            return Pixels[x + (y * Width)];
        }

        /// <summary>
        /// Gets the pixel at the specified coordinates.
        /// </summary>
        /// <param name="co">The coordinate of the pixel.</param>
        /// <returns>The color value at the specified coordinate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the requested coordinate is outsid the image.</exception>
        public ColorBytes GetPixel(IntVector co)
        {
            return GetPixel(co.X, co.Y);
        }

        /// <summary>
        /// Gets a copy of all the pixels in this image.
        /// </summary>
        /// <returns>A newly allocated copy of the pixels.</returns>
        public ColorBytes[] GetPixels()
        {
            var pixels = new ColorBytes[Pixels.Length];
            Array.Copy(Pixels, pixels, Pixels.Length);
            return pixels;
        }

        /// <summary>
        /// Copies the image pixels into an already allocated buffer.
        /// </summary>
        /// <param name="buffer">A span of the buffer to copy into.</param>
        /// <returns>A newly allocated copy of the pixels.</returns>
        public void GetPixels(Span<ColorBytes> buffer)
        {
            if (buffer.Length < Pixels.Length) { throw new ArgumentException("Copy buffer was not large enough."); }
            Pixels.CopyTo(buffer);
        }

        /// <summary>
        /// Copies the image pixels into an already allocated buffer.
        /// </summary>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="offset">The offset of buffer to copy into.</param>
        /// <returns>A newly allocated copy of the pixels.</returns>
        public void GetPixels(ColorBytes[] buffer, int offset)
        {
            GetPixels(new Span<ColorBytes>(buffer, offset, Pixels.Length));
        }

        /// <summary>
        /// Sets the color of a pixel at the specified coordinate.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <param name="color">The color to assign to the pixel.</param>
        public void SetPixel(int x, int y, in ColorBytes color)
        {
            Pixels[x + (y * Width)] = color;
            IncrementVersion();
        }

        /// <summary>
        /// Sets the color of a pixel at the specified coordinate.
        /// </summary>
        /// <param name="co">The coordinate of the pixel.</param>
        /// <param name="color">The color to assign to the pixel.</param>
        public void SetPixel(IntVector co, in ColorBytes color)
        {
            SetPixel(co.X, co.Y, in color);
        }

        /// <summary>
        /// Sets the color of all pixels in the image.
        /// </summary>
        /// <param name="pixels">Th</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pixels"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the input array identical length as the internal pixel array.</exception>
        public unsafe void SetPixels(ColorBytes[] pixels)
        {
            if (pixels is null) { throw new ArgumentNullException(nameof(pixels)); }
            if (Pixels.Length != pixels.Length) { throw new ArgumentException("Incoming pixel array must be the same size as image."); }

            fixed (ColorBytes* src = pixels)
            fixed (ColorBytes* dst = Pixels)
            {
                var len = Pixels.Length * 4;
                Buffer.MemoryCopy((void*) src, (void*) dst, len, len);
                IncrementVersion();
            }
        }

        #endregion

        #region Clear Pixels

        /// <summary>
        /// Sets all pixels in the image to the specified color.
        /// </summary>
        public unsafe void Clear(ColorBytes pixel)
        {
            fixed (ColorBytes* ptr = Pixels)
            {
                // Fill entire data buffer
                for (var i = 0; i < Pixels.Length; i++)
                {
                    *(ptr + i) = pixel;
                }
            }

            IncrementVersion();
        }

        #endregion

        #region Flip Pixels

        /// <summary>
        /// Flips the image on the specified axis.
        /// </summary>
        /// <param name="axis"></param>
        public void Flip(Axis axis)
        {
            if (axis == Axis.Vertical) { FlipVertical(); }
            else { FlipHorizontal(); }
        }

        private void FlipVertical()
        {
            for (var y = 0; y < Height / 2; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var i0 = x + (y * Width);
                    var i1 = x + ((Height - y - 1) * Width);

                    // 
                    Calc.Swap(ref Pixels[i0], ref Pixels[i1]);
                }
            }
        }

        private void FlipHorizontal()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width / 2; x++)
                {
                    var i0 = x + 0 + (y * Width);
                    var i1 = (Width - x - 1) + (y * Width);

                    // 
                    Calc.Swap(ref Pixels[i0], ref Pixels[i1]);
                }
            }
        }

        #endregion

        #region Copy To

        /// <summary>
        /// Copies a region of this image to another.
        /// </summary>
        /// <param name="region">The region to copy.</param>
        /// <param name="target">The target image to copy pixels to.</param>
        /// <param name="targetOffset">The offset within the target image to copy to.</param>
        public void CopyTo(in IntRectangle region, Image target, in IntVector targetOffset)
        {
            Copy(this, in region, target, in targetOffset);
        }

        /// <summary>
        /// Copies this image to another image.
        /// </summary>
        /// <param name="target">The target image to copy pixels to.</param>
        /// <param name="targetOffset">The offset within the target image to copy to.</param>
        public void CopyTo(Image target, in IntVector targetOffset)
        {
            Copy(this, (0, 0, Width, Height), target, in targetOffset);
        }

        #endregion

        #region Clone

        /// <summary>
        /// Creates a clone of this image.
        /// </summary>
        public Image Clone()
        {
            var clone = new Image(Size);
            CopyTo(clone, (0, 0));
            return clone;
        }

        #endregion 

        #region Procedural Images (Static)

        /// <summary>
        /// Create an image with checkerboard pattern.
        /// </summary>
        /// <param name="size">Size of the image in pixels.</param>
        /// <param name="color">Color to base the checkerboard pattern on.</param>
        /// <param name="cellSize">Size of each "checker" in the checkerboard.</param>
        /// <returns>An image filled with the checkerboard pattern.</returns>
        public static Image CreateCheckerboardPattern(IntSize size, Color color, int cellSize = 16)
        {
            return CreateCheckerboardPattern(size.Width, size.Height, color, cellSize);
        }

        /// <summary>
        /// Create an image with checkerboard pattern.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="color">Color to base the checkerboard pattern on.</param>
        /// <param name="cellSize">Size of each "checker" in the checkerboard.</param>
        /// <returns>An image filled with the checkerboard pattern.</returns>
        public static Image CreateCheckerboardPattern(int width, int height, Color color, int cellSize = 16)
        {
            var im = new Image(width, height);

            // Compute checkerboard texture
            foreach (var p in Rasterizer.Rectangle(0, 0, im.Width, im.Height))
            {
                var flag = ((p.Y & cellSize) == 0) ^ (p.X & cellSize) == 0;
                var pixel = (ColorBytes) ((flag ? Color.LightGray : Color.White) * color);
                im.SetPixel(p.X, p.Y, pixel);
            }

            // Draw border
            foreach (var edge in Rasterizer.RectangleOutline(0, 0, width, height))
            {
                var pixel = (ColorBytes) (Color.Gray * color);
                im.SetPixel(edge.X, edge.Y, pixel);
            }

            return im;
        }

        /// <summary>
        /// Create an image with a grid pattern.
        /// </summary>
        /// <param name="size">Size of the image in pixels.</param>
        /// <param name="color">Color to base the grid pattern on.</param>
        /// <param name="cellSize">The size of a grid cell in pixels.</param>
        /// <param name="borderWidth">Size of the line between each cell.</param>
        /// <returns>An image filled with the grid pattern.</returns>
        public static Image CreateGridPattern(IntSize size, Color color, int cellSize, int borderWidth = 1)
        {
            return CreateGridPattern(size.Width, size.Height, color, cellSize, borderWidth);
        }

        /// <summary>
        /// Create an image with a grid pattern.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="color">Color to base the grid pattern on.</param>
        /// <param name="cellSize">The size of a grid cell in pixels.</param>
        /// <param name="borderWidth">Size of the line between each cell.</param>
        /// <returns>An image filled with the grid pattern.</returns>
        public static Image CreateGridPattern(int width, int height, Color color, int cellSize, int borderWidth = 1)
        {
            var im = new Image(width, height);

            // Compute checkerboard texture
            foreach (var p in Rasterizer.Rectangle(0, 0, im.Width, im.Height))
            {
                var flag = ((p.X % cellSize) < borderWidth) || ((p.Y % cellSize) < borderWidth);
                var pixel = (ColorBytes) ((flag ? Color.LightGray : Color.White) * color);
                im.SetPixel(p.X, p.Y, pixel);
            }

            return im;
        }

        /// <summary>
        /// Creates an image filled with a solid color.
        /// </summary>
        /// <param name="size">Size of the image in pixels.</param>
        /// <param name="color">Color to fill the image with.</param>
        /// <returns>An image of only the specified color.</returns>
        public static Image CreateColor(IntSize size, Color color)
        {
            return CreateColor(size.Width, size.Height, color);
        }

        /// <summary>
        /// Creates an image filled with a solid color.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="color">Color to fill the image with.</param>
        /// <returns>An image of only the specified color.</returns>
        public static Image CreateColor(int width, int height, Color color)
        {
            var im = new Image(width, height);

            var pixel = (ColorBytes) color;

            // Draw border
            foreach (var p in Rasterizer.Rectangle(0, 0, width, height))
            {
                im.SetPixel(p.X, p.Y, pixel);
            }

            return im;
        }

        /// <summary>
        /// Creates an image filled with noise.
        /// </summary>
        /// <param name="size">Size of the image in pixels.</param>
        /// <param name="scale">The approximate size of a 'noise blob'.</param>
        /// <param name="octaves">Number of noise layers.</param>
        /// <param name="persistence">How persistent each noise layer is.</param>
        /// <param name="offset">Value to offset the noise by.</param>
        /// <returns>A noisy image with noise generated on all four components.</returns>
        public static Image CreateNoise(IntSize size, float scale = 1, int octaves = 4, float persistence = 0.5F, Vector offset = default)
        {
            return CreateNoise(size.Width, size.Height, scale, octaves, persistence, offset);
        }

        /// <summary>
        /// Creates an image filled with noise.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="scale">The approximate size of a 'noise blob'.</param>
        /// <param name="octaves">Number of noise layers.</param>
        /// <param name="persistence">How persistent each noise layer is.</param>
        /// <param name="offset">Value to offset the noise by.</param>
        /// <returns>A noisy image with noise generated on all four components.</returns>
        public static Image CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5F, Vector offset = default)
        {
            return CreateNoise(width, height, Calc.Simplex, scale, octaves, persistence, offset);
        }

        /// <summary>
        /// Creates an image filled with noise, provided with an instance of <see cref="INoise2D"/>.
        /// </summary>
        /// <param name="size">Size of the image in pixels.</param>
        /// <param name="noise">A 2D noise generator.</param>
        /// <param name="scale">The approximate size of a 'noise blob'.</param>
        /// <param name="octaves">Number of noise layers.</param>
        /// <param name="persistence">How persistent each noise layer is.</param>
        /// <param name="offset">Value to offset the noise by.</param>
        /// <returns>A noisy image with noise generated on all four components.</returns>
        public static Image CreateNoise(IntSize size, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5F, Vector offset = default)
        {
            return CreateNoise(size.Width, size.Height, noise, scale, octaves, persistence, offset);
        }

        /// <summary>
        /// Creates an image filled with noise, provided with an instance of <see cref="INoise2D"/>.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="noise">A 2D noise generator.</param>
        /// <param name="scale">The approximate size of a 'noise blob'.</param>
        /// <param name="octaves">Number of noise layers.</param>
        /// <param name="persistence">How persistent each noise layer is.</param>
        /// <param name="offset">Value to offset the noise by.</param>
        /// <returns>A noisy image with noise generated on all four components.</returns>
        public static Image CreateNoise(int width, int height, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5F, Vector offset = default)
        {
            if (noise is null) { throw new ArgumentNullException(nameof(noise)); }

            var im = new Image(width, height);

            // 
            scale = 1F / scale;

            // Write pixels in parallel
            Parallel.ForEach(Rasterizer.Rectangle(0, 0, width, height), co =>
            {
                var p0 = ((Vector) co + new Vector(0, 0) + offset) * scale;
                var p1 = ((Vector) co + new Vector(10000, 0) + offset) * scale;
                var p2 = ((Vector) co + new Vector(0, 10000) + offset) * scale;
                var p3 = ((Vector) co + new Vector(10000, 10000) + offset) * scale;

                var n0 = (noise.Sample(p0, octaves, persistence) + 1F) / 2F;
                var n1 = (noise.Sample(p1, octaves, persistence) + 1F) / 2F;
                var n2 = (noise.Sample(p2, octaves, persistence) + 1F) / 2F;
                var n3 = (noise.Sample(p3, octaves, persistence) + 1F) / 2F;

                // 
                var color = (ColorBytes) new Color(n0, n1, n2, n3);
                im.SetPixel(co.X, co.Y, color);
            });

            return im;
        }

        #endregion

        #region Copy (Static)

        /// <summary>
        /// Copies a region of the <paramref name="source"/> image into the <paramref name="target"/> image at the specified offset.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <param name="sourceRegion">The region to copy within the source image.</param>
        /// <param name="target">The target image.</param>
        /// <param name="targetOffset">The offset in the target to copy the source rectangle to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either image is null.</exception>
        public static unsafe void Copy(Image source, in IntRectangle sourceRegion,
                                       Image target, in IntVector targetOffset)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            if (source == target) { throw new ArgumentException("Unable to copy from self to self."); }

            fixed (ColorBytes* sourcePtr = source.Pixels)
            fixed (ColorBytes* targetPtr = target.Pixels)
            {
                Copy(sourcePtr, source.Width, in sourceRegion,
                     targetPtr, target.Width, in targetOffset);

                // Notify target of mutation
                target.IncrementVersion();
            }
        }

        /// <summary>
        /// Copies a region of the <paramref name="source"/> image into the <paramref name="targetPtr"/> buffer at the specified offset.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <param name="sourceRegion">The region to copy within the source image.</param>
        /// <param name="targetPtr">The target image buffer.</param>
        /// <param name="targetWidth">The width of the target image.</param>
        /// <param name="targetOffset">The offset in the target to copy the source rectangle to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either image is null.</exception>
        public static unsafe void Copy(Image source, in IntRectangle sourceRegion,
                                       ColorBytes* targetPtr, int targetWidth, in IntVector targetOffset)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }

            fixed (ColorBytes* sourcePtr = source.Pixels)
            {
                Copy(sourcePtr, source.Width, in sourceRegion,
                     targetPtr, targetWidth, in targetOffset);
            }
        }

        /// <summary>
        /// Copies a region of the <paramref name="sourcePtr"/> image into the <paramref name="target"/> buffer at the specified offset.
        /// </summary>
        /// <param name="sourcePtr">The source image buffer.</param>
        /// <param name="sourceWidth">The width of the source image.</param>
        /// <param name="sourceRegion">The region to copy within the source image.</param>
        /// <param name="target">The target image.</param>
        /// <param name="targetOffset">The offset in the target to copy the source rectangle to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either image is null.</exception>
        public static unsafe void Copy(ColorBytes* sourcePtr, int sourceWidth, in IntRectangle sourceRegion,
                                       Image target, in IntVector targetOffset)
        {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            fixed (ColorBytes* targetPtr = target.Pixels)
            {
                Copy(sourcePtr, sourceWidth, in sourceRegion,
                     targetPtr, target.Width, in targetOffset);

                // Notify target of mutation
                target.IncrementVersion();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePtr">The source image buffer.</param>
        /// <param name="sourceWidth">The width of the source image.</param>
        /// <param name="sourceRegion">The region to copy within the source image.</param>
        /// <param name="targetPtr">The target image buffer.</param>
        /// <param name="targetWidth">The width of the target image.</param>
        /// <param name="targetOffset">The offset in the target to copy the source rectangle to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either image is null.</exception>
        public static unsafe void Copy(ColorBytes* sourcePtr, int sourceWidth, in IntRectangle sourceRegion,
                                       ColorBytes* targetPtr, int targetWidth, in IntVector targetOffset)
        {
            if (sourcePtr == targetPtr) { throw new ArgumentException("Unable to copy from self to self."); }
            if (sourcePtr == (void*) 0) { throw new ArgumentNullException(nameof(sourcePtr)); }
            if (targetPtr == (void*) 0) { throw new ArgumentNullException(nameof(targetPtr)); }

            // Compute the number of bytes to copy per row.
            var rowByteLength = sourceRegion.Width * sizeof(ColorBytes);

            // todo: Validate regions are sane with information given.

            for (var y = 0; y < sourceRegion.Height; y++)
            {
                var sourceRow = sourcePtr + (sourceWidth * (y + sourceRegion.Y)) + sourceRegion.X;
                var targetRow = targetPtr + (targetWidth * (y + targetOffset.Y)) + targetOffset.X;
                Buffer.MemoryCopy(sourceRow, targetRow, rowByteLength, rowByteLength);
            }
        }

        #endregion

        #region Write (Static)

        [ThreadStatic] private static Stream _writeStream;

        /// <summary>
        /// Writes the image to the stream as a PNG file format.
        /// </summary>
        public unsafe void WritePNG(Stream stream)
        {
            if (stream is null) { throw new ArgumentNullException(nameof(stream)); }

            lock (Pixels)
            {
                _writeStream = stream;

                // Flip vertically!
                stbi_flip_vertically_on_write(1);

                fixed (ColorBytes* pPixels = Pixels)
                {
                    if (stbi_write_png_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, Width * 4) == 0)
                    {
                        throw new InvalidOperationException("Unable to write png image to stream.");
                    }
                }
            }
        }

        /// <summary>
        /// Writes the image to the stream as a PNG file format.
        /// </summary>
        public unsafe void WriteJPG(Stream stream, int quality = 85)
        {
            lock (Pixels)
            {
                if (quality < 1 || quality > 100)
                {
                    throw new ArgumentException("Jpeg quality must be from 1 - 100.", nameof(quality));
                }

                _writeStream = stream;

                // Flip vertically!
                stbi_flip_vertically_on_write(1);

                fixed (ColorBytes* pPixels = Pixels)
                {
                    if (stbi_write_jpg_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, quality) == 0)
                    {
                        throw new InvalidOperationException("Unable to write jpg image to stream.");
                    }
                }
            }
        }

        private static unsafe int WriteImageCallback(void* context, void* data, int size)
        {
            if (data == null || size <= 0)
            {
                return 0;
            }

            // Copy bytes into temporary buffer
            var buffer = ArrayPool<byte>.Shared.Rent(size);
            Marshal.Copy((IntPtr) data, buffer, 0, size);

            // Write buffer into stream
            _writeStream.Write(buffer, 0, size);

            // Return temporary buffer
            ArrayPool<byte>.Shared.Return(buffer);

            return size;
        }

        #endregion 
    }
}
