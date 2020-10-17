using System;

using Meadows.Drawing;
using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows.Runtime.Desktop
{
    public sealed class Window : Screen
    {
        public Window(string title, IntSize size, MultisampleQuality multisample = MultisampleQuality.None)
            : base(size, multisample)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; set; }

        public bool IsResizable { get; init; }

        public override GraphicsContext Graphics => throw new NotImplementedException();

        public override KeyboardDevice Keyboard => throw new NotImplementedException();

        public override MouseDevice Mouse => throw new NotImplementedException();

        public override GamepadDevice Gamepad => throw new NotImplementedException();

        public override TouchDevice Touch => throw new NotImplementedException();
    }
}
