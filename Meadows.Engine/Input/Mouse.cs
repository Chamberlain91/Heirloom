using System;

using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows.Engine.Input
{
    public static class Mouse
    {
        /// <summary>
        /// Gets a value that determines if mouse input is supported on this device.
        /// </summary>
        public static bool IsSupported => throw new NotImplementedException();

        /// <summary>
        /// Gets the latest mouse position.
        /// </summary>
        public static Vector MousePosition => throw new NotImplementedException();

        /// <summary>
        /// Gets the latest mouse position delta.
        /// </summary>
        public static Vector MouseDelta => throw new NotImplementedException();

        /// <summary>
        /// Gets the latest scroll wheel delta values.
        /// </summary>
        public static Vector Scroll => throw new NotImplementedException();

        /// <summary>
        /// Gets the latest state of a mouse button.
        /// </summary>
        /// <param name="button">Some button.</param>
        public static ButtonState GetButton(MouseButton button)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the lastest state of a mouse button matcheas the desired state.
        /// </summary>
        /// <param name="button">Some button.</param>
        /// <param name="state">Some desired comparison state.</param>
        public static bool CheckButton(MouseButton button, ButtonState state)
        {
            return GetButton(button).HasFlag(state);
        }

        internal static void SetDevice(MouseDevice mouse)
        {
            throw new NotImplementedException();
        }
    }
}
