using System;

using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareScreen : IScreen
    {
        private SoftwareGraphicsContext _graphicsContext;

        private readonly IntSize _size;

        public SoftwareScreen(IntSize size)
        {
            Surface = new Surface(size, MultisampleQuality.None, SurfaceFormat.UnsignedByte);
            _size = size;

            // todo: potentially dummy "null" input devices?
        }

        public void SetGraphicsContext(SoftwareGraphicsContext graphics)
        {
            _graphicsContext = graphics;
        }

        public IntSize Size
        {
            get => _size;
            set => throw new NotImplementedException();
        }

        public GraphicsContext Graphics => _graphicsContext;

        public Surface Surface { get; }

        public event Action<IScreen, IntSize> Resized;

        public event Action<IScreen, IntSize> SurfaceResized;

        public void Refresh()
        {
            Graphics.CompleteFrame();
        }
    }
}
