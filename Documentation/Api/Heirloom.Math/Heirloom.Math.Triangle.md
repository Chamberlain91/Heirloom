# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Triangle (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Triangle>, IEnumerable\<Vector>, IEnumerable</small>  
<small>`DefaultMemberAttribute`</small>

| Fields          | Summary           |
|-----------------|-------------------|
| [A](#ACDCAB7DD) | The first point.  |
| [B](#BCDCAB7E0) | The second point. |
| [C](#CCDCAB7DF) | The third point.  |

| Properties               | Summary                                              |
|--------------------------|------------------------------------------------------|
| [Bounds](#BOUBCFE829)    | Gets the bounds of this triangle.                    |
| [Area](#ARE9F5286F)      | Gets the area of this triangle.                      |
| [Centroid](#CENE921BA8E) | Gets the center of triangle (mean of corner points). |
| [Item](#ITE8B5A2F95)     |                                                      |

| Methods                            | Summary                                                                                   |
|------------------------------------|-------------------------------------------------------------------------------------------|
| [Set](#SETC49C14ED)                | Sets each point of the triangle.                                                          |
| [ToPolygon](#TOP74E314EF)          | Create a polygon from this triangle.                                                      |
| [GetClosestPoint](#GETDAC09B5B)    | Gets the closest point on the triangle to the specified point.                            |
| [Contains](#CON33387C1A)           | Determines if this triangle contains the specified point.                                 |
| [Overlaps](#OVE450AB809)           | Determines if this triangle overlaps another shape.                                       |
| [Overlaps](#OVEE125CFD7)           | Determines if this triangle overlaps the specified circle.                                |
| [Overlaps](#OVEB6714E43)           | Determines if this triangle overlaps another triangle.                                    |
| [Overlaps](#OVE5BEF9A70)           | Determines if this triangle overlaps the specified rectangle.                             |
| [Overlaps](#OVE89F258A7)           | Determines if this triangle overlaps the specified convex polygon.                        |
| [Overlaps](#OVE90B1A9F6)           | Determines if this triangle overlaps the specified simple polygon.                        |
| [Project](#PRODD6295AA)            | Project this polygon onto the specified axis.                                             |
| [Raycast](#RAYACE7FDBA)            | Peforms a raycast onto this rectangle, returning true upon intersection.                  |
| [Raycast](#RAY4B66C4A9)            | Peforms a raycast onto this rectangle, returning true upon intersection.                  |
| [Barycentric](#BAR12B7451)         | Computes the barycentric coefficients of the point `p` within the triangle.               |
| [GetEdge](#GETDC025800)            |                                                                                           |
| [Deconstruct](#DEC6E47A108)        |                                                                                           |
| [GetEnumerator](#GETE15EECC3)      |                                                                                           |
| [ContainsPoint](#CONBCA85FE8)      | Determines if the triangle defined by `a`, `b`, `c` contains the specified point.         |
| [Barycentric](#BARD70755F5)        | Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`. |
| [CreateCircumcircle](#CRE786283F6) | Computes the circumcircle for the specified triangle.                                     |
| [CreateCircumcircle](#CRE92BAA96D) | Computes the circumcircle for the specified triangle.                                     |

### Fields

#### <a name="ACDCAB7DD"></a>A : [Vector](Heirloom.Math.Vector.md)

The first point.

#### <a name="BCDCAB7E0"></a>B : [Vector](Heirloom.Math.Vector.md)

The second point.

#### <a name="CCDCAB7DF"></a>C : [Vector](Heirloom.Math.Vector.md)

The third point.

### Constructors

#### Triangle([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) c)

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounds of this triangle.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this triangle.

#### <a name="CENE921BA8E"></a>Centroid : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the center of triangle (mean of corner points).

#### <a name="ITE8B5A2F95"></a>Item : [Vector](Heirloom.Math.Vector.md)


### Methods

#### <a name="SETC49C14ED"></a>Set(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : void

Sets each point of the triangle.


#### <a name="TOP74E314EF"></a>ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this triangle.

#### <a name="GETDAC09B5B"></a>GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the closest point on the triangle to the specified point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this triangle contains the specified point.


#### <a name="OVE450AB809"></a>Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this triangle overlaps another shape.


#### <a name="OVEE125CFD7"></a>Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this triangle overlaps the specified circle.


#### <a name="OVEB6714E43"></a>Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this triangle overlaps another triangle.


#### <a name="OVE5BEF9A70"></a>Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this triangle overlaps the specified rectangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this triangle overlaps the specified convex polygon.


#### <a name="OVE90B1A9F6"></a>Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this triangle overlaps the specified simple polygon.


#### <a name="PRODD6295AA"></a>Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this polygon onto the specified axis.


#### <a name="RAYACE7FDBA"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  

#### <a name="RAY4B66C4A9"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

#### <a name="BAR12B7451"></a>Barycentric(in [Vector](Heirloom.Math.Vector.md) p, out float u, out float v, out float w) : void

Computes the barycentric coefficients of the point `p` within the triangle.


#### <a name="GETDC025800"></a>GetEdge(int index) : [LineSegment](Heirloom.Math.LineSegment.md)


#### <a name="DEC6E47A108"></a>Deconstruct(out [Vector](Heirloom.Math.Vector.md) a, out [Vector](Heirloom.Math.Vector.md) b, out [Vector](Heirloom.Math.Vector.md) c) : void


#### <a name="GETE15EECC3"></a>GetEnumerator() : IEnumerator\<Vector>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

#### <a name="CONBCA85FE8"></a>ContainsPoint(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Determines if the triangle defined by `a`, `b`, `c` contains the specified point.


#### <a name="BARD70755F5"></a>Barycentric(in [Vector](Heirloom.Math.Vector.md) p, in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, out float u, out float v, out float w) : void
<small>`Static`</small>

Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`.


#### <a name="CRE786283F6"></a>CreateCircumcircle(in [Triangle](Heirloom.Math.Triangle.md) tri) : [Circle](Heirloom.Math.Circle.md)
<small>`Static`</small>

Computes the circumcircle for the specified triangle.


#### <a name="CRE92BAA96D"></a>CreateCircumcircle(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : [Circle](Heirloom.Math.Circle.md)
<small>`Static`</small>

Computes the circumcircle for the specified triangle.


