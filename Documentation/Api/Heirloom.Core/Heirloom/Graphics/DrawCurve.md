# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawCurve (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawCurve(in Vector, in Vector, in Vector, float)

Draws a quadratic curve using three control points to the current surface.

```cs
public void DrawCurve(in Vector p0, in Vector p1, in Vector p2, float width = 1)
```

| Name  | Type        | Summary                              |
|-------|-------------|--------------------------------------|
| p0    | [Vector][2] | The first control point.             |
| p1    | [Vector][2] | The second control point.            |
| p2    | [Vector][2] | The third control point.             |
| width | `float`     | The thickness of the line in pixels. |

> **Returns** - `void`

### DrawCurve(in Vector, in Vector, in Vector, in Vector, float)

Draws a cubic curve using four control points to the current surface.

```cs
public void DrawCurve(in Vector p0, in Vector p1, in Vector p2, in Vector p3, float width = 1)
```

| Name  | Type        | Summary                              |
|-------|-------------|--------------------------------------|
| p0    | [Vector][2] | The first control point.             |
| p1    | [Vector][2] | The second control point.            |
| p2    | [Vector][2] | The third control point.             |
| p3    | [Vector][2] | The fourth control point.            |
| width | `float`     | The thickness of the line in pixels. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../Vector.md
