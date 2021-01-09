
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareFloatColorBuffer : ISoftwareColorBuffer
    {
        public Color[,] Colors;

        public SoftwareFloatColorBuffer(int width, int height)
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
}
