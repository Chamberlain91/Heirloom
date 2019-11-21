using System;

namespace Heirloom.GLFW
{
    [Serializable]
    public class GlfwException : Exception
    {
        internal GlfwException(string message)
            : base(message)
        { }
    }
}
