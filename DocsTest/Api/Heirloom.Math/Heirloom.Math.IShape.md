# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IShape (Interface)
<small>**Namespace**: Heirloom.Math</sub></small>  

Represents the general interface of a shape and common operators each shape should support.

| Properties          | Summary                                    |
|---------------------|--------------------------------------------|
| [Bounds](#BOUNBCFE) | Gets the bounding rectangle of this shape. |
| [Area](#AREA9F52)   | Gets the area of this shape.               |

| Methods                      | Summary                                                     |
|------------------------------|-------------------------------------------------------------|
| [GetClosestPoint](#GETC53DD) | Gets the nearest point on the shape to the specified point. |
| [Contains](#CONTD0AE)        | Determines if this shape contains the specified point.      |
| [Overlaps](#OVER7F2D)        | Determines if this shape overlaps the specified shape.      |
| [Raycast](#RAYC408E)         | Performs a raycast against this shape.                      |
| [Raycast](#RAYC408E)         | Performs a raycast against this shape.                      |
| [Project](#PROJAD47)         | Project this shape onto the specified axis.                 |

### Properties

#### <a name="BOUNBCFE"></a> Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this shape.

#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of this shape.

### Methods

#### <a name="GETCDAC0"></a> GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Abstract`</small>

Gets the nearest point on the shape to the specified point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Abstract`</small>

Determines if this shape contains the specified point.


#### <a name="OVER450A"></a> Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Abstract`</small>

Determines if this shape overlaps the specified shape.


#### <a name="RAYC4B66"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="RAYCACE7"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool
<small>`Abstract`</small>

Performs a raycast against this shape.


#### <a name="PROJDD62"></a> Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)
<small>`Abstract`</small>

Project this shape onto the specified axis.


