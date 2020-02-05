using Heirloom.Math;

namespace Heirloom.Drawing
{
    internal interface IImageSource
    {
        IntSize Size { get; }

        Vector Origin { get; set; }

        InterpolationMode InterpolationMode { get; set; }

        uint Version { get; set; }
    }
}
