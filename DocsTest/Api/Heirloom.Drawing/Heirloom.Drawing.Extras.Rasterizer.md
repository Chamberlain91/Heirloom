# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Rasterizer (Static Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

Contains rasterization methods for rendering on the CPU.

| Methods                          | Summary                                        |
|----------------------------------|------------------------------------------------|
| [Rectangle](#RECDECA78E1)        | Rasterize a rectangular region.                |
| [Rectangle](#RECD028156F)        | Rasterize a rectangular region.                |
| [Rectangle](#REC28237985)        | Rasterize a rectangular region.                |
| [Rectangle](#RECC6025A58)        | Rasterize a rectangular region.                |
| [RectangleOutline](#REC44029BC9) | Rasterize the outline of a rectangular region. |
| [Line](#LINFBC9F047)             | Rasterize along a line.                        |
| [Line](#LIN5B58C631)             | Rasterize along a line.                        |
| [Line](#LIN6A6A37CE)             | Rasterize along a line.                        |
| [Line](#LIN56286E65)             | Rasterize along a line.                        |
| [Triangle](#TRID0579333)         | Rasterize a triangle.                          |

### Methods

#### <a name="RECDECA78E1"></a>Rectangle(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a rectangular region.


#### <a name="RECD028156F"></a>Rectangle([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rect) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC28237985"></a>Rectangle([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) position, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="RECC6025A58"></a>Rectangle([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC44029BC9"></a>RectangleOutline(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize the outline of a rectangular region.


#### <a name="LINFBC9F047"></a>Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  

#### <a name="LIN5B58C631"></a>Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1,  byte pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LIN6A6A37CE"></a>Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, ushort pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LIN56286E65"></a>Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, uint pattern) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="TRID0579333"></a>Triangle([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p2) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a triangle.


