# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Triangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](heirloom.math.ishape.md), IEquatable\<Triangle>, IEnumerable\<Vector>, IEnumerable</small>  

| Fields | Summary |
|-------|---------|
| [A](#ACDCAB7DD) | The first point. |
| [B](#BCDCAB7E0) | The second point. |
| [C](#CCDCAB7DF) | The third point. |

| Properties | Summary |
|------------|---------|
| [Bounds](#BOUBCFE829) | Gets the bounds of this triangle. |
| [Area](#ARE9F5286F) | Gets the area of this triangle. |
| [Centroid](#CENE921BA8E) | Gets the center of triangle (mean of corner points). |
| [Item](#ITE8B5A2F95) |  |

| Methods | Summary |
|---------|---------|
| [ToPolygon](#TOP6F8ECA4F) | Create a polygon from this triangle. |
| [GetClosestPoint](#GETCEB6999B) | Gets the closest point on the triangle to the specified point. |
| [Contains](#CONE7A5727A) | Determines if this triangle contains the specified point. |
| [Overlaps](#OVEBC208089) | Determines if this triangle overlaps another shape. |
| [Overlaps](#OVE1C4FD437) | Determines if this triangle overlaps the specified circle. |
| [Overlaps](#OVE30299463) | Determines if this triangle overlaps another triangle. |
| [Overlaps](#OVED270B350) | Determines if this triangle overlaps the specified rectangle. |
| [Overlaps](#OVE89F258A7) | Determines if this triangle overlaps the specified convex polygon. |
| [Overlaps](#OVE24834456) | Determines if this triangle overlaps the specified simple polygon. |
| [Project](#PROEEB2942A) | Project this polygon onto the specified axis. |
| [Raycast](#RAYE998F35A) | Peforms a raycast onto this rectangle, returning true upon intersection. |
| [Raycast](#RAY8D0F18C9) | Peforms a raycast onto this rectangle, returning true upon intersection. |
| [Barycentric](#BAR3BE11D31) | Computes the barycentric coefficients of the point `p` within the triangle. |
| [GetEdge](#GET68AB5F40) |  |
| [Deconstruct](#DEC82374B28) |  |
| [GetEnumerator](#GETE15EECC3) |  |
| [ContainsPoint](#CONCE4B8D68) | Determines if the triangle defined by `a`, `b`, `c` contains the specified point. |
| [Barycentric](#BAR6D7A2A75) | Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`. |
| [CreateCircumcircle](#CRE191548B6) | Computes the circumcircle for the specified triangle. |
| [CreateCircumcircle](#CRE4ECA38ED) | Computes the circumcircle for the specified triangle. |

### Fields

#### <a name="ACDCAB7DD"></a>A : [Vector](heirloom.math.vector.md)

The first point.

#### <a name="BCDCAB7E0"></a>B : [Vector](heirloom.math.vector.md)

The second point.

#### <a name="CCDCAB7DF"></a>C : [Vector](heirloom.math.vector.md)

The third point.

### Constructors

#### Triangle([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b, [Vector](heirloom.math.vector.md) c)

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](heirloom.math.rectangle.md)

<small>`Read Only`</small>

Gets the bounds of this triangle.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this triangle.

#### <a name="CENE921BA8E"></a>Centroid : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the center of triangle (mean of corner points).

#### <a name="ITE8B5A2F95"></a>Item : [Vector](heirloom.math.vector.md)


### Methods

#### <a name="TOP6F8ECA4F"></a>ToPolygon() : [Polygon](heirloom.math.polygon.md)


Create a polygon from this triangle.

#### <a name="GETCEB6999B"></a>GetClosestPoint(in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)


Gets the closest point on the triangle to the specified point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool


Determines if this triangle contains the specified point.


#### <a name="OVEBC208089"></a>Overlaps([IShape](heirloom.math.ishape.md) shape) : bool

<small>`Virtual`</small>

Determines if this triangle overlaps another shape.


#### <a name="OVE1C4FD437"></a>Overlaps(in [Circle](heirloom.math.circle.md) circle) : bool


Determines if this triangle overlaps the specified circle.


#### <a name="OVE30299463"></a>Overlaps(in [Triangle](heirloom.math.triangle.md) triangle) : bool


Determines if this triangle overlaps another triangle.


#### <a name="OVED270B350"></a>Overlaps(in [Rectangle](heirloom.math.rectangle.md) rectangle) : bool


Determines if this triangle overlaps the specified rectangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool


Determines if this triangle overlaps the specified convex polygon.


#### <a name="OVE24834456"></a>Overlaps([Polygon](heirloom.math.polygon.md) polygon) : bool


Determines if this triangle overlaps the specified simple polygon.


#### <a name="PROEEB2942A"></a>Project(in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)


Project this polygon onto the specified axis.


#### <a name="RAYE998F35A"></a>Raycast(in [Ray](heirloom.math.ray.md) ray) : bool


Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param>  
</small>

#### <a name="RAY8D0F18C9"></a>Raycast(in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) contact) : bool


Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param>  
</small>
<small>**contact**: <param name="contact">Ray intersection information.</param>  
</small>

#### <a name="BAR3BE11D31"></a>Barycentric(in [Vector](heirloom.math.vector.md) p, out float u, out float v, out float w) : void


Computes the barycentric coefficients of the point `p` within the triangle.


#### <a name="GET68AB5F40"></a>GetEdge(int index) : [LineSegment](heirloom.math.linesegment.md)



#### <a name="DEC82374B28"></a>Deconstruct(out [Vector](heirloom.math.vector.md) a, out [Vector](heirloom.math.vector.md) b, out [Vector](heirloom.math.vector.md) c) : void



#### <a name="GETE15EECC3"></a>GetEnumerator() : IEnumerator\<Vector>

<small>`Virtual`, `IteratorStateMachineAttribute`</small>

#### <a name="CONCE4B8D68"></a>ContainsPoint(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in [Vector](heirloom.math.vector.md) point) : bool

<small>`Static`</small>

Determines if the triangle defined by `a`, `b`, `c` contains the specified point.


#### <a name="BAR6D7A2A75"></a>Barycentric(in [Vector](heirloom.math.vector.md) p, in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, out float u, out float v, out float w) : void

<small>`Static`</small>

Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`.


#### <a name="CRE191548B6"></a>CreateCircumcircle(in [Triangle](heirloom.math.triangle.md) tri) : [Circle](heirloom.math.circle.md)

<small>`Static`</small>

Computes the circumcircle for the specified triangle.


#### <a name="CRE4ECA38ED"></a>CreateCircumcircle(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c) : [Circle](heirloom.math.circle.md)

<small>`Static`</small>

Computes the circumcircle for the specified triangle.


