using System.Runtime.CompilerServices;

namespace Heirloom.Desktop
{
    public static class Keyboard
    {
        /// <summary>
        /// Gets the printable name of a key.
        /// </summary>
        public static string GetPrintableKeyName(Key key)
        {
            return GetPrintableKeyName(key, -1);
        }

        /// <summary>
        /// Gets the printable name of a key specified by scancode.
        /// </summary>
        public static string GetPrintableKeyName(int scancode)
        {
            return GetPrintableKeyName(Key.Unknown, scancode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetPrintableKeyName(Key key, int scancode)
        {
            return Application.Invoke(() => Glfw.GetKeyName(key, scancode));
        }
    }
}
