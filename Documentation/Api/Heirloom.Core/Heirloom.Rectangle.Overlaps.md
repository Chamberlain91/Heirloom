# Rectangle.Overlaps

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Rectangle][1]

--------------------------------------------------------------------------------

### Overlaps(IShape)

Determines if this rectangle overlaps another shape.

```cs
public bool Overlaps(IShape shape)
```

### Overlaps(in Circle)

Determines if this rectangle overlaps the specified circle.

```cs
public bool Overlaps(in Circle circle)
```

### Overlaps(in Triangle)

Determines if this rectangle overlaps the specified triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

### Overlaps(in Rectangle)

Determines if this rectangle overlaps another rectangle.

```cs
public bool Overlaps(in Rectangle other)
```

### Overlaps(IReadOnlyList<Vector>)

Determines if this rectangle overlaps the specified convex polygon.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

### Overlaps(Polygon)

Determines if this rectangle overlaps the specified simple polygon.

```cs
public bool Overlaps(Polygon polygon)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Rectangle.md
