# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayoutState (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<TextLayoutState></small>  

Represents information of any particular glyph during text layout.

| Fields | Summary |
|-------|---------|
| [Position](#POSF46C3C91) | The position of top left corner of the current glyph image. |

| Properties | Summary |
|------------|---------|
| [Character](#CHA601A43FD) | The current character. |
| [Metrics](#MET4AD7589B) | The metrics of the glyph being rendered. |

### Fields

#### <a name="POSF46C3C91"></a>Position : [Vector](../heirloom.math/heirloom.math.vector.md)

The position of top left corner of the current glyph image.

### Properties

#### <a name="CHA601A43FD"></a>Character : [UnicodeCharacter](heirloom.drawing.unicodecharacter.md)

<small>`Read Only`</small>

The current character.

#### <a name="MET4AD7589B"></a>Metrics : [GlyphMetrics](heirloom.drawing.glyphmetrics.md)

<small>`Read Only`</small>

The metrics of the glyph being rendered.

