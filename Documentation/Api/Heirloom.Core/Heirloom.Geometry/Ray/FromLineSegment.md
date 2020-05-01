# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Ray.FromLineSegment (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Ray][1]

### FromLineSegment(in Vector, in Vector)

Creates a ray from a line segment.

```cs
public static Ray FromLineSegment(in Vector origin, in Vector target)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| origin | [Vector][2] |         |
| target | [Vector][2] |         |

> **Returns** - [Ray][1]

### FromLineSegment(in LineSegment)

Creates a ray from a line segment.

```cs
public static Ray FromLineSegment(in LineSegment segment)
```

| Name    | Type             | Summary |
|---------|------------------|---------|
| segment | [LineSegment][3] |         |

> **Returns** - [Ray][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Ray.md
[2]: ../../Heirloom/Vector.md
[3]: ../LineSegment.md
