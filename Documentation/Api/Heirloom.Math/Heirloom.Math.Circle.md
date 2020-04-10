# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Circle (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md), IEquatable\<Circle></small>  

Represents a circle via center position and radius.

| Fields                   | Summary                            |
|--------------------------|------------------------------------|
| [Position](#POSF46C3C91) | The center position of the circle. |
| [Radius](#RAD6E859F5C)   | The radius of the circle.          |

| Properties            | Summary                                     |
|-----------------------|---------------------------------------------|
| [Area](#ARE9F5286F)   | Gets the area of the circle.                |
| [Bounds](#BOUBCFE829) | Gets the bounding rectangle of this circle. |

| Methods                         | Summary                                                               |
|---------------------------------|-----------------------------------------------------------------------|
| [ToPolygon](#TOP74E314EF)       | Create a polygon from this rectangle.                                 |
| [GetClosestPoint](#GETDAC09B5B) | Gets the nearest point on the circle to the specified point.          |
| [Contains](#CON33387C1A)        | Determines if the specified point is contained by the circle.         |
| [Contains](#CON78E57F16)        | Determines if this circle contains another circle.                    |
| [Overlaps](#OVE450AB809)        | Determines if this circle overlaps another shape.                     |
| [Overlaps](#OVEF01FC2EF)        | Determines if this circle overlaps another circle.                    |
| [Overlaps](#OVE5BEF9A70)        | Determines if this circle overlaps the specified rectangle.           |
| [Overlaps](#OVEB6714E43)        | Determines if this circle overlaps the specified triangle.            |
| [Overlaps](#OVE90B1A9F6)        | Determines if this circle overlaps the specified simple polygon.      |
| [Overlaps](#OVE89F258A7)        | Determines if this circle overlaps the specified convex polygon.      |
| [Project](#PRODD6295AA)         | Project this circle onto the specified axis.                          |
| [Raycast](#RAYACE7FDBA)         | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast](#RAY4B66C4A9)         | Peforms a raycast onto this circle, returning true upon intersection. |

### Fields

#### <a name="POSF46C3C91"></a>Position : [Vector](Heirloom.Math.Vector.md)

The center position of the circle.

#### <a name="RAD6E859F5C"></a>Radius : float

The radius of the circle.

### Constructors

#### Circle(float x, float y, float radius)

#### Circle([Vector](Heirloom.Math.Vector.md) position, float radius)

### Properties

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of the circle.

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this circle.

### Methods

#### <a name="TOP74E314EF"></a>ToPolygon() : [Polygon](Heirloom.Math.Polygon.md)

Create a polygon from this rectangle.

#### <a name="GETDAC09B5B"></a>GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the nearest point on the circle to the specified point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if the specified point is contained by the circle.


#### <a name="CON78E57F16"></a>Contains(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this circle contains another circle.


#### <a name="OVE450AB809"></a>Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Determines if this circle overlaps another shape.


#### <a name="OVEF01FC2EF"></a>Overlaps(in [Circle](Heirloom.Math.Circle.md) b) : bool

Determines if this circle overlaps another circle.


#### <a name="OVE5BEF9A70"></a>Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this circle overlaps the specified rectangle.


#### <a name="OVEB6714E43"></a>Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this circle overlaps the specified triangle.


#### <a name="OVE90B1A9F6"></a>Overlaps([Polygon](Heirloom.Math.Polygon.md) polygon) : bool

Determines if this circle overlaps the specified simple polygon.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this circle overlaps the specified convex polygon.


#### <a name="PRODD6295AA"></a>Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this circle onto the specified axis.


#### <a name="RAYACE7FDBA"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Peforms a raycast onto this circle, returning true upon intersection.


#### <a name="RAY4B66C4A9"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool

Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param></small>  
<small>**contact**: <param name="contact">Ray intersection information.</param></small>  

