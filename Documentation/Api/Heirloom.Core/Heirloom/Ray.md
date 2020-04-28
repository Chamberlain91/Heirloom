# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Ray

> **Namespace**: [Heirloom][0]  

Represents a ray by orgin and direction vectors.

```cs
public struct Ray : IEquatable<Ray>
```

### Inherits

IEquatable\<Ray>

#### Fields

[Origin][1], [Direction][2]

#### Methods

[Set][3], [GetPoint][4], [Deconstruct][5]

#### Static Methods

[FromLineSegment][6], [FromAngle][7], [Intersects][8]

## Fields

| Name           | Summary                   |
|----------------|---------------------------|
| [Origin][1]    | The origin of the ray.    |
| [Direction][2] | The direction of the ray. |

## Methods

| Name                 | Summary                                                                |
|----------------------|------------------------------------------------------------------------|
| [Set][3]             | Sets the components of this size.                                      |
| [GetPoint][4]        | Gets a point along the ray.                                            |
| [Deconstruct][5]     |                                                                        |
| [FromLineSegment][6] | Creates a ray from a line segment.                                     |
| [FromLineSegment][6] | Creates a ray from a line segment.                                     |
| [FromAngle][7]       | Creates a ray pointed along the specified angle from the origin given. |
| [Intersects][8]      | Computes the intersection of two rays.                                 |

[0]: ../../Heirloom.Core.md
[1]: Ray/Origin.md
[2]: Ray/Direction.md
[3]: Ray/Set.md
[4]: Ray/GetPoint.md
[5]: Ray/Deconstruct.md
[6]: Ray/FromLineSegment.md
[7]: Ray/FromAngle.md
[8]: Ray/Intersects.md
