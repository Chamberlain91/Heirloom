# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## UnicodeRange Struct

> **Namespace**: [Heirloom][0]  

Represents a range of unicode 32 bit code points.

```cs
public struct UnicodeRange : IEnumerable<UnicodeCharacter>, IEnumerable, IEquatable<UnicodeRange>
```

### Inherits

IEnumerable\<UnicodeCharacter>, IEnumerable, IEquatable\<UnicodeRange>

#### Properties

[Start][1], [End][2]

#### Methods

[GetEnumerator][3]

#### Static Fields

[BasicLatin][4], [Latin1Supplement][5], [LatinExtendedA][6], [LatinExtendedB][7], [Cyrillic][8], [CyrillicSupplement][9], [CJKPunctuation][10], [Hiragana][11], [Katakana][12]

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

[0]: ../../Heirloom.Core.md
[1]: UnicodeRange/Start.md
[2]: UnicodeRange/End.md
[3]: UnicodeRange/GetEnumerator.md
[4]: UnicodeRange/BasicLatin.md
[5]: UnicodeRange/Latin1Supplement.md
[6]: UnicodeRange/LatinExtendedA.md
[7]: UnicodeRange/LatinExtendedB.md
[8]: UnicodeRange/Cyrillic.md
[9]: UnicodeRange/CyrillicSupplement.md
[10]: UnicodeRange/CJKPunctuation.md
[11]: UnicodeRange/Hiragana.md
[12]: UnicodeRange/Katakana.md
