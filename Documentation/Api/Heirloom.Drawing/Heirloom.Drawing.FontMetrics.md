# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## FontMetrics (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>`IsReadOnlyAttribute`</small>

Contains information about a font (ie, the vertical metrics).

| Fields                  | Summary                                                   |
|-------------------------|-----------------------------------------------------------|
| [Ascent](#ASC316359EC)  | The measure from the baseline to the top of any glyphs.   |
| [Descent](#DESD1D79FC4) | The measure from the baseline to the bottom of any glyph. |
| [LineGap](#LINF437CC38) | The spacing between lines.                                |

| Properties                  | Summary                                          |
|-----------------------------|--------------------------------------------------|
| [Height](#HEIE098AAEB)      | The height of the line.                          |
| [LineAdvance](#LINFB1BC226) | The spacing between baselines of multiline text. |

### Fields

#### <a name="ASC316359EC"></a>Ascent : float
<small>`Read Only`</small>

The measure from the baseline to the top of any glyphs.

#### <a name="DESD1D79FC4"></a>Descent : float
<small>`Read Only`</small>

The measure from the baseline to the bottom of any glyph.

#### <a name="LINF437CC38"></a>LineGap : float
<small>`Read Only`</small>

The spacing between lines.

### Constructors

#### FontMetrics(float ascent, float descent, float lineGap)

### Properties

#### <a name="HEIE098AAEB"></a>Height : float

<small>`Read Only`</small>

The height of the line.

#### <a name="LINFB1BC226"></a>LineAdvance : float

<small>`Read Only`</small>

The spacing between baselines of multiline text.

