# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Polygon.Overlaps

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Polygon][1]  

### Overlaps(IShape)

Checks for an overlap between this polygon and another shape.

```cs
public bool Overlaps(IShape shape)
```

### Overlaps(in Rectangle)

Determines if this polygon overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

### Overlaps(in Circle)

Determines if this polygon overlaps the specified circle.

```cs
public bool Overlaps(in Circle circle)
```

### Overlaps(in Triangle)

Determines if this polygon overlaps the specified triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

### Overlaps(IReadOnlyList<Vector>)

Determines if this polygon overlaps the specified triangle.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
