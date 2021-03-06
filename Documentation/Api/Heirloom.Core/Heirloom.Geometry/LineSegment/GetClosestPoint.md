# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LineSegment.GetClosestPoint (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [LineSegment][1]

### GetClosestPoint(Vector)

Gets the closest point on the line segment to the specified point.

```cs
public Vector GetClosestPoint(Vector p)
```

| Name | Type        | Summary |
|------|-------------|---------|
| p    | [Vector][2] |         |

> **Returns** - [Vector][2]

### GetClosestPoint(Vector, Vector, Vector)

Gets the closest point on a line segment to the specified point.

```cs
public static Vector GetClosestPoint(Vector a, Vector b, Vector p)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][2] |         |
| b    | [Vector][2] |         |
| p    | [Vector][2] |         |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../LineSegment.md
[2]: ../../Heirloom/Vector.md
