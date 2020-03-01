# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Ray (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Ray></small>  

Represents a ray by orgin and direction vectors.

| Fields                    | Summary                   |
|---------------------------|---------------------------|
| [Origin](#ORI85E4C2C0)    | The origin of the ray.    |
| [Direction](#DIR7D7E04D1) | The direction of the ray. |

| Methods                         | Summary                                                                |
|---------------------------------|------------------------------------------------------------------------|
| [GetPoint](#GET3C443BA5)        | Gets a point along the ray.                                            |
| [Deconstruct](#DEC1943369C)     |                                                                        |
| [FromLineSegment](#FRO3D5915DC) | Creates a ray from a line segment.                                     |
| [FromLineSegment](#FRO3497ACD6) | Creates a ray from a line segment.                                     |
| [FromAngle](#FRO9C3DECF5)       | Creates a ray pointed along the specified angle from the origin given. |
| [Intersects](#INT4939C76B)      | Computes the intersection of two rays.                                 |

### Fields

#### <a name="ORI85E4C2C0"></a>Origin : [Vector](Heirloom.Math.Vector.md)

The origin of the ray.

#### <a name="DIR7D7E04D1"></a>Direction : [Vector](Heirloom.Math.Vector.md)

The direction of the ray.

### Constructors

#### Ray([Vector](Heirloom.Math.Vector.md) origin, [Vector](Heirloom.Math.Vector.md) direction)

### Methods

#### <a name="GET3C443BA5"></a>GetPoint(float distance) : [Vector](Heirloom.Math.Vector.md)

Gets a point along the ray.


#### <a name="DEC1943369C"></a>Deconstruct(out [Vector](Heirloom.Math.Vector.md) origin, out [Vector](Heirloom.Math.Vector.md) direction) : void


#### <a name="FRO3D5915DC"></a>FromLineSegment(in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) target) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FRO3497ACD6"></a>FromLineSegment(in [LineSegment](Heirloom.Math.LineSegment.md) segment) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray from a line segment.


#### <a name="FRO9C3DECF5"></a>FromAngle([Vector](Heirloom.Math.Vector.md) origin, float angle) : [Ray](Heirloom.Math.Ray.md)
<small>`Static`</small>

Creates a ray pointed along the specified angle from the origin given.


#### <a name="INT4939C76B"></a>Intersects([Ray](Heirloom.Math.Ray.md) r1, [Ray](Heirloom.Math.Ray.md) r2, out float t1, out float t2) : bool
<small>`Static`</small>

Computes the intersection of two rays.


