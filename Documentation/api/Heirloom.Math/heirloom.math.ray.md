# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Ray (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Ray></small>  

Represents a ray by orgin and direction vectors.

| Fields | Summary |
|-------|---------|
| [Origin](#ORI85E4C2C0) | The origin of the ray. |
| [Direction](#DIR7D7E04D1) | The direction of the ray. |

| Methods | Summary |
|---------|---------|
| [GetPoint](#GET8130B845) | Gets a point along the ray. |
| [Deconstruct](#DEC9C34B59C) |  |
| [FromLineSegment](#FRO4F40ECBC) | Creates a ray from a line segment. |
| [FromLineSegment](#FRO25D225B6) | Creates a ray from a line segment. |
| [FromAngle](#FROEF5B6935) | Creates a ray pointed along the specified angle from the origin given. |
| [Intersects](#INT3C431F6B) | Computes the intersection of two rays. |

### Fields

#### <a name="ORI85E4C2C0"></a>Origin : [Vector](heirloom.math.vector.md)

The origin of the ray.

#### <a name="DIR7D7E04D1"></a>Direction : [Vector](heirloom.math.vector.md)

The direction of the ray.

### Constructors

#### Ray([Vector](heirloom.math.vector.md) origin, [Vector](heirloom.math.vector.md) direction)

### Methods

#### <a name="GET8130B845"></a>GetPoint(float distance) : [Vector](heirloom.math.vector.md)


Gets a point along the ray.


#### <a name="DEC9C34B59C"></a>Deconstruct(out [Vector](heirloom.math.vector.md) origin, out [Vector](heirloom.math.vector.md) direction) : void



#### <a name="FRO4F40ECBC"></a>FromLineSegment(in [Vector](heirloom.math.vector.md) origin, in [Vector](heirloom.math.vector.md) target) : [Ray](heirloom.math.ray.md)

<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FRO25D225B6"></a>FromLineSegment(in [LineSegment](heirloom.math.linesegment.md) segment) : [Ray](heirloom.math.ray.md)

<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FROEF5B6935"></a>FromAngle([Vector](heirloom.math.vector.md) origin, float angle) : [Ray](heirloom.math.ray.md)

<small>`Static`</small>

Creates a ray pointed along the specified angle from the origin given.


#### <a name="INT3C431F6B"></a>Intersects([Ray](heirloom.math.ray.md) r1, [Ray](heirloom.math.ray.md) r2, out float t1, out float t2) : bool

<small>`Static`</small>

Computes the intersection of two rays.


