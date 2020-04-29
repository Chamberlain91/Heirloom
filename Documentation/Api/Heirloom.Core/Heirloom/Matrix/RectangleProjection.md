# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.RectangleProjection (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### RectangleProjection(Rectangle)

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.

```cs
public static Matrix RectangleProjection(Rectangle rectangle)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][2] |         |

> **Returns** - [Matrix][1]

### RectangleProjection(float, float, float, float)

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.

```cs
public static Matrix RectangleProjection(float left, float top, float right, float bottom)
```

| Name   | Type    | Summary |
|--------|---------|---------|
| left   | `float` |         |
| top    | `float` |         |
| right  | `float` |         |
| bottom | `float` |         |

> **Returns** - [Matrix][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Rectangle.md
