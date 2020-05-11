# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LineSegment.Intersects (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [LineSegment][1]

### Intersects(LineSegment)

Checks if this line segment intersects another.

```cs
public bool Intersects(LineSegment other)
```

| Name  | Type             | Summary |
|-------|------------------|---------|
| other | [LineSegment][1] |         |

> **Returns** - `bool`

### Intersects(LineSegment, out Vector)

Checks if this line segment intersects another.

```cs
public bool Intersects(LineSegment other, out Vector point)
```

| Name  | Type             | Summary |
|-------|------------------|---------|
| other | [LineSegment][1] |         |
| point | [Vector][2]      |         |

> **Returns** - `bool`

### Intersects(LineSegment, LineSegment)

Checks if two line segments intersect.

```cs
public static bool Intersects(LineSegment s1, LineSegment s2)
```

| Name | Type             | Summary |
|------|------------------|---------|
| s1   | [LineSegment][1] |         |
| s2   | [LineSegment][1] |         |

> **Returns** - `bool`

### Intersects(LineSegment, LineSegment, out Vector)

Checks if two line segments intersect.

```cs
public static bool Intersects(LineSegment s1, LineSegment s2, out Vector point)
```

| Name  | Type             | Summary |
|-------|------------------|---------|
| s1    | [LineSegment][1] |         |
| s2    | [LineSegment][1] |         |
| point | [Vector][2]      |         |

> **Returns** - `bool`

### Intersects(Vector, Vector, Vector, Vector)

Checks if two line segments intersect.

```cs
public static bool Intersects(Vector p1, Vector p2, Vector q1, Vector q2)
```

| Name | Type        | Summary |
|------|-------------|---------|
| p1   | [Vector][2] |         |
| p2   | [Vector][2] |         |
| q1   | [Vector][2] |         |
| q2   | [Vector][2] |         |

> **Returns** - `bool`

### Intersects(Vector, Vector, Vector, Vector, out Vector)

Checks if two line segments intersect.

```cs
public static bool Intersects(Vector p1, Vector p2, Vector p3, Vector p4, out Vector point)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| p1    | [Vector][2] |         |
| p2    | [Vector][2] |         |
| p3    | [Vector][2] |         |
| p4    | [Vector][2] |         |
| point | [Vector][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../LineSegment.md
[2]: ../../Heirloom/Vector.md
