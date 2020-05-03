# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Ray (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Represents a ray by orgin point and directional vector.

```cs
public struct Ray : IEquatable<Ray>
```

### Inherits

IEquatable\<Ray>

### Fields

[Direction][1], [Origin][2]

### Methods

[Deconstruct][3], [GetPoint][4], [Set][5]

### Static Methods

[FromAngle][6], [FromLineSegment][7], [Intersects][8]

## Fields

#### Instance

| Name           | Type        | Summary                   |
|----------------|-------------|---------------------------|
| [Direction][1] | [Vector][9] | The direction of the ray. |
| [Origin][2]    | [Vector][9] | The origin of the ray.    |

## Methods

#### Instance

| Name                           | Return Type | Summary                           |
|--------------------------------|-------------|-----------------------------------|
| [Deconstruct(out Vector...][3] | `void`      |                                   |
| [GetPoint(float)][4]           | [Vector][9] | Gets a point along the ray.       |
| [Set(in Vector, in Vector)][5] | `void`      | Sets the components of this size. |

#### Static

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [FromAngle(Vector, float)][6]  | [Ray][10]   | Creates a ray pointed along the specified angle from the origin given. |
| [FromLineSegment(in Vec...][7] | [Ray][10]   | Creates a ray from a line segment.                                     |
| [FromLineSegment(in Lin...][7] | [Ray][10]   | Creates a ray from a line segment.                                     |
| [Intersects(Ray, Ray, o...][8] | `bool`      | Computes the intersection of two rays.                                 |

[0]: ../../Heirloom.Core.md
[1]: Ray/Direction.md
[2]: Ray/Origin.md
[3]: Ray/Deconstruct.md
[4]: Ray/GetPoint.md
[5]: Ray/Set.md
[6]: Ray/FromAngle.md
[7]: Ray/FromLineSegment.md
[8]: Ray/Intersects.md
[9]: ../Heirloom/Vector.md
[10]: Ray.md
