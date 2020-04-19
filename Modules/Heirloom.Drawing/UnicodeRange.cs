using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a range of unicode 32 bit code points.
    /// </summary>
    public struct UnicodeRange : IEnumerable<UnicodeCharacter>, IEquatable<UnicodeRange>
    {
        #region Common Ranges

        // todo: more from https://en.wikipedia.org/wiki/Unicode_block

        /// <summary>
        /// The basic latin unicode block.
        /// </summary>
        public static readonly UnicodeRange BasicLatin =
            new UnicodeRange((char) 0x0020, (char) 0x007F);

        /// <summary>
        /// The Latin-1 Supplement unicode block.
        /// </summary>
        public static readonly UnicodeRange Latin1Supplement =
            new UnicodeRange((char) 0x00A0, (char) 0x00FF);

        /// <summary>
        /// The Latin Extended-A unicode block.
        /// </summary>
        public static readonly UnicodeRange LatinExtendedA =
            new UnicodeRange((char) 0x0100, (char) 0x017F);

        /// <summary>
        /// The Latin Extended-B unicode block.
        /// </summary>
        public static readonly UnicodeRange LatinExtendedB =
            new UnicodeRange((char) 0x0180, (char) 0x024F);

        /// <summary>
        /// The Cyrillic unicode block.
        /// </summary>
        public static readonly UnicodeRange Cyrillic =
            new UnicodeRange((char) 0x0400, (char) 0x04FF);

        /// <summary>
        /// The Cyrillic Supplement unicode block.
        /// </summary>
        public static readonly UnicodeRange CyrillicSupplement =
            new UnicodeRange((char) 0x0500, (char) 0x052F);

        /// <summary>
        /// The CJK Symbols and Punctuation unicode block.
        /// </summary>
        public static readonly UnicodeRange CJKPunctuation =
            new UnicodeRange((char) 0x3000, (char) 0x303F);

        /// <summary>
        /// The Hiragana unicode block.
        /// </summary>
        public static readonly UnicodeRange Hiragana =
            new UnicodeRange((char) 0x3040, (char) 0x309F);

        /// <summary>
        /// The Katakana unicode block.
        /// </summary>
        public static readonly UnicodeRange Katakana =
            new UnicodeRange((char) 0x30A0, (char) 0x30FF);

        #endregion

        #region Constructors

        public UnicodeRange(UnicodeCharacter start, UnicodeCharacter end)
        {
            Start = start;
            End = end;
        }

        public UnicodeRange(UnicodeCharacter single)
            : this(single, single)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The first character in the range.
        /// </summary>
        public UnicodeCharacter Start
        {
            get; private set;
        }

        /// <summary>
        /// The last character in the range.
        /// </summary>
        public UnicodeCharacter End
        {
            get; private set;
        }

        #endregion

        public IEnumerator<UnicodeCharacter> GetEnumerator()
        {
            var sequence = Enumerable.Range((int) Start, (int) End).Select(i => (UnicodeCharacter) i);
            return sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Equality Operators

        public static bool operator ==(UnicodeRange left, UnicodeRange right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UnicodeRange left, UnicodeRange right)
        {
            return !(left == right);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is UnicodeRange range
                && Equals(range);
        }

        public bool Equals(UnicodeRange other)
        {
            return (Start == other.Start)
                && (End == other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        #endregion
    }
}
