using System;

namespace Heirloom.Drawing.OpenGLES
{
    internal class OpenGLException : InvalidOperationException
    {
        public OpenGLException(string message)
            : base(message)
        { }
    }
}
