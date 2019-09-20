using Heirloom.Math;

namespace Heirloom.Drawing
{
    public sealed class Surface : ImageSource
    {
        #region Constructors

        public Surface(int width, int height)
        {
            Size = new IntSize(width, height);
        }

        #endregion

        #region Properties

        public override IntSize Size { get; protected set; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        #endregion

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
