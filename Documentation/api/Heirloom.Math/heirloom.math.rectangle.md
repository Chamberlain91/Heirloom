# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Rectangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](heirloom.math.ishape.md), IEquatable\<Rectangle></small>  

| Fields | Summary |
|-------|---------|
| [X](#XCDCAB7F6) | The x-coordinate of this rectangle. |
| [Y](#YCDCAB7F5) | The y-coordinate of this rectangle. |
| [Width](#WID68924896) | The width of this rectangle. |
| [Height](#HEIE098AAEB) | The height of this rectangle. |

| Properties | Summary |
|------------|---------|
| [Heirloom.Math.IShape.Bounds](#HEI6069BB9C) |  |
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
| [IsValid](#ISVE38FCA8) | Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>. |
| [InvertedInfinite](#INVBAFDB881) | A rectangle that spans the entire 2D plane (but inverted, with min and max reversed). |
| [Infinite](#INFDABEDF6) | A rectangle that spans the entire 2D plane. |
| [One](#ONE62466566) | A 1x1 rectangle that is positioned at the origin. |
| [Zero](#ZERC7D5C0B8) | A 0x0 rectangle that is positioned at the origin. |

| Methods | Summary |
|---------|---------|
| [ToPolygon](#TOP6F8ECA4F) | Create a polygon from this rectangle. |
| [Offset](#OFF4948B8EF) | Translates this rectangle. |
| [Offset](#OFF24321A4E) | Translates this rectangle. |
| [Transform](#TRAE88F4FD7) | Transforms the four corners of this rectangle and updates itself to bound these points. |
| [Include](#INCFFCF31B0) | Mutates this rectangle to accommodate the given point. |
| [Include](#INCEDC440C3) | Mutates this rectangle to accommodate the given rectangle. |
| [Inflate](#INF28D34C57) | Expands (or shrinks) the rectangle by a factor on both axis. |
| [Inflate](#INF9C2C923F) | Expands (or shrinks) the rectangle by a factor on each axis. |
| [GetClosestPoint](#GETCEB6999B) | Returns the nearest point on the rectangle to the given point. |
| [Contains](#CONE7A5727A) | Determines if this rectangle contains the given point? |
| [Contains](#CON266836C0) | Determines if this rectangle contains another rectangle? |
| [Overlaps](#OVEBC208089) | Determines if this rectangle overlaps another shape. |
| [Overlaps](#OVE1C4FD437) | Determines if this rectangle overlaps the specified circle. |
| [Overlaps](#OVE30299463) | Determines if this rectangle overlaps the specified triangle. |
| [Overlaps](#OVEB34D0AA9) | Determines if this rectangle overlaps another rectangle. |
| [Overlaps](#OVE89F258A7) | Determines if this rectangle overlaps the specified convex polygon. |
| [Overlaps](#OVE24834456) | Determines if this rectangle overlaps the specified simple polygon. |
| [Project](#PROEEB2942A) | Project this rectangle onto the specified axis. |
| [Raycast](#RAYE998F35A) | Peforms a raycast onto this rectangle, returning true upon intersection. |
| [Raycast](#RAY8D0F18C9) | Peforms a raycast onto this circle, returning true upon intersection. |
| [Deconstruct](#DEC7AB8A711) |  |
| [Deconstruct](#DEC40E4DAF7) |  |
| [Offset](#OFF18D2723F) | Copies and translates the given rectangle. |
| [Offset](#OFF492D0202) | Copies and translates the given rectangle. |
| [Transform](#TRA8445C47D) | Transforms the four corners of this rectangle and returns the bounding rectangle of these points. |
| [Merge](#MER33ED5F27) | Merges the given rectangles into one potentially larger rectangle. |
| [Merge](#MER661D5452) | Merges the given rectangles into one potentially larger rectangle. |
| [Inflate](#INF7FC5CDB7) | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate](#INF9F33F16F) | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [FromPoints](#FROBA01C83B) | Computes the bounding rectangle of the given set of points. |
| [FromPoints](#FROAB639938) | Computes the bounding rectangle of the given set of points. |

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

#### Rectangle([Vector](heirloom.math.vector.md) position, [Size](heirloom.math.size.md) size)

#### Rectangle([Vector](heirloom.math.vector.md) min, [Vector](heirloom.math.vector.md) max)

### Properties

#### <a name="HEI6069BB9C"></a>Heirloom.Math.IShape.Bounds : [Rectangle](heirloom.math.rectangle.md)


#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this rectangle.

#### <a name="SIZ9C9392F9"></a>Size : [Size](heirloom.math.size.md)


Gets or sets the size of this rectangle.

#### <a name="POSF46C3C91"></a>Position : [Vector](heirloom.math.vector.md)


Gets or sets the position of this rectangle.

#### <a name="CEN7CD91D4B"></a>Center : [Vector](heirloom.math.vector.md)


Gets or sets the center position of this rectangle.

#### <a name="MINBF9EF002"></a>Min : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the minimum corner of this rectangle.

#### <a name="MAXD4DA94E4"></a>Max : [Vector](heirloom.math.vector.md)

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

#### <a name="TOPC1C81AF0"></a>TopLeft : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the top left corner of this rectangle.

#### <a name="BOTFF353200"></a>BottomLeft : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the bottom left corner of this rectangle.

#### <a name="BOT19A55B57"></a>BottomRight : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the bottom right corner of this rectangle.

#### <a name="TOPEDB84AB7"></a>TopRight : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the top right corner of this rectangle.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c>.

#### <a name="INVBAFDB881"></a>InvertedInfinite : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).

#### <a name="INFDABEDF6"></a>Infinite : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`, `Read Only`</small>

A rectangle that spans the entire 2D plane.

#### <a name="ONE62466566"></a>One : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`, `Read Only`</small>

A 1x1 rectangle that is positioned at the origin.

#### <a name="ZERC7D5C0B8"></a>Zero : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`, `Read Only`</small>

A 0x0 rectangle that is positioned at the origin.

### Methods

#### <a name="TOP6F8ECA4F"></a>ToPolygon() : [Polygon](heirloom.math.polygon.md)


Create a polygon from this rectangle.

#### <a name="OFF4948B8EF"></a>Offset(float x, float y) : void


Translates this rectangle.


#### <a name="OFF24321A4E"></a>Offset([Vector](heirloom.math.vector.md) offset) : void


Translates this rectangle.


#### <a name="TRAE88F4FD7"></a>Transform(in [Matrix](heirloom.math.matrix.md) matrix) : [Rectangle](heirloom.math.rectangle.md)


Transforms the four corners of this rectangle and updates itself to bound these points.


#### <a name="INCFFCF31B0"></a>Include([Vector](heirloom.math.vector.md) point) : void


Mutates this rectangle to accommodate the given point.

<small>**point**: <param name="point">Some point to include.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INCEDC440C3"></a>Include(in [Rectangle](heirloom.math.rectangle.md) rect) : void


Mutates this rectangle to accommodate the given rectangle.

<small>**rect**: <param name="rect">Some rectangle to include.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INF28D34C57"></a>Inflate(float factor) : void


Expands (or shrinks) the rectangle by a factor on both axis.


#### <a name="INF9C2C923F"></a>Inflate(float xFactor, float yFactor) : void


Expands (or shrinks) the rectangle by a factor on each axis.


#### <a name="GETCEB6999B"></a>GetClosestPoint(in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)


Returns the nearest point on the rectangle to the given point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool


Determines if this rectangle contains the given point?


#### <a name="CON266836C0"></a>Contains(in [Rectangle](heirloom.math.rectangle.md) other) : bool


Determines if this rectangle contains another rectangle?


#### <a name="OVEBC208089"></a>Overlaps([IShape](heirloom.math.ishape.md) shape) : bool

<small>`Virtual`</small>

Determines if this rectangle overlaps another shape.


#### <a name="OVE1C4FD437"></a>Overlaps(in [Circle](heirloom.math.circle.md) circle) : bool


Determines if this rectangle overlaps the specified circle.


#### <a name="OVE30299463"></a>Overlaps(in [Triangle](heirloom.math.triangle.md) triangle) : bool


Determines if this rectangle overlaps the specified triangle.


#### <a name="OVEB34D0AA9"></a>Overlaps(in [Rectangle](heirloom.math.rectangle.md) other) : bool


Determines if this rectangle overlaps another rectangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool


Determines if this rectangle overlaps the specified convex polygon.


#### <a name="OVE24834456"></a>Overlaps([Polygon](heirloom.math.polygon.md) polygon) : bool


Determines if this rectangle overlaps the specified simple polygon.


#### <a name="PROEEB2942A"></a>Project(in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)


Project this rectangle onto the specified axis.


#### <a name="RAYE998F35A"></a>Raycast(in [Ray](heirloom.math.ray.md) ray) : bool


Peforms a raycast onto this rectangle, returning true upon intersection.


#### <a name="RAY8D0F18C9"></a>Raycast(in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) contact) : bool


Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param>  
</small>
<small>**contact**: <param name="contact">Ray intersection information.</param>  
</small>

#### <a name="DEC7AB8A711"></a>Deconstruct(out float x, out float y, out float w, out float h) : void



#### <a name="DEC40E4DAF7"></a>Deconstruct(out [Vector](heirloom.math.vector.md) position, out [Size](heirloom.math.size.md) size) : void



#### <a name="OFF18D2723F"></a>Offset([Rectangle](heirloom.math.rectangle.md) rect, float x, float y) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="OFF492D0202"></a>Offset([Rectangle](heirloom.math.rectangle.md) rect, [Vector](heirloom.math.vector.md) offset) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Copies and translates the given rectangle.


#### <a name="TRA8445C47D"></a>Transform([Rectangle](heirloom.math.rectangle.md) rectangle, in [Matrix](heirloom.math.matrix.md) matrix) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Transforms the four corners of this rectangle and returns the bounding rectangle of these points.


#### <a name="MER33ED5F27"></a>Merge(in [Rectangle](heirloom.math.rectangle.md) a, in [Rectangle](heirloom.math.rectangle.md) b) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**a**: <param name="a">Some rectangle '<paramref name="a" />'.</param>  
</small>
<small>**b**: <param name="b">Some rectangle '<paramref name="b" />'.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="MER661D5452"></a>Merge(params [Rectangle[]](heirloom.math.rectangle.md) rects) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Merges the given rectangles into one potentially larger rectangle.

<small>**rects**: <param name="rects">A collection of rectangles to merge.</param>  
</small>

Useful for computing a bounding rectangle.

#### <a name="INF7FC5CDB7"></a>Inflate([Rectangle](heirloom.math.rectangle.md) rect, float factor) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on both axis.


#### <a name="INF9F33F16F"></a>Inflate([Rectangle](heirloom.math.rectangle.md) rect, float xFactor, float yFactor) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Expands (or shrinks) the input rectangle by a factor on each axis.


#### <a name="FROBA01C83B"></a>FromPoints(params [Vector[]](heirloom.math.vector.md) points) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


#### <a name="FROAB639938"></a>FromPoints(IEnumerable\<Vector> points) : [Rectangle](heirloom.math.rectangle.md)

<small>`Static`</small>

Computes the bounding rectangle of the given set of points.


