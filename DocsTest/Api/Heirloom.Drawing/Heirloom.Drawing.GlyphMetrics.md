# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GlyphMetrics (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Contains information about a glyph (ie, the horizontal metrics).

| Fields                    | Summary                                                                                               |
|---------------------------|-------------------------------------------------------------------------------------------------------|
| [AdvanceWidth](#ADVAD761) | The advance width of the glyph. This is the spacing between the glyph's left edge and the next glyph. |
| [Bearing](#BEAR4DA5)      | The bearing of this glyph.                                                                            |

| Properties          | Summary                                 |
|---------------------|-----------------------------------------|
| [Offset](#OFFS1FA8) | The glyph offset from the pen position. |
| [Size](#SIZE9C93)   | The glyph bounds size.                  |

### Fields

#### <a name="ADVAD761"></a> AdvanceWidth : float
<small>`Read Only`</small>

The advance width of the glyph. This is the spacing between the glyph's left edge and the next glyph.

#### <a name="BEAR4DA5"></a> Bearing : float
<small>`Read Only`</small>

The bearing of this glyph.

### Constructors

#### GlyphMetrics(float advanceWidth, float bearing, [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) box)

### Properties

#### <a name="OFFS1FA8"></a> Offset : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

The glyph offset from the pen position.

#### <a name="SIZE9C93"></a> Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

The glyph bounds size.

