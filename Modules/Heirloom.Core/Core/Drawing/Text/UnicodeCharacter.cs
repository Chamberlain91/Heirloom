using System;

namespace Heirloom
{
    /// <summary>
    /// Represents a single 32 bit Unicode character.
    /// </summary>
    /// <category>Text</category>
    public readonly struct UnicodeCharacter : IComparable<UnicodeCharacter>, IEquatable<UnicodeCharacter>
    {
        private readonly int _value;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeCharacter"/> struct.
        /// </summary>
        /// <param name="codePoint">The 32-bit value of the codepoint.</param>
        public UnicodeCharacter(int codePoint)
        {
            _value = codePoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeCharacter"/> struct.
        /// </summary>
        /// <param name="character">The 16-bit value of the codepoint.</param>
        public UnicodeCharacter(char character)
        {
            _value = character;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeCharacter"/> struct.
        /// </summary>
        /// <param name="highSurrogate">The first member of a surrogate pair representing the codepoint.</param>
        /// <param name="lowSurrogate">The second member of a surrogate pair representing the codepoint.</param>
        public UnicodeCharacter(char highSurrogate, char lowSurrogate)
        {
            _value = char.ConvertToUtf32(highSurrogate, lowSurrogate);
        }

        #endregion

        #region Equals / GetHashCode / ToString

        /// <summary>
        /// Compares this unicode character for equality with another unicode character.
        /// </summary>
        public bool Equals(UnicodeCharacter other)
        {
            return _value.Equals(other._value);
        }


        /// <summary>
        /// Compares this unicode character for equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is UnicodeCharacter ch) { return Equals(ch); }
            else { return false; }
        }

        /// <summary>
        /// Returns the hash code for this <see cref="UnicodeCharacter"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }

        /// <summary>
        /// Returns the string representation of this <see cref="UnicodeCharacter"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (string) this;
        }

        #endregion

        #region Relational Operators

        /// <summary>
        /// Implements the equality operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(UnicodeCharacter left, UnicodeCharacter right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the inequality operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(UnicodeCharacter left, UnicodeCharacter right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Implements the less-than operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(UnicodeCharacter left, UnicodeCharacter right)
        {
            return left._value < right._value;
        }

        /// <summary>
        /// Implements the greater-than operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(UnicodeCharacter left, UnicodeCharacter right)
        {
            return left._value > right._value;
        }

        /// <summary>
        /// Implements the less-than-or-equal-to operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(UnicodeCharacter left, UnicodeCharacter right)
        {
            return left._value <= right._value;
        }

        /// <summary>
        /// Implements the greater-than-or-equal-to operator.
        /// </summary>
        /// <param name="left">The left hand side of the operator.</param>
        /// <param name="right">The right hand side of the operator.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(UnicodeCharacter left, UnicodeCharacter right)
        {
            return left._value >= right._value;
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// Implements an explicit conversion from integer to <see cref="UnicodeCharacter"/>.
        /// </summary>
        /// <param name="codePoint">The codepoint value.</param>
        public static explicit operator UnicodeCharacter(int codePoint)
        {
            return new UnicodeCharacter(codePoint);
        }

        /// <summary>
        /// Implements an implicit conversion from character to <see cref="UnicodeCharacter"/>.
        /// </summary>
        /// <param name="character">The character value.</param>
        public static implicit operator UnicodeCharacter(char character)
        {
            return new UnicodeCharacter(character);
        }

        /// <summary>
        /// Implements an explicit conversion from <see cref="UnicodeCharacter"/> to character.
        /// </summary>
        /// <param name="codePoint">The codepoint value.</param>
        public static explicit operator char(UnicodeCharacter codePoint)
        {
            return (char) codePoint._value;
        }

        /// <summary>
        /// Implements an explicit conversion from <see cref="UnicodeCharacter"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="codePoint">The codepoint value.</param>
        public static explicit operator string(UnicodeCharacter codePoint)
        {
            return char.ConvertFromUtf32(codePoint._value);
        }

        #endregion

        /// <summary>
        /// Compares this instance to the specified value.
        /// </summary>
        /// <param name="other">The value to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="other"/>.</returns>
        public int CompareTo(UnicodeCharacter other)
        {
            return _value.CompareTo(other._value);
        }
    }
}
