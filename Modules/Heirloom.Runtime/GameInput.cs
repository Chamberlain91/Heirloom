using System;
using System.Collections.Generic;

using Heirloom.Input;

namespace Heirloom.Runtime
{
    public sealed class GameInput
    {
        private Dictionary<Key, InputState> _keyState;
        private Dictionary<Key, bool> _keyDown;

        public readonly static Key[] Keys = (Key[]) Enum.GetValues(typeof(Key));

        internal GameInput()
        {
            // 
            _keyState = new Dictionary<Key, InputState>();
            _keyDown = new Dictionary<Key, bool>();

            //
            Game.Keyboard.KeyDown += KeyInput;
            Game.Keyboard.KeyUp += KeyInput;

            // Set initial state
            foreach (var key in Keys)
            {
                _keyState[key] = InputState.Up;
                _keyDown[key] = false;
            }
        }

        private void KeyInput(object sender, KeyEventArgs e)
        {
            _keyDown[e.Key] = e.IsDown;
        }

        internal void Update()
        {
            foreach (var key in Keys)
            {
                var prev = _keyState[key];
                var down = _keyDown[key];

                // Was down, and is still down
                if (prev.HasFlag(InputState.Down))
                {
                    // Still down
                    if (down) { _keyState[key] = InputState.Down; }
                    // Just up
                    else { _keyState[key] = InputState.Up | InputState.Now; }
                }
                else
                {
                    // Just down
                    if (down) { _keyState[key] = InputState.Down | InputState.Now; }
                    // Still up
                    else { _keyState[key] = InputState.Up; }
                }
            }
        }

        public bool GetKey(Key key)
        {
            return _keyState[key].HasFlag(InputState.Down);
        }

        public bool GetKeyDown(Key key)
        {
            return _keyState[key].HasFlag(InputState.Down | InputState.Now);
        }

        public bool GetKeyUp(Key key)
        {
            return _keyState[key].HasFlag(InputState.Up | InputState.Now);
        }
    }

    [Flags]
    public enum InputState
    {
        Up = 0,
        Down = 1 << 0,
        Now = 1 << 1
    }
}
