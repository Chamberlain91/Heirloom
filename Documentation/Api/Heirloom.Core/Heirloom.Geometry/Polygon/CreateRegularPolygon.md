# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.CreateRegularPolygon (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Polygon][1]

### CreateRegularPolygon(Vector, int, float)

Construct a regular polygon.

```cs
public static Polygon CreateRegularPolygon(Vector center, int segments, float radius)
```

| Name     | Type        | Summary |
|----------|-------------|---------|
| center   | [Vector][2] |         |
| segments | `int`       |         |
| radius   | `float`     |         |

> **Returns** - [Polygon][1]

### CreateRegularPolygon(int, float)

Construct a regular polygon.

```cs
public static Polygon CreateRegularPolygon(int segments, float radius)
```

| Name     | Type    | Summary |
|----------|---------|---------|
| segments | `int`   |         |
| radius   | `float` |         |

> **Returns** - [Polygon][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
[2]: ../../Heirloom/Vector.md
