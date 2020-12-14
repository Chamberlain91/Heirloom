
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal interface ISoftwareColorBuffer
    {
        Color Get(IntVector co);

        void Set(IntVector co, Color color);

        Image GrabPixels(IntRectangle region);
    }
}
