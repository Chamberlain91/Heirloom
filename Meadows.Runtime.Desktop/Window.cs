using System;

using Meadows.Drawing;
using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows.Runtime.Desktop
{
    public sealed class Window : Screen
    {
        public Window(string title, IntSize size)
            : base(size)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; set; }

        public bool IsResizable { get; set; }

        public override KeyboardDevice Keyboard => throw new NotImplementedException();

        public override MouseDevice Mouse => throw new NotImplementedException();

        public override GamepadDevice Gamepad => throw new NotImplementedException();

        public override TouchDevice Touch => throw new NotImplementedException();

        public override Surface Surface => throw new NotImplementedException();

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
