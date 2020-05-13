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

[Equals][3], [GetClosestPoint][4], [GetHashCode][5], [GetPoint][6], [Intersects][7], [Set][8]

### Static Fields

[IntersectionTolerance][9]

### Static Methods

[ClosestPoint][10], [GetClosestPoint][4], [Intersects][7]

## Fields

#### Instance

| Name   | Type         | Summary               |
|--------|--------------|-----------------------|
| [A][1] | [Vector][11] | The first end-point.  |
| [B][2] | [Vector][11] | The second end-point. |

#### Static

| Name                       | Type    | Summary                                                                |
|----------------------------|---------|------------------------------------------------------------------------|
| [IntersectionTolerance][9] | `float` | A value to adjust the intersection tolerance to compensate for floa... |

## Methods

#### Instance

| Name                           | Return Type  | Summary                                                            |
|--------------------------------|--------------|--------------------------------------------------------------------|
| [Equals(object)][3]            | `bool`       | Compares this LineSegment for equality with another object.        |
| [Equals(LineSegment)][3]       | `bool`       | Compares this LineSegment for equality with another LineSegment .  |
| [GetClosestPoint(Vector)][4]   | [Vector][11] | Gets the closest point on the line segment to the specified point. |
| [GetHashCode()][5]             | `int`        | Returns the hash code for this LineSegment .                       |
| [GetPoint(float)][6]           | [Vector][11] | Gets a intermediate point along the line segment.                  |
| [Intersects(LineSegment)][7]   | `bool`       | Checks if this line segment intersects another.                    |
| [Intersects(LineSegment...][7] | `bool`       | Checks if this line segment intersects another.                    |
| [Set(in Vector, in Vector)][8] | `void`       | Sets the components of this line segment.                          |

#### Static

| Name                            | Return Type  | Summary                                                          |
|---------------------------------|--------------|------------------------------------------------------------------|
| [ClosestPoint(Vector, V...][10] | [Vector][11] | Gets the closest point on a line segment to the specified point. |
| [GetClosestPoint(Vector...][4]  | [Vector][11] | Gets the closest point on a line segment to the specified point. |
| [Intersects(LineSegment...][7]  | `bool`       | Checks if two line segments intersect.                           |
| [Intersects(LineSegment...][7]  | `bool`       | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][7]  | `bool`       | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][7]  | `bool`       | Checks if two line segments intersect.                           |
| [Intersects(LineSegment...][7]  | `bool`       | Checks if two line segments intersect.                           |
| [Intersects(Vector, Vec...][7]  | `bool`       | Checks if two line segments intersect.                           |

## Operators

| Name                            | Return Type | Summary                                               |
|---------------------------------|-------------|-------------------------------------------------------|
| [Equality(LineSegment, ...][12] | `bool`      | Compares two instances of LineSegment for equality.   |
| [Inequality(LineSegment...][13] | `bool`      | Compares two instances of LineSegment for inequality. |

[0]: ../../Heirloom.Core.md
[1]: LineSegment/A.md
[2]: LineSegment/B.md
[3]: LineSegment/Equals.md
[4]: LineSegment/GetClosestPoint.md
[5]: LineSegment/GetHashCode.md
[6]: LineSegment/GetPoint.md
[7]: LineSegment/Intersects.md
[8]: LineSegment/Set.md
[9]: LineSegment/IntersectionTolerance.md
[10]: LineSegment/ClosestPoint.md
[11]: ../Heirloom/Vector.md
[12]: LineSegment/op_Equality.md
[13]: LineSegment/op_Inequality.md
