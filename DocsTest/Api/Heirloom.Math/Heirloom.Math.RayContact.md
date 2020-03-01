# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## RayContact (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<RayContact></small>  

Represents the result of a ray-shape intersection.

| Fields                | Summary                                            |
|-----------------------|----------------------------------------------------|
| [Position](#POSIF46C) | The point of contact from a collision or ray.      |
| [Normal](#NORM3009)   | The normal direction of the contacted surface.     |
| [Distance](#DIST3A36) | The separating distance from the point of contact. |

### Fields

#### <a name="POSIF46C"></a> Position : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

The point of contact from a collision or ray.

#### <a name="NORM3009"></a> Normal : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

The normal direction of the contacted surface.

#### <a name="DIST3A36"></a> Distance : float
<small>`Read Only`</small>

The separating distance from the point of contact.

### Constructors

#### RayContact([Vector](Heirloom.Math.Vector.md) position, [Vector](Heirloom.Math.Vector.md) normal, float distance)

