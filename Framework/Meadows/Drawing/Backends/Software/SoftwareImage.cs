
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareImage : ISoftwareTexture
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

        public void Dispose()
        {
            // Does nothing
        }
    }
}
