# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Glyph (Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

A glyph represents the metrics and rendering of a character from the associated [Font](heirloom.drawing.font.md).

| Properties | Summary |
|------------|---------|
| [Font](#FON9AF9D8E3) | Gets the associated font. |
| [Character](#CHA601A43FD) | Gets the character this glyph represents. |
| [HasCodepoint](#HAS859D7D5B) |  |
| [CanBeRendered](#CAN2821D71C) | Get a value that determines if this glyph can be rendered. |

| Methods | Summary |
|---------|---------|
| [GetMetrics](#GETDA27C09C) | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderTo](#RENF7D1BB4C) | Renders the glyph into the image. |

### Constructors

#### Glyph([Font](heirloom.drawing.font.md) font, int index)

### Properties

#### <a name="FON9AF9D8E3"></a>Font : [Font](heirloom.drawing.font.md)

<small>`Read Only`</small>

Gets the associated font.

#### <a name="CHA601A43FD"></a>Character : [UnicodeCharacter](heirloom.drawing.unicodecharacter.md)

<small>`Read Only`</small>

Gets the character this glyph represents.

#### <a name="HAS859D7D5B"></a>HasCodepoint : bool


#### <a name="CAN2821D71C"></a>CanBeRendered : bool

<small>`Read Only`</small>

Get a value that determines if this glyph can be rendered.

### Methods

#### <a name="GETDA27C09C"></a>GetMetrics(float size) : [GlyphMetrics](heirloom.drawing.glyphmetrics.md)


Get the horizontal metrics of the this glyph at the specified size.

<small>**size**: <param name="size">The size of the font.</param>  
</small>

#### <a name="RENF7D1BB4C"></a>RenderTo([Image](heirloom.drawing.image.md) image, int x, int y, float size) : void


Renders the glyph into the image.

<small>**image**: <param name="image">Some image to render the glyph into.</param>  
</small>
<small>**x**: <param name="x">Offset on the x-axis in pixels.</param>  
</small>
<small>**y**: <param name="y">Offset on the y-axis in pixels.</param>  
</small>
<small>**size**: <param name="size">The font size of the rendered glyph.</param>  
</small>

