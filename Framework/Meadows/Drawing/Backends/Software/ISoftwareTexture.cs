
using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal interface ISoftwareTexture : IDisposable
    {
        Color Sample(Vector uv, InterpolationMode interpolation, RepeatMode repeat);
    }
}
