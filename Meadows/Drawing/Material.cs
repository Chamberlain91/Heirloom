using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public struct Material<X> where X : unmanaged
    {
        public Texture Image;

        public Rectangle Clip;

        public InterpolationMode Interpolation;

        public BlendingMode Blending;

        public Shader<X> Shader;

        public X Parameters;
    }
}
