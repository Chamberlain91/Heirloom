using System;

namespace Heirloom.Android.EGL
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
