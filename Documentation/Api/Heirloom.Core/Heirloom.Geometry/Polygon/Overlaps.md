# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.Overlaps (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Polygon][1]

### Overlaps(IShape)

Checks for an overlap between this polygon and another shape.

```cs
public bool Overlaps(IShape shape)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| shape | [IShape][2] |         |

> **Returns** - `bool`

### Overlaps(in Rectangle)

Determines if this polygon overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][3] |         |

> **Returns** - `bool`

### Overlaps(in Circle)

Determines if this polygon overlaps the specified circle.

```cs
public bool Overlaps(in Circle circle)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| circle | [Circle][4] |         |

> **Returns** - `bool`

### Overlaps(in Triangle)

Determines if this polygon overlaps the specified triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

| Name     | Type          | Summary |
|----------|---------------|---------|
| triangle | [Triangle][5] |         |

> **Returns** - `bool`

### Overlaps(IReadOnlyList<Vector>)

Determines if this polygon overlaps the specified triangle.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

| Name    | Type                    | Summary |
|---------|-------------------------|---------|
| polygon | `IReadOnlyList<Vector>` |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
[2]: ../IShape.md
[3]: ../../Heirloom/Rectangle.md
[4]: ../Circle.md
[5]: ../Triangle.md
