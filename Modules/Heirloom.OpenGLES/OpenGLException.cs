using System;

namespace Heirloom.OpenGLES
{
    internal class OpenGLException : InvalidOperationException
    {
        public OpenGLException(string message)
            : base(message)
        { }
    }
}
