# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntRectangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntRectangle></small>  

Represents a rectangle defined with integer coordinates.

| Fields              | Summary                             |
|---------------------|-------------------------------------|
| [X](#XCDCA)         | The x-coordinate of this rectangle. |
| [Y](#YCDCA)         | The y-coordinate of this rectangle. |
| [Width](#WIDT6892)  | The width of this rectangle.        |
| [Height](#HEIGE098) | The height of this rectangle.       |

| Properties                    | Summary                                                                                                                           |
|-------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| [Area](#AREA9F52)             | Gets the area of this rectangle.                                                                                                  |
| [Size](#SIZE9C93)             | Gets or sets the size of this rectangle.                                                                                          |
| [Position](#POSIF46C)         | Gets or sets the position of this rectangle.                                                                                      |
| [Center](#CENT7CD9)           | Gets or sets the center position of this rectangle.                                                                               |
| [Min](#MINBF9E)               | Gets the minimum corner of this rectangle.                                                                                        |
| [Max](#MAXD4DA)               | Gets the maximum corner of this rectangle.                                                                                        |
| [Left](#LEFT9A90)             | Gets the left extent of this rectangle.                                                                                           |
| [Top](#TOP4EDD)               | Gets the top extent of this rectangle.                                                                                            |
| [Right](#RIGH1DA7)            | Gets the right extent of this rectangle.                                                                                          |
| [Bottom](#BOTTA278)           | Gets the bottom extent of this rectangle.                                                                                         |
| [TopLeft](#TOPLC1C8)          | Gets the top left corner of this rectangle.                                                                                       |
| [BottomLeft](#BOTTFF35)       | Gets the bottom left corner of this rectangle.                                                                                    |
| [BottomRight](#BOTT19A5)      | Gets the bottom right corner of this rectangle.                                                                                   |
| [TopRight](#TOPREDB8)         | Gets the top right corner of this rectangle.                                                                                      |
| [IsValid](#ISVAE38F)          | Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom. |
| [InvertedInfinite](#INVEBAFD) | A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).                                     |
| [Infinite](#INFIDABE)         | A rectangle that spans the entire 2D integer plane.                                                                               |
| [One](#ONE6246)               | A 1x1 rectangle that is positioned at the origin.                                                                                 |
| [Zero](#ZEROC7D5)             | A 0x0 rectangle that is positioned at the origin.                                                                                 |

| Methods                   | Summary                                                            |
|---------------------------|--------------------------------------------------------------------|
| [ToPolygon](#TOPO44DC)    | Create a polygon from this rectangle.                              |
| [Offset](#OFFS1FA8)       | Translates this rectangle.                                         |
| [Offset](#OFFS1FA8)       | Translates this rectangle.                                         |
| [Include](#INCL2EBA)      | Mutates this rectangle to accommodate the given point.             |
| [Include](#INCL2EBA)      | Mutates this rectangle to accommodate the given rectangle.         |
| [Inflate](#INFL4434)      | Expands (or shrinks) the rectangle by a factor on both axis.       |
| [Inflate](#INFL4434)      | Expands (or shrinks) the rectangle by a factor on each axis.       |
| [ClosestPoint](#CLOSC5B5) | Returns the nearest point on the rectangle to the given point.     |
| [Contains](#CONTD0AE)     | Determines if this rectangle contains the given point?             |
| [Contains](#CONTD0AE)     | Determines if this rectangle contains the given point?             |
| [Contains](#CONTD0AE)     | Determines if this rectangle contains another rectangle?           |
| [Overlaps](#OVER7F2D)     | Determines if this rectangle overlaps another rectangle.           |
| [Deconstruct](#DECOC188)  |                                                                    |
| [Deconstruct](#DECOC188)  |                                                                    |
| [Offset](#OFFS1FA8)       | Copies and translates the given rectangle.                         |
| [Offset](#OFFS1FA8)       | Copies and translates the given rectangle.                         |
| [Merge](#MERG30DF)        | Merges the given rectangles into one potentially larger rectangle. |
| [Merge](#MERG30DF)        | Merges the given rectangles into one potentially larger rectangle. |
| [Inflate](#INFL4434)      | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate](#INFL4434)      | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [FromPoints](#FROM77DF)   | Computes the bounding rectangle of the given set of points.        |
| [FromPoints](#FROM77DF)   | Computes the bounding rectangle of the given set of points.        |

### Fields

#### <a name="XCDCA"></a> X : int

The x-coordinate of this rectangle.

#### <a name="YCDCA"></a> Y : int

The y-coordinate of this rectangle.

#### <a name="WIDT6892"></a> Width : int

The width of this rectangle.

#### <a name="HEIGE098"></a> Height : int

The height of this rectangle.

### Constructors

#### IntRectangle(int x, int y, int width, int height)

#### IntRectangle([IntVector](Heirloom.Math.IntVector.md) position, [IntSize](Heirloom.Math.IntSize.md) size)

#### IntRectangle([IntVector](Heirloom.Math.IntVector.md) min, [IntVector](Heirloom.Math.IntVector.md) max)

### Properties

#### <a name="AREA9F52"></a> Area : int

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZE9C93"></a> Size : [IntSize](Heirloom.Math.IntSize.md)


Gets or sets the size of this rectangle.

#### <a name="POSIF46C"></a> Position : [IntVector](Heirloom.Math.IntVector.md)


Gets or sets the position of this rectangle.

#### <a name="CENT7CD9"></a> Center : [IntVector](Heirloom.Math.IntVector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9E"></a> Min : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA"></a> Max : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the maximum corner of this rectangle.

#### <a name="LEFT9A90"></a> Left : int

<small>`Read Only`</small>

Gets the left extent of this rectangle.

#### <a name="TOP4EDD"></a> Top : int

<small>`Read Only`</small>

Gets the top extent of this rectangle.

#### <a name="RIGH1DA7"></a> Right : int

<small>`Read Only`</small>

Gets the right extent of this rectangle.

#### <a name="BOTTA278"></a> Bottom : int

<small>`Read Only`</small>

Gets the bottom extent of this rectangle.

#### <a name="TOPLC1C8"></a> TopLeft : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTTFF35"></a> BottomLeft : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOTT19A5"></a> BottomRight : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPREDB8"></a> TopRight : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVAE38F"></a> IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom.

#### <a name="INVEBAFD"></a> InvertedInfinite : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).

#### <a name="INFIDABE"></a> Infinite : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane.

#### <a name="ONE6246"></a> One : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZEROC7D5"></a> Zero : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOPO74E3"></a> ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="OFFSE1B0"></a> Offset(int x, int y) : void

Translates this rectangle.


#### <a name="OFFSCA7A"></a> Offset([IntVector](Heirloom.Math.IntVector.md) offset) : void

Translates this rectangle.


#### <a name="INCLE62B"></a> Include([IntVector](Heirloom.Math.IntVector.md) point) : void

Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INCL8A77"></a> Include(in [IntRectangle](Heirloom.Math.IntRectangle.md) rect) : void

Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INFL68BE"></a> Inflate(int factor) : void

Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INFL43C5"></a> Inflate(int xFactor, int yFactor) : void

Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="CLOS8A3F"></a> ClosestPoint(in [IntVector](Heirloom.Math.IntVector.md) point) : [IntVector](Heirloom.Math.IntVector.md)

Returns the nearest point on the rectangle to the given point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONTC965"></a> Contains(in [IntVector](Heirloom.Math.IntVector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONTB7D0"></a> Contains(in [IntRectangle](Heirloom.Math.IntRectangle.md) other) : bool

Determines if this rectangle contains another rectangle?


#### <a name="OVER58E7"></a> Overlaps([IntRectangle](Heirloom.Math.IntRectangle.md) other) : bool

Determines if this rectangle overlaps another rectangle.


#### <a name="DECO3AA0"></a> Deconstruct(out int x, out int y, out int w, out int h) : void


#### <a name="DECOAD54"></a> Deconstruct(out [IntVector](Heirloom.Math.IntVector.md) position, out [IntSize](Heirloom.Math.IntSize.md) size) : void


#### <a name="OFFS886E"></a> Offset([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int x, int y) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFFS3BAE"></a> Offset([IntRectangle](Heirloom.Math.IntRectangle.md) rect, [IntVector](Heirloom.Math.IntVector.md) offset) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="MERGF77E"></a> Merge(in [IntRectangle](Heirloom.Math.IntRectangle.md) a, in [IntRectangle](Heirloom.Math.IntRectangle.md) b) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param></small>  
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="MERG3F1A"></a> Merge(params [IntRectangle[]](Heirloom.Math.IntRectangle.md) rects) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**rects**: <param name="rects">A collection of rectangles to merge.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INFLE3C9"></a> Inflate([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int factor) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INFL1642"></a> Inflate([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int xFactor, int yFactor) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FROMD23D"></a> FromPoints(params [IntVector[]](Heirloom.Math.IntVector.md) points) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


#### <a name="FROM7E89"></a> FromPoints(IEnumerable\<IntVector> points) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


