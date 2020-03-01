# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntRectangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntRectangle></small>  

Represents a rectangle defined with integer coordinates.

| Fields                 | Summary                             |
|------------------------|-------------------------------------|
| [X](#XCDCAB7F6)        | The x-coordinate of this rectangle. |
| [Y](#YCDCAB7F5)        | The y-coordinate of this rectangle. |
| [Width](#WID68924896)  | The width of this rectangle.        |
| [Height](#HEIE098AAEB) | The height of this rectangle.       |

| Properties                       | Summary                                                                                                                           |
|----------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| [Area](#ARE9F5286F)              | Gets the area of this rectangle.                                                                                                  |
| [Size](#SIZ9C9392F9)             | Gets or sets the size of this rectangle.                                                                                          |
| [Position](#POSF46C3C91)         | Gets or sets the position of this rectangle.                                                                                      |
| [Center](#CEN7CD91D4B)           | Gets or sets the center position of this rectangle.                                                                               |
| [Min](#MINBF9EF002)              | Gets the minimum corner of this rectangle.                                                                                        |
| [Max](#MAXD4DA94E4)              | Gets the maximum corner of this rectangle.                                                                                        |
| [Left](#LEF9A907773)             | Gets the left extent of this rectangle.                                                                                           |
| [Top](#TOP4EDDB13)               | Gets the top extent of this rectangle.                                                                                            |
| [Right](#RIG1DA76FF8)            | Gets the right extent of this rectangle.                                                                                          |
| [Bottom](#BOTA278763B)           | Gets the bottom extent of this rectangle.                                                                                         |
| [TopLeft](#TOPC1C81AF0)          | Gets the top left corner of this rectangle.                                                                                       |
| [BottomLeft](#BOTFF353200)       | Gets the bottom left corner of this rectangle.                                                                                    |
| [BottomRight](#BOT19A55B57)      | Gets the bottom right corner of this rectangle.                                                                                   |
| [TopRight](#TOPEDB84AB7)         | Gets the top right corner of this rectangle.                                                                                      |
| [IsValid](#ISVE38FCA8)           | Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom. |
| [InvertedInfinite](#INVBAFDB881) | A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).                                     |
| [Infinite](#INFDABEDF6)          | A rectangle that spans the entire 2D integer plane.                                                                               |
| [One](#ONE62466566)              | A 1x1 rectangle that is positioned at the origin.                                                                                 |
| [Zero](#ZERC7D5C0B8)             | A 0x0 rectangle that is positioned at the origin.                                                                                 |

| Methods                     | Summary                                                            |
|-----------------------------|--------------------------------------------------------------------|
| [ToPolygon](#TOP74E314EF)   | Create a polygon from this rectangle.                              |
| [Offset](#OFFE1B0A55B)      | Translates this rectangle.                                         |
| [Offset](#OFFCA7AF18E)      | Translates this rectangle.                                         |
| [Include](#INCE62B0D30)     | Mutates this rectangle to accommodate the given point.             |
| [Include](#INC8A77FFFF)     | Mutates this rectangle to accommodate the given rectangle.         |
| [Inflate](#INF68BECCD2)     | Expands (or shrinks) the rectangle by a factor on both axis.       |
| [Inflate](#INF43C5F36B)     | Expands (or shrinks) the rectangle by a factor on each axis.       |
| [ClosestPoint](#CLO8A3F00D) | Returns the nearest point on the rectangle to the given point.     |
| [Contains](#CON33387C1A)    | Determines if this rectangle contains the given point?             |
| [Contains](#CONC965D5FA)    | Determines if this rectangle contains the given point?             |
| [Contains](#CONB7D08672)    | Determines if this rectangle contains another rectangle?           |
| [Overlaps](#OVE58E7EB96)    | Determines if this rectangle overlaps another rectangle.           |
| [Deconstruct](#DEC3AA0C4F1) |                                                                    |
| [Deconstruct](#DECAD541BCF) |                                                                    |
| [Offset](#OFF886E255B)      | Copies and translates the given rectangle.                         |
| [Offset](#OFF3BAEDB92)      | Copies and translates the given rectangle.                         |
| [Merge](#MERF77E122B)       | Merges the given rectangles into one potentially larger rectangle. |
| [Merge](#MER3F1A9762)       | Merges the given rectangles into one potentially larger rectangle. |
| [Inflate](#INFE3C98146)     | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate](#INF1642C30B)     | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [FromPoints](#FROD23D7A77)  | Computes the bounding rectangle of the given set of points.        |
| [FromPoints](#FRO7E8915A5)  | Computes the bounding rectangle of the given set of points.        |

### Fields

#### <a name="XCDCAB7F6"></a>X : int

The x-coordinate of this rectangle.

#### <a name="YCDCAB7F5"></a>Y : int

The y-coordinate of this rectangle.

#### <a name="WID68924896"></a>Width : int

The width of this rectangle.

#### <a name="HEIE098AAEB"></a>Height : int

The height of this rectangle.

### Constructors

#### IntRectangle(int x, int y, int width, int height)

#### IntRectangle([IntVector](Heirloom.Math.IntVector.md) position, [IntSize](Heirloom.Math.IntSize.md) size)

#### IntRectangle([IntVector](Heirloom.Math.IntVector.md) min, [IntVector](Heirloom.Math.IntVector.md) max)

### Properties

#### <a name="ARE9F5286F"></a>Area : int

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](Heirloom.Math.IntSize.md)


Gets or sets the size of this rectangle.

#### <a name="POSF46C3C91"></a>Position : [IntVector](Heirloom.Math.IntVector.md)


Gets or sets the position of this rectangle.

#### <a name="CEN7CD91D4B"></a>Center : [IntVector](Heirloom.Math.IntVector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9EF002"></a>Min : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA94E4"></a>Max : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the maximum corner of this rectangle.

#### <a name="LEF9A907773"></a>Left : int

<small>`Read Only`</small>

Gets the left extent of this rectangle.

#### <a name="TOP4EDDB13"></a>Top : int

<small>`Read Only`</small>

Gets the top extent of this rectangle.

#### <a name="RIG1DA76FF8"></a>Right : int

<small>`Read Only`</small>

Gets the right extent of this rectangle.

#### <a name="BOTA278763B"></a>Bottom : int

<small>`Read Only`</small>

Gets the bottom extent of this rectangle.

#### <a name="TOPC1C81AF0"></a>TopLeft : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTFF353200"></a>BottomLeft : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOT19A55B57"></a>BottomRight : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPEDB84AB7"></a>TopRight : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom.

#### <a name="INVBAFDB881"></a>InvertedInfinite : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).

#### <a name="INFDABEDF6"></a>Infinite : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane.

#### <a name="ONE62466566"></a>One : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntRectangle](Heirloom.Math.IntRectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOP74E314EF"></a>ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="OFFE1B0A55B"></a>Offset(int x, int y) : void

Translates this rectangle.


#### <a name="OFFCA7AF18E"></a>Offset([IntVector](Heirloom.Math.IntVector.md) offset) : void

Translates this rectangle.


#### <a name="INCE62B0D30"></a>Include([IntVector](Heirloom.Math.IntVector.md) point) : void

Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INC8A77FFFF"></a>Include(in [IntRectangle](Heirloom.Math.IntRectangle.md) rect) : void

Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INF68BECCD2"></a>Inflate(int factor) : void

Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INF43C5F36B"></a>Inflate(int xFactor, int yFactor) : void

Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="CLO8A3F00D"></a>ClosestPoint(in [IntVector](Heirloom.Math.IntVector.md) point) : [IntVector](Heirloom.Math.IntVector.md)

Returns the nearest point on the rectangle to the given point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONC965D5FA"></a>Contains(in [IntVector](Heirloom.Math.IntVector.md) point) : bool

Determines if this rectangle contains the given point?


#### <a name="CONB7D08672"></a>Contains(in [IntRectangle](Heirloom.Math.IntRectangle.md) other) : bool

Determines if this rectangle contains another rectangle?


#### <a name="OVE58E7EB96"></a>Overlaps([IntRectangle](Heirloom.Math.IntRectangle.md) other) : bool

Determines if this rectangle overlaps another rectangle.


#### <a name="DEC3AA0C4F1"></a>Deconstruct(out int x, out int y, out int w, out int h) : void


#### <a name="DECAD541BCF"></a>Deconstruct(out [IntVector](Heirloom.Math.IntVector.md) position, out [IntSize](Heirloom.Math.IntSize.md) size) : void


#### <a name="OFF886E255B"></a>Offset([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int x, int y) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFF3BAEDB92"></a>Offset([IntRectangle](Heirloom.Math.IntRectangle.md) rect, [IntVector](Heirloom.Math.IntVector.md) offset) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="MERF77E122B"></a>Merge(in [IntRectangle](Heirloom.Math.IntRectangle.md) a, in [IntRectangle](Heirloom.Math.IntRectangle.md) b) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param></small>  
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="MER3F1A9762"></a>Merge(params [IntRectangle[]](Heirloom.Math.IntRectangle.md) rects) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**rects**: <param name="rects">A collection of rectangles to merge.</param></small>  

Useful for computing a bounding rectangle.

#### <a name="INFE3C98146"></a>Inflate([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int factor) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INF1642C30B"></a>Inflate([IntRectangle](Heirloom.Math.IntRectangle.md) rect, int xFactor, int yFactor) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FROD23D7A77"></a>FromPoints(params [IntVector[]](Heirloom.Math.IntVector.md) points) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


#### <a name="FRO7E8915A5"></a>FromPoints(IEnumerable\<IntVector> points) : [IntRectangle](Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


