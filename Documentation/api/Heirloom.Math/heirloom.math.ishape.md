# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## IShape (Interface)
<small>**Namespace**: Heirloom.Math</sub></small>  

Represents the general interface of a shape and common operators each shape should support.

| Properties | Summary |
|------------|---------|
| [Bounds](#BOUBCFE829) | Gets the bounding rectangle of this shape. |
| [Area](#ARE9F5286F) | Gets the area of this shape. |

| Methods | Summary |
|---------|---------|
| [GetClosestPoint](#GETCEB6999B) | Gets the nearest point on the shape to the specified point. |
| [Contains](#CONE7A5727A) | Determines if this shape contains the specified point. |
| [Overlaps](#OVEBC208089) | Determines if this shape overlaps the specified shape. |
| [Raycast](#RAY8D0F18C9) | Performs a raycast against this shape. |
| [Raycast](#RAYE998F35A) | Performs a raycast against this shape. |
| [Project](#PROEEB2942A) | Project this shape onto the specified axis. |

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](heirloom.math.rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this shape.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this shape.

### Methods

#### <a name="GETCEB6999B"></a>GetClosestPoint(in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)

<small>`Abstract`</small>

Gets the nearest point on the shape to the specified point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool

<small>`Abstract`</small>

Determines if this shape contains the specified point.


#### <a name="OVEBC208089"></a>Overlaps([IShape](heirloom.math.ishape.md) shape) : bool

<small>`Abstract`</small>

Determines if this shape overlaps the specified shape.


#### <a name="RAY8D0F18C9"></a>Raycast(in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) contact) : bool

<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="RAYE998F35A"></a>Raycast(in [Ray](heirloom.math.ray.md) ray) : bool

<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="PROEEB2942A"></a>Project(in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)

<small>`Abstract`</small>

Project this shape onto the specified axis.


