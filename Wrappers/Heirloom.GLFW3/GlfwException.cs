using System;

namespace Heirloom.GLFW3
{
    [Serializable]
    internal class GlfwException : Exception
    {
        public GlfwException(string message)
            : base(message)
        { }
    }
}
