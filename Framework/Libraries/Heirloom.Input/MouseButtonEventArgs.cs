using System;

namespace Heirloom.Input
{
    public class MouseButtonEventArgs : EventArgs
    {
        public readonly MouseButton Button;

        public readonly KeyModifiers Modifiers;

        public bool IsDown;

        public MouseButtonEventArgs(MouseButton button, KeyModifiers modifiers, bool isDown)
        {
            Button = button;
            Modifiers = modifiers;
            IsDown = isDown;
        }
    }
}
