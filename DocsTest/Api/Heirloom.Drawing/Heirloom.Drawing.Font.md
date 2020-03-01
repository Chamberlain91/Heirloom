# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Font (Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

| Properties           | Summary                                                                                               |
|----------------------|-------------------------------------------------------------------------------------------------------|
| [Default](#DEFACF6E) | A default pixel font for easily rendering text to debug, show metrics, etc. Recommended size is 16px. |

| Methods                 | Summary                                                               |
|-------------------------|-----------------------------------------------------------------------|
| [GetMetrics](#GETM3646) | Get the vertical metrics of the this font at the specified size.      |
| [GetKerning](#GETKCB2D) | Gets the spacing adjustment (ie, kerning) between any two characters. |
| [GetGlyph](#GETGB5C6)   | Gets the information about a particular glyph in this font.           |
| [GetGlyph](#GETGB5C6)   | Gets the information about a particular glyph in this font.           |
| [Dispose](#DISP8A0D)    |                                                                       |
| [Dispose](#DISP8A0D)    |                                                                       |

### Constructors

#### Font(string path)

Loads a font specified by path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Font(Stream stream)

Loads a font from a stream.

#### Font(byte file)

Loads a font from a block of bytes.

### Properties

#### <a name="DEFACF6E"></a> Default : [Font](Heirloom.Drawing.Font.md)

<small>`Static`, `Read Only`</small>

A default pixel font for easily rendering text to debug, show metrics, etc. Recommended size is 16px.

https://datagoblin.itch.io/monogram

### Methods

#### <a name="GETM2E9B"></a> GetMetrics(float size) : [FontMetrics](Heirloom.Drawing.FontMetrics.md)

Get the vertical metrics of the this font at the specified size.

<small>**size**: <param name="size">The size of the font.</param></small>  

#### <a name="GETK1484"></a> GetKerning([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) cp1, [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) cp2, float size) : float

Gets the spacing adjustment (ie, kerning) between any two characters.


#### <a name="GETG4AD7"></a> GetGlyph(char ch) : [Glyph](Heirloom.Drawing.Glyph.md)

Gets the information about a particular glyph in this font.

<small>**ch**: <param name="ch">Some character.</param></small>  

#### <a name="GETG2101"></a> GetGlyph([UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md) ch) : [Glyph](Heirloom.Drawing.Glyph.md)

Gets the information about a particular glyph in this font.

<small>**ch**: <param name="ch">Some character.</param></small>  

#### <a name="DISPFDE7"></a> Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DISP4E62"></a> Dispose() : void
<small>`Virtual`</small>

