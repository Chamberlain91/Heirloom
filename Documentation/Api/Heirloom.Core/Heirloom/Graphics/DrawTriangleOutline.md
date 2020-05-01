# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawTriangleOutline (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawTriangleOutline(in Triangle, float)

Draw a triangle outline to the current surface.

```cs
public void DrawTriangleOutline(in Triangle triangle, float width = 1)
```

| Name     | Type          | Summary                              |
|----------|---------------|--------------------------------------|
| triangle | [Triangle][2] | The triangle to draw.                |
| width    | `float`       | The thickness of the line in pixels. |

> **Returns** - `void`

### DrawTriangleOutline(in Vector, in Vector, in Vector, float)

Draw a triangle outline to the current surface.

```cs
public void DrawTriangleOutline(in Vector a, in Vector b, in Vector c, float width = 1)
```

| Name  | Type        | Summary                              |
|-------|-------------|--------------------------------------|
| a     | [Vector][3] | The first point.                     |
| b     | [Vector][3] | The second point.                    |
| c     | [Vector][3] | The third point.                     |
| width | `float`     | The thickness of the line in pixels. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../../Heirloom.Geometry/Triangle.md
[3]: ../Vector.md
