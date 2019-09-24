using Heirloom.Math;

namespace Heirloom.Drawing
{
    public sealed class Surface : ImageSource
    {
        #region Constructors

        public Surface(int width, int height, MultisampleQuality multisample = MultisampleQuality.None)
        {
            Size = new IntSize(width, height);
            Multisample = multisample;
        }

        #endregion

        #region Properties

        public override IntSize Size { get; protected set; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        public MultisampleQuality Multisample { get; }

        #endregion

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
