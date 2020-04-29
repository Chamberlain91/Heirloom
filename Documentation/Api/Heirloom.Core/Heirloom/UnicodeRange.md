# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## UnicodeRange (Struct)

> **Namespace**: [Heirloom][0]

Represents a range of unicode 32 bit code points.

```cs
public struct UnicodeRange : IEnumerable<UnicodeCharacter>, IEnumerable, IEquatable<UnicodeRange>
```

### Inherits

IEnumerable\<UnicodeCharacter>, IEnumerable, IEquatable\<UnicodeRange>

### Properties

[End][1], [Start][2]

### Methods

[GetEnumerator][3]

### Static Fields

[BasicLatin][4], [CJKPunctuation][5], [Cyrillic][6], [CyrillicSupplement][7], [Hiragana][8], [Katakana][9], [Latin1Supplement][10], [LatinExtendedA][11], [LatinExtendedB][12]

## Fields

| Name                    | Type               | Summary                                        |
|-------------------------|--------------------|------------------------------------------------|
| [BasicLatin][4]         | [UnicodeRange][13] | The basic latin unicode block.                 |
| [CJKPunctuation][5]     | [UnicodeRange][13] | The CJK Symbols and Punctuation unicode block. |
| [Cyrillic][6]           | [UnicodeRange][13] | The Cyrillic unicode block.                    |
| [CyrillicSupplement][7] | [UnicodeRange][13] | The Cyrillic Supplement unicode block.         |
| [Hiragana][8]           | [UnicodeRange][13] | The Hiragana unicode block.                    |
| [Katakana][9]           | [UnicodeRange][13] | The Katakana unicode block.                    |
| [Latin1Supplement][10]  | [UnicodeRange][13] | The Latin-1 Supplement unicode block.          |
| [LatinExtendedA][11]    | [UnicodeRange][13] | The Latin Extended-A unicode block.            |
| [LatinExtendedB][12]    | [UnicodeRange][13] | The Latin Extended-B unicode block.            |

## Properties

#### Instance

| Name       | Type                   | Summary                           |
|------------|------------------------|-----------------------------------|
| [End][1]   | [UnicodeCharacter][14] | The last character in the range.  |
| [Start][2] | [UnicodeCharacter][14] | The first character in the range. |

## Methods

#### Instance

| Name                 | Return Type                      | Summary |
|----------------------|----------------------------------|---------|
| [GetEnumerator()][3] | `IEnumerator\<UnicodeCharacter>` |         |

[0]: ../../Heirloom.Core.md
[1]: UnicodeRange/End.md
[2]: UnicodeRange/Start.md
[3]: UnicodeRange/GetEnumerator.md
[4]: UnicodeRange/BasicLatin.md
[5]: UnicodeRange/CJKPunctuation.md
[6]: UnicodeRange/Cyrillic.md
[7]: UnicodeRange/CyrillicSupplement.md
[8]: UnicodeRange/Hiragana.md
[9]: UnicodeRange/Katakana.md
[10]: UnicodeRange/Latin1Supplement.md
[11]: UnicodeRange/LatinExtendedA.md
[12]: UnicodeRange/LatinExtendedB.md
[13]: UnicodeRange.md
[14]: UnicodeCharacter.md
