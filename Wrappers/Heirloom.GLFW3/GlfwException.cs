using System;

namespace Heirloom.GLFW3
{
    [Serializable]
    public class GlfwException : Exception
    {
        internal GlfwException(string message)
            : base(message)
        { }
    }
}
