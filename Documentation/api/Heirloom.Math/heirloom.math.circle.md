# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Circle (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](heirloom.math.ishape.md), IEquatable\<Circle></small>  

Represents a circle via center position and radius.

| Fields | Summary |
|-------|---------|
| [Position](#POSF46C3C91) | The center position of the circle. |
| [Radius](#RAD6E859F5C) | The radius of the circle. |

| Properties | Summary |
|------------|---------|
| [Area](#ARE9F5286F) | Gets the area of the circle. |
| [Bounds](#BOUBCFE829) | Gets the bounding rectangle of this circle. |

| Methods | Summary |
|---------|---------|
| [ToPolygon](#TOP6F8ECA4F) | Create a polygon from this rectangle. |
| [GetClosestPoint](#GETCEB6999B) | Gets the nearest point on the circle to the specified point. |
| [Contains](#CONE7A5727A) | Determines if the specified point is contained by the circle. |
| [Contains](#CONAF8FC5F6) | Determines if this circle contains another circle. |
| [Overlaps](#OVEBC208089) | Determines if this circle overlaps another shape. |
| [Overlaps](#OVE624FF54F) | Determines if this circle overlaps another circle. |
| [Overlaps](#OVED270B350) | Determines if this circle overlaps the specified rectangle. |
| [Overlaps](#OVE30299463) | Determines if this circle overlaps the specified triangle. |
| [Overlaps](#OVE24834456) | Determines if this circle overlaps the specified simple polygon. |
| [Overlaps](#OVE89F258A7) | Determines if this circle overlaps the specified convex polygon. |
| [Project](#PROEEB2942A) | Project this circle onto the specified axis. |
| [Raycast](#RAYE998F35A) | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast](#RAY8D0F18C9) | Peforms a raycast onto this circle, returning true upon intersection. |

### Fields

#### <a name="POSF46C3C91"></a>Position : [Vector](heirloom.math.vector.md)

The center position of the circle.

#### <a name="RAD6E859F5C"></a>Radius : float

The radius of the circle.

### Constructors

#### Circle(float x, float y, float radius)

#### Circle([Vector](heirloom.math.vector.md) position, float radius)

### Properties

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of the circle.

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](heirloom.math.rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this circle.

### Methods

#### <a name="TOP6F8ECA4F"></a>ToPolygon() : [Polygon](heirloom.math.polygon.md)


Create a polygon from this rectangle.

#### <a name="GETCEB6999B"></a>GetClosestPoint(in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)


Gets the nearest point on the circle to the specified point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool


Determines if the specified point is contained by the circle.


#### <a name="CONAF8FC5F6"></a>Contains(in [Circle](heirloom.math.circle.md) circle) : bool


Determines if this circle contains another circle.


#### <a name="OVEBC208089"></a>Overlaps([IShape](heirloom.math.ishape.md) shape) : bool

<small>`Virtual`</small>

Determines if this circle overlaps another shape.


#### <a name="OVE624FF54F"></a>Overlaps(in [Circle](heirloom.math.circle.md) b) : bool


Determines if this circle overlaps another circle.


#### <a name="OVED270B350"></a>Overlaps(in [Rectangle](heirloom.math.rectangle.md) rectangle) : bool


Determines if this circle overlaps the specified rectangle.


#### <a name="OVE30299463"></a>Overlaps(in [Triangle](heirloom.math.triangle.md) triangle) : bool


Determines if this circle overlaps the specified triangle.


#### <a name="OVE24834456"></a>Overlaps([Polygon](heirloom.math.polygon.md) polygon) : bool


Determines if this circle overlaps the specified simple polygon.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool


Determines if this circle overlaps the specified convex polygon.


#### <a name="PROEEB2942A"></a>Project(in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)


Project this circle onto the specified axis.


#### <a name="RAYE998F35A"></a>Raycast(in [Ray](heirloom.math.ray.md) ray) : bool


Peforms a raycast onto this circle, returning true upon intersection.


#### <a name="RAY8D0F18C9"></a>Raycast(in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) contact) : bool


Peforms a raycast onto this circle, returning true upon intersection.

<small>**ray**: <param name="ray">Some ray.</param>  
</small>
<small>**contact**: <param name="contact">Ray intersection information.</param>  
</small>

