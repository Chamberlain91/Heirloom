# LineSegment

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a line segment defined by two [Vector][1] .

```cs
public struct LineSegment : IEquatable<LineSegment>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<LineSegment>

**Fields**: [A][2], [B][3]

**Methods**: [Set][4], [GetPoint][5], [Intersects][6], [GetClosestPoint][7]

**Static Fields**: [IntersectionTolerance][8]

**Static Methods**: [Intersects][6], [GetClosestPoint][7], [ClosestPoint][9]

--------------------------------------------------------------------------------

## Fields

| Name                       | Summary                                                                              |
|----------------------------|--------------------------------------------------------------------------------------|
| [A][2]                     | The first end-point.                                                                 |
| [B][3]                     | The second end-point.                                                                |
| [IntersectionTolerance][8] | A value to adjust the intersection tolerance to compensate for floating-point error. |

## Methods

| Name                 | Summary                                                            |
|----------------------|--------------------------------------------------------------------|
| [Set][4]             | Sets the components of this line segment.                          |
| [GetPoint][5]        | Gets a intermediate point along the line segment.                  |
| [Intersects][6]      | Checks if this line segment intersects another.                    |
| [Intersects][6]      | Checks if this line segment intersects another.                    |
| [GetClosestPoint][7] | Gets the closest point on the line segment to the specified point. |
| [Intersects][6]      | Checks if two line segments intersect.                             |
| [Intersects][6]      | Checks if two line segments intersect.                             |
| [Intersects][6]      | Checks if two line segments intersect.                             |
| [Intersects][6]      | Checks if two line segments intersect.                             |
| [GetClosestPoint][7] | Gets the closest point on a line segment to the specified point.   |
| [ClosestPoint][9]    | Gets the closest point on a line segment to the specified point.   |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Vector.md
[2]: Heirloom.LineSegment.A.md
[3]: Heirloom.LineSegment.B.md
[4]: Heirloom.LineSegment.Set.md
[5]: Heirloom.LineSegment.GetPoint.md
[6]: Heirloom.LineSegment.Intersects.md
[7]: Heirloom.LineSegment.GetClosestPoint.md
[8]: Heirloom.LineSegment.IntersectionTolerance.md
[9]: Heirloom.LineSegment.ClosestPoint.md
