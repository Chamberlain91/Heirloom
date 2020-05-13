using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Heirloom
{
    /// <summary>
    /// Provides a simple mechanism to log debug and info messages.
    /// </summary>
    /// <category>Logging</category>
    public static class Log
    {
#if DEBUG
        private const LogVerbosity DefaultVerbosity = LogVerbosity.Debug;
#else
        private const LogVerbosity DefaultVerbosity = LogVerbosity.Warning;
#endif
        private static readonly Dictionary<string, LogVerbosity> _verbosity = new Dictionary<string, LogVerbosity>();
        private static ILogHandler _logHandler = new ConsoleDefaultHandler();

        /// <summary>
        /// Gets or sets the current log handler.
        /// </summary>
        public static ILogHandler LogHandler
        {
            get => _logHandler;
            set => _logHandler = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Sets the verbosity level of the calling assembly.
        /// </summary>
        public static void SetVerbosity(LogVerbosity verbosity)
        {
            SetVerbosity(verbosity, GetTag(Assembly.GetCallingAssembly()));
        }

        /// <summary>
        /// Sets the verbosity level of a specific assembly.
        /// </summary>
        public static void SetVerbosity(LogVerbosity verbosity, Assembly assembly)
        {
            SetVerbosity(verbosity, GetTag(assembly));
        }

        /// <summary>
        /// Sets the verbosity level of a specific assembly.
        /// </summary>
        public static void SetVerbosity(LogVerbosity verbosity, string assembly)
        {
            if (string.IsNullOrWhiteSpace(assembly)) { throw new ArgumentException("Tag must not be blank or null.", nameof(assembly)); }
            _verbosity[assembly] = verbosity;
        }

        private static LogVerbosity GetVerbosity(string tag)
        {
            if (_verbosity.TryGetValue(tag, out var verbosity))
            {
                return verbosity;
            }
            else
            {
                verbosity = Debugger.IsAttached ? LogVerbosity.Debug : DefaultVerbosity;
                SetVerbosity(verbosity, tag);
                return verbosity;
            }
        }

        private static string GetTag(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        public static void Debug(object message)
        {
            var tag = GetTag(Assembly.GetCallingAssembly());

            if (GetVerbosity(tag) >= LogVerbosity.Debug)
            {
                LogHandler?.Debug($"[{tag}] {message}");
            }
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        public static void Warning(object message)
        {
            var tag = GetTag(Assembly.GetCallingAssembly());

            if (GetVerbosity(tag) >= LogVerbosity.Warning)
            {
                LogHandler?.Warning($"[{tag}] {message}");
            }
        }

        /// <summary>
        /// Logs a error message.
        /// </summary>
        public static void Info(object message)
        {
            var tag = GetTag(Assembly.GetCallingAssembly());

            if (GetVerbosity(tag) >= LogVerbosity.Info)
            {
                LogHandler?.Info($"[{tag}] {message}");
            }
        }

        /// <summary>
        /// Logs a error message.
        /// </summary>
        public static void Error(object message)
        {
            var tag = GetTag(Assembly.GetCallingAssembly());

            if (GetVerbosity(tag) >= LogVerbosity.Error)
            {
                LogHandler?.Error($"[{tag}] {message}");
            }
        }

        private class ConsoleDefaultHandler : ILogHandler
        {
            public void Debug(object message)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public void Warning(object message)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public void Error(object message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public void Info(object message)
            {
                Console.WriteLine(message);
            }
        }
    }
}
