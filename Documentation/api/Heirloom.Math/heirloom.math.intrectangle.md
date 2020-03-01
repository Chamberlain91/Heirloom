# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## IntRectangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntRectangle></small>  

Represents a rectangle defined with integer coordinates.

| Fields | Summary |
|-------|---------|
| [X](#XCDCAB7F6) | The x-coordinate of this rectangle. |
| [Y](#YCDCAB7F5) | The y-coordinate of this rectangle. |
| [Width](#WID68924896) | The width of this rectangle. |
| [Height](#HEIE098AAEB) | The height of this rectangle. |

| Properties | Summary |
|------------|---------|
| [Area](#ARE9F5286F) | Gets the area of this rectangle. |
| [Size](#SIZ9C9392F9) | Gets or sets the size of this rectangle. |
| [Position](#POSF46C3C91) | Gets or sets the position of this rectangle. |
| [Center](#CEN7CD91D4B) | Gets or sets the center position of this rectangle. |
| [Min](#MINBF9EF002) | Gets the minimum corner of this rectangle. |
| [Max](#MAXD4DA94E4) | Gets the maximum corner of this rectangle. |
| [Left](#LEF9A907773) | Gets the left extent of this rectangle. |
| [Top](#TOP4EDDB13) | Gets the top extent of this rectangle. |
| [Right](#RIG1DA76FF8) | Gets the right extent of this rectangle. |
| [Bottom](#BOTA278763B) | Gets the bottom extent of this rectangle. |
| [TopLeft](#TOPC1C81AF0) | Gets the top left corner of this rectangle. |
| [BottomLeft](#BOTFF353200) | Gets the bottom left corner of this rectangle. |
| [BottomRight](#BOT19A55B57) | Gets the bottom right corner of this rectangle. |
| [TopRight](#TOPEDB84AB7) | Gets the top right corner of this rectangle. |
| [IsValid](#ISVE38FCA8) | Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom. |
| [InvertedInfinite](#INVBAFDB881) | A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed). |
| [Infinite](#INFDABEDF6) | A rectangle that spans the entire 2D integer plane. |
| [One](#ONE62466566) | A 1x1 rectangle that is positioned at the origin. |
| [Zero](#ZERC7D5C0B8) | A 0x0 rectangle that is positioned at the origin. |

| Methods | Summary |
|---------|---------|
| [ToPolygon](#TOP6F8ECA4F) | Create a polygon from this rectangle. |
| [Offset](#OFFE1B0A55B) | Translates this rectangle. |
| [Offset](#OFFFD16BB8E) | Translates this rectangle. |
| [Include](#INCD8B68470) | Mutates this rectangle to accommodate the given point. |
| [Include](#INC7A11B2BF) | Mutates this rectangle to accommodate the given rectangle. |
| [Inflate](#INF68BECCD2) | Expands (or shrinks) the rectangle by a factor on both axis. |
| [Inflate](#INF43C5F36B) | Expands (or shrinks) the rectangle by a factor on each axis. |
| [ClosestPoint](#CLO73D9698D) | Returns the nearest point on the rectangle to the given point. |
| [Contains](#CONE7A5727A) | Determines if this rectangle contains the given point? |
| [Contains](#CON89659F3A) | Determines if this rectangle contains the given point? |
| [Contains](#CON54A77272) | Determines if this rectangle contains another rectangle? |
| [Overlaps](#OVE8FAB6F96) | Determines if this rectangle overlaps another rectangle. |
| [Deconstruct](#DEC3AA0C4F1) |  |
| [Deconstruct](#DECBDD0480F) |  |
| [Offset](#OFFB5FD89B) | Copies and translates the given rectangle. |
| [Offset](#OFFF1EF2392) | Copies and translates the given rectangle. |
| [Merge](#MER60D846AB) | Merges the given rectangles into one potentially larger rectangle. |
| [Merge](#MERB0B63222) | Merges the given rectangles into one potentially larger rectangle. |
| [Inflate](#INF373F8B46) | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate](#INF84E3ED0B) | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [FromPoints](#FRO8E043077) | Computes the bounding rectangle of the given set of points. |
| [FromPoints](#FROAC9393A5) | Computes the bounding rectangle of the given set of points. |

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

#### IntRectangle([IntVector](heirloom.math.intvector.md) position, [IntSize](heirloom.math.intsize.md) size)

#### IntRectangle([IntVector](heirloom.math.intvector.md) min, [IntVector](heirloom.math.intvector.md) max)

### Properties

#### <a name="ARE9F5286F"></a>Area : int

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](heirloom.math.intsize.md)


Gets or sets the size of this rectangle.

#### <a name="POSF46C3C91"></a>Position : [IntVector](heirloom.math.intvector.md)


Gets or sets the position of this rectangle.

#### <a name="CEN7CD91D4B"></a>Center : [IntVector](heirloom.math.intvector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9EF002"></a>Min : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA94E4"></a>Max : [IntVector](heirloom.math.intvector.md)

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

#### <a name="TOPC1C81AF0"></a>TopLeft : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTFF353200"></a>BottomLeft : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOT19A55B57"></a>BottomRight : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPEDB84AB7"></a>TopRight : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom.

#### <a name="INVBAFDB881"></a>InvertedInfinite : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).

#### <a name="INFDABEDF6"></a>Infinite : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D integer plane.

#### <a name="ONE62466566"></a>One : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOP6F8ECA4F"></a>ToPolygon() : [Polygon](heirloom.math.polygon.md)


Create a polygon from this rectangle.

#### <a name="OFFE1B0A55B"></a>Offset(int x, int y) : void


Translates this rectangle.


#### <a name="OFFFD16BB8E"></a>Offset([IntVector](heirloom.math.intvector.md) offset) : void


Translates this rectangle.


#### <a name="INCD8B68470"></a>Include([IntVector](heirloom.math.intvector.md) point) : void


Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INC7A11B2BF"></a>Include(in [IntRectangle](heirloom.math.intrectangle.md) rect) : void


Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INF68BECCD2"></a>Inflate(int factor) : void


Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INF43C5F36B"></a>Inflate(int xFactor, int yFactor) : void


Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="CLO73D9698D"></a>ClosestPoint(in [IntVector](heirloom.math.intvector.md) point) : [IntVector](heirloom.math.intvector.md)


Returns the nearest point on the rectangle to the given point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool


Determines if this rectangle contains the given point?


#### <a name="CON89659F3A"></a>Contains(in [IntVector](heirloom.math.intvector.md) point) : bool


Determines if this rectangle contains the given point?


#### <a name="CON54A77272"></a>Contains(in [IntRectangle](heirloom.math.intrectangle.md) other) : bool


Determines if this rectangle contains another rectangle?


#### <a name="OVE8FAB6F96"></a>Overlaps([IntRectangle](heirloom.math.intrectangle.md) other) : bool


Determines if this rectangle overlaps another rectangle.


#### <a name="DEC3AA0C4F1"></a>Deconstruct(out int x, out int y, out int w, out int h) : void



#### <a name="DECBDD0480F"></a>Deconstruct(out [IntVector](heirloom.math.intvector.md) position, out [IntSize](heirloom.math.intsize.md) size) : void



#### <a name="OFFB5FD89B"></a>Offset([IntRectangle](heirloom.math.intrectangle.md) rect, int x, int y) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFFF1EF2392"></a>Offset([IntRectangle](heirloom.math.intrectangle.md) rect, [IntVector](heirloom.math.intvector.md) offset) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="MER60D846AB"></a>Merge(in [IntRectangle](heirloom.math.intrectangle.md) a, in [IntRectangle](heirloom.math.intrectangle.md) b) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param>  
</small>
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="MERB0B63222"></a>Merge(params [IntRectangle[]](heirloom.math.intrectangle.md) rects) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**rects**: <param name="rects">A collection of rectangles to merge.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INF373F8B46"></a>Inflate([IntRectangle](heirloom.math.intrectangle.md) rect, int factor) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INF84E3ED0B"></a>Inflate([IntRectangle](heirloom.math.intrectangle.md) rect, int xFactor, int yFactor) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FRO8E043077"></a>FromPoints(params [IntVector[]](heirloom.math.intvector.md) points) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


#### <a name="FROAC9393A5"></a>FromPoints(IEnumerable\<IntVector> points) : [IntRectangle](heirloom.math.intrectangle.md)

<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


