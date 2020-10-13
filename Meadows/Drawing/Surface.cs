using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public sealed class Surface : Texture
    {
        #region Constructors

        internal Surface(IntSize size, MultisampleQuality multisample, SurfaceFormat format, Screen screen)
        {
            Screen = screen;
            Multisample = multisample;
            Format = format;
            Size = size;
        }

        public Surface(IntSize size, MultisampleQuality multisample = MultisampleQuality.None, SurfaceFormat format = SurfaceFormat.UnsignedByte)
            : this(size, multisample, format, null)
        { }

        public Surface(IntSize size, MultisampleQuality multisample)
            : this(size, multisample, SurfaceFormat.UnsignedByte)
        { }

        #endregion

        public override IntSize Size { get; protected set; }

        public MultisampleQuality Multisample { get; }

        public SurfaceFormat Format { get; }

        internal Screen Screen { get; }
    }
}
