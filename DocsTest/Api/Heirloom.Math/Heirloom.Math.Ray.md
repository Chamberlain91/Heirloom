# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Ray (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Ray></small>  

Represents a ray by orgin and direction vectors.

| Fields                 | Summary                   |
|------------------------|---------------------------|
| [Origin](#ORIG85E4)    | The origin of the ray.    |
| [Direction](#DIRE7D7E) | The direction of the ray. |

| Methods                      | Summary                                                                |
|------------------------------|------------------------------------------------------------------------|
| [GetPoint](#GETP8949)        | Gets a point along the ray.                                            |
| [Deconstruct](#DECOC188)     |                                                                        |
| [FromLineSegment](#FROMB22C) | Creates a ray from a line segment.                                     |
| [FromLineSegment](#FROMB22C) | Creates a ray from a line segment.                                     |
| [FromAngle](#FROM9C6B)       | Creates a ray pointed along the specified angle from the origin given. |
| [Intersects](#INTE62E2)      | Computes the intersection of two rays.                                 |

### Fields

#### <a name="ORIG85E4"></a> Origin : [Vector](Heirloom.Math.Vector.md)

The origin of the ray.

#### <a name="DIRE7D7E"></a> Direction : [Vector](Heirloom.Math.Vector.md)

The direction of the ray.

### Constructors

#### Ray([Vector](Heirloom.Math.Vector.md) origin, [Vector](Heirloom.Math.Vector.md) direction)

### Methods

#### <a name="GETP3C44"></a> GetPoint(float distance) : [Vector](Heirloom.Math.Vector.md)

Gets a point along the ray.


#### <a name="DECO1943"></a> Deconstruct(out [Vector](Heirloom.Math.Vector.md) origin, out [Vector](Heirloom.Math.Vector.md) direction) : void


#### <a name="FROM3D59"></a> FromLineSegment(in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) target) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FROM3497"></a> FromLineSegment(in [LineSegment](Heirloom.Math.LineSegment.md) segment) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FROM9C3D"></a> FromAngle([Vector](Heirloom.Math.Vector.md) origin, float angle) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray pointed along the specified angle from the origin given.


#### <a name="INTE4939"></a> Intersects([Ray](Heirloom.Math.Ray.md) r1, [Ray](Heirloom.Math.Ray.md) r2, out float t1, out float t2) : bool
<small>`Static`</small>

Computes the intersection of two rays.


