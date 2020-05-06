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

[Deconstruct][3], [Equals][4], [GetHashCode][5], [GetPoint][6], [Set][7], [ToString][8]

### Static Methods

[FromAngle][9], [FromLineSegment][10], [Intersects][11]

## Fields

#### Instance

| Name           | Type         | Summary                   |
|----------------|--------------|---------------------------|
| [Direction][1] | [Vector][12] | The direction of the ray. |
| [Origin][2]    | [Vector][12] | The origin of the ray.    |

## Methods

#### Instance

| Name                           | Return Type  | Summary                           |
|--------------------------------|--------------|-----------------------------------|
| [Deconstruct(out Vector...][3] | `void`       |                                   |
| [Equals(object)][4]            | `bool`       |                                   |
| [Equals(Ray)][4]               | `bool`       |                                   |
| [GetHashCode()][5]             | `int`        |                                   |
| [GetPoint(float)][6]           | [Vector][12] | Gets a point along the ray.       |
| [Set(in Vector, in Vector)][7] | `void`       | Sets the components of this size. |
| [ToString()][8]                | `string`     |                                   |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromAngle(Vector, float)][9]   | [Ray][13]   | Creates a ray pointed along the specified angle from the origin given. |
| [FromLineSegment(in Vec...][10] | [Ray][13]   | Creates a ray from a line segment.                                     |
| [FromLineSegment(in Lin...][10] | [Ray][13]   | Creates a ray from a line segment.                                     |
| [Intersects(Ray, Ray, o...][11] | `bool`      | Computes the intersection of two rays.                                 |

[0]: ../../Heirloom.Core.md
[1]: Ray/Direction.md
[2]: Ray/Origin.md
[3]: Ray/Deconstruct.md
[4]: Ray/Equals.md
[5]: Ray/GetHashCode.md
[6]: Ray/GetPoint.md
[7]: Ray/Set.md
[8]: Ray/ToString.md
[9]: Ray/FromAngle.md
[10]: Ray/FromLineSegment.md
[11]: Ray/Intersects.md
[12]: ../Heirloom/Vector.md
[13]: Ray.md
