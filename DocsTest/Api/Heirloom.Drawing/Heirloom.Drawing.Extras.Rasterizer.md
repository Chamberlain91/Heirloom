# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Rasterizer (Static Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

Contains rasterization methods for rendering on the CPU.

| Methods                       | Summary                                        |
|-------------------------------|------------------------------------------------|
| [Rectangle](#RECTCDED)        | Rasterize a rectangular region.                |
| [Rectangle](#RECTCDED)        | Rasterize a rectangular region.                |
| [Rectangle](#RECTCDED)        | Rasterize a rectangular region.                |
| [Rectangle](#RECTCDED)        | Rasterize a rectangular region.                |
| [RectangleOutline](#RECT47F4) | Rasterize the outline of a rectangular region. |
| [Line](#LINE9C93)             | Rasterize along a line.                        |
| [Line](#LINE9C93)             | Rasterize along a line.                        |
| [Line](#LINE9C93)             | Rasterize along a line.                        |
| [Line](#LINE9C93)             | Rasterize along a line.                        |
| [Triangle](#TRIA72D4)         | Rasterize a triangle.                          |

### Methods

#### <a name="RECTDECA"></a> Rectangle(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a rectangular region.


#### <a name="RECTD028"></a> Rectangle([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rect) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="RECT2823"></a> Rectangle([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) position, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="RECTC602"></a> Rectangle([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="RECT4402"></a> RectangleOutline(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize the outline of a rectangular region.


#### <a name="LINEFBC9"></a> Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  

#### <a name="LINE5B58"></a> Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1,  byte pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LINE6A6A"></a> Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, ushort pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LINE5628"></a> Line([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, uint pattern) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="TRIAD057"></a> Triangle([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p0, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p1, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) p2) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a triangle.


