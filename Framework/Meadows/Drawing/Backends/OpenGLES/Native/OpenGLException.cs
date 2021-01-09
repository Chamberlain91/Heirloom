using System;

namespace Meadows.Drawing.OpenGLES
{
    internal class OpenGLException : InvalidOperationException
    {
        public OpenGLException(string message)
            : base(message)
        { }
    }
}
