# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Circle.Overlaps

> **Namespace**: [Heirloom][0]  
> **Type**: [Circle][1]  

### Overlaps(IShape)

Determines if this circle overlaps another shape.

```cs
public bool Overlaps(IShape shape)
```

### Overlaps(in Circle)

Determines if this circle overlaps another circle.

```cs
public bool Overlaps(in Circle b)
```

### Overlaps(in Rectangle)

Determines if this circle overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

### Overlaps(in Triangle)

Determines if this circle overlaps the specified triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

### Overlaps(Polygon)

Determines if this circle overlaps the specified simple polygon.

```cs
public bool Overlaps(Polygon polygon)
```

### Overlaps(IReadOnlyList<Vector>)

Determines if this circle overlaps the specified convex polygon.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Circle.md
