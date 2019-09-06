using System;

namespace Heirloom.GLFW3
{
    [Serializable]
    public class GlfwException : Exception
    {
        public GlfwException(string message)
            : base(message)
        { }
    }
}
