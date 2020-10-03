using Meadows.Drawing;
using Meadows.Engine.Input;

namespace Meadows.Engine
{
    public abstract class GameLoop
    {
        public Screen Screen { get; }

        public GameLoop(Screen screen)
        {
            Screen = screen;

            ConfigureInputDevices(screen);
        }

        private static void ConfigureInputDevices(Screen screen)
        {
            // Register input devices
            Keyboard.SetDevice(screen.Keyboard);
            Mouse.SetDevice(screen.Mouse);
            Gamepad.SetDevice(screen.Gamepad);
            Touch.SetDevice(screen.Touch);
        }

        protected virtual void Update(Surface surface)
        {
            Screen.Refresh();
        }
    }
}
