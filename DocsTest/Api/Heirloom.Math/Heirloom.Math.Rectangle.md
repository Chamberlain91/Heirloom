# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Rectangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Rectangle></small>  

| Fields              | Summary                             |
|---------------------|-------------------------------------|
| [X](#XCDCA)         | The x-coordinate of this rectangle. |
| [Y](#YCDCA)         | The y-coordinate of this rectangle. |
| [Width](#WIDT6892)  | The width of this rectangle.        |
| [Height](#HEIGE098) | The height of this rectangle.       |

| Properties                               | Summary                                                                                                                                             |
|------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| [Heirloom.Math.IShape.Bounds](#HEIR6069) |                                                                                                                                                     |
| [Area](#AREA9F52)                        | Gets the area of this rectangle.                                                                                                                    |
| [Size](#SIZE9C93)                        | Gets or sets the size of this rectangle.                                                                                                            |
| [Position](#POSIF46C)                    | Gets or sets the position of this rectangle.                                                                                                        |
| [Center](#CENT7CD9)                      | Gets or sets the center position of this rectangle.                                                                                                 |
| [Min](#MINBF9E)                          | Gets the minimum corner of this rectangle.                                                                                                          |
| [Max](#MAXD4DA)                          | Gets the maximum corner of this rectangle.                                                                                                          |
| [Left](#LEFT9A90)                        | Gets the left extent of this rectangle.                                                                                                             |
| [Top](#TOP4EDD)                          | Gets the top extent of this rectangle.                                                                                                              |
| [Right](#RIGH1DA7)                       | Gets the right extent of this rectangle.                                                                                                            |
| [Bottom](#BOTTA278)                      | Gets the bottom extent of this rectangle.                                                                                                           |
| [TopLeft](#TOPLC1C8)                     | Gets the top left corner of this rectangle.                                                                                                         |
| [BottomLeft](#BOTTFF35)                  | Gets the bottom left corner of this rectangle.                                                                                                      |
| [BottomRight](#BOTT19A5)                 | Gets the bottom right corner of this rectangle.                                                                                                     |
| [TopRight](#TOPREDB8)                    | Gets the top right corner of this rectangle.                                                                                                        |
| [IsValid](#ISVAE38F)                     | Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>. |
| [InvertedInfinite](#INVEBAFD)            | A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).                                                               |
| [Infinite](#INFIDABE)                    | A rectangle that spans the entire 2D plane.                                                                                                         |
| [One](#ONE6246)                          | A 1x1 rectangle that is positioned at the origin.                                                                                                   |
| [Zero](#ZEROC7D5)                        | A 0x0 rectangle that is positioned at the origin.                                                                                                   |

| Methods                      | Summary                                                                                           |
|------------------------------|---------------------------------------------------------------------------------------------------|
| [ToPolygon](#TOPO44DC)       | Create a polygon from this rectangle.                                                             |
| [Offset](#OFFS1FA8)          | Translates this rectangle.                                                                        |
| [Offset](#OFFS1FA8)          | Translates this rectangle.                                                                        |
| [Transform](#TRAN97DF)       | Transforms the four corners of this rectangle and updates itself to bound these points.           |
| [Include](#INCL2EBA)         | Mutates this rectangle to accommodate the given point.                                            |
| [Include](#INCL2EBA)         | Mutates this rectangle to accommodate the given rectangle.                                        |
| [Inflate](#INFL4434)         | Expands (or shrinks) the rectangle by a factor on both axis.                                      |
| [Inflate](#INFL4434)         | Expands (or shrinks) the rectangle by a factor on each axis.                                      |
| [GetClosestPoint](#GETC53DD) | Returns the nearest point on the rectangle to the given point.                                    |
| [Contains](#CONTD0AE)        | Determines if this rectangle contains the given point?                                            |
| [Contains](#CONTD0AE)        | Determines if this rectangle contains another rectangle?                                          |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps another shape.                                              |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps the specified circle.                                       |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps the specified triangle.                                     |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps another rectangle.                                          |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps the specified convex polygon.                               |
| [Overlaps](#OVER7F2D)        | Determines if this rectangle overlaps the specified simple polygon.                               |
| [Project](#PROJAD47)         | Project this rectangle onto the specified axis.                                                   |
| [Raycast](#RAYC408E)         | Peforms a raycast onto this rectangle, returning true upon intersection.                          |
| [Raycast](#RAYC408E)         | Peforms a raycast onto this circle, returning true upon intersection.                             |
| [Deconstruct](#DECOC188)     |                                                                                                   |
| [Deconstruct](#DECOC188)     |                                                                                                   |
| [Offset](#OFFS1FA8)          | Copies and translates the given rectangle.                                                        |
| [Offset](#OFFS1FA8)          | Copies and translates the given rectangle.                                                        |
| [Transform](#TRAN97DF)       | Transforms the four corners of this rectangle and returns the bounding rectangle of these points. |
| [Merge](#MERG30DF)           | Merges the given rectangles into one potentially larger rectangle.                                |
| [Merge](#MERG30DF)           | Merges the given rectangles into one potentially larger rectangle.                                |
| [Inflate](#INFL4434)         | Expands (or shrinks) the input rectangle by a factor on both axis.                                |
| [Inflate](#INFL4434)         | Expands (or shrinks) the input rectangle by a factor on each axis.                                |
| [FromPoints](#FROM77DF)      | Computes the bounding rectangle of the given set of points.                                       |
| [FromPoints](#FROM77DF)      | Computes the bounding rectangle of the given set of points.                                       |

### Fields

#### <a name="XCDCA"></a> X : float

The x-coordinate of this rectangle.

#### <a name="YCDCA"></a> Y : float

The y-coordinate of this rectangle.

#### <a name="WIDT6892"></a> Width : float

The width of this rectangle.

#### <a name="HEIGE098"></a> Height : float

The height of this rectangle.

### Constructors

#### Rectangle(float x, float y, float width, float height)

#### Rectangle([Vector](Heirloom.Math.Vector.md) position, [Size](Heirloom.Math.Size.md) size)

#### Rectangle([Vector](Heirloom.Math.Vector.md) min, [Vector](Heirloom.Math.Vector.md) max)

### Properties

#### <a name="HEIR6069"></a> Heirloom.Math.IShape.Bounds : [Rectangle](Heirloom.Math.Rectangle.md)


#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZE9C93"></a> Size : [Size](Heirloom.Math.Size.md)


Gets or sets the size of this rectangle.

#### <a name="POSIF46C"></a> Position : [Vector](Heirloom.Math.Vector.md)


Gets or sets the position of this rectangle.

#### <a name="CENT7CD9"></a> Center : [Vector](Heirloom.Math.Vector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9E"></a> Min : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA"></a> Max : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the maximum corner of this rectangle.

#### <a name="LEFT9A90"></a> Left : float

<small>`Read Only`</small>

Gets the left extent of this rectangle.

#### <a name="TOP4EDD"></a> Top : float

<small>`Read Only`</small>

Gets the top extent of this rectangle.

#### <a name="RIGH1DA7"></a> Right : float

<small>`Read Only`</small>

Gets the right extent of this rectangle.

#### <a name="BOTTA278"></a> Bottom : float

<small>`Read Only`</small>

Gets the bottom extent of this rectangle.

#### <a name="TOPLC1C8"></a> TopLeft : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTTFF35"></a> BottomLeft : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOTT19A5"></a> BottomRight : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPREDB8"></a> TopRight : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVAE38F"></a> IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>.

#### <a name="INVEBAFD"></a> InvertedInfinite : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).

#### <a name="INFIDABE"></a> Infinite : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane.

#### <a name="ONE6246"></a> One : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZEROC7D5"></a> Zero : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOPO74E3"></a> ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="OFFS4948"></a> Offset(float x, float y) : void

Translates this rectangle.


#### <a name="OFFSA44B"></a> Offset([Vector](Heirloom.Math.Vector.md) offset) : void

Translates this rectangle.


#### <a name="TRAN5849"></a> Transform(in [Matrix](Heirloom.Math.Matrix.md) matrix) : [Rectangle](Heirloom.Math.Rectangle.md)

Transforms the four corners of this rectangle and updates itself to bound these points.


#### <a name="INCL4DBF"></a> Include([Vector](Heirloom.Math.Vector.md) point) : void

Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INCL8D0B"></a> Include(in [Rectangle](Heirloom.Math.Rectangle.md) rect) : void

Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INFL28D3"></a> Inflate(float factor) : void

Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INFL9C2C"></a> Inflate(float xFactor, float yFactor) : void

Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="GETCDAC0"></a> GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Returns the nearest point on the rectangle to the given point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONTDF22"></a> Contains(in [Rectangle](Heirloom.Math.Rectangle.md) other) : bool

Determines if this rectangle contains another rectangle?


#### <a name="OVER450A"></a> Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this rectangle overlaps another shape.


#### <a name="OVERE125"></a> Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this rectangle overlaps the specified circle.


#### <a name="OVERB671"></a> Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this rectangle overlaps the specified triangle.


#### <a name="OVERB68B"></a> Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) other) : bool

Determines if this rectangle overlaps another rectangle.


#### <a name="OVER89F2"></a> Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this rectangle overlaps the specified convex polygon.


#### <a name="OVER90B1"></a> Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this rectangle overlaps the specified simple polygon.


#### <a name="PROJDD62"></a> Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this rectangle onto the specified axis.


#### <a name="RAYCACE7"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.


#### <a name="RAYC4B66"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

#### <a name="DECO7AB8"></a> Deconstruct(out float x, out float y, out float w, out float h) : void


#### <a name="DECO2C82"></a> Deconstruct(out [Vector](Heirloom.Math.Vector.md) position, out [Size](Heirloom.Math.Size.md) size) : void


#### <a name="OFFS8A69"></a> Offset([Rectangle](Heirloom.Math.Rectangle.md) rect, float x, float y) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFFSD620"></a> Offset([Rectangle](Heirloom.Math.Rectangle.md) rect, [Vector](Heirloom.Math.Vector.md) offset) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="TRANB319"></a> Transform([Rectangle](Heirloom.Math.Rectangle.md) rectangle, in [Matrix](Heirloom.Math.Matrix.md) matrix) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Transforms the four corners of this rectangle and returns the bounding rectangle of these points.


#### <a name="MERG8FBD"></a> Merge(in [Rectangle](Heirloom.Math.Rectangle.md) a, in [Rectangle](Heirloom.Math.Rectangle.md) b) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param></small>  
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="MERG5376"></a> Merge(params [Rectangle[]](Heirloom.Math.Rectangle.md) rects) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**rects**: <param name="rects">A collection of rectangles to merge.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INFL7C81"></a> Inflate([Rectangle](Heirloom.Math.Rectangle.md) rect, float factor) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INFL45D1"></a> Inflate([Rectangle](Heirloom.Math.Rectangle.md) rect, float xFactor, float yFactor) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FROMC7F5"></a> FromPoints(params [Vector[]](Heirloom.Math.Vector.md) points) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


#### <a name="FROMCDF0"></a> FromPoints(IEnumerable\<Vector> points) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


