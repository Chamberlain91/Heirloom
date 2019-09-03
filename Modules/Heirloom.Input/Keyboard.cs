using System;

namespace Heirloom.Input
{
    public abstract class Keyboard
    {
        public event EventHandler<CharacterTypedEventArgs> CharacterTyped;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<KeyEventArgs> KeyDown;

        protected virtual void OnCharacterInput(int unicodeCharacter)
        {
            var args = new CharacterTypedEventArgs(unicodeCharacter);
            CharacterTyped?.Invoke(this, args);
        }

        protected virtual void OnInteractKey(Key key, int scancode, KeyModifiers modifiers, bool isDown)
        {
            var args = new KeyEventArgs(key, scancode, modifiers, isDown);
            if (isDown) { KeyDown?.Invoke(this, args); }
            else { KeyUp?.Invoke(this, args); }
        }

        public abstract int GetScanCode(Key key);

        /// <summary>
        /// A hollow implementation that always reports zero, disconnected, etc.
        /// </summary>
        public static Keyboard Null { get; } = new DummyKeyboard();

        private sealed class DummyKeyboard : Keyboard
        {
            public override int GetScanCode(Key key)
            {
                return 0;
            }
        }
    }
}
