using Heirloom.Drawing;
using Heirloom.GLFW;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public readonly struct KeyboardEvent
    {
        public readonly Key Key;

        public readonly int ScanCode;

        public readonly ButtonAction Action;

        public readonly KeyModifiers Modifiers;

        internal KeyboardEvent(Key key, int scanCode, ButtonAction action, KeyModifiers modifiers)
        {
            Key = key;
            ScanCode = scanCode;
            Action = action;
            Modifiers = modifiers;
        }
    }

    public readonly struct CharEvent
    {
        public readonly UnicodeCharacter Character;

        internal CharEvent(UnicodeCharacter character)
        {
            Character = character;
        }
    }

    public readonly struct MouseButtonEvent
    {
        public readonly int Button;

        public readonly ButtonAction Action;

        public readonly KeyModifiers Modifiers;

        public readonly Vector Position;

        internal MouseButtonEvent(int button, ButtonAction action, KeyModifiers modifiers, Vector position)
        {
            Button = button;
            Action = action;
            Modifiers = modifiers;
            Position = position;
        }
    }

    public readonly struct MouseMoveEvent
    {
        public readonly Vector Position;

        internal MouseMoveEvent(float x, float y)
        {
            Position = new Vector(x, y);
        }
    }

    public readonly struct MouseScrollEvent
    {
        public readonly Vector Scroll;

        internal MouseScrollEvent(float x, float y)
        {
            Scroll = new Vector(x, y);
        }
    }

    public readonly struct ContentScaleEvent
    {
        public readonly float XScale;

        public readonly float YScale;

        internal ContentScaleEvent(float xScale, float yScale)
        {
            XScale = xScale;
            YScale = yScale;
        }

        public Vector ContentScale => new Vector(XScale, YScale);
    }
}
