namespace Heirloom
{
    /// <summary>
    /// Represents the interface for handling log messages from <see cref="Log"/>.
    /// </summary>
    public interface ILogHandler
    {
        /// <summary>
        /// Logs a debug message.
        /// </summary>
        void Debug(object message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        void Warning(object message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        void Error(object message);

        /// <summary>
        /// Logs a information message.
        /// </summary>
        void Info(object message);
    }
}
