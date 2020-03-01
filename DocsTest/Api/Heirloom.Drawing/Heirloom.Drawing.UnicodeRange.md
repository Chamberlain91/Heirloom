# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## UnicodeRange (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEnumerable\<UnicodeCharacter>, IEnumerable, IEquatable\<UnicodeRange></small>  

Represents a range of unicode 32 bit code points.

| Fields                          | Summary                                        |
|---------------------------------|------------------------------------------------|
| [BasicLatin](#BASIE074)         | The basic latin unicode block.                 |
| [Latin1Supplement](#LATI97FF)   | The Latin-1 Supplement unicode block.          |
| [LatinExtendedA](#LATID809)     | The Latin Extended-A unicode block.            |
| [LatinExtendedB](#LATIB028)     | The Latin Extended-B unicode block.            |
| [Cyrillic](#CYRI2C48)           | The Cyrillic unicode block.                    |
| [CyrillicSupplement](#CYRIDC51) | The Cyrillic Supplement unicode block.         |
| [CJKPunctuation](#CJKP619A)     | The CJK Symbols and Punctuation unicode block. |
| [Hiragana](#HIRA5925)           | The Hiragana unicode block.                    |
| [Katakana](#KATA2E4B)           | The Katakana unicode block.                    |

| Properties         | Summary                           |
|--------------------|-----------------------------------|
| [Start](#STARC183) | The first character in the range. |
| [End](#END6246)    | The last character in the range.  |

| Methods                    | Summary |
|----------------------------|---------|
| [GetEnumerator](#GETEF1F9) |         |

### Fields

#### <a name="BASIE074"></a> BasicLatin : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The basic latin unicode block.

#### <a name="LATI97FF"></a> Latin1Supplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin-1 Supplement unicode block.

#### <a name="LATID809"></a> LatinExtendedA : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin Extended-A unicode block.

#### <a name="LATIB028"></a> LatinExtendedB : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Latin Extended-B unicode block.

#### <a name="CYRI2C48"></a> Cyrillic : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Cyrillic unicode block.

#### <a name="CYRIDC51"></a> CyrillicSupplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Cyrillic Supplement unicode block.

#### <a name="CJKP619A"></a> CJKPunctuation : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### <a name="HIRA5925"></a> Hiragana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Hiragana unicode block.

#### <a name="KATA2E4B"></a> Katakana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Read Only`</small>

The Katakana unicode block.

#### <a name="BASIE074"></a> BasicLatin : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The basic latin unicode block.

#### <a name="LATI97FF"></a> Latin1Supplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin-1 Supplement unicode block.

#### <a name="LATID809"></a> LatinExtendedA : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-A unicode block.

#### <a name="LATIB028"></a> LatinExtendedB : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Latin Extended-B unicode block.

#### <a name="CYRI2C48"></a> Cyrillic : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic unicode block.

#### <a name="CYRIDC51"></a> CyrillicSupplement : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Cyrillic Supplement unicode block.

#### <a name="CJKP619A"></a> CJKPunctuation : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The CJK Symbols and Punctuation unicode block.

#### <a name="HIRA5925"></a> Hiragana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Hiragana unicode block.

#### <a name="KATA2E4B"></a> Katakana : [UnicodeRange](Heirloom.Drawing.UnicodeRange.md)
<small>`Static`, `Read Only`</small>

The Katakana unicode block.

### Constructors

#### UnicodeRange([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) start, [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) end)

#### UnicodeRange([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) single)

### Properties

#### <a name="STARC183"></a> Start : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

The first character in the range.

#### <a name="END6246"></a> End : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

The last character in the range.

### Methods

#### <a name="GETE2D4D"></a> GetEnumerator() : IEnumerator\<UnicodeCharacter>
<small>`Virtual`</small>

