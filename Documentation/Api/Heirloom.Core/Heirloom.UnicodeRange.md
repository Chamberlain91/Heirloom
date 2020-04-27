# UnicodeRange

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a range of unicode 32 bit code points.

```cs
public struct UnicodeRange : IEnumerable<UnicodeCharacter>, IEnumerable, IEquatable<UnicodeRange>
```

--------------------------------------------------------------------------------

**Inherits**: IEnumerable\<UnicodeCharacter>, IEnumerable, IEquatable\<UnicodeRange>

**Properties**: [Start][1], [End][2]

**Methods**: [GetEnumerator][3]

**Static Fields**: [BasicLatin][4], [Latin1Supplement][5], [LatinExtendedA][6], [LatinExtendedB][7], [Cyrillic][8], [CyrillicSupplement][9], [CJKPunctuation][10], [Hiragana][11], [Katakana][12]

--------------------------------------------------------------------------------

## Constructors

### UnicodeRange(UnicodeCharacter, UnicodeCharacter)

```cs
public UnicodeRange(UnicodeCharacter start, UnicodeCharacter end)
```

### UnicodeRange(UnicodeCharacter)

```cs
public UnicodeRange(UnicodeCharacter single)
```

## Fields

| Name                    | Summary                                        |
|-------------------------|------------------------------------------------|
| [BasicLatin][4]         | The basic latin unicode block.                 |
| [Latin1Supplement][5]   | The Latin-1 Supplement unicode block.          |
| [LatinExtendedA][6]     | The Latin Extended-A unicode block.            |
| [LatinExtendedB][7]     | The Latin Extended-B unicode block.            |
| [Cyrillic][8]           | The Cyrillic unicode block.                    |
| [CyrillicSupplement][9] | The Cyrillic Supplement unicode block.         |
| [CJKPunctuation][10]    | The CJK Symbols and Punctuation unicode block. |
| [Hiragana][11]          | The Hiragana unicode block.                    |
| [Katakana][12]          | The Katakana unicode block.                    |

## Properties

| Name       | Summary                           |
|------------|-----------------------------------|
| [Start][1] | The first character in the range. |
| [End][2]   | The last character in the range.  |

## Methods

| Name               | Summary |
|--------------------|---------|
| [GetEnumerator][3] |         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.UnicodeRange.Start.md
[2]: Heirloom.UnicodeRange.End.md
[3]: Heirloom.UnicodeRange.GetEnumerator.md
[4]: Heirloom.UnicodeRange.BasicLatin.md
[5]: Heirloom.UnicodeRange.Latin1Supplement.md
[6]: Heirloom.UnicodeRange.LatinExtendedA.md
[7]: Heirloom.UnicodeRange.LatinExtendedB.md
[8]: Heirloom.UnicodeRange.Cyrillic.md
[9]: Heirloom.UnicodeRange.CyrillicSupplement.md
[10]: Heirloom.UnicodeRange.CJKPunctuation.md
[11]: Heirloom.UnicodeRange.Hiragana.md
[12]: Heirloom.UnicodeRange.Katakana.md
