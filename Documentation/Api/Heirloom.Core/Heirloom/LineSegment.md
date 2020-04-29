# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LineSegment (Struct)

> **Namespace**: [Heirloom][0]

Represents a line segment defined by two [Vector][1] .

```cs
public struct LineSegment : IEquatable<LineSegment>
```

### Inherits

IEquatable\<LineSegment>

### Fields

[A][2], [B][3]

### Methods

[GetClosestPoint][4], [GetPoint][5], [Intersects][6], [Set][7]

### Static Fields

[IntersectionTolerance][8]

### Static Methods

[ClosestPoint][9], [GetClosestPoint][4], [Intersects][6]

## Fields

#### Instance

| Name   | Type        | Summary               |
|--------|-------------|-----------------------|
| [A][2] | [Vector][1] | The first end-point.  |
| [B][3] | [Vector][1] | The second end-point. |

#### Static

| Name                       | Type    | Summary                                                                |
|----------------------------|---------|------------------------------------------------------------------------|
| [IntersectionTolerance][8] | `float` | A value to adjust the intersection tolerance to compensate for floa... |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                            |
|--------------------------------|-------------|--------------------------------------------------------------------|
| [GetClosestPoint(Vector)][4]   | [Vector][1] | Gets the closest point on the line segment to the specified point. |
| [GetPoint(float)][5]           | [Vector][1] | Gets a intermediate point along the line segment.                  |
| [Intersects(LineSegment)][6]   | `bool`      | Checks if this line segment intersects another.                    |
| [Intersects(LineSegment...][6] | `bool`      | Checks if this line segment intersects another.                    |
| [Set(in Vector, in Vector)][7] | `void`      | Sets the components of this line segment.                          |

#### Static

| Name                           | Return Type | Summary                                                          |
|--------------------------------|-------------|------------------------------------------------------------------|
| [ClosestPoint(Vector, V...][9] | [Vector][1] | Gets the closest point on a line segment to the specified point. |
| [GetClosestPoint(Vector...][4] | [Vector][1] | Gets the closest point on a line segment to the specified point. |
| [Intersects(LineSegment...][6] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(LineSegment...][6] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][6] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][6] | `bool`      | Checks if two line segments intersect.                           |

[0]: ../../Heirloom.Core.md
[1]: Vector.md
[2]: LineSegment/A.md
[3]: LineSegment/B.md
[4]: LineSegment/GetClosestPoint.md
[5]: LineSegment/GetPoint.md
[6]: LineSegment/Intersects.md
[7]: LineSegment/Set.md
[8]: LineSegment/IntersectionTolerance.md
[9]: LineSegment/ClosestPoint.md
