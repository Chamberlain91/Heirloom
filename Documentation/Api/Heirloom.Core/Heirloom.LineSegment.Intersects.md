# LineSegment.Intersects

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [LineSegment][1]  

--------------------------------------------------------------------------------

### Intersects(LineSegment)

Checks if this line segment intersects another.

```cs
public bool Intersects(LineSegment other)
```

### Intersects(LineSegment, out Vector)

Checks if this line segment intersects another.

```cs
public bool Intersects(LineSegment other, out Vector point)
```

### Intersects(LineSegment, LineSegment)

Checks if two line segments intersect.

```cs
public bool Intersects(LineSegment s1, LineSegment s2)
```

### Intersects(LineSegment, LineSegment, out Vector)

Checks if two line segments intersect.

```cs
public bool Intersects(LineSegment s1, LineSegment s2, out Vector point)
```

### Intersects(Vector, Vector, Vector, Vector)

Checks if two line segments intersect.

```cs
public bool Intersects(Vector p1, Vector p2, Vector q1, Vector q2)
```

### Intersects(Vector, Vector, Vector, Vector, out Vector)

Checks if two line segments intersect.

```cs
public bool Intersects(Vector p1, Vector p2, Vector p3, Vector p4, out Vector point)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.LineSegment.md
