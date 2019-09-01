using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom.Drawing
{
    public struct UnicodeRange : IEnumerable<UnicodeCharacter>
    {
        public static readonly UnicodeRange BasicLatin =
            new UnicodeRange((char) 0x0020, (char) 0x007F);

        public static readonly UnicodeRange Latin1Supplement =
            new UnicodeRange((char) 0x00A0, (char) 0x00FF);

        public static readonly UnicodeRange LatinExtendedA =
            new UnicodeRange((char) 0x0100, (char) 0x017F);

        public static readonly UnicodeRange LatinExtendedB =
            new UnicodeRange((char) 0x0180, (char) 0x024F);

        public static readonly UnicodeRange Cyrillic =
            new UnicodeRange((char) 0x0400, (char) 0x04FF);

        public static readonly UnicodeRange CyrillicSupplement =
            new UnicodeRange((char) 0x0500, (char) 0x052F);

        public static readonly UnicodeRange Hiragana =
            new UnicodeRange((char) 0x3040, (char) 0x309F);

        public static readonly UnicodeRange Katakana =
            new UnicodeRange((char) 0x30A0, (char) 0x30FF);

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

        public UnicodeCharacter Start
        {
            get; private set;
        }

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
    }
}
