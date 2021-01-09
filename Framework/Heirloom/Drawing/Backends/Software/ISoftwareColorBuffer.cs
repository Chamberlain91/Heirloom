
using Heirloom.Mathematics;

namespace Heirloom.Drawing.Software
{
    internal interface ISoftwareColorBuffer
    {
        Color Get(IntVector co);

        void Set(IntVector co, Color color);

        Image GrabPixels(IntRectangle region);
    }
}
