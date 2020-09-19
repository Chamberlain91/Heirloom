using Meadows.Engine.Input;

namespace Meadows.Engine
{
    public abstract class GameLoop
    {
        public GameLoop(Screen screen)
        {
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
    }
}
