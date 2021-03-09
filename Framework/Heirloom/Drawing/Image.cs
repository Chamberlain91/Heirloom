using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Heirloom.Collections;
using Heirloom.IO;
using Heirloom.Mathematics;

using StbImageSharp;

using static StbImageWriteSharp.StbImageWrite;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents an image as a grid of <see cref="ColorBytes"/>.
    /// </summary>
    /// <category>Drawing</category>
    public sealed class Image : Texture, IFiniteGrid<ColorBytes>
    {
        /// <summary>
        /// The max allowable image size for any dimension.
        /// </summary>
        public const int MaxImageDimension = 8192;

        /// <summary>
        /// The underlying pixel data.
        /// </summary>
        internal readonly ColorBytes[] Pixels;

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
            ValidateImageSize(width, height);

            // Allocate pixels
            Pixels = new ColorBytes[width * height];
            Size = new IntSize(width, height);

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
            ValidateImageSize(width, height);

            // Allocate pixels
            Pixels = new ColorBytes[width * height];
            Size = new IntSize(width, height);
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

        private static void ValidateImageSize(int width, int height)
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
        public void SetPixel(int x, int y, ColorBytes color)
        {
            Pixels[x + (y * Width)] = color;
            IncrementVersion();
        }

        /// <summary>
        /// Sets the color of a pixel at the specified coordinate.
        /// </summary>
        /// <param name="co">The coordinate of the pixel.</param>
        /// <param name="color">The color to assign to the pixel.</param>
        public void SetPixel(IntVector co, ColorBytes color)
        {
            SetPixel(co.X, co.Y, color);
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
                Buffer.MemoryCopy(src, dst, len, len);
                IncrementVersion();
            }
        }

        #endregion

        #region Sample

        /// <summary>
        /// Samples an image for a color at the specified coordinate.
        /// </summary>
        /// <param name="co">Some coordinate,</param>
        /// <param name="normalized">A value determining if the input coordinates are normalized.</param>
        /// <param name="interpolationMode">The interpolation mode, controlling how pixels are interpolated.</param>
        /// <param name="repeatMode">The repeat mode, controlling the behaviour of out-of-bounds coordinates.</param>
        /// <returns>The pixel color sampled at the specified coordinates.</returns>
        public Color Sample(Vector co, InterpolationMode interpolationMode = InterpolationMode.Linear, RepeatMode repeatMode = RepeatMode.Repeat, bool normalized = false)
        {
            return Sample(co.X, co.Y, interpolationMode, repeatMode, normalized);
        }

        /// <summary>
        /// Samples an image for a color at the specified coordinate.
        /// </summary>
        /// <param name="x">Some x-coordinate,</param>
        /// <param name="y">Some y-coordinate.</param>
        /// <param name="normalized">A value determining if the input coordinates are normalized.</param>
        /// <param name="interpolationMode">The interpolation mode, controlling how pixels are interpolated.</param>
        /// <param name="repeatMode">The repeat mode, controlling the behaviour of out-of-bounds coordinates.</param>
        /// <returns>The pixel color sampled at the specified coordinates.</returns>
        public Color Sample(float x, float y, InterpolationMode interpolationMode = InterpolationMode.Linear, RepeatMode repeatMode = RepeatMode.Repeat, bool normalized = false)
        {
            // Compute image space coordinate
            if (normalized)
            {
                x *= Width;
                y *= Height;
            }

            switch (interpolationMode)
            {
                default:
                    throw new ArgumentException("Invalid interpolation mode, unable to sample image.");

                case InterpolationMode.Nearest:
                {
                    // Get integer (pixel) coordinate
                    var px = Calc.Floor(x);
                    var py = Calc.Floor(y);

                    return getPixel(px, py);
                }

                case InterpolationMode.Linear:
                {
                    x -= 0.5F;
                    y -= 0.5F;

                    // Get fractional value of coordinates
                    var fx = Calc.Fraction(x);
                    var fy = Calc.Fraction(y);

                    // Sample the 4 corners
                    var p00 = Sample(x + 0, y + 0, InterpolationMode.Nearest, repeatMode, false);
                    var p10 = Sample(x + 1, y + 0, InterpolationMode.Nearest, repeatMode, false);
                    var p11 = Sample(x + 1, y + 1, InterpolationMode.Nearest, repeatMode, false);
                    var p01 = Sample(x + 0, y + 1, InterpolationMode.Nearest, repeatMode, false);

                    // Interpolate columns
                    var p0 = Color.Lerp(p00, p10, fx);
                    var p1 = Color.Lerp(p01, p11, fx);

                    // Interpolate rows
                    return Color.Lerp(p0, p1, fy);
                }
            }

            Color getPixel(int x, int y)
            {
                switch (repeatMode)
                {
                    default:
                        throw new ArgumentException("Invalid repeat mode, unable to sample image.");

                    // Clamp to edge
                    case RepeatMode.Clamp:
                        x = Calc.Clamp(x, 0, Width - 1);
                        y = Calc.Clamp(y, 0, Height - 1);
                        break;

                    // Transparent beyond edge
                    case RepeatMode.Blank:
                        if (x < 0 || y < 0 || x >= Width || y >= Height)
                        {
                            return Color.TransparentBlack;
                        }

                        break;

                    // Repeate image after edge
                    case RepeatMode.Repeat:
                        x = Calc.Wrap(x, Width);
                        y = Calc.Wrap(y, Height);
                        break;
                }

                return GetPixel(x, y);
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
        /// <param name="axis">The axid to flip the image.</param>
        public void Flip(Axis axis)
        {
            if (axis == Axis.Vertical) { FlipVertical(); }
            else { FlipHorizontal(); }

            void FlipVertical()
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

            void FlipHorizontal()
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
        }

        #endregion

        #region Copy To

        /// <summary>
        /// Copies a region of this image to another.
        /// </summary>
        /// <param name="region">The region to copy.</param>
        /// <param name="target">The target image to copy pixels to.</param>
        /// <param name="targetOffset">The offset within the target image to copy to.</param>
        public void CopyTo(IntRectangle region, Image target, IntVector targetOffset)
        {
            Copy(this, region, target, targetOffset);
        }

        /// <summary>
        /// Copies this image to another image.
        /// </summary>
        /// <param name="target">The target image to copy pixels to.</param>
        /// <param name="targetOffset">The offset within the target image to copy to.</param>
        public void CopyTo(Image target, IntVector targetOffset)
        {
            Copy(this, (0, 0, Width, Height), target, targetOffset);
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

        /// <summary>
        /// Creates a clone of a subregion of this image.
        /// </summary>
        public Image Clone(IntRectangle region)
        {
            return CreateCopy(this, region);
        }

        #endregion

        #region Procedural Images (Static)

        /// <summary>
        /// Creates a procedurally generated image.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="generatePixel">A function to generate the pixel color for some coordinate.</param>
        /// <param name="runParallel">If true, will process each pixel multi-threaded.</param> 
        /// <returns>An image filled procedurally.</returns>
        public static Image CreateProcedural(int width, int height, Func<IntVector, Color> generatePixel, bool runParallel = true)
        {
            if (generatePixel is null) { throw new ArgumentNullException(nameof(generatePixel)); }

            var im = new Image(width, height);

            if (runParallel)
            {
                // Generate the color for each pixel
                Parallel.ForEach(Rasterizer.Rectangle(0, 0, width, height), co =>
                {
                    im.SetPixel(co, generatePixel(co));
                });
            }
            else
            {
                // Generate the color for each pixel
                foreach (var co in Rasterizer.Rectangle(0, 0, im.Width, im.Height))
                {
                    im.SetPixel(co, generatePixel(co));
                }
            }

            return im;
        }

        #region Checkerboard

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
            return CreateCheckerboardPattern(width, height, color, color * Color.Gray, cellSize);
        }

        /// <summary>
        /// Create an image with checkerboard pattern.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="color1">Color of the first type of tile.</param>
        /// <param name="color2">Color of the second type of tile.</param>
        /// <param name="cellSize">Size of each "checker" in the checkerboard.</param>
        /// <returns>An image filled with the checkerboard pattern.</returns>
        public static Image CreateCheckerboardPattern(int width, int height, Color color1, Color color2, int cellSize = 16)
        {
            return CreateProcedural(width, height, co =>
            {
                // Computes checkerboard pattern
                var flag = ((co.Y & cellSize) == 0) ^ (co.X & cellSize) == 0;
                var pixel = (ColorBytes) (flag ? color1 : color2);

                return pixel;
            });
        }

        #endregion

        #region Grid

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
            return CreateProcedural(width, height, co =>
            {
                var flag = ((co.X % cellSize) < borderWidth) || ((co.Y % cellSize) < borderWidth);
                return (flag ? Color.LightGray : Color.White) * color;
            });
        }

        #endregion

        #region Solid Color

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
            var image = CreateProcedural(width, height, co => color);
            image.Repeat = RepeatMode.Repeat;
            return image;
        }

        #endregion

        #region Gradient

        /// <summary>
        /// Creates an image filled with a gradient.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="gradient">Gradient to fill the image with.</param>
        /// <param name="axis">The axis the gradient will fill across.</param>
        /// <returns>An image filled with the specified gradient.</returns>
        public static Image CreateGradient(int width, int height, Gradient gradient, Axis axis = Axis.Vertical)
        {
            if (axis == Axis.Vertical)
            {
                // Evaluate gradient from top to bottom
                return CreateProcedural(width, height, co => gradient.Evaluate(co.Y / (float) height));
            }
            else
            {
                // Evaluate gradient from left to right
                return CreateProcedural(width, height, co => gradient.Evaluate(co.X / (float) width));
            }
        }

        /// <summary>
        /// Creates an image filled with a radial gradient.
        /// </summary>
        /// <param name="size">Dimensions of the image in pixels.</param>
        /// <param name="gradient">Gradient to fill the image with.</param> 
        /// <returns>An image filled with the specified gradient.</returns>
        public static Image CreateRadialGradient(IntSize size, Gradient gradient)
        {
            return CreateRadialGradient(size.Width, size.Height, gradient);
        }

        /// <summary>
        /// Creates an image filled with a radial gradient.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="gradient">Gradient to fill the image with.</param> 
        /// <returns>An image filled with the specified gradient.</returns>
        public static Image CreateRadialGradient(int width, int height, Gradient gradient)
        {
            var center = new Vector(width, height) / 2F;
            var sqrt2 = Calc.Sqrt(2);

            return CreateProcedural(width, height, co =>
            {
                // Compute normalized radial vector across image
                var tx = (co.X - center.X) / center.X;
                var ty = (co.Y - center.Y) / center.Y;

                // Compute radial magnitude and normalize
                var t = Calc.Clamp(Calc.Sqrt((tx * tx) + (ty * ty)) / sqrt2, 0F, 1F);
                return gradient.Evaluate(t);
            });
        }

        #endregion

        #region Noise

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

            // 
            scale = 1F / scale;

            // Create 4 offsets for unique "noise planes"
            var seed0 = Calc.Random.NextVector((0, 0, ushort.MaxValue, ushort.MaxValue));
            var seed1 = Calc.Random.NextVector((0, 0, ushort.MaxValue, ushort.MaxValue));
            var seed2 = Calc.Random.NextVector((0, 0, ushort.MaxValue, ushort.MaxValue));
            var seed3 = Calc.Random.NextVector((0, 0, ushort.MaxValue, ushort.MaxValue));

            return CreateProcedural(width, height, runParallel: true, generatePixel: co =>
            {
                // Compute the 4 offset positions for unique "noise planes"
                var p0 = ((Vector) co + seed0 + offset) * scale;
                var p1 = ((Vector) co + seed1 + offset) * scale;
                var p2 = ((Vector) co + seed2 + offset) * scale;
                var p3 = ((Vector) co + seed3 + offset) * scale;

                // Compute the value of each "noise plane"
                var n0 = (noise.Sample(p0, octaves, persistence) + 1F) / 2F;
                var n1 = (noise.Sample(p1, octaves, persistence) + 1F) / 2F;
                var n2 = (noise.Sample(p2, octaves, persistence) + 1F) / 2F;
                var n3 = (noise.Sample(p3, octaves, persistence) + 1F) / 2F;

                // Return the 4 "noise planes" as each color component
                return new Color(n0, n1, n2, n3);
            });
        }

        #endregion

        #endregion

        #region Copy (Static)

        /// <summary>
        /// Creates a copy of a region of some image.
        /// </summary>
        /// <param name="source">Some image</param>
        /// <param name="region">Some region within the source image.</param>
        /// <returns>A copy of the subregion of the source image as its own image.</returns>
        public static Image CreateCopy(Image source, IntRectangle region)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (region.Width <= 0 || region.Height <= 0)
            {
                throw new ArgumentException("Region size must be greater than zero.");
            }

            if (region.X < 0 || region.Y < 0 || region.Right >= source.Width || region.Bottom >= source.Height)
            {
                throw new ArgumentException("Region outside bounds of image", nameof(region));
            }

            // Copy the image
            var target = new Image(region.Size);
            Copy(source, new IntRectangle(IntVector.Zero, source.Size), target, IntVector.Zero);
            return target;
        }

        /// <summary>
        /// Copies a region of the <paramref name="source"/> image into the <paramref name="target"/> image at the specified offset.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <param name="sourceRegion">The region to copy within the source image.</param>
        /// <param name="target">The target image.</param>
        /// <param name="targetOffset">The offset in the target to copy the source rectangle to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either image is null.</exception>
        public static unsafe void Copy(Image source, IntRectangle sourceRegion,
                                       Image target, IntVector targetOffset)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            if (source == target) { throw new ArgumentException("Unable to copy from self to self."); }

            fixed (ColorBytes* sourcePtr = source.Pixels)
            fixed (ColorBytes* targetPtr = target.Pixels)
            {
                Copy(sourcePtr, source.Width, sourceRegion,
                     targetPtr, target.Width, targetOffset);

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
        public static unsafe void Copy(Image source, IntRectangle sourceRegion,
                                       ColorBytes* targetPtr, int targetWidth, IntVector targetOffset)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }

            fixed (ColorBytes* sourcePtr = source.Pixels)
            {
                Copy(sourcePtr, source.Width, sourceRegion,
                     targetPtr, targetWidth, targetOffset);
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
        public static unsafe void Copy(ColorBytes* sourcePtr, int sourceWidth, IntRectangle sourceRegion,
                                       Image target, IntVector targetOffset)
        {
            if (target is null) { throw new ArgumentNullException(nameof(target)); }

            fixed (ColorBytes* targetPtr = target.Pixels)
            {
                Copy(sourcePtr, sourceWidth, sourceRegion,
                     targetPtr, target.Width, targetOffset);

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
        public static unsafe void Copy(ColorBytes* sourcePtr, int sourceWidth, IntRectangle sourceRegion,
                                       ColorBytes* targetPtr, int targetWidth, IntVector targetOffset)
        {
            if (sourcePtr == (void*) 0) { throw new ArgumentNullException(nameof(sourcePtr)); }
            if (targetPtr == (void*) 0) { throw new ArgumentNullException(nameof(targetPtr)); }
            if (sourcePtr == targetPtr) { throw new ArgumentException("Unable to copy from self to self."); }

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

        #region Downsampling (Static)

        /// <summary>
        /// Creates a set of mipmaps by repeatedly invoking <see cref="Downsample(Image)"/> for the specified image.
        /// The returned array begins with the input image.
        /// </summary>
        public static Image[] CreateMipChain(Image image)
        {
            if (image is null) { throw new ArgumentNullException(nameof(image)); }

            var mips = new List<Image> { image };

            while (mips[^1].Width > 1 && mips[^1].Height > 1)
            {
                // Compute half-size image
                mips.Add(Downsample(mips[^1]));
            }

            return mips.ToArray();
        }

        /// <summary>
        /// Downsamples an image (half size) using a simple averaging filter.
        /// </summary>
        public static Image Downsample(Image image)
        {
            var resized = new Image(image.Width / 2, image.Height / 2);

            // Compute in parallel for each 2x2 patch
            var coordinates = Rasterizer.Rectangle(resized.Size);
            Parallel.ForEach(coordinates, AveragePool);

            return resized;

            void AveragePool(IntVector coord)
            {
                var x2 = coord.X * 2;
                var y2 = coord.Y * 2;

                var pixel = (Color) image.GetPixel(x2, y2);

                var c = Color.TransparentBlack;
                var w = 0F;

                Contribute(x2, y2);

                // If 2x2 patch does not exceed image bounds, average.
                if (x2 + 1 <= image.Width && y2 + 1 <= image.Height)
                {
                    // Average 2x2 patch
                    Contribute(x2 + 1, y2);
                    Contribute(x2 + 1, y2 + 1);
                    Contribute(x2, y2 + 1);
                }

                // Divide to compute average.
                if (w <= 0) { c = Color.TransparentBlack; }
                else { c /= w; }

                resized.SetPixel(coord, c);

                void Contribute(int x, int y)
                {
                    var pixel = (Color) image.GetPixel(x, y);

                    c += pixel * pixel.A;
                    w += pixel.A;
                }
            }
        }

        #endregion

        #region Write Encoding (Static)

        /// <summary>
        /// Writes the image to a file encoded as one of the specified formats.
        /// </summary>
        /// <param name="file">The path on disk write to disk with.</param>
        /// <param name="quality">The compression quality (0-100) for image formats that this is relevant.</param>
        public unsafe void Write(string file, int quality = 85)
        {
            using var stream = new FileStream(file, FileMode.Create);
            var extension = Path.GetExtension(file).ToLower();

            var format = extension switch
            {
                ".jpg" => ImageEncoding.Jpg,
                ".jpeg" => ImageEncoding.Jpg,
                ".png" => ImageEncoding.Png,
                _ => throw new ArgumentException($"Unknown image type '{extension}'.")
            };

            Write(stream, format, quality);
        }

        /// <summary>
        /// Writes the image to the stream encoded as one of the specified formats.
        /// </summary>
        /// <param name="stream">The stream to write the encoded image to.</param>
        /// <param name="format">The image encoding format to use.</param>
        /// <param name="quality">The compression quality (0-100) for image formats that this is relevant.</param>
        public unsafe void Write(Stream stream, ImageEncoding format, int quality = 85)
        {
            switch (format)
            {
                case ImageEncoding.Jpg:
                    WriteJPG(stream, quality);
                    break;

                case ImageEncoding.Png:
                    WritePNG(stream);
                    break;

                default:
                    throw new InvalidOperationException("Unable to write image to unkown format.");
            }

            unsafe void WritePNG(Stream stream)
            {
                if (stream is null) { throw new ArgumentNullException(nameof(stream)); }

                lock (Pixels)
                {
                    // Flip vertically!
                    stbi_flip_vertically_on_write(0);

                    fixed (ColorBytes* pPixels = Pixels)
                    {
                        if (stbi_write_png_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, Width * 4) == 0)
                        {
                            throw new InvalidOperationException("Unable to write png image to stream.");
                        }
                    }
                }
            }

            unsafe void WriteJPG(Stream stream, int quality = 85)
            {
                lock (Pixels)
                {
                    if (quality < 1 || quality > 100)
                    {
                        throw new ArgumentException("Jpeg quality must be from 1 - 100.", nameof(quality));
                    }

                    // Flip vertically!
                    stbi_flip_vertically_on_write(0);

                    fixed (ColorBytes* pPixels = Pixels)
                    {
                        if (stbi_write_jpg_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, quality) == 0)
                        {
                            throw new InvalidOperationException("Unable to write jpg image to stream.");
                        }
                    }
                }
            }

            unsafe int WriteImageCallback(void* context, void* data, int size)
            {
                if (data == null || size <= 0)
                {
                    return 0;
                }

                // Copy bytes into temporary buffer
                var buffer = ArrayPool<byte>.Shared.Rent(size);
                Marshal.Copy((IntPtr) data, buffer, 0, size);

                // Write buffer into stream
                stream.Write(buffer, 0, size);

                // Return temporary buffer
                ArrayPool<byte>.Shared.Return(buffer);

                return size;
            }
        }

        #endregion

        #region IFiniteGrid

        int IFiniteGrid<ColorBytes>.Width => Width;

        int IFiniteGrid<ColorBytes>.Height => Height;

        void IFiniteGrid<ColorBytes>.Clear(ColorBytes color)
        {
            Clear(color);
        }

        void IGrid<ColorBytes>.Clear()
        {
            Clear(Color.TransparentBlack);
        }

        bool IReadOnlyGrid<ColorBytes>.IsValidCoordinate(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        bool IReadOnlyGrid<ColorBytes>.IsValidCoordinate(IntVector co)
        {
            var grid = (IReadOnlyGrid<ColorBytes>) this;
            return grid.IsValidCoordinate(co.X, co.Y);
        }

        #endregion
    }
}
