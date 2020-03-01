# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Font (Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

| Properties | Summary |
|------------|---------|
| [Default](#DEFCF6EDD47) | A default pixel font for easily rendering text to debug, show metrics, etc. Recommended size is 16px. |

| Methods | Summary |
|---------|---------|
| [GetMetrics](#GET1941E29E) | Get the vertical metrics of the this font at the specified size. |
| [GetKerning](#GET81F114B5) | Gets the spacing adjustment (ie, kerning) between any two characters. |
| [GetGlyph](#GET32ADAE33) | Gets the information about a particular glyph in this font. |
| [GetGlyph](#GET2F054902) | Gets the information about a particular glyph in this font. |
| [Dispose](#DISFDE72264) |  |
| [Dispose](#DIS4E62D250) |  |

### Constructors

#### Font(string path)

Loads a font specified by path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Font(Stream stream)

Loads a font from a stream.

#### Font(byte file)

Loads a font from a block of bytes.

### Properties

#### <a name="DEFCF6EDD47"></a>Default : [Font](heirloom.drawing.font.md)

<small>`Static`, `Read Only`</small>

A default pixel font for easily rendering text to debug, show metrics, etc. Recommended size is 16px.

https://datagoblin.itch.io/monogram

### Methods

#### <a name="GET1941E29E"></a>GetMetrics(float size) : [FontMetrics](heirloom.drawing.fontmetrics.md)


Get the vertical metrics of the this font at the specified size.

<small>**size**: <param name="size">The size of the font.</param>  
</small>

#### <a name="GET81F114B5"></a>GetKerning([UnicodeCharacter](heirloom.drawing.unicodecharacter.md) cp1, [UnicodeCharacter](heirloom.drawing.unicodecharacter.md) cp2, float size) : float


Gets the spacing adjustment (ie, kerning) between any two characters.


#### <a name="GET32ADAE33"></a>GetGlyph(char ch) : [Glyph](heirloom.drawing.glyph.md)


Gets the information about a particular glyph in this font.

<small>**ch**: <param name="ch">Some character.</param>  
</small>

#### <a name="GET2F054902"></a>GetGlyph([UnicodeCharacter](heirloom.drawing.unicodecharacter.md) ch) : [Glyph](heirloom.drawing.glyph.md)


Gets the information about a particular glyph in this font.

<small>**ch**: <param name="ch">Some character.</param>  
</small>

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void

<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void

<small>`Virtual`</small>

