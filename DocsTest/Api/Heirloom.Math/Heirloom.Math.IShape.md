# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IShape (Interface)
<small>**Namespace**: Heirloom.Math</sub></small>  

Represents the general interface of a shape and common operators each shape should support.

| Properties            | Summary                                    |
|-----------------------|--------------------------------------------|
| [Bounds](#BOUBCFE829) | Gets the bounding rectangle of this shape. |
| [Area](#ARE9F5286F)   | Gets the area of this shape.               |

| Methods                         | Summary                                                     |
|---------------------------------|-------------------------------------------------------------|
| [GetClosestPoint](#GETDAC09B5B) | Gets the nearest point on the shape to the specified point. |
| [Contains](#CON33387C1A)        | Determines if this shape contains the specified point.      |
| [Overlaps](#OVE450AB809)        | Determines if this shape overlaps the specified shape.      |
| [Raycast](#RAY4B66C4A9)         | Performs a raycast against this shape.                      |
| [Raycast](#RAYACE7FDBA)         | Performs a raycast against this shape.                      |
| [Project](#PRODD6295AA)         | Project this shape onto the specified axis.                 |

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this shape.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this shape.

### Methods

#### <a name="GETDAC09B5B"></a>GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Abstract`</small>

Gets the nearest point on the shape to the specified point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Abstract`</small>

Determines if this shape contains the specified point.


#### <a name="OVE450AB809"></a>Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Abstract`</small>

Determines if this shape overlaps the specified shape.


#### <a name="RAY4B66C4A9"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="RAYACE7FDBA"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool
<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="PRODD6295AA"></a>Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)
<small>`Abstract`</small>

Project this shape onto the specified axis.


