# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Glyph (Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

A glyph represents the metrics and rendering of a character from the associated [Font](Heirloom.Drawing.Font.md).

| Properties                 | Summary                                                    |
|----------------------------|------------------------------------------------------------|
| [Font](#FONT9AF9)          | Gets the associated font.                                  |
| [Character](#CHAR601A)     | Gets the character this glyph represents.                  |
| [HasCodepoint](#HASC859D)  |                                                            |
| [CanBeRendered](#CANB2821) | Get a value that determines if this glyph can be rendered. |

| Methods                 | Summary                                                             |
|-------------------------|---------------------------------------------------------------------|
| [GetMetrics](#GETM3646) | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderTo](#RENDE3F2)   | Renders the glyph into the image.                                   |

### Constructors

#### Glyph([Font](Heirloom.Drawing.Font.md) font, int index)

### Properties

#### <a name="FONT9AF9"></a> Font : [Font](Heirloom.Drawing.Font.md)

<small>`Read Only`</small>

Gets the associated font.

#### <a name="CHAR601A"></a> Character : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

Gets the character this glyph represents.

#### <a name="HASC859D"></a> HasCodepoint : bool


#### <a name="CANB2821"></a> CanBeRendered : bool

<small>`Read Only`</small>

Get a value that determines if this glyph can be rendered.

### Methods

#### <a name="GETM8C55"></a> GetMetrics(float size) : [GlyphMetrics](Heirloom.Drawing.GlyphMetrics.md)

Get the horizontal metrics of the this glyph at the specified size.

<small>**size**: <param name="size">The size of the font.</param></small>  

#### <a name="REND5B26"></a> RenderTo([Image](Heirloom.Drawing.Image.md) image, int x, int y, float size) : void

Renders the glyph into the image.

<small>**image**: <param name="image">Some image to render the glyph into.</param></small>  
<small>**x**: <param name="x">Offset on the x-axis in pixels.</param></small>  
<small>**y**: <param name="y">Offset on the y-axis in pixels.</param></small>  
<small>**size**: <param name="size">The font size of the rendered glyph.</param></small>  

