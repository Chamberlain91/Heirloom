# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LineSegment (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Represents a line segment defined by two end points.

```cs
public struct LineSegment : IEquatable<LineSegment>
```

### Inherits

IEquatable\<LineSegment>

### Fields

[A][1], [B][2]

### Methods

[GetClosestPoint][3], [GetPoint][4], [Intersects][5], [Set][6]

### Static Fields

[IntersectionTolerance][7]

### Static Methods

[ClosestPoint][8], [GetClosestPoint][3], [Intersects][5]

## Fields

#### Instance

| Name   | Type        | Summary               |
|--------|-------------|-----------------------|
| [A][1] | [Vector][9] | The first end-point.  |
| [B][2] | [Vector][9] | The second end-point. |

#### Static

| Name                       | Type    | Summary                                                                |
|----------------------------|---------|------------------------------------------------------------------------|
| [IntersectionTolerance][7] | `float` | A value to adjust the intersection tolerance to compensate for floa... |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                            |
|--------------------------------|-------------|--------------------------------------------------------------------|
| [GetClosestPoint(Vector)][3]   | [Vector][9] | Gets the closest point on the line segment to the specified point. |
| [GetPoint(float)][4]           | [Vector][9] | Gets a intermediate point along the line segment.                  |
| [Intersects(LineSegment)][5]   | `bool`      | Checks if this line segment intersects another.                    |
| [Intersects(LineSegment...][5] | `bool`      | Checks if this line segment intersects another.                    |
| [Set(in Vector, in Vector)][6] | `void`      | Sets the components of this line segment.                          |

#### Static

| Name                           | Return Type | Summary                                                          |
|--------------------------------|-------------|------------------------------------------------------------------|
| [ClosestPoint(Vector, V...][8] | [Vector][9] | Gets the closest point on a line segment to the specified point. |
| [GetClosestPoint(Vector...][3] | [Vector][9] | Gets the closest point on a line segment to the specified point. |
| [Intersects(LineSegment...][5] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(LineSegment...][5] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][5] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][5] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(LineSegment...][5] | `bool`      | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][5] | `bool`      | Checks if two line segments intersect.                           |

[0]: ../../Heirloom.Core.md
[1]: LineSegment/A.md
[2]: LineSegment/B.md
[3]: LineSegment/GetClosestPoint.md
[4]: LineSegment/GetPoint.md
[5]: LineSegment/Intersects.md
[6]: LineSegment/Set.md
[7]: LineSegment/IntersectionTolerance.md
[8]: LineSegment/ClosestPoint.md
[9]: ../Heirloom/Vector.md
