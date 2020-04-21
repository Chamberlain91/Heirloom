namespace Heirloom.IO
{
    /// <summary>
    /// Controls the verbosity of <see cref="Log"/>.
    /// </summary>
    public enum LogVerbosity
    {
        /// <summary>
        /// No messages are processed.
        /// </summary>
        None,

        /// <summary>
        /// Only error messages are processed.
        /// </summary>
        Error,

        /// <summary>
        /// Only error and info logs are processed.
        /// </summary>
        Info,

        /// <summary>
        /// Only error, info and warning messages are processed.
        /// </summary>
        Warning,

        /// <summary>
        /// All messages are processed.
        /// </summary>
        Debug
    }
}
