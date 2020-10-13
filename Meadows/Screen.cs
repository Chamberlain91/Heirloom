using Meadows.Drawing;
using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows
{
    public abstract class Screen
    {
        protected Screen(IntSize size, MultisampleQuality multisample)
        {
            Surface = new Surface(size, multisample, SurfaceFormat.UnsignedByte, this);
            Size = size;
        }

        public IntSize Size { get; }

        public abstract KeyboardDevice Keyboard { get; }

        public abstract MouseDevice Mouse { get; }

        public abstract GamepadDevice Gamepad { get; }

        public abstract TouchDevice Touch { get; }

        public abstract GraphicsContext Graphics { get; }

        public Surface Surface { get; }

        public void Refresh()
        {
            // todo: poll input...?
            Graphics.CompleteFrame();
        }
    }
}
