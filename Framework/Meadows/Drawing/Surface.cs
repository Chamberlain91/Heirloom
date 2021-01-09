using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public sealed class Surface : Texture
    {
        #region Constructors

        internal Surface(MultisampleQuality multisample, SurfaceFormat format, IScreen screen)
        {
            Screen = screen;
            Format = format;

            // Keep minimum allowable value
            Multisample = Calc.Min(GraphicsBackend.Current.Capabilities.MaxSupportedMultisample, multisample);
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

        public MultisampleQuality Multisample { get; internal set; }

        public SurfaceFormat Format { get; }

        internal IScreen Screen { get; }

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
