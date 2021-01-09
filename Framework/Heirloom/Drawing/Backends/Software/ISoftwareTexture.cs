
using System;

using Heirloom.Mathematics;

namespace Heirloom.Drawing.Software
{
    internal interface ISoftwareTexture : IDisposable
    {
        Color Sample(Vector uv, InterpolationMode interpolation, RepeatMode repeat);
    }
}
