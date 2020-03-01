# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Triangle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Triangle>, IEnumerable\<Vector>, IEnumerable</small>  

| Fields      | Summary           |
|-------------|-------------------|
| [A](#ACDCA) | The first point.  |
| [B](#BCDCA) | The second point. |
| [C](#CCDCA) | The third point.  |

| Properties            | Summary                                              |
|-----------------------|------------------------------------------------------|
| [Bounds](#BOUNBCFE)   | Gets the bounds of this triangle.                    |
| [Area](#AREA9F52)     | Gets the area of this triangle.                      |
| [Centroid](#CENTE921) | Gets the center of triangle (mean of corner points). |
| [Item](#ITEM8B5A)     |                                                      |

| Methods                         | Summary                                                                                   |
|---------------------------------|-------------------------------------------------------------------------------------------|
| [ToPolygon](#TOPO44DC)          | Create a polygon from this triangle.                                                      |
| [GetClosestPoint](#GETC53DD)    | Gets the closest point on the triangle to the specified point.                            |
| [Contains](#CONTD0AE)           | Determines if this triangle contains the specified point.                                 |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps another shape.                                       |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps the specified circle.                                |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps another triangle.                                    |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps the specified rectangle.                             |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps the specified convex polygon.                        |
| [Overlaps](#OVER7F2D)           | Determines if this triangle overlaps the specified simple polygon.                        |
| [Project](#PROJAD47)            | Project this polygon onto the specified axis.                                             |
| [Raycast](#RAYC408E)            | Peforms a raycast onto this rectangle, returning true upon intersection.                  |
| [Raycast](#RAYC408E)            | Peforms a raycast onto this rectangle, returning true upon intersection.                  |
| [Barycentric](#BARYFD93)        | Computes the barycentric coefficients of the point `p` within the triangle.               |
| [GetEdge](#GETEC1CA)            |                                                                                           |
| [Deconstruct](#DECOC188)        |                                                                                           |
| [GetEnumerator](#GETEF1F9)      |                                                                                           |
| [ContainsPoint](#CONT881C)      | Determines if the triangle defined by `a`, `b`, `c` contains the specified point.         |
| [Barycentric](#BARYFD93)        | Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`. |
| [CreateCircumcircle](#CREA2E12) | Computes the circumcircle for the specified triangle.                                     |
| [CreateCircumcircle](#CREA2E12) | Computes the circumcircle for the specified triangle.                                     |

### Fields

#### <a name="ACDCA"></a> A : [Vector](Heirloom.Math.Vector.md)

The first point.

#### <a name="BCDCA"></a> B : [Vector](Heirloom.Math.Vector.md)

The second point.

#### <a name="CCDCA"></a> C : [Vector](Heirloom.Math.Vector.md)

The third point.

### Constructors

#### Triangle([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) c)

### Properties

#### <a name="BOUNBCFE"></a> Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounds of this triangle.

#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of this triangle.

#### <a name="CENTE921"></a> Centroid : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the center of triangle (mean of corner points).

#### <a name="ITEM8B5A"></a> Item : [Vector](Heirloom.Math.Vector.md)


### Methods

#### <a name="TOPO74E3"></a> ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this triangle.

#### <a name="GETCDAC0"></a> GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the closest point on the triangle to the specified point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if this triangle contains the specified point.


#### <a name="OVER450A"></a> Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this triangle overlaps another shape.


#### <a name="OVERE125"></a> Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this triangle overlaps the specified circle.


#### <a name="OVERB671"></a> Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this triangle overlaps another triangle.


#### <a name="OVER5BEF"></a> Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this triangle overlaps the specified rectangle.


#### <a name="OVER89F2"></a> Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this triangle overlaps the specified convex polygon.


#### <a name="OVER90B1"></a> Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this triangle overlaps the specified simple polygon.


#### <a name="PROJDD62"></a> Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this polygon onto the specified axis.


#### <a name="RAYCACE7"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  

#### <a name="RAYC4B66"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this rectangle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

#### <a name="BARY12B7"></a> Barycentric(in [Vector](Heirloom.Math.Vector.md) p, out float u, out float v, out float w) : void

Computes the barycentric coefficients of the point `p` within the triangle.


#### <a name="GETEDC02"></a> GetEdge(int index) : [LineSegment](Heirloom.Math.LineSegment.md)


#### <a name="DECO6E47"></a> Deconstruct(out [Vector](Heirloom.Math.Vector.md) a, out [Vector](Heirloom.Math.Vector.md) b, out [Vector](Heirloom.Math.Vector.md) c) : void


#### <a name="GETEE15E"></a> GetEnumerator() : IEnumerator\<Vector>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

#### <a name="CONTBCA8"></a> ContainsPoint(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Determines if the triangle defined by `a`, `b`, `c` contains the specified point.


#### <a name="BARYD707"></a> Barycentric(in [Vector](Heirloom.Math.Vector.md) p, in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, out float u, out float v, out float w) : void
<small>`Static`</small>

Computes the barycentric coefficients of the point `p` within the triangle `a`, `b`, `c`.


#### <a name="CREA7862"></a> CreateCircumcircle(in [Triangle](Heirloom.Math.Triangle.md) tri) : [Circle](Heirloom.Math.Circle.md)
<small>`Static`</small>

Computes the circumcircle for the specified triangle.


#### <a name="CREA92BA"></a> CreateCircumcircle(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : [Circle](Heirloom.Math.Circle.md)
<small>`Static`</small>

Computes the circumcircle for the specified triangle.


