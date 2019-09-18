using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Heirloom.Drawing.Utilities;
using Heirloom.Math;

using StbSharp;

namespace Heirloom.Drawing
{
    public sealed partial class Image : ImageSource
    {
        private Pixel[] _pixels;

        private Image _source;
        private Image _root;

        #region Constants

        public static Image White = CreateColor(1, 1, Pixel.White);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new image with the given dimensions.
        /// </summary>
        /// <param name="size">Dimensions in pixels.</param>
        public Image(IntSize size)
            : this(size.Width, size.Height)
        { }

        /// <summary>
        /// Constructs a new image with the given dimensions.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        public Image(int width, int height)
            : this(null, new IntRectangle(0, 0, width, height), false, false)
        { }

        /// <summary>
        /// Constructs an image backed by a region of the given image. <para/>
        /// If <paramref name="clone"/> is true, this will fully duplicate image data from the specified region of the given image.
        /// </summary>
        /// <param name="source">The base image.</param>
        /// <param name="region"></param>
        /// <param name="clone"></param>
        public Image(Image source, IntRectangle region, bool clone = false)
            : this(source, region, clone, true)
        { }

        internal Image(Image source, IntRectangle region, bool clone, bool noSourceNoCloneException)
        {
            // Constraint disallowing using the (source,region,clone) constructor to mimic
            // the (width, height) constructor.
            if (noSourceNoCloneException && source == null && !clone)
            {
                throw new ArgumentException();
            }

            // Region must be positioned at zero when not sourced from another image, because
            // we will occupy the complete pixel region as base image.
            if (source == null && (Region.X != 0 || Region.Y != 0))
            {
                throw new ArgumentException("Creating an non-subimage image with offset in region.");
            }

            // Unable to clone image from nothing
            if (source == null && clone)
            {
                throw new ArgumentNullException(nameof(source), "Unable to create clone image from null base image");
            }

            // Set region and source
            Source = clone ? null : source;
            Region = region;

            // 
            ComputeUVRectangle();

            // If we are cloning data or not sourcing an image, create data.
            if (clone || source == null)
            {
                // Create pixel data
                _pixels = new Pixel[Width * Height];

                if (clone)
                {
                    // Copies data from source into this image
                    foreach (var co in Rasterizer.Rectangle(region))
                    {
                        SetPixel(co, source.GetPixel(co - region.Position));
                    }
                }

            }
        }

        public Image(Stream stream)
            : this(ReadAllBytes(stream))
        { }

