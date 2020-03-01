# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## TextLayoutState (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<TextLayoutState></small>  

Represents information of any particular glyph during text layout.

| Fields                | Summary                                                     |
|-----------------------|-------------------------------------------------------------|
| [Position](#POSIF46C) | The position of top left corner of the current glyph image. |

| Properties             | Summary                                  |
|------------------------|------------------------------------------|
| [Character](#CHAR601A) | The current character.                   |
| [Metrics](#METR4AD7)   | The metrics of the glyph being rendered. |

### Fields

#### <a name="POSIF46C"></a> Position : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)

The position of top left corner of the current glyph image.

### Properties

#### <a name="CHAR601A"></a> Character : [UnicodeCharacter](Heirloom.Drawing.UnicodeCharacter.md)

<small>`Read Only`</small>

The current character.

#### <a name="METR4AD7"></a> Metrics : [GlyphMetrics](Heirloom.Drawing.GlyphMetrics.md)

<small>`Read Only`</small>

The metrics of the glyph being rendered.

