# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
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

#### <a name="BASE0740926"></a>BasicLatin : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The basic latin unicode block.

#### <a name="LAT97FFA600"></a>Latin1Supplement : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Latin-1 Supplement unicode block.

#### <a name="LATD8093DC"></a>LatinExtendedA : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Latin Extended-A unicode block.

#### <a name="LATB0280877"></a>LatinExtendedB : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Latin Extended-B unicode block.

#### <a name="CYR2C48C1B3"></a>Cyrillic : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Cyrillic unicode block.

#### <a name="CYRDC51EC54"></a>CyrillicSupplement : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Cyrillic Supplement unicode block.

#### <a name="CJK619AD324"></a>CJKPunctuation : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### <a name="HIR5925AA37"></a>Hiragana : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Hiragana unicode block.

#### <a name="KAT2E4B338"></a>Katakana : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Read Only`</small>

The Katakana unicode block.

#### <a name="BASE0740926"></a>BasicLatin : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The basic latin unicode block.

#### <a name="LAT97FFA600"></a>Latin1Supplement : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Latin-1 Supplement unicode block.

#### <a name="LATD8093DC"></a>LatinExtendedA : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-A unicode block.

#### <a name="LATB0280877"></a>LatinExtendedB : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-B unicode block.

#### <a name="CYR2C48C1B3"></a>Cyrillic : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic unicode block.

#### <a name="CYRDC51EC54"></a>CyrillicSupplement : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic Supplement unicode block.

#### <a name="CJK619AD324"></a>CJKPunctuation : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### <a name="HIR5925AA37"></a>Hiragana : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Hiragana unicode block.

#### <a name="KAT2E4B338"></a>Katakana : [UnicodeRange](heirloom.drawing.unicoderange.md)
<small>`Static`, `Read Only`</small>

The Katakana unicode block.

### Constructors

#### UnicodeRange([UnicodeCharacter](heirloom.drawing.unicodecharacter.md) start, [UnicodeCharacter](heirloom.drawing.unicodecharacter.md) end)

#### UnicodeRange([UnicodeCharacter](heirloom.drawing.unicodecharacter.md) single)

### Properties

#### <a name="STAC1832F72"></a>Start : [UnicodeCharacter](heirloom.drawing.unicodecharacter.md)

<small>`Read Only`</small>

The first character in the range.

#### <a name="END6246639B"></a>End : [UnicodeCharacter](heirloom.drawing.unicodecharacter.md)

<small>`Read Only`</small>

The last character in the range.

### Methods

#### <a name="GET2D4D64BC"></a>GetEnumerator() : IEnumerator\<UnicodeCharacter>

<small>`Virtual`</small>

