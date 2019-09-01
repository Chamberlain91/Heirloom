using System;

namespace Heirloom.Input
{
    public abstract class Gamepad
    // todo: switch to event model?
    {
        public event EventHandler<GamepadConnectedEventArgs> Connected;

        public event EventHandler<GamepadConnectedEventArgs> Disconnected;

        public abstract bool IsConnected(GamepadIndex gamepad);

        public abstract string GetName(GamepadIndex gamepad);

        public abstract bool GetButton(GamepadIndex gamepad, GamepadButton button);

        public abstract float GetAxis(GamepadIndex gamepad, GamepadAxis axis);

        public (float x, float y) GetStick(GamepadIndex gamepad, GamepadStick stick)
        {
            var x = GetAxis(gamepad, stick == GamepadStick.Left ? GamepadAxis.LeftX : GamepadAxis.RightX);
            var y = GetAxis(gamepad, stick == GamepadStick.Left ? GamepadAxis.LeftY : GamepadAxis.RightY);
            return (x, y);
        }

        /// <summary>
        /// A hollow implementation that always reports zero, disconnected, etc.
        /// </summary>
        public static Gamepad Null { get; } = new DummyGamepad();

        private sealed class DummyGamepad : Gamepad
        {
            public override bool IsConnected(GamepadIndex gamepad)
            {
                return false;
            }

            public override string GetName(GamepadIndex gamepad)
            {
                return string.Empty;
            }

            public override bool GetButton(GamepadIndex gamepad, GamepadButton button)
            {
                return false;
            }

            public override float GetAxis(GamepadIndex gamepad, GamepadAxis axis)
            {
                return 0F;
            }
        }
    }
}
