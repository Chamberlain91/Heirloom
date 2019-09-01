using System;

using Heirloom.Input;

namespace Heirloom.Platforms.Desktop.Input
{
    internal sealed class GamepadDevice : Gamepad
    {
        public override bool IsConnected(GamepadIndex gamepad)
        {
            throw new NotImplementedException();
        }

        public override string GetName(GamepadIndex gamepad)
        {
            throw new NotImplementedException();
        }

        public override bool GetButton(GamepadIndex gamepad, GamepadButton button)
        {
            throw new NotImplementedException();
        }

        public override float GetAxis(GamepadIndex gamepad, GamepadAxis axis)
        {
            throw new NotImplementedException();
        }
    }
}
