using System;
using System.Diagnostics;

namespace Heirloom.Runtime
{
    internal static class Log
    {
        [Conditional("TRACE")]
        internal static void Info(object message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        [Conditional("DEBUG")]
        internal static void Debug(object message)
        {
            Console.WriteLine($"DEBUG: {message}");
        }

        internal static void Warn(object message)
        {
            Console.WriteLine($"WARN: {message}");
        }
    }
}
