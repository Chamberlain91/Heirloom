# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Graphics.DrawCounts (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

A little structure to keep tracking of counts of a drawn frame.

| Fields                        | Summary                        |
|-------------------------------|--------------------------------|
| [BatchCount](#BAT27B69C73)    | The number of batches.         |
| [DrawCount](#DRA5740BB87)     | The number of 'things' drawn.  |
| [TriangleCount](#TRIFB928221) | The number of triangles drawn. |

### Fields

#### <a name="BAT27B69C73"></a>BatchCount : int

The number of batches.

#### <a name="DRA5740BB87"></a>DrawCount : int

The number of 'things' drawn.

#### <a name="TRIFB928221"></a>TriangleCount : int

The number of triangles drawn.

A simple image (ie, Quad) consists of two triangles.

