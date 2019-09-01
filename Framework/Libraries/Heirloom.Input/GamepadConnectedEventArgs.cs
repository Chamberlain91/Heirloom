using System;

namespace Heirloom.Input
{
    public class GamepadConnectedEventArgs : EventArgs
    {
        public readonly GamepadIndex Index;

        public readonly bool WasConnected;

        public GamepadConnectedEventArgs(GamepadIndex index, bool wasConnected)
        {
            Index = index;
            WasConnected = wasConnected;
        }
    }
}
