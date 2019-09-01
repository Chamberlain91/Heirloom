using System;
using Heirloom.OpenGLES.Platform;

namespace Heirloom.OpenGLES
{
    public class EglException : Exception
    {
        internal EglErrorCode ErrorCode { get; private set; }

        internal EglException(string message)
            : this(Egl.GetError(), message)
        { }

        private EglException(EglErrorCode error, string message)
            : base($"{error} - {message} - {Egl.GetErrorMessage(error)}")
        {
            ErrorCode = error;
        }
    }
}
