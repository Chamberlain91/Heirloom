# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Circle.Overlaps (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Circle][1]

### Overlaps(IShape)

Determines if this circle overlaps another shape.

```cs
public bool Overlaps(IShape shape)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| shape | [IShape][2] |         |

> **Returns** - `bool`

### Overlaps(in Circle)

Determines if this circle overlaps another circle.

```cs
public bool Overlaps(in Circle b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| b    | [Circle][1] |         |

> **Returns** - `bool`

### Overlaps(in Rectangle)

Determines if this circle overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][3] |         |

> **Returns** - `bool`

### Overlaps(in Triangle)

Determines if this circle overlaps the specified triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

| Name     | Type          | Summary |
|----------|---------------|---------|
| triangle | [Triangle][4] |         |

> **Returns** - `bool`

### Overlaps(Polygon)

Determines if this circle overlaps the specified simple polygon.

```cs
public bool Overlaps(Polygon polygon)
```

| Name    | Type         | Summary |
|---------|--------------|---------|
| polygon | [Polygon][5] |         |

> **Returns** - `bool`

### Overlaps(IReadOnlyList<Vector>)

Determines if this circle overlaps the specified convex polygon.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

| Name    | Type                    | Summary |
|---------|-------------------------|---------|
| polygon | `IReadOnlyList<Vector>` |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Circle.md
[2]: ../IShape.md
[3]: ../../Heirloom/Rectangle.md
[4]: ../Triangle.md
[5]: ../Polygon.md
