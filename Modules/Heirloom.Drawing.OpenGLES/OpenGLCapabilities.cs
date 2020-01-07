//using System;

//namespace Heirloom.Drawing.OpenGLES
//{
//    public readonly struct OpenGLCapabilities : IEquatable<OpenGLCapabilities>
//    {
//        public readonly int MaxTextureUnits;

//        public OpenGLCapabilities(int maxTextureUnits)
//        {
//            MaxTextureUnits = maxTextureUnits;
//        }

//        #region Equality

//        public override bool Equals(object obj)
//        {
//            return (obj is OpenGLCapabilities c) && Equals(c);
//        }

//        public bool Equals(OpenGLCapabilities other)
//        {
//            return MaxTextureUnits == other.MaxTextureUnits;
//        }

//        public override int GetHashCode()
//        {
//            return HashCode.Combine(MaxTextureUnits);
//        }

//        public static bool operator ==(OpenGLCapabilities left, OpenGLCapabilities right)
//        {
//            return left.Equals(right);
//        }

//        public static bool operator !=(OpenGLCapabilities left, OpenGLCapabilities right)
//        {
//            return !(left == right);
//        }

//        #endregion
//    }
//}
