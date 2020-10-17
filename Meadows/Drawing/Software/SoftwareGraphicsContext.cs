using System;
using System.Threading.Tasks;

using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    public sealed class SoftwareGraphicsContext : GraphicsContext
    {
        private bool _stencilEnable;
        private byte _stencilReference;
        private bool _stencilWrite;
        private bool _colorWrite = true;

        public SoftwareGraphicsContext(IntSize size)
            : base(new SoftwareScreen(size))
        {
            var softwareScreen = Screen as SoftwareScreen;
            softwareScreen.SetGraphicsContext(this);
        }

        public SoftwareGraphicsContext(int width, int height)
            : this((width, height))
        { }

        public override void Clear(Color color)
        {
            var surface = GetGlobalNativeObject<SoftwareSurface>(Surface);
            foreach (var co in Rasterizer.Rectangle(Viewport))
            {
                surface.ColorBuffer.Set(co, color);
            }
        }

        public override void Draw(Texture texture, Rectangle uvRegion, Mesh mesh, Matrix matrix)
        {
            var softwareSurface = GetGlobalNativeObject<SoftwareSurface>(Surface);
            var softwareTexture = GetGlobalNativeObject<ISoftwareTexture>(texture);

            // Compute final transformation matrix
            var transform = Matrix.RectangleProjection(0, 0, Viewport.Width, Viewport.Height);
            Matrix.Multiply(transform, CameraMatrix, ref transform);
            Matrix.Multiply(transform, matrix, ref transform);

            // For each triangle of the mesh
            for (var i = 0; i < mesh.Vertices.Count; i += 3)
            {
                // Triangle vertices
                var a = mesh.Vertices[i + 0];
                var b = mesh.Vertices[i + 1];
                var c = mesh.Vertices[i + 2];

                // note: this is the vertex shader phase
                var va = MapToViewport(transform * a.Position);
                var vb = MapToViewport(transform * b.Position);
                var vc = MapToViewport(transform * c.Position);

                // Rasterize the triangle
                Parallel.ForEach(Rasterizer.Triangle(va, vb, vc), co =>
                {
                    // Only process pixel if within the viewport.
                    // This mimics viewport + scissor in GL.
                    if (Viewport.Contains(co))
                    {
                        var stencilPass = _stencilReference == softwareSurface.StencilBuffer.GetValue(co);

                        // Stencil pass (same as reference)
                        if (_stencilWrite || (_stencilEnable && stencilPass) || !_stencilEnable)
                        {
                            // Compute interpolation factor
                            Triangle.Barycentric(co, va, vb, vc, out var wa, out var wb, out var wc);

                            // Compute interpolated UV and Color
                            var uv = (wa * a.UV) + (wb * b.UV) + (wc * c.UV);
                            var color = (wa * a.Color) + (wb * b.Color) + (wc * c.Color);

                            // Sample texture (this is the fragment shader phase)
                            color *= softwareTexture.Sample(uv, InterpolationMode, texture.Repeat);
                            color *= Color;

                            // Discard alpha values too small
                            if (color.A < 0.005F) { return; }

                            // Skip writing color
                            if (_colorWrite)
                            {
                                // Apply pixel/fragment to surface
                                var prior = softwareSurface.ColorBuffer.Get(co);
                                softwareSurface.ColorBuffer.Set(co, BlendColor(prior, color));
                            }
                        }

                        if (_stencilWrite)
                        {
                            // Write into stencil buffer
                            softwareSurface.StencilBuffer.SetValue(co, _stencilReference);
                        }
                    }
                });

                //// Draw wireframe of triangle
                //foreach (var co in Rasterizer.Line(va, vb)) { if (Viewport.Contains(co)) { softwareSurface.ColorBuffer.Set(co, Color.Red); } }
                //foreach (var co in Rasterizer.Line(vb, vc)) { if (Viewport.Contains(co)) { softwareSurface.ColorBuffer.Set(co, Color.Red); } }
                //foreach (var co in Rasterizer.Line(vc, va)) { if (Viewport.Contains(co)) { softwareSurface.ColorBuffer.Set(co, Color.Red); } }
            }

            IntVector MapToViewport(Vector vector)
            {
                vector = (vector + Vector.One) / 2F;
                vector = Viewport.Position + (vector * (Vector) Viewport.Size);
                return (IntVector) Vector.Floor(vector);
            }

            Color BlendColor(Color dst, Color src)
            {
                switch (BlendingMode)
                {
                    case BlendingMode.Opaque:
                    {
                        var srcF = 1F;
                        var dstF = 0F;

                        return Additive(dst, src, srcF, dstF, srcF, dstF);
                    }

                    case BlendingMode.Alpha:
                    {
                        var cSrc = src.A;
                        var cDst = 1F - src.A;
                        var aSrc = 1F;
                        var aDst = 1F - src.A;

                        return Additive(dst, src, cSrc, cDst, aSrc, aDst);
                    }

                    case BlendingMode.Additive:
                    {
                        var srcF = src.A;
                        var dstF = 1F;

                        return Additive(dst, src, srcF, dstF, srcF, dstF);
                    }

                    case BlendingMode.Subtractive:
                    {
                        var srcF = src.A;
                        var dstF = 1F;

                        return ReverseSubtractive(dst, src, srcF, dstF, srcF, dstF);
                    }

                    case BlendingMode.Multiply:
                    {
                        // todo: is this correct?
                        // Might not be correct in OpenGL/Heirloom either!
                        return (src * dst) + ((1F - src.A) * dst);
                    }

                    case BlendingMode.Invert:
                    {
                        var cSrc = 1F;
                        var cDst = 1F;
                        var aSrc = 1F;
                        var aDst = 0F;

                        return Subtractive(dst, src, cSrc, cDst, aSrc, aDst);
                    }

                    default:
                        throw new InvalidOperationException("Unable to blend colors, unknown blending mode set.");
                }

                static Color Additive(Color dst, Color src, float cSrc, float cDst, float aSrc, float aDst)
                {
                    var r = (src.R * cSrc) + (dst.R * cDst);
                    var g = (src.G * cSrc) + (dst.G * cDst);
                    var b = (src.B * cSrc) + (dst.B * cDst);
                    var a = (src.A * aSrc) + (dst.A * aDst);
                    return new Color(r, g, b, a);
                }

                static Color Subtractive(Color dst, Color src, float cSrc, float cDst, float aSrc, float aDst)
                {
                    var r = (src.R * cSrc) - (dst.R * cDst);
                    var g = (src.G * cSrc) - (dst.G * cDst);
                    var b = (src.B * cSrc) - (dst.B * cDst);
                    var a = (src.A * aSrc) - (dst.A * aDst);
                    return new Color(r, g, b, a);
                }

                static Color ReverseSubtractive(Color dst, Color src, float cSrc, float cDst, float aSrc, float aDst)
                {
                    var r = (dst.R * cDst) - (src.R * cSrc);
                    var g = (dst.G * cDst) - (src.G * cSrc);
                    var b = (dst.B * cDst) - (src.B * cSrc);
                    var a = (dst.A * aDst) - (src.A * aSrc);
                    return new Color(r, g, b, a);
                }
            }
        }

        public override void ClearStencil()
        {
            _stencilWrite = false;
            _stencilEnable = false;
            _colorWrite = true;
        }

        public override void BeginStencil()
        {
            _stencilEnable = true;
            _stencilWrite = true;
            _colorWrite = false;

            // Update stencil reference number
            _stencilReference++;
            if (_stencilReference == byte.MaxValue)
            {
                _stencilReference = 0;
            }
        }

        public override void EndStencil()
        {
            _stencilWrite = false;
            _colorWrite = true;
        }

        public override Image GrabPixels(IntRectangle region)
        {
            // todo: validate region will indeed fit in region
            var surface = GetGlobalNativeObject<SoftwareSurface>(Surface);
            return surface.ColorBuffer.GrabPixels(region);
        }

        protected override void SwapBuffers()
        {
            throw new NotImplementedException("Unable to swap buffers of a software screen.");
        }

        protected override void Flush(bool blockCompletion = false)
        {
            // Nothing to do, all drawing happens immediately in this software context.
            // Just carry on!
        }

        protected override object GenerateContextNativeObject(NativeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override object GenerateGlobalNativeObject(NativeResource resource)
        {
            switch (resource)
            {
                case Surface surface:
                    return new SoftwareSurface(surface);

                case Image image:
                    return new SoftwareImage(image);

                default:
                    throw new InvalidOperationException($"Unable to generate native reresentation of {resource}");
            }
        }

        #region Software Implementation Resources

        private sealed class SoftwareScreen : Screen
        {
            private SoftwareGraphicsContext _graphicsContext;

            public SoftwareScreen(IntSize size)
                : base(size, MultisampleQuality.None)
            {
                // todo: potentially dummy "null" input devices?
            }

            public void SetGraphicsContext(SoftwareGraphicsContext graphics)
            {
                _graphicsContext = graphics;
            }

            public override GraphicsContext Graphics => _graphicsContext;

            public override KeyboardDevice Keyboard { get; }

            public override MouseDevice Mouse { get; }

            public override GamepadDevice Gamepad { get; }

            public override TouchDevice Touch { get; }
        }

        public interface ISoftwareTexture
        {
            Color Sample(Vector uv, InterpolationMode interpolation, RepeatMode repeat);
        }

        private sealed class SoftwareImage : ISoftwareTexture
        {
            public readonly Image Image;

            public SoftwareImage(Image image)
            {
                Image = image;
            }

            public Color Sample(Vector uv, InterpolationMode interpolation, RepeatMode repeat)
            {
                return Image.Sample(uv, interpolation, repeat, true);
            }
        }

        private sealed class SoftwareSurface : ISoftwareTexture
        {
            public readonly Surface Surface;

            public readonly IColorBuffer ColorBuffer;

            public readonly StencilBuffer StencilBuffer;

            public SoftwareSurface(Surface surface)
            {
                Surface = surface;
                StencilBuffer = new StencilBuffer(surface.Size);
                ColorBuffer = CreateColorBuffer(surface);
            }

            private static IColorBuffer CreateColorBuffer(Surface texture)
            {
                IColorBuffer buffer;
                if (texture.Format == SurfaceFormat.UnsignedByte)
                {
                    buffer = new UByteColorBuffer(texture.Width, texture.Height);
                }
                else
                {
                    buffer = new FloatColorBuffer(texture.Width, texture.Height);
                }

                return buffer;
            }

            public Color Sample(Vector uv, InterpolationMode interpolation, RepeatMode repeat)
            {
                var pos = (Vector) Surface.Size * uv;

                if (interpolation == InterpolationMode.Nearest)
                {
                    return ColorBuffer.Get((IntVector) Vector.Floor(pos));
                }
                else
                {
                    var co = (IntVector) Vector.Floor(pos);
                    var fr = Vector.Fraction(pos);

                    var c00 = ColorBuffer.Get(co + (0, 0));
                    var c10 = ColorBuffer.Get(co + (1, 0));
                    var c01 = ColorBuffer.Get(co + (0, 1));
                    var c11 = ColorBuffer.Get(co + (1, 1));

                    var c0 = Color.Lerp(c00, c10, fr.X);
                    var c1 = Color.Lerp(c01, c11, fr.X);

                    return Color.Lerp(c0, c1, fr.Y);
                }
            }
        }

        #region Surface Buffers

        public interface IColorBuffer
        {
            Color Get(IntVector co);

            void Set(IntVector co, Color color);

            Image GrabPixels(IntRectangle region);
        }

        private sealed class UByteColorBuffer : IColorBuffer
        {
            public Image Image;

            public UByteColorBuffer(int width, int height)
            {
                Image = new Image(width, height);
            }

            public Color Get(IntVector co)
            {
                return Image.GetPixel(co);
            }

            public void Set(IntVector co, Color color)
            {
                // Clamp (saturate)
                // todo: does this mimic GL properly?
                color.R = Calc.Clamp(color.R, 0F, 1F);
                color.G = Calc.Clamp(color.G, 0F, 1F);
                color.B = Calc.Clamp(color.B, 0F, 1F);
                color.A = Calc.Clamp(color.A, 0F, 1F);

                // 
                Image.SetPixel(co, color);
            }

            public Image GrabPixels(IntRectangle region)
            {
                var output = new Image(region.Size);
                Image.CopyTo(region, output, IntVector.Zero);
                return output;
            }
        }

        private sealed class FloatColorBuffer : IColorBuffer
        {
            public Color[,] Colors;

            public FloatColorBuffer(int width, int height)
            {
                Colors = new Color[height, width];
            }

            public Color Get(IntVector co)
            {
                return Colors[co.Y, co.X];
            }

            public void Set(IntVector co, Color color)
            {
                Colors[co.Y, co.X] = color;
            }

            public Image GrabPixels(IntRectangle region)
            {
                var output = new Image(region.Size);
                foreach (var co in Rasterizer.Rectangle(region))
                {
                    output.SetPixel(co - region.Position, Colors[co.Y, co.X]);
                }
                return output;
            }
        }

        private sealed class StencilBuffer
        {
            private readonly byte[,] _byte;

            public StencilBuffer(IntSize size)
            {
                _byte = new byte[size.Height, size.Width];
            }

            public byte GetValue(IntVector co)
            {
                return _byte[co.Y, co.X];
            }

            public void SetValue(IntVector co, byte value)
            {
                _byte[co.Y, co.X] = value;
            }
        }

        #endregion

        #endregion
    }
}
