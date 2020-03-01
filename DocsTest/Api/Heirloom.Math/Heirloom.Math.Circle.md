# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Circle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Circle></small>  

Represents a circle via center position and radius.

| Fields                | Summary                            |
|-----------------------|------------------------------------|
| [Position](#POSIF46C) | The center position of the circle. |
| [Radius](#RADI6E85)   | The radius of the circle.          |

| Properties          | Summary                                     |
|---------------------|---------------------------------------------|
| [Area](#AREA9F52)   | Gets the area of the circle.                |
| [Bounds](#BOUNBCFE) | Gets the bounding rectangle of this circle. |

| Methods                      | Summary                                                               |
|------------------------------|-----------------------------------------------------------------------|
| [ToPolygon](#TOPO44DC)       | Create a polygon from this rectangle.                                 |
| [GetClosestPoint](#GETC53DD) | Gets the nearest point on the circle to the specified point.          |
| [Contains](#CONTD0AE)        | Determines if the specified point is contained by the circle.         |
| [Contains](#CONTD0AE)        | Determines if this circle contains another circle.                    |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps another shape.                     |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps another circle.                    |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps the specified rectangle.           |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps the specified triangle.            |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps the specified simple polygon.      |
| [Overlaps](#OVER7F2D)        | Determines if this circle overlaps the specified convex polygon.      |
| [Project](#PROJAD47)         | Project this circle onto the specified axis.                          |
| [Raycast](#RAYC408E)         | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast](#RAYC408E)         | Peforms a raycast onto this circle, returning true upon intersection. |

### Fields

#### <a name="POSIF46C"></a> Position : [Vector](Heirloom.Math.Vector.md)

The center position of the circle.

#### <a name="RADI6E85"></a> Radius : float

The radius of the circle.

### Constructors

#### Circle(float x, float y, float radius)

#### Circle([Vector](Heirloom.Math.Vector.md) position, float radius)

### Properties

#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of the circle.

#### <a name="BOUNBCFE"></a> Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this circle.

### Methods

#### <a name="TOPO74E3"></a> ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="GETCDAC0"></a> GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the nearest point on the circle to the specified point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if the specified point is contained by the circle.


#### <a name="CONT78E5"></a> Contains(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this circle contains another circle.


#### <a name="OVER450A"></a> Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this circle overlaps another shape.


#### <a name="OVERF01F"></a> Overlaps(in [Circle](Heirloom.Math.Circle.md) b) : bool

Determines if this circle overlaps another circle.


#### <a name="OVER5BEF"></a> Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this circle overlaps the specified rectangle.


#### <a name="OVERB671"></a> Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this circle overlaps the specified triangle.


#### <a name="OVER90B1"></a> Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this circle overlaps the specified simple polygon.


#### <a name="OVER89F2"></a> Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this circle overlaps the specified convex polygon.


#### <a name="PROJDD62"></a> Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this circle onto the specified axis.


#### <a name="RAYCACE7"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this circle, returning true upon intersection.


#### <a name="RAYC4B66"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

