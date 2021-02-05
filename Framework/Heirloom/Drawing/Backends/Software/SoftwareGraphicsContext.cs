using System;
using System.Threading.Tasks;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.Software
{
    public sealed class SoftwareGraphicsContext : GraphicsContext
    {
        private bool _stencilEnable;
        private byte _stencilReference;
        private bool _stencilWrite;
        private bool _colorWrite = true;

        internal SoftwareGraphicsContext(SoftwareGraphicsBackend backend, IntSize size)
            : base(backend, new SoftwareScreen(size))
        {
            if (!(GraphicsBackend.Current is SoftwareGraphicsBackend))
            {
                throw new InvalidOperationException("A software graphics context is only allowed with the software backend.");
            }

            // Associate this context with the virtual screen
            var softwareScreen = Screen as SoftwareScreen;
            softwareScreen.SetGraphicsContext(this);

            // Ready to go immediately
            Initialize();
        }

        protected override bool HasPendingWork => false;

        public override void Clear(Color color)
        {
            var surface = Backend.GetNativeObject<SoftwareSurface>(Surface);
            foreach (var co in Rasterizer.Rectangle(Viewport))
            {
                surface.ColorBuffer.Set(co, color);
            }
        }

        public override void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix matrix)
        {
            var softwareSurface = Backend.GetNativeObject<SoftwareSurface>(Surface);
            var softwareTexture = Backend.GetNativeObject<ISoftwareTexture>(texture);

            // Compute final transformation matrix
            var transform = Matrix.RectangleProjection(0, 0, Viewport.Width, Viewport.Height);
            Matrix.Multiply(transform, CompositeMatrix, ref transform);
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
                            color *= softwareTexture.Sample(uv, texture.Interpolation, texture.Repeat);
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
                        // todo: Validate blending mode truly mimic the OpenGL behaviour
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

        public override void SetUniform<T>(string name, T value)
        {
            throw new NotImplementedException("Unable to set uniforms on software context");
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
            // Validate region size is at least 1x1
            if (region.Width == 0 || region.Height == 0)
            {
                throw new InvalidOperationException("Unable to grab pixels, region size is zero.");
            }

            // Validate region
            if (region.Left < 0 || region.Top < 0 || region.Right >= Surface.Width || region.Bottom >= Surface.Height)
            {
                throw new ArgumentException("Unable to grab pixels, region outside the bounds of the surface.", nameof(region));
            }

            var surface = Backend.GetNativeObject<SoftwareSurface>(Surface);
            var image = surface.ColorBuffer.GrabPixels(region);
            image.Flip(Axis.Vertical); // because surface
            return image;
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

        protected override object GenerateNativeObject(GraphicsResource resource)
        {
            throw new NotImplementedException();
        }
    }
}
