using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareScreen : Screen
    {
        private SoftwareGraphicsContext _graphicsContext;

        private readonly IntSize _size;

        public SoftwareScreen(IntSize size)
            : base(MultisampleQuality.None)
        {
            Surface.SetSize(size);
            _size = size;

            // todo: potentially dummy "null" input devices?
        }

        public void SetGraphicsContext(SoftwareGraphicsContext graphics)
        {
            _graphicsContext = graphics;
        }

        public override IntSize Size
        {
            get => _size;
            set => throw new NotImplementedException();
        }

        public override GraphicsContext Graphics => _graphicsContext;
    }
}
