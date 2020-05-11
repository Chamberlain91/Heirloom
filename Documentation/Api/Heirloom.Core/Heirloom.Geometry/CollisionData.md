# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## CollisionData (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Contains the results of a collision function in [Collision][1] .

```cs
public struct CollisionData
```

### Properties

[Count][2], [Normal][3], [Penetration][4]

### Methods

[GetPoint][5]

## Properties

#### Instance

| Name             | Type        | Summary                                              |
|------------------|-------------|------------------------------------------------------|
| [Count][2]       | `int`       | Gets the number of contact points.                   |
| [Normal][3]      | [Vector][6] | Gets the normal vector of the collision.             |
| [Penetration][4] | `float`     | Gets the measure of how much overlap the shapes had. |

## Methods

#### Instance

| Name               | Return Type | Summary               |
|--------------------|-------------|-----------------------|
| [GetPoint(int)][5] | [Vector][6] | Gets a contact point. |

[0]: ../../Heirloom.Core.md
[1]: Collision.md
[2]: CollisionData/Count.md
[3]: CollisionData/Normal.md
[4]: CollisionData/Penetration.md
[5]: CollisionData/GetPoint.md
[6]: ../Heirloom/Vector.md
