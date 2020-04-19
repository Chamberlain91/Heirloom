# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Rasterizer (Static Class)
<small>**Namespace**: Heirloom.Math.Extras</small>  

Contains rasterization methods for iterating over pixel positions.

This is useful beyond drawing images. For example, in a city builder the `Heirloom.Math.Extras.Rasterizer.Rectangle(Heirloom.Math.IntRectangle)` or `Heirloom.Math.Extras.Rasterizer.Line(Heirloom.Math.IntVector,Heirloom.Math.IntVector)` commands can be used to compute positions to place tiles when drawing building plots or roads.

| Methods                          | Summary                                        |
|----------------------------------|------------------------------------------------|
| [Rectangle](#RECDECA78E1)        | Rasterize a rectangular region.                |
| [Rectangle](#REC9EC3524C)        | Rasterize a rectangular region.                |
| [Rectangle](#REC5387F9D5)        | Rasterize a rectangular region.                |
| [Rectangle](#REC939D264B)        | Rasterize a rectangular region.                |
| [RectangleOutline](#REC44029BC9) | Rasterize the outline of a rectangular region. |
| [Circle](#CIRD12D7DC0)           | Rasterizes a filled circle.                    |
| [CircleOutline](#CIRA9FB513A)    | Rasterizes a circle outline.                   |
| [Triangle](#TRI3B91E810)         | Rasterize a triangle.                          |
| [Line](#LINC5D7E0CF)             | Rasterize along a line.                        |
| [Line](#LINF10D0665)             | Rasterize along a line.                        |
| [Line](#LIND050C4DA)             | Rasterize along a line.                        |
| [Line](#LIN31C83CF1)             | Rasterize along a line.                        |
| [HLine](#HLICC230B77)            | Iterate over a perfectly horizontal line.      |
| [VLine](#VLIBF3C19F8)            | Iterate over a perfectly vertical line.        |

### Methods

#### <a name="RECDECA78E1"></a>Rectangle(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a rectangular region.


#### <a name="REC9EC3524C"></a>Rectangle([IntRectangle](Heirloom.Math.IntRectangle.md) rect) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC5387F9D5"></a>Rectangle([IntVector](Heirloom.Math.IntVector.md) position, [IntSize](Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC939D264B"></a>Rectangle([IntSize](Heirloom.Math.IntSize.md) size) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize a rectangular region.


#### <a name="REC44029BC9"></a>RectangleOutline(int x, int y, int width, int height) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize the outline of a rectangular region.


#### <a name="CIRD12D7DC0"></a>Circle(int cx, int cy, int r) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterizes a filled circle.

<small>**cx**: <param name="cx">The center x coordinate of the circle.</param></small>  
<small>**cy**: <param name="cy">The center y coordinate of the circle.</param></small>  
<small>**r**: <param name="r">The radius of the circle.</param></small>  

#### <a name="CIRA9FB513A"></a>CircleOutline(int cx, int cy, int r) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterizes a circle outline.

<small>**cx**: <param name="cx">The center x coordinate of the circle.</param></small>  
<small>**cy**: <param name="cy">The center y coordinate of the circle.</param></small>  
<small>**r**: <param name="r">The radius of the circle.</param></small>  

#### <a name="TRI3B91E810"></a>Triangle([IntVector](Heirloom.Math.IntVector.md) p0, [IntVector](Heirloom.Math.IntVector.md) p1, [IntVector](Heirloom.Math.IntVector.md) p2) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize a triangle.


#### <a name="LINC5D7E0CF"></a>Line([IntVector](Heirloom.Math.IntVector.md) p0, [IntVector](Heirloom.Math.IntVector.md) p1) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  

#### <a name="LINF10D0665"></a>Line([IntVector](Heirloom.Math.IntVector.md) p0, [IntVector](Heirloom.Math.IntVector.md) p1,  byte pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LIND050C4DA"></a>Line([IntVector](Heirloom.Math.IntVector.md) p0, [IntVector](Heirloom.Math.IntVector.md) p1, ushort pattern) : IEnumerable\<IntVector>
<small>`Static`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="LIN31C83CF1"></a>Line([IntVector](Heirloom.Math.IntVector.md) p0, [IntVector](Heirloom.Math.IntVector.md) p1, uint pattern) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Rasterize along a line.

<small>**p0**: <param name="p0">Starting point.</param></small>  
<small>**p1**: <param name="p1">Ending point.</param></small>  
<small>**pattern**: <param name="pattern">Sequence of bits to mask drawing the line.</param></small>  

#### <a name="HLICC230B77"></a>HLine(int x1, int x2, int y) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Iterate over a perfectly horizontal line.

<small>**x1**: <param name="x1">Line start x coordinate.</param></small>  
<small>**x2**: <param name="x2">Line end x coordinate.</param></small>  
<small>**y**: <param name="y">Line y coordinate.</param></small>  

#### <a name="VLIBF3C19F8"></a>VLine(int y1, int y2, int x) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Iterate over a perfectly vertical line.

<small>**y1**: <param name="y1">Line start y coordinate.</param></small>  
<small>**y2**: <param name="y2">Line end y coordinate.</param></small>  
<small>**x**: <param name="x">Line x coordinate.</param></small>  

