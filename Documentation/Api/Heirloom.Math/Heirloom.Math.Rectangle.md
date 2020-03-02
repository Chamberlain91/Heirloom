# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Rectangle (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Rectangle></small>  

| Fields                 | Summary                             |
|------------------------|-------------------------------------|
| [X](#XCDCAB7F6)        | The x-coordinate of this rectangle. |
| [Y](#YCDCAB7F5)        | The y-coordinate of this rectangle. |
| [Width](#WID68924896)  | The width of this rectangle.        |
| [Height](#HEIE098AAEB) | The height of this rectangle.       |

| Properties                                  | Summary                                                                                                                                             |
|---------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| [Heirloom.Math.IShape.Bounds](#HEI6069BB9C) |                                                                                                                                                     |
| [Area](#ARE9F5286F)                         | Gets the area of this rectangle.                                                                                                                    |
| [Size](#SIZ9C9392F9)                        | Gets or sets the size of this rectangle.                                                                                                            |
| [Position](#POSF46C3C91)                    | Gets or sets the position of this rectangle.                                                                                                        |
| [Center](#CEN7CD91D4B)                      | Gets or sets the center position of this rectangle.                                                                                                 |
| [Min](#MINBF9EF002)                         | Gets the minimum corner of this rectangle.                                                                                                          |
| [Max](#MAXD4DA94E4)                         | Gets the maximum corner of this rectangle.                                                                                                          |
| [Left](#LEF9A907773)                        | Gets the left extent of this rectangle.                                                                                                             |
| [Top](#TOP4EDDB13)                          | Gets the top extent of this rectangle.                                                                                                              |
| [Right](#RIG1DA76FF8)                       | Gets the right extent of this rectangle.                                                                                                            |
| [Bottom](#BOTA278763B)                      | Gets the bottom extent of this rectangle.                                                                                                           |
| [TopLeft](#TOPC1C81AF0)                     | Gets the top left corner of this rectangle.                                                                                                         |
| [BottomLeft](#BOTFF353200)                  | Gets the bottom left corner of this rectangle.                                                                                                      |
| [BottomRight](#BOT19A55B57)                 | Gets the bottom right corner of this rectangle.                                                                                                     |
| [TopRight](#TOPEDB84AB7)                    | Gets the top right corner of this rectangle.                                                                                                        |
| [IsValid](#ISVE38FCA8)                      | Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>. |
| [InvertedInfinite](#INVBAFDB881)            | A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).                                                               |
| [Infinite](#INFDABEDF6)                     | A rectangle that spans the entire 2D plane.                                                                                                         |
| [One](#ONE62466566)                         | A 1x1 rectangle that is positioned at the origin.                                                                                                   |
| [Zero](#ZERC7D5C0B8)                        | A 0x0 rectangle that is positioned at the origin.                                                                                                   |

| Methods                         | Summary                                                                                           |
|---------------------------------|---------------------------------------------------------------------------------------------------|
| [ToPolygon](#TOP74E314EF)       | Create a polygon from this rectangle.                                                             |
| [Offset](#OFF4948B8EF)          | Translates this rectangle.                                                                        |
| [Offset](#OFFA44B506E)          | Translates this rectangle.                                                                        |
| [Transform](#TRA5849DF97)       | Transforms the four corners of this rectangle and updates itself to bound these points.           |
| [Include](#INC4DBFDA50)         | Mutates this rectangle to accommodate the given point.                                            |
| [Include](#INC8D0BD7E3)         | Mutates this rectangle to accommodate the given rectangle.                                        |
| [Inflate](#INF28D34C57)         | Expands (or shrinks) the rectangle by a factor on both axis.                                      |
| [Inflate](#INF9C2C923F)         | Expands (or shrinks) the rectangle by a factor on each axis.                                      |
| [GetClosestPoint](#GETDAC09B5B) | Returns the nearest point on the rectangle to the given point.                                    |
| [Contains](#CON33387C1A)        | Determines if this rectangle contains the given point?                                            |
| [Contains](#CONDF22FE60)        | Determines if this rectangle contains another rectangle?                                          |
| [Overlaps](#OVE450AB809)        | Determines if this rectangle overlaps another shape.                                              |
| [Overlaps](#OVEE125CFD7)        | Determines if this rectangle overlaps the specified circle.                                       |
| [Overlaps](#OVEB6714E43)        | Determines if this rectangle overlaps the specified triangle.                                     |
| [Overlaps](#OVEB68BDCC9)        | Determines if this rectangle overlaps another rectangle.                                          |
| [Overlaps](#OVE89F258A7)        | Determines if this rectangle overlaps the specified convex polygon.                               |
| [Overlaps](#OVE90B1A9F6)        | Determines if this rectangle overlaps the specified simple polygon.                               |
| [Project](#PRODD6295AA)         | Project this rectangle onto the specified axis.                                                   |
| [Raycast](#RAYACE7FDBA)         | Peforms a raycast onto this rectangle, returning true upon intersection.                          |
| [Raycast](#RAY4B66C4A9)         | Peforms a raycast onto this circle, returning true upon intersection.                             |
| [Deconstruct](#DEC7AB8A711)     |                                                                                                   |
| [Deconstruct](#DEC2C8278F7)     |                                                                                                   |
| [Offset](#OFF8A69213F)          | Copies and translates the given rectangle.                                                        |
| [Offset](#OFFD620F9A2)          | Copies and translates the given rectangle.                                                        |
| [Transform](#TRAB319FCDD)       | Transforms the four corners of this rectangle and returns the bounding rectangle of these points. |
| [Merge](#MER8FBD7147)           | Merges the given rectangles into one potentially larger rectangle.                                |
| [Merge](#MER53763ED2)           |                                                                                                   |
| [Inflate](#INF7C81AA37)         | Expands (or shrinks) the input rectangle by a factor on both axis.                                |
| [Inflate](#INF45D1582F)         | Expands (or shrinks) the input rectangle by a factor on each axis.                                |
| [FromPoints](#FROC7F5F8BB)      |                                                                                                   |
| [FromPoints](#FROCDF0BA98)      | Computes the bounding rectangle of the given set of points.                                       |

### Fields

#### <a name="XCDCAB7F6"></a>X : float

The x-coordinate of this rectangle.

#### <a name="YCDCAB7F5"></a>Y : float

The y-coordinate of this rectangle.

#### <a name="WID68924896"></a>Width : float

The width of this rectangle.

#### <a name="HEIE098AAEB"></a>Height : float

The height of this rectangle.

### Constructors

#### Rectangle(float x, float y, float width, float height)

#### Rectangle([Vector](Heirloom.Math.Vector.md) position, [Size](Heirloom.Math.Size.md) size)

#### Rectangle([Vector](Heirloom.Math.Vector.md) min, [Vector](Heirloom.Math.Vector.md) max)

### Properties

#### <a name="HEI6069BB9C"></a>Heirloom.Math.IShape.Bounds : [Rectangle](Heirloom.Math.Rectangle.md)


#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZ9C9392F9"></a>Size : [Size](Heirloom.Math.Size.md)


Gets or sets the size of this rectangle.

#### <a name="POSF46C3C91"></a>Position : [Vector](Heirloom.Math.Vector.md)


Gets or sets the position of this rectangle.

#### <a name="CEN7CD91D4B"></a>Center : [Vector](Heirloom.Math.Vector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9EF002"></a>Min : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA94E4"></a>Max : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the maximum corner of this rectangle.

#### <a name="LEF9A907773"></a>Left : float

<small>`Read Only`</small>

Gets the left extent of this rectangle.

#### <a name="TOP4EDDB13"></a>Top : float

<small>`Read Only`</small>

Gets the top extent of this rectangle.

#### <a name="RIG1DA76FF8"></a>Right : float

<small>`Read Only`</small>

Gets the right extent of this rectangle.

#### <a name="BOTA278763B"></a>Bottom : float

<small>`Read Only`</small>

Gets the bottom extent of this rectangle.

#### <a name="TOPC1C81AF0"></a>TopLeft : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTFF353200"></a>BottomLeft : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOT19A55B57"></a>BottomRight : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPEDB84AB7"></a>TopRight : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>.

#### <a name="INVBAFDB881"></a>InvertedInfinite : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).

#### <a name="INFDABEDF6"></a>Infinite : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane.

#### <a name="ONE62466566"></a>One : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZERC7D5C0B8"></a>Zero : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOP74E314EF"></a>ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="OFF4948B8EF"></a>Offset(float x, float y) : void

Translates this rectangle.


#### <a name="OFFA44B506E"></a>Offset([Vector](Heirloom.Math.Vector.md) offset) : void

Translates this rectangle.


#### <a name="TRA5849DF97"></a>Transform(in [Matrix](Heirloom.Math.Matrix.md) matrix) : [Rectangle](Heirloom.Math.Rectangle.md)

Transforms the four corners of this rectangle and updates itself to bound these points.


#### <a name="INC4DBFDA50"></a>Include([Vector](Heirloom.Math.Vector.md) point) : void

Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INC8D0BD7E3"></a>Include(in [Rectangle](Heirloom.Math.Rectangle.md) rect) : void

Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INF28D34C57"></a>Inflate(float factor) : void

Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INF9C2C923F"></a>Inflate(float xFactor, float yFactor) : void

Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="GETDAC09B5B"></a>GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Returns the nearest point on the rectangle to the given point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONDF22FE60"></a>Contains(in [Rectangle](Heirloom.Math.Rectangle.md) other) : bool

Determines if this rectangle contains another rectangle?


#### <a name="OVE450AB809"></a>Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this rectangle overlaps another shape.


#### <a name="OVEE125CFD7"></a>Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this rectangle overlaps the specified circle.


#### <a name="OVEB6714E43"></a>Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this rectangle overlaps the specified triangle.


#### <a name="OVEB68BDCC9"></a>Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) other) : bool

Determines if this rectangle overlaps another rectangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this rectangle overlaps the specified convex polygon.


#### <a name="OVE90B1A9F6"></a>Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this rectangle overlaps the specified simple polygon.


#### <a name="PRODD6295AA"></a>Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this rectangle onto the specified axis.


#### <a name="RAYACE7FDBA"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.


#### <a name="RAY4B66C4A9"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

#### <a name="DEC7AB8A711"></a>Deconstruct(out float x, out float y, out float w, out float h) : void


#### <a name="DEC2C8278F7"></a>Deconstruct(out [Vector](Heirloom.Math.Vector.md) position, out [Size](Heirloom.Math.Size.md) size) : void


#### <a name="OFF8A69213F"></a>Offset([Rectangle](Heirloom.Math.Rectangle.md) rect, float x, float y) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFFD620F9A2"></a>Offset([Rectangle](Heirloom.Math.Rectangle.md) rect, [Vector](Heirloom.Math.Vector.md) offset) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="TRAB319FCDD"></a>Transform([Rectangle](Heirloom.Math.Rectangle.md) rectangle, in [Matrix](Heirloom.Math.Matrix.md) matrix) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Transforms the four corners of this rectangle and returns the bounding rectangle of these points.


#### <a name="MER8FBD7147"></a>Merge(in [Rectangle](Heirloom.Math.Rectangle.md) a, in [Rectangle](Heirloom.Math.Rectangle.md) b) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param></small>  
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="MER53763ED2"></a>Merge(params [Rectangle[]](Heirloom.Math.Rectangle.md) rects) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>


#### <a name="INF7C81AA37"></a>Inflate([Rectangle](Heirloom.Math.Rectangle.md) rect, float factor) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INF45D1582F"></a>Inflate([Rectangle](Heirloom.Math.Rectangle.md) rect, float xFactor, float yFactor) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FROC7F5F8BB"></a>FromPoints(params [Vector[]](Heirloom.Math.Vector.md) points) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>


#### <a name="FROCDF0BA98"></a>FromPoints(IEnumerable\<Vector> points) : [Rectangle](Heirloom.Math.Rectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


