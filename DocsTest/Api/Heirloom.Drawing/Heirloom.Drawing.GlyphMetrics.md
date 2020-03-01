# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GlyphMetrics (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Contains information about a glyph (ie, the horizontal metrics).

| Fields                       | Summary                                                                                               |
|------------------------------|-------------------------------------------------------------------------------------------------------|
| [AdvanceWidth](#ADVD761AEDC) | The advance width of the glyph. This is the spacing between the glyph's left edge and the next glyph. |
| [Bearing](#BEA4DA56914)      | The bearing of this glyph.                                                                            |

| Properties            | Summary                                 |
|-----------------------|-----------------------------------------|
| [Offset](#OFF1FA8EDD) | The glyph offset from the pen position. |
| [Size](#SIZ9C9392F9)  | The glyph bounds size.                  |

### Fields

#### <a name="ADVD761AEDC"></a>AdvanceWidth : float
<small>`Read Only`</small>

The advance width of the glyph. This is the spacing between the glyph's left edge and the next glyph.

#### <a name="BEA4DA56914"></a>Bearing : float
<small>`Read Only`</small>

The bearing of this glyph.

### Constructors

#### GlyphMetrics(float advanceWidth, float bearing, [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) box)

### Properties

#### <a name="OFF1FA8EDD"></a>Offset : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

The glyph offset from the pen position.

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

The glyph bounds size.

