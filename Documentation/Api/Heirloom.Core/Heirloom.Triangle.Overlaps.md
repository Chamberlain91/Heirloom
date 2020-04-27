# Triangle.Overlaps

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Triangle][1]  

--------------------------------------------------------------------------------

### Overlaps(IShape)

Determines if this triangle overlaps another shape.

```cs
public bool Overlaps(IShape shape)
```

### Overlaps(in Circle)

Determines if this triangle overlaps the specified circle.

```cs
public bool Overlaps(in Circle circle)
```

### Overlaps(in Triangle)

Determines if this triangle overlaps another triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

### Overlaps(in Rectangle)

Determines if this triangle overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

### Overlaps(IReadOnlyList<Vector>)

Determines if this triangle overlaps the specified convex polygon.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

### Overlaps(Polygon)

Determines if this triangle overlaps the specified simple polygon.

```cs
public bool Overlaps(Polygon polygon)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Triangle.md
