# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Offset (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Offset(float, float)

Translates this rectangle.

```cs
public void Offset(float x, float y)
```

| Name | Type    | Summary |
|------|---------|---------|
| x    | `float` |         |
| y    | `float` |         |

> **Returns** - `void`

### Offset(Vector)

Translates this rectangle.

```cs
public void Offset(Vector offset)
```

| Name   | Type        | Summary |
|--------|-------------|---------|
| offset | [Vector][2] |         |

> **Returns** - `void`

### Offset(Rectangle, float, float)

Copies and translates the given rectangle.

```cs
public static Rectangle Offset(Rectangle rect, float x, float y)
```

| Name | Type           | Summary |
|------|----------------|---------|
| rect | [Rectangle][1] |         |
| x    | `float`        |         |
| y    | `float`        |         |

> **Returns** - [Rectangle][1]

### Offset(Rectangle, Vector)

Copies and translates the given rectangle.

```cs
public static Rectangle Offset(Rectangle rect, Vector offset)
```

| Name   | Type           | Summary |
|--------|----------------|---------|
| rect   | [Rectangle][1] |         |
| offset | [Vector][2]    |         |

> **Returns** - [Rectangle][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
[2]: ../Vector.md
