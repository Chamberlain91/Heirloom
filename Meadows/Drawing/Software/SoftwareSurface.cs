
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareSurface : ISoftwareTexture
    {
        public readonly Surface Surface;

        public readonly ISoftwareColorBuffer ColorBuffer;

        public readonly SoftwareStencilBuffer StencilBuffer;

        public SoftwareSurface(Surface surface)
        {
            Surface = surface;
            StencilBuffer = new SoftwareStencilBuffer(surface.Size);
            ColorBuffer = CreateColorBuffer(surface);
        }

        private static ISoftwareColorBuffer CreateColorBuffer(Surface texture)
        {
            ISoftwareColorBuffer buffer;
            if (texture.Format == SurfaceFormat.UnsignedByte)
            {
                buffer = new SoftwareByteColorBuffer(texture.Width, texture.Height);
            }
            else
            {
                buffer = new SoftwareFloatColorBuffer(texture.Width, texture.Height);
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

        public void Dispose()
        {
            // Does nothing
        }
    }
}
