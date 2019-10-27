using System;

namespace Heirloom.Drawing.OpenGLES
{
    public readonly struct OpenGLVersion : IEquatable<OpenGLVersion>
    {
        public readonly string Vendor;
        public readonly string Renderer;

        public readonly int Major;
        public readonly int Minor;

        public readonly bool IsEmbedded;

        public OpenGLVersion(string vendor, string renderer, int major, int minor, bool isEmbedded)
        {
            Vendor = vendor;
            Renderer = renderer;

            Major = major;
            Minor = minor;

            IsEmbedded = isEmbedded;
        }

        public override string ToString()
        {
            if (IsEmbedded) { return $"OpenGL ES {Major}.{Minor} - {Vendor} - {Renderer}"; }
            else { return $"OpenGL {Major}.{Minor} - {Vendor} - {Renderer}"; }
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return (obj is OpenGLVersion v) && Equals(v);
        }

        public bool Equals(OpenGLVersion other)
        {
            return Vendor == other.Vendor &&
                   Renderer == other.Renderer &&
                   Major == other.Major &&
                   Minor == other.Minor &&
                   IsEmbedded == other.IsEmbedded;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vendor, Renderer, Major, Minor, IsEmbedded);
        }

        public static bool operator ==(OpenGLVersion left, OpenGLVersion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(OpenGLVersion left, OpenGLVersion right)
        {
            return !(left == right);
        }

        #endregion
    }
}
