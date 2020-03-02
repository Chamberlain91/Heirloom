# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Glyph (Class)
<small>**Namespace**: Heirloom.Drawing</small>  

A glyph represents the metrics and rendering of a character from the associated [Font](Heirloom.Drawing.Font.md).

| Properties                    | Summary                                                    |
|-------------------------------|------------------------------------------------------------|
| [Font](#FON9AF9D8E3)          | Gets the associated font.                                  |
| [Character](#CHA601A43FD)     | Gets the character this glyph represents.                  |
| [HasCodepoint](#HAS859D7D5B)  |                                                            |
| [CanBeRendered](#CAN2821D71C) | Get a value that determines if this glyph can be rendered. |

| Methods                    | Summary                                                             |
|----------------------------|---------------------------------------------------------------------|
| [GetMetrics](#GET8C5581DC) | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderTo](#REN5B264EAC)   | Renders the glyph into the image.                                   |

### Constructors

#### Glyph([Font](Heirloom.Drawing.Font.md) font, int index)

### Properties

#### <a name="FON9AF9D8E3"></a>Font : [Font](Heirloom.Drawing.Font.md)

<small>`Read Only`</small>

Gets the associated font.

#### <a name="CHA601A43FD"></a>Character : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

Gets the character this glyph represents.

#### <a name="HAS859D7D5B"></a>HasCodepoint : bool


#### <a name="CAN2821D71C"></a>CanBeRendered : bool

<small>`Read Only`</small>

Get a value that determines if this glyph can be rendered.

### Methods

#### <a name="GET8C5581DC"></a>GetMetrics(float size) : [GlyphMetrics](Heirloom.Drawing.GlyphMetrics.md)

Get the horizontal metrics of the this glyph at the specified size.

<small>**size**: <param name="size">The size of the font.</param></small>  

#### <a name="REN5B264EAC"></a>RenderTo([Image](Heirloom.Drawing.Image.md) image, int x, int y, float size) : void

Renders the glyph into the image.

<small>**image**: <param name="image">Some image to render the glyph into.</param></small>  
<small>**x**: <param name="x">Offset on the x-axis in pixels.</param></small>  
<small>**y**: <param name="y">Offset on the y-axis in pixels.</param></small>  
<small>**size**: <param name="size">The font size of the rendered glyph.</param></small>  

