
using Heirloom.Mathematics;

namespace Heirloom.Drawing.Software
{
    internal sealed class SoftwareByteColorBuffer : ISoftwareColorBuffer
    {
        public Image Image;

        public SoftwareByteColorBuffer(int width, int height)
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
}
