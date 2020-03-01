# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## FontMetrics (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Contains information about a font (ie, the vertical metrics).

| Fields               | Summary                                                   |
|----------------------|-----------------------------------------------------------|
| [Ascent](#ASCE3163)  | The measure from the baseline to the top of any glyphs.   |
| [Descent](#DESCD1D7) | The measure from the baseline to the bottom of any glyph. |
| [LineGap](#LINEF437) | The spacing between lines.                                |

| Properties               | Summary                                          |
|--------------------------|--------------------------------------------------|
| [Height](#HEIGE098)      | The height of the line.                          |
| [LineAdvance](#LINEFB1B) | The spacing between baselines of multiline text. |

### Fields

#### <a name="ASCE3163"></a> Ascent : float
<small>`Read Only`</small>

The measure from the baseline to the top of any glyphs.

#### <a name="DESCD1D7"></a> Descent : float
<small>`Read Only`</small>

The measure from the baseline to the bottom of any glyph.

#### <a name="LINEF437"></a> LineGap : float
<small>`Read Only`</small>

The spacing between lines.

### Constructors

#### FontMetrics(float ascent, float descent, float lineGap)

### Properties

#### <a name="HEIGE098"></a> Height : float

<small>`Read Only`</small>

The height of the line.

#### <a name="LINEFB1B"></a> LineAdvance : float

<small>`Read Only`</small>

The spacing between baselines of multiline text.

