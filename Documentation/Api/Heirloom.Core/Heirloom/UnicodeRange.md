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

[Equals][3], [GetEnumerator][4], [GetHashCode][5]

### Static Fields

[BasicLatin][6], [CJKPunctuation][7], [Cyrillic][8], [CyrillicSupplement][9], [Hiragana][10], [Katakana][11], [Latin1Supplement][12], [LatinExtendedA][13], [LatinExtendedB][14]

## Fields

| Name                    | Type               | Summary                                        |
|-------------------------|--------------------|------------------------------------------------|
| [BasicLatin][6]         | [UnicodeRange][15] | The basic latin unicode block.                 |
| [CJKPunctuation][7]     | [UnicodeRange][15] | The CJK Symbols and Punctuation unicode block. |
| [Cyrillic][8]           | [UnicodeRange][15] | The Cyrillic unicode block.                    |
| [CyrillicSupplement][9] | [UnicodeRange][15] | The Cyrillic Supplement unicode block.         |
| [Hiragana][10]          | [UnicodeRange][15] | The Hiragana unicode block.                    |
| [Katakana][11]          | [UnicodeRange][15] | The Katakana unicode block.                    |
| [Latin1Supplement][12]  | [UnicodeRange][15] | The Latin-1 Supplement unicode block.          |
| [LatinExtendedA][13]    | [UnicodeRange][15] | The Latin Extended-A unicode block.            |
| [LatinExtendedB][14]    | [UnicodeRange][15] | The Latin Extended-B unicode block.            |

## Properties

#### Instance

| Name       | Type                   | Summary                           |
|------------|------------------------|-----------------------------------|
| [End][1]   | [UnicodeCharacter][16] | The last character in the range.  |
| [Start][2] | [UnicodeCharacter][16] | The first character in the range. |

## Methods

#### Instance

| Name                      | Return Type                     | Summary |
|---------------------------|---------------------------------|---------|
| [Equals(object)][3]       | `bool`                          |         |
| [Equals(UnicodeRange)][3] | `bool`                          |         |
| [GetEnumerator()][4]      | `IEnumerator<UnicodeCharacter>` |         |
| [GetHashCode()][5]        | `int`                           |         |

[0]: ../../Heirloom.Core.md
[1]: UnicodeRange/End.md
[2]: UnicodeRange/Start.md
[3]: UnicodeRange/Equals.md
[4]: UnicodeRange/GetEnumerator.md
[5]: UnicodeRange/GetHashCode.md
[6]: UnicodeRange/BasicLatin.md
[7]: UnicodeRange/CJKPunctuation.md
[8]: UnicodeRange/Cyrillic.md
[9]: UnicodeRange/CyrillicSupplement.md
[10]: UnicodeRange/Hiragana.md
[11]: UnicodeRange/Katakana.md
[12]: UnicodeRange/Latin1Supplement.md
[13]: UnicodeRange/LatinExtendedA.md
[14]: UnicodeRange/LatinExtendedB.md
[15]: UnicodeRange.md
[16]: UnicodeCharacter.md
