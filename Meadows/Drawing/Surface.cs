using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public sealed class Surface : Texture
    {
        #region Constructors

        internal Surface(MultisampleQuality multisample, SurfaceFormat format, Screen screen)
        {
            Screen = screen;
            Multisample = multisample;
            Format = format;
        }

        public Surface(IntSize size, MultisampleQuality multisample = MultisampleQuality.None, SurfaceFormat format = SurfaceFormat.UnsignedByte)
            : this(multisample, format, null)
        {
            Size = size;
        }

        public Surface(IntSize size, MultisampleQuality multisample)
            : this(size, multisample, SurfaceFormat.UnsignedByte)
        { }

        #endregion

        public MultisampleQuality Multisample { get; }

        public SurfaceFormat Format { get; }

        internal Screen Screen { get; }

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
