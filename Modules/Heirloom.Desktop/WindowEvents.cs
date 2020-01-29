using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public readonly struct KeyEvent
    {
        public readonly Key Key;

        public readonly int ScanCode;

        public readonly ButtonAction Action;

        public readonly KeyModifiers Modifiers;

        internal KeyEvent(Key key, int scanCode, ButtonAction action, KeyModifiers modifiers)
        {
            Key = key;
            ScanCode = scanCode;
            Action = action;
            Modifiers = modifiers;
        }
    }

    public readonly struct CharacterEvent
    {
        public readonly UnicodeCharacter Character;

        internal CharacterEvent(UnicodeCharacter character)
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

        public readonly Vector Delta;

        internal MouseMoveEvent(Vector position, Vector delta)
        {
            Position = position;
            Delta = delta;
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
