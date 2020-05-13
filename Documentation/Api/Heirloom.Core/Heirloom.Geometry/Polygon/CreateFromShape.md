# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.CreateFromShape (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Polygon][1]

### CreateFromShape(IShape)

Constructs a polygon representation of the specified shape.

```cs
public static Polygon CreateFromShape(IShape shape)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| shape | [IShape][2] |         |

> **Returns** - [Polygon][1]

### CreateFromShape(in Triangle)

Constructs a polygon representation of the specified triangle.

```cs
public static Polygon CreateFromShape(in Triangle triangle)
```

| Name     | Type          | Summary |
|----------|---------------|---------|
| triangle | [Triangle][3] |         |

> **Returns** - [Polygon][1]

### CreateFromShape(in Rectangle)

Constructs a polygon representation of the specified rectangle.

```cs
public static Polygon CreateFromShape(in Rectangle rectangle)
```

| Name      | Type           | Summary |
|-----------|----------------|---------|
| rectangle | [Rectangle][4] |         |

> **Returns** - [Polygon][1]

### CreateFromShape(in Circle)

Constructs a polygon representation of the specified circle.

```cs
public static Polygon CreateFromShape(in Circle circle)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| circle | [Circle][5] |         |

> **Returns** - [Polygon][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
[2]: ../IShape.md
[3]: ../Triangle.md
[4]: ../../Heirloom/Rectangle.md
[5]: ../Circle.md
