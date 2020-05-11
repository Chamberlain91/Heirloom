# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.DrawCurve (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### DrawCurve(Vector, Vector, Vector, float)

Draws a quadratic curve using three control points to the current surface.

```cs
public void DrawCurve(Vector p0, Vector p1, Vector p2, float width = 1)
```

| Name  | Type        | Summary                              |
|-------|-------------|--------------------------------------|
| p0    | [Vector][2] | The first control point.             |
| p1    | [Vector][2] | The second control point.            |
| p2    | [Vector][2] | The third control point.             |
| width | `float`     | The thickness of the line in pixels. |

> **Returns** - `void`

### DrawCurve(Vector, Vector, Vector, Vector, float)

Draws a cubic curve using four control points to the current surface.

```cs
public void DrawCurve(Vector p0, Vector p1, Vector p2, Vector p3, float width = 1)
```

| Name  | Type        | Summary                              |
|-------|-------------|--------------------------------------|
| p0    | [Vector][2] | The first control point.             |
| p1    | [Vector][2] | The second control point.            |
| p2    | [Vector][2] | The third control point.             |
| p3    | [Vector][2] | The fourth control point.            |
| width | `float`     | The thickness of the line in pixels. |

> **Returns** - `void`

### DrawCurve(Curve, float)

```cs
public void DrawCurve(Curve curve, float width = 1)
```

| Name  | Type       | Summary |
|-------|------------|---------|
| curve | [Curve][3] |         |
| width | `float`    |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../Vector.md
[3]: ../../Heirloom.Geometry/Curve.md
