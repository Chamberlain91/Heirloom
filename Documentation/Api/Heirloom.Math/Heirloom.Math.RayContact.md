# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## RayContact (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<RayContact></small>  
<small>`IsReadOnlyAttribute`</small>

Represents the result of a ray-shape intersection.

| Fields                   | Summary                                            |
|--------------------------|----------------------------------------------------|
| [Position](#POSF46C3C91) | The point of contact from a collision or ray.      |
| [Normal](#NOR300902F)    | The normal direction of the contacted surface.     |
| [Distance](#DIS3A367EAF) | The separating distance from the point of contact. |

### Fields

#### <a name="POSF46C3C91"></a>Position : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

The point of contact from a collision or ray.

#### <a name="NOR300902F"></a>Normal : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

The normal direction of the contacted surface.

#### <a name="DIS3A367EAF"></a>Distance : float
<small>`Read Only`</small>

The separating distance from the point of contact.

### Constructors

#### RayContact([Vector](Heirloom.Math.Vector.md) position, [Vector](Heirloom.Math.Vector.md) normal, float distance)

