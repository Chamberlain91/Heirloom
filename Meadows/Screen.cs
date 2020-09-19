using Meadows.Drawing;
using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows
{
    public abstract class Screen
    {
        protected Screen(IntSize size)
        {
            Size = size;
        }

        public IntSize Size { get; }

        public abstract KeyboardDevice Keyboard { get; }

        public abstract MouseDevice Mouse { get; }

        public abstract GamepadDevice Gamepad { get; }

        public abstract TouchDevice Touch { get; }

        public abstract Surface Surface { get; }

        public abstract void Refresh();
    }
}
