# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Triangle.Overlaps (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Triangle][1]

### Overlaps(IShape)

Determines if this triangle overlaps another shape.

```cs
public bool Overlaps(IShape shape)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| shape | [IShape][2] |         |

> **Returns** - `bool`

### Overlaps(in Circle)

Determines if this triangle overlaps the specified circle.

```cs
public bool Overlaps(in Circle circle)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| circle | [Circle][3] |         |

> **Returns** - `bool`

### Overlaps(in Triangle)

Determines if this triangle overlaps another triangle.

```cs
public bool Overlaps(in Triangle triangle)
```

| Name     | Type          | Summary |
|----------|---------------|---------|
| triangle | [Triangle][1] |         |

> **Returns** - `bool`

### Overlaps(in Rectangle)

Determines if this triangle overlaps the specified rectangle.

```cs
public bool Overlaps(in Rectangle rectangle)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][4] |         |

> **Returns** - `bool`

### Overlaps(IReadOnlyList<Vector>)

Determines if this triangle overlaps the specified convex polygon.

```cs
public bool Overlaps(IReadOnlyList<Vector> polygon)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |

> **Returns** - `bool`

### Overlaps(Polygon)

Determines if this triangle overlaps the specified simple polygon.

```cs
public bool Overlaps(Polygon polygon)
```

| Name    | Type         | Summary |
|---------|--------------|---------|
| polygon | [Polygon][5] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Triangle.md
[2]: ../IShape.md
[3]: ../Circle.md
[4]: ../../Heirloom/Rectangle.md
[5]: ../Polygon.md
