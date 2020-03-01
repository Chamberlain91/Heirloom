# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Rasterizer (Static Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

Contains rasterization methods for rendering on the CPU.

| Methods | Summary |
|---------|---------|
| [Rectangle](#RECDECA78E1) | Rasterize a rectangular region. |
| [Rectangle](#RECF1AB72EF) | Rasterize a rectangular region. |
| [Rectangle](#RECF754ED45) | Rasterize a rectangular region. |
| [Rectangle](#REC3E9D5518) | Rasterize a rectangular region. |
| [RectangleOutline](#REC44029BC9) | Rasterize the outline of a rectangular region. |
| [Line](#LINFFAB2F07) | Rasterize along a line. |
| [Line](#LINDE1192F1) | Rasterize along a line. |
| [Line](#LINFF95B80E) | Rasterize along a line. |
| [Line](#LIN18C5BA25) | Rasterize along a line. |
| [Triangle](#TRI4677BAB3) | Rasterize a triangle. |

### Methods

#### <a name="RECDECA78E1"></a>Rectangle(int x, int y, int width, int height) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a rectangular region.


#### <a name="RECF1AB72EF"></a>Rectangle([IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) rect) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="RECF754ED45"></a>Rectangle([IntVector](../heirloom.math/heirloom.math.intvector.md) position, [IntSize](../heirloom.math/heirloom.math.intsize.md) size) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC3E9D5518"></a>Rectangle([IntSize](../heirloom.math/heirloom.math.intsize.md) size) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC44029BC9"></a>RectangleOutline(int x, int y, int width, int height) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize the outline of a rectangular region.


#### <a name="LINFFAB2F07"></a>Line([IntVector](../heirloom.math/heirloom.math.intvector.md) p0, [IntVector](../heirloom.math/heirloom.math.intvector.md) p1) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param>  
</small>
<small>**p1**: <param name="p1">Ending point.</param>  
</small>

#### <a name="LINDE1192F1"></a>Line([IntVector](../heirloom.math/heirloom.math.intvector.md) p0, [IntVector](../heirloom.math/heirloom.math.intvector.md) p1,  byte pattern) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param>  
</small>
<small>**p1**: <param name="p1">Ending point.</param>  
</small>
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param>  
</small>

#### <a name="LINFF95B80E"></a>Line([IntVector](../heirloom.math/heirloom.math.intvector.md) p0, [IntVector](../heirloom.math/heirloom.math.intvector.md) p1, ushort pattern) : IEnumerable\<IntVector>

<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param>  
</small>
<small>**p1**: <param name="p1">Ending point.</param>  
</small>
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param>  
</small>

#### <a name="LIN18C5BA25"></a>Line([IntVector](../heirloom.math/heirloom.math.intvector.md) p0, [IntVector](../heirloom.math/heirloom.math.intvector.md) p1, uint pattern) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param>  
</small>
<small>**p1**: <param name="p1">Ending point.</param>  
</small>
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param>  
</small>

#### <a name="TRI4677BAB3"></a>Triangle([IntVector](../heirloom.math/heirloom.math.intvector.md) p0, [IntVector](../heirloom.math/heirloom.math.intvector.md) p1, [IntVector](../heirloom.math/heirloom.math.intvector.md) p2) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a triangle.


