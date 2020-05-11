# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GeometryTools.GenerateRegularPolygon (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [GeometryTools][1]

### GenerateRegularPolygon(int, float)

Generates the points of a regular polygon.

```cs
public static IEnumerable<Vector> GenerateRegularPolygon(int segments, float radius)
```

| Name     | Type    | Summary |
|----------|---------|---------|
| segments | `int`   |         |
| radius   | `float` |         |

> **Returns** - `IEnumerable<Vector>`

### GenerateRegularPolygon(Vector, int, float)

Generates the points of a regular polygon.

```cs
public static IEnumerable<Vector> GenerateRegularPolygon(Vector center, int segments, float radius)
```

| Name     | Type        | Summary |
|----------|-------------|---------|
| center   | [Vector][2] |         |
| segments | `int`       |         |
| radius   | `float`     |         |

> **Returns** - `IEnumerable<Vector>`

[0]: ../../../Heirloom.Core.md
[1]: ../GeometryTools.md
[2]: ../../Heirloom/Vector.md
