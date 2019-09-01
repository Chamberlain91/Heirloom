using System;

using Heirloom.GLFW;
using Heirloom.Input;

namespace Heirloom.Platforms.Desktop.Input
{
    internal sealed class KeyboardDevice : Keyboard
    {
        private Glfw.CharCallback _charCallback;
        private Glfw.KeyCallback _keyCallback;

        internal KeyboardDevice(Glfw.Window window)
        {
            GraphicsContext.Invoke(() =>
            {
                // Character Typed (for text input)
                Glfw.SetCharCallback(window, _charCallback = (_, unicodeCharacter) =>
                {
                    // Character Typed 
                    OnCharacterInput((int) unicodeCharacter);
                });

                // Key Pressed/Released (for keyboard buttons)
                Glfw.SetKeyCallback(window, _keyCallback = (_, key, code, action, modifiers) =>
                {
                    // KeyDown
                    if (action == Glfw.Press)
                    {
                        OnInteractKey((Key) key, code, (KeyModifiers) modifiers, true);
                    }
                    else
                    // KeyUp
                    if (action == Glfw.Release)
                    {
                        OnInteractKey((Key) key, code, (KeyModifiers) modifiers, false);
                    }
                    // KeyRepeat
                    else
                    {
                        /* Repeat, ignored */
                    }
                });
            });
        }

        ~KeyboardDevice()
        {
            GC.KeepAlive(_charCallback);
            GC.KeepAlive(_keyCallback);
        }

        public override int GetScanCode(Key key)
        {
            return GraphicsContext.Invoke(() => Glfw.GetKeyScancode((int) key));
        }
    }
}
