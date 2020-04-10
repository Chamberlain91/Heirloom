using System;

namespace Heirloom.Game
{
    /// <summary>
    /// A critical exception for when the internal state of the game library fails to work properly.
    /// </summary>
    internal class CriticalStateException : Exception
    {
        public CriticalStateException()
        { }

        public CriticalStateException(string message)
            : base(Wrap(message))
        { }

        public CriticalStateException(string message, Exception innerException)
            : base(Wrap(message), innerException)
        { }

        private static string Wrap(string message)
        {
            return $"Critical Internal Exception: {message}\nPlease report any unexpected encounter with this exception with the Heirloom repository.";
        }
    }
}
