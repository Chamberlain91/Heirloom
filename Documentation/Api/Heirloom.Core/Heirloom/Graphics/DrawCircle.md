# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawCircle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawCircle(in Circle)

Draws a circle to the current surface.

```cs
public void DrawCircle(in Circle circle)
```

| Name   | Type        | Summary             |
|--------|-------------|---------------------|
| circle | [Circle][2] | The circle to draw. |

> **Returns** - `void`

### DrawCircle(in Vector, float)

Draws a circle to the current surface.

```cs
public void DrawCircle(in Vector position, float radius)
```

| Name     | Type        | Summary                   |
|----------|-------------|---------------------------|
| position | [Vector][3] | The center of the circle. |
| radius   | `float`     | The radius of the circle. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../../Heirloom.Geometry/Circle.md
[3]: ../Vector.md
