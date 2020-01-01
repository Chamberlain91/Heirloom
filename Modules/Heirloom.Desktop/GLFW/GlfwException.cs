using System;

namespace Heirloom.Desktop
{
    internal class GlfwException : Exception
    {
        internal GlfwException(string message)
            : base(message)
        { }

        internal GlfwException(string message, Exception innerException)
            : base(message, innerException)
        { }

        internal GlfwException()
            : this("Unexpected GLFW Exception")
        { }
    }
}
