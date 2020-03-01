# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## UnicodeRange (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEnumerable\<UnicodeCharacter>, IEnumerable, IEquatable\<UnicodeRange></small>  

Represents a range of unicode 32 bit code points.

| Fields | Summary |
|-------|---------|
| [BasicLatin](#BASE0740926) | The basic latin unicode block. |
| [Latin1Supplement](#LAT97FFA600) | The Latin-1 Supplement unicode block. |
| [LatinExtendedA](#LATD8093DC) | The Latin Extended-A unicode block. |
| [LatinExtendedB](#LATB0280877) | The Latin Extended-B unicode block. |
| [Cyrillic](#CYR2C48C1B3) | The Cyrillic unicode block. |
| [CyrillicSupplement](#CYRDC51EC54) | The Cyrillic Supplement unicode block. |
| [CJKPunctuation](#CJK619AD324) | The CJK Symbols and Punctuation unicode block. |
| [Hiragana](#HIR5925AA37) | The Hiragana unicode block. |
| [Katakana](#KAT2E4B338) | The Katakana unicode block. |

| Properties | Summary |
|------------|---------|
| [Start](#STAC1832F72) | The first character in the range. |
| [End](#END6246639B) | The last character in the range. |

| Methods | Summary |
|---------|---------|
| [GetEnumerator](#GET2D4D64BC) |  |

### Fields

#### BasicLatin : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The basic latin unicode block.

#### Latin1Supplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin-1 Supplement unicode block.

#### LatinExtendedA : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin Extended-A unicode block.

#### LatinExtendedB : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin Extended-B unicode block.

#### Cyrillic : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Cyrillic unicode block.

#### CyrillicSupplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Cyrillic Supplement unicode block.

#### CJKPunctuation : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### Hiragana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Hiragana unicode block.

#### Katakana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Katakana unicode block.

#### BasicLatin : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The basic latin unicode block.

#### Latin1Supplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin-1 Supplement unicode block.

#### LatinExtendedA : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-A unicode block.

#### LatinExtendedB : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-B unicode block.

#### Cyrillic : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic unicode block.

#### CyrillicSupplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic Supplement unicode block.

#### CJKPunctuation : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### Hiragana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Hiragana unicode block.

#### Katakana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Katakana unicode block.

### Constructors

#### UnicodeRange([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) start, [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) end)

#### UnicodeRange([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) single)

### Properties

#### <a name="STAC1832F72"></a>Start : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

The first character in the range.

#### <a name="END6246639B"></a>End : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

The last character in the range.

### Methods

#### <a name="GET2D4D64BC"></a>GetEnumerator() : IEnumerator\<UnicodeCharacter>

<small>`Virtual`</small>