        public unsafe Image(byte[] file)
        {
            fixed (byte* buffer = file)
            {
                // Decode from file to raw RGBA bytes
                int width, height, comp;
                var pResult = Stb.stbi_load_from_memory(buffer, file.Length, &width, &height, &comp, 4);

                // Copy from unmanaged to managed
                var pixels = new byte[width * height * 4];
                Marshal.Copy((IntPtr) pResult, pixels, 0, pixels.Length);

                // Free stb bitmap
                Stb.stbi_image_free(pResult);

                // Fully occupied and no parenting image
                _pixels = new Pixel[width * height];
                Region = new IntRectangle(0, 0, width, height);
                Source = null;

                // Allocate pixel storage, and set from decoded pixels
                SetPixels(pixels);

                // 
                ComputeUVRectangle();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        public int Width => Region.Width;

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public int Height => Region.Height;

        /// <summary>
        /// The size of the image in pixels.
        /// </summary>
        public override IntSize Size
        {
            get => Region.Size;
            protected set => throw new InvalidOperationException();
        }

        /// <summary>
        /// The images aspect ratio.
        /// </summary>
        public float Aspect => Width / (float) Height;

        /// <summary>
        /// This is image instance a subregion of another?
        /// </summary>
        public bool IsSubImage => Source != null;

        /// <summary>
        /// The region this image occupies in the root image.
        /// </summary>
        public IntRectangle Region { get; private set; }

        /// <summary>
        /// The normalized region this image occupies in the root image.
        /// </summary>
        public Rectangle UVRect { get; private set; }

        /// <summary>
        /// The source image where this image object references its pixels from.
        /// If null, this image actually contains its own pixel data.
        /// </summary>
        /// TODO: Make it reference itself if self-contained?
        public Image Source
        {
            get => _source;

            private set
            {
                // Assign source
                _source = value;

                // No source (image is self contained)
                if (_source == null) { _root = this; }
                // Has a source, so walk up to the true root
                else
                {
                    // Assign root
                    _root = _source;

                    // Walk to root image
                    while (_root._source != null)
                    {
                        _root = _root._source;
                    }
                }
            }
        }

        /// <summary>
        /// The root source image where this image data is stored.
        /// This is the top reference in a chain of <see cref="Source"/> links.
        /// </summary>
        public Image Root => _root;

        #endregion

        #region Indexers

        public Pixel this[int x, int y]
        {
            get => GetPixel(x, y);
            set => SetPixel(x, y, value);
        }

        public Pixel this[IntVector coord]
        {
            get => GetPixel(coord);
            set => SetPixel(coord, value);
        }

        #endregion

        #region Access / Mutators

        /// <summary>
        /// Sets all pixels in the image to the specified color.
        /// </summary>
        public unsafe void Clear(Pixel pixel)
        {
            if (IsSubImage)
            {
                // 
                foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                {
                    SetPixel(co, pixel);
                }
            }
            else
            {
                fixed (Pixel* ptr = _pixels)
                {
                    // Fill entire data buffer
                    for (var i = 0; i < _pixels.Length; i++)
                    {
                        *(ptr + i) = pixel;
                    }
                }
            }

            UpdateVersionNumber();
        }

        #region Get/SetPixel

        /// <summary>
        /// Get some pixel within the image.
        /// </summary>
        public Pixel GetPixel(IntVector coord)
        {
            return GetPixel(coord.X, coord.Y);
        }

        /// <summary>
        /// Set some pixel within the image.
        /// </summary>
        public void SetPixel(IntVector coord, Pixel pixel)
        {
            SetPixel(coord.X, coord.Y, pixel);
        }

        /// <summary>
        /// Get some pixel within the image.
        /// </summary>
        public Pixel GetPixel(int x, int y)
        {
            if (IsSubImage)
            {
                // TODO: Validate coordinate within region
                return Source.GetPixel(Region.X + x, Region.Y + y);
            }
            else
            {
                if (x >= Width || y >= Height)
                {
                    return Pixel.Black;
                }

                if (x < 0 || y < 0)
                {
                    return Pixel.Black;
                }

                // TODO: Validate coordinate within image
                return _pixels[(y * Width) + x];
            }
        }

        /// <summary>
        /// Set some pixel within the image.
        /// </summary>
        public void SetPixel(int x, int y, Pixel pixel)
        {
            // 
            if (IsSubImage)
            {
                // TODO: Validate coordinate within region
                Source.SetPixel(Region.X + x, Region.Y + y, pixel);
            }
            else
            {
                if (x >= Width || y >= Height)
                {
                    return;
                }

                if (x < 0 || y < 0)
                {
                    return;
                }

                // TODO: Validate coordinate within image
                _pixels[(y * Width) + x] = pixel;
            }

            // 
            UpdateVersionNumber();
        }

        /// <summary>
        /// Replace all pixels in this image.
        /// </summary>
        public unsafe void SetPixels(Pixel[] pixels)
        {
            if (pixels.Length != (Width * Height))
            {
                throw new ArgumentException("Must specify same number of pixels as image.");
            }

            if (IsSubImage)
            {
                // TODO: Can probably optimize with processing pointers + stride
                foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                {
                    var i = (co.Y * Width) + co.X;
                    SetPixel(Region.Min + co, pixels[i]);
                }
            }
            else
            {
                fixed (Pixel* src = pixels)
                fixed (Pixel* dst = _pixels)
                {
                    var len = _pixels.Length * 4;
                    Buffer.MemoryCopy((void*) src, (void*) dst, len, len);
                    UpdateVersionNumber();
                }
            }
        }

        /// <summary>
        /// Replace all pixels in this image, assumes uints are RGBA encoded.
        /// </summary>
        public unsafe void SetPixels(uint[] pixels)
        {
            if (pixels.Length != (Width * Height))
            {
                throw new ArgumentException("Must specify same number of pixels as image.");
            }

            if (IsSubImage)
            {
                fixed (uint* pPixels = pixels)
                {
                    // TODO: Can probably optimize with processing pointers + stride
                    foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                    {
                        var i = (co.Y * Width) + co.X;

                        // 
                        var addr = (Pixel*) &pPixels[i];
                        SetPixel(Region.Min + co, *addr);
                    }
                }
            }
            else
            {
                fixed (uint* src = pixels)
                fixed (Pixel* dst = _pixels)
                {
                    Buffer.MemoryCopy((void*) src, (void*) dst, 4 * pixels.Length, 4 * pixels.Length);
                    UpdateVersionNumber();
                }
            }
        }

        /// <summary>
        /// Replace all pixels in this image, assumes bytes are 8-bit interleved RGBA encoded and correct dimensions.
        /// </summary>
        public unsafe void SetPixels(byte[] pixels)
        {
            if (pixels.Length != (4 * Width * Height))
            {
                throw new ArgumentException("Must specify same number of pixels as image.");
            }

            if (IsSubImage)
            {
                fixed (byte* pPixels = pixels)
                {
                    // TODO: Can probably optimize with processing pointers + stride
                    foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                    {
                        var i = (co.Y * Width) + co.X;

                        // 
                        var addr = (Pixel*) &pPixels[i * 4];
                        SetPixel(Region.Min + co, *addr);
                    }
                }
            }
            else
            {
                fixed (byte* src = pixels)
                fixed (Pixel* dst = _pixels)
                {
                    Buffer.MemoryCopy((void*) src, (void*) dst, pixels.Length, pixels.Length);
                    UpdateVersionNumber();
                }
            }
        }

        /// <summary>
        /// Returns a copy of the pixels in this image.
        /// </summary>
        public Pixel[] GetPixels()
        {
            if (IsSubImage)
            {
                var pixels = new Pixel[Width * Height];

                // TODO: Can probably optimize with processing pointers + stride
                foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                {
                    var i = (co.Y * Width) + co.X;
                    pixels[i] = GetPixel(Region.Min + co);
                }

                return pixels;
            }
            else
            {
                var pixels = new Pixel[_pixels.Length];
                Array.Copy(_pixels, pixels, _pixels.Length);
                return pixels;
            }
        }

        #endregion

        #region Sample

        /// <summary>
        /// Samples the image via normalized coordinates.
        /// </summary>
        public Color Sample(Vector uv, SampleMode mode = SampleMode.Linear)
        {
            return Sample(uv.X, uv.Y, mode);
        }

        /// <summary>
        /// Samples the image via normalized coordinates.
        /// </summary>
        public Color Sample(float u, float v, SampleMode mode = SampleMode.Linear)
        {
            switch (mode)
            {
                default:
                case SampleMode.Point:
                    return SamplePoint(u, v);

                case SampleMode.Linear:
                    return SampleLinear(u, v);
            }
        }

        private Color SamplePoint(float u, float v)
        {
            var xf = u * Width;
            var yf = v * Height;

            // Bilinear interpolation
            var x1 = Calc.Floor(xf);
            var y1 = Calc.Floor(yf);

            // Read pixels
            return (Color) GetPixel(x1, y1);
        }

        private Color SampleLinear(float u, float v)
        {
            var xf = u * Width;
            var yf = v * Height;

            var xt = Calc.Fraction(xf);
            var yt = Calc.Fraction(yf);

            // Bilinear interpolation
            var x1 = Calc.Floor(xf);
            var y1 = Calc.Floor(yf);
            var x2 = Calc.Ceil(xf);
            var y2 = Calc.Ceil(yf);

            // Read pixels
            var c00 = GetPixel(x1, y1);
            var c10 = GetPixel(x2, y1);
            var c11 = GetPixel(x2, y2);
            var c01 = GetPixel(x1, y2);

            // 
            var q1 = Pixel.Lerp(c00, c10, xt);
            var q2 = Pixel.Lerp(c01, c11, xt);

            return (Color) Pixel.Lerp(q1, q2, yt);
        }

        #endregion

        #endregion

        #region Copy Data

        /// <summary>
        /// Copies pixel data to the image.
        /// The data must be contiguous and the same size of the image.
        /// </summary>
        public unsafe void CopyFrom(Pixel* src, bool swapBGRA = true)
        {
            var len = Width * Height * sizeof(Pixel);

            if (IsSubImage)
            {
                foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                {
                    var i = (co.Y * Width) + co.X;
                    SetPixel(Region.Min + co, src[i]);
                }
            }
            else
            {
                fixed (Pixel* dst = _pixels)
                {
                    if (swapBGRA)
                    {
                        // Copy per-pixel (RGBA)
                        for (var i = 0; i < len / sizeof(Pixel); i++)
                        {
                            var p = src[i];
                            if (swapBGRA) { Calc.Swap(ref p.R, ref p.B); }
                            dst[i] = p;
                        }
                    }
                    else
                    {
                        // Copy block (BGRA)
                        Buffer.MemoryCopy(src, dst, len, len);
                    }
                }
            }
        }

        /// <summary>
        /// Copies pixel data to the image.
        /// The data must be contiguous and the same size of the image.
        /// </summary>
        public unsafe void CopyFrom(IntPtr src, bool swapBGRA = true)
        {
            CopyFrom((Pixel*) src, swapBGRA);
        }

        /// <summary>
        /// Copies image data from the image to destination.
        /// The data must be contiguous and the same size of the image.
        /// </summary>
        public unsafe void CopyTo(IntPtr dst, bool swapBGRA = true)
        {
            CopyTo((Pixel*) dst, swapBGRA);
        }

        /// <summary>
        /// Copies image data from the image to destination.
        /// The data must be contiguous and the same size of the image.
        /// </summary>
        public unsafe void CopyTo(Pixel* dst, bool swapBGRA = true)
        {
            var len = Width * Height * 4; // Number of bytes in the pixel data

            if (IsSubImage)
            {
                foreach (var co in Rasterizer.Rectangle(0, 0, Width, Height))
                {
                    var i = (co.Y * Width) + co.X;
                    var p = GetPixel(Region.Min + co);
                    *(dst + i) = p;
                }
            }
            else
            {
                fixed (Pixel* src = _pixels)
                {
                    if (swapBGRA)
                    {
                        // Copy per-pixel (RGBA)
                        for (var i = 0; i < len / sizeof(Pixel); i++)
                        {
                            var p = *(src + i);
                            if (swapBGRA) { Calc.Swap(ref p.R, ref p.B); }
                            *(dst + i) = p;
                        }
                    }
                    else
                    {
                        // Copy block (BGRA)
                        Buffer.MemoryCopy(src, (void*) dst, len, len);
                    }
                }
            }
        }

        #endregion

        #region Write

        [ThreadStatic] private static Stream _writeStream;
        [ThreadStatic] private static byte[] _buffer = new byte[0];

        public unsafe void WriteToPng(Stream stream)
        {
            lock (_pixels)
            {
                _writeStream = stream;

                // StbSharp is out of date...?
                // Stb.stbi_flip_vertically_on_write(1);

                fixed (Pixel* pPixels = _pixels)
                {
                    if (Stb.stbi_write_png_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, Width * 4) == 0)
                    {
                        throw new InvalidOperationException("Unable to write png image to stream.");
                    }
                }
            }
        }

        public unsafe void WriteToJpg(Stream stream, int quality = 80)
        {
            lock (_pixels)
            {
                if (quality < 1 || quality > 100)
                {
                    throw new ArgumentException("Jpeg quality must be from 1 - 100.", nameof(quality));
                }

                _writeStream = stream;

                // StbSharp is out of date...?
                // Stb.stbi_flip_vertically_on_write(1);

                fixed (Pixel* pPixels = _pixels)
                {
                    if (Stb.stbi_write_jpg_to_func(WriteImageCallback, null, Width, Height, 4, pPixels, quality) == 0)
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

            // Ensure buffer is large enough
            if (_buffer.Length < size) { Array.Resize(ref _buffer, size * 2); }

            // Copy from unmanaged memory into buffer and then into stream
            Marshal.Copy((IntPtr) data, _buffer, 0, size);
            _writeStream.Write(_buffer, 0, size);

            return size;
        }

        #endregion

        #region Atlas Utilities

        /// <summary>
        /// Inserts the given image into the region of this image.
        /// The given image data is subsequently backed by this image.
        /// This is a feature to construct or create texture atlases.
        /// </summary>
        /// <seealso cref="CreateAtlas(IEnumerable{Image})"/>
        public void Insert(Image image, IntVector position)
        {
            if (image == null) { throw new ArgumentNullException(nameof(image)); }

            // Copy pixels from image (other) to atlas (self)
            foreach (var co in Rasterizer.Rectangle(image.Size))
            {
                SetPixel(position + co, image.GetPixel(co));
            }

            // Assign image as new sub image
            image._pixels = null; // Clear buffer, now backed by the base image
            image.Region = new IntRectangle(position, image.Size);
            image.Source = this;

            image.ComputeUVRectangle();
        }

        /// <summary>
        /// Combines the given input into a new atlas image, reassigning each image's source image to be the atlas.
        /// </summary>
        public static Image CreateAtlas(IEnumerable<Image> images)
        {
            var packer = new RectanglePacker<Image>();

            // Pack Images
            foreach (var image in images)
            {
                packer.Insert(image, image.Size);
            }

            // Create image (atlas)
            var atlas = new Image(packer.Bounds.Width, packer.Bounds.Height);
            atlas.Clear(Pixel.Red);

            // Insert images into atlas
            foreach (var image in packer.Keys)
            {
                // Copies the image into the atlas, removing its personal data
                // The image is now a sub-image of the atlas.
                var region = packer.GetRectangle(image);
                atlas.Insert(image, region.Position);
            }

            return atlas;
        }

        private void ComputeUVRectangle()
        {
            if (Source == null)
            {
                // Full image
                UVRect = (0F, 0F, 1F, 1F);
            }
            else
            {
                // Compute uv rectangle
                const float edgeErrorN = 0.1F;
                const float edgeErrorF = 2 * edgeErrorN;

                // 
                UVRect = new Rectangle
                {
                    X = (Region.X + edgeErrorN) / _root.Size.Width,
                    Y = (Region.Y + edgeErrorN) / _root.Size.Height,
                    Width = (Region.Width - edgeErrorF) / _root.Size.Width,
                    Height = (Region.Height - edgeErrorF) / _root.Size.Height
                };
            }
        }

        #endregion

        #region Split / Slice / Sprite Sheet Utilities

        /// <summary>
        /// Splits the image into cells of the specified size.
        /// The image dimensions must match perfectly.
        /// </summary>
        public Image[] SplitByCells(int width, int height)
        {
            if (Width % width != 0 || Height % height != 0)
            {
                throw new ArgumentException("Unable to split image, grid size mismatch.");
            }

            var wc = Width / width;
            var hc = Height / height;

            var images = new Image[wc * hc];
            for (var y = 0; y < hc; y++)
            {
                for (var x = 0; x < wc; x++)
                {
                    images[x + y * width] = new Image(this, new IntRectangle(x * width, y * height, width, height));
                }
            }

            return images;
        }

        /// <summary>
        /// Splits the image into segments along the given axis.
        /// The segments must divide the image perfectly.
        /// </summary>
        public Image[] SplitBySegments(int segments, Axis axis)
        {
            // 
            if (segments <= 1) { throw new ArgumentException("Unable to split image, must specify more than one segment."); }

            // 
            var images = new Image[segments];

            // 
            if (axis == Axis.Horizontal)
            {
                if (Width % segments != 0)
                {
                    throw new ArgumentException("Unable to split image horizontally, segments must divide image perfectly.");
                }

                var width = Width / segments;
                for (var i = 0; i < segments; i++)
                {
                    images[i] = new Image(this, new IntRectangle(i * width, 0, width, Height));
                }
            }
            else
            {
                if (Height % segments != 0)
                {
                    throw new ArgumentException("Unable to split image vetically, segments must divide image perfectly.");
                }

                var height = Height / segments;
                for (var i = 0; i < segments; i++)
                {
                    images[i] = new Image(this, new IntRectangle(0, i * height, height, Height));
                }
            }

            return images;
        }

        #endregion

        #region Procedural Textures

        /// <summary>
        /// Create an image with checkerboard pattern.
        /// </summary>
        public static Image CreateCheckerboardPattern(int width, int height, Pixel color, int cellSize = 16)
        {
            var im = new Image(width, height);

            // Compute checkerboard texture
            foreach (var p in Rasterizer.Rectangle(0, 0, im.Width, im.Height))
            {
                var flag = ((p.Y & cellSize) == 0) ^ (p.X & cellSize) == 0;
                var pixel = (flag ? Pixel.LightGray : Pixel.White) * color;
                im.SetPixel(p.X, p.Y, pixel);
            }

            // Draw border
            foreach (var edge in Rasterizer.RectangleOutline(0, 0, width, height))
            {
                var pixel = Pixel.Gray * color;
                im.SetPixel(edge.X, edge.Y, pixel);
            }

            return im;
        }

        /// <summary>
        /// Creates an image with a radial gradient.
        /// </summary>
        public static Image CreateRadialGradient(IntSize size, Color inner, Color outer)
        {
            var image = new Image(size);
            foreach (var co in Rasterizer.Rectangle(image.Size))
            {
                var nco = (co / (Vector) size * 2F) - Vector.One;
                var distance = Vector.Distance(nco, Vector.Zero);
                if (distance <= 1) { image.SetPixel(co, (Pixel) Color.Lerp(inner, outer, distance)); }
                else { image.SetPixel(co, (Pixel) outer); }
            }
            return image;
        }

        /// <summary>
        /// Creates an image with a radial gradient.
        /// </summary>
        public static Image CreateCircle(int size, Color color, Color edge, float edgeWidth = 1)
        {
            var image = new Image(size, size);

            foreach (var co in Rasterizer.Rectangle(image.Size))
            {
                var normCo = ((Vector) co / size * 2F) - Vector.One;
                var distance = Vector.Distance(normCo, Vector.Zero);

                if (distance <= 1)
                {
                    if (distance >= 1 - (edgeWidth / size))
                    {
                        // Set edge color
                        image.SetPixel(co, (Pixel) edge);
                    }
                    else
                    {
                        // Set fill color
                        image.SetPixel(co, (Pixel) color);
                    }
                }
                else
                {
                    // Outside circle
                    image.SetPixel(co, Pixel.Transparent);
                }
            }

            return image;
        }

        /// <summary>
        /// Creates an image filled with a solid color.
        /// </summary
        public static Image CreateColor(int width, int height, Pixel color)
        {
            var im = new Image(width, height);

            // Draw border
            foreach (var p in Rasterizer.Rectangle(0, 0, width, height))
            {
                im.SetPixel(p.X, p.Y, color);
            }

            return im;
        }

        /// <summary>
        /// Creates an image filled with noise.
        /// </summary
        public static Image CreateNoise(int width, int height, INoise2D noise, float scale, int octaves = 4, float persistence = 0.5F)
        {
            var im = new Image(width, height);

            // 
            scale = 1F / scale;

            // Draw border
            Parallel.ForEach(Rasterizer.Rectangle(0, 0, width, height), co =>
            {
                var p0 = ((Vector) co + new Vector(0, 0)) * scale;
                var p1 = ((Vector) co + new Vector(10000, 0)) * scale;
                var p2 = ((Vector) co + new Vector(0, 10000)) * scale;
                var p3 = ((Vector) co + new Vector(10000, 10000)) * scale;

                var n0 = (noise.Sample(p0, octaves, persistence) + 1F) / 2F;
                var n1 = (noise.Sample(p1, octaves, persistence) + 1F) / 2F;
                var n2 = (noise.Sample(p2, octaves, persistence) + 1F) / 2F;
                var n3 = (noise.Sample(p3, octaves, persistence) + 1F) / 2F;

                // 
                var color = (Pixel) new Color(n0, n1, n2, n3);
                im.SetPixel(co.X, co.Y, color);
            });

            return im;
        }

        #endregion

        private static byte[] ReadAllBytes(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
