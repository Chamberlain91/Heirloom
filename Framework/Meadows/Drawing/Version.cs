using System;
using System.Collections.Generic;

namespace Meadows.Drawing
{
    /// <summary>
    /// Utility object track the version of an object.
    /// </summary>
    public sealed class Version : IEquatable<Version>
    {
        private bool _isVersionDirty;
        private uint _version;

        /// <summary>
        /// The version number of the associated object.
        /// </summary>
        /// <remarks>
        /// While unlikely, if the version is incremented beyond <see cref="uint.MaxValue"/>, it is reset to zero.
        /// </remarks>
        public uint Number
        {
            get
            {
                if (_isVersionDirty)
                {
                    // Increment version number
                    if (_version == uint.MaxValue) { _version = 0; }
                    else { _version++; }

                    // Mark as no longer dirty
                    _isVersionDirty = false;
                }

                return _version;
            }
        }

        /// <summary>
        /// Increments the version number.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note: This doesn't actually change the version number. Instead it marks the version
        /// as dirty until the version number is next read.
        /// </para>
        /// <para>
        /// This helps prevent having thousands of increments when updating an image pixel
        /// by pixel, for example.
        /// </para>
        /// </remarks>
        public void Increment()
        {
            _isVersionDirty = true;
        }

        #region Conversion Operators

        public static implicit operator uint(Version version)
        {
            return version.Number;
        }

        #endregion

        #region Equality & Operators

        public static bool operator ==(Version left, Version right)
        {
            return EqualityComparer<Version>.Default.Equals(left, right);
        }

        public static bool operator !=(Version left, Version right)
        {
            return !(left == right);
        }

        public bool Equals(Version other)
        {
            return other?.Number == Number;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Version);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_isVersionDirty, _version, Number);
        }

        #endregion
    }
}
