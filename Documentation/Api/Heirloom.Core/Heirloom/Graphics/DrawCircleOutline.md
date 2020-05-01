# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawCircleOutline (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawCircleOutline(in Circle, float)

Draws the outline of a circle to the current surface.

```cs
public void DrawCircleOutline(in Circle circle, float width = 1)
```

| Name   | Type        | Summary                         |
|--------|-------------|---------------------------------|
| circle | [Circle][2] | The circle to draw.             |
| width  | `float`     | Width of the outline in pixels. |

> **Returns** - `void`

### DrawCircleOutline(in Vector, float, float)

Draws the outline of a circle to the current surface.

```cs
public void DrawCircleOutline(in Vector position, float radius, float width = 1)
```

| Name     | Type        | Summary                         |
|----------|-------------|---------------------------------|
| position | [Vector][3] | The centr of the circle.        |
| radius   | `float`     | The radius of the circle.       |
| width    | `float`     | Width of the outline in pixels. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../../Heirloom.Geometry/Circle.md
[3]: ../Vector.md
