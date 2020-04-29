# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawTriangle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawTriangle(in Triangle)

Draw a triangle to the current surface.

```cs
public void DrawTriangle(in Triangle triangle)
```

| Name     | Type          | Summary               |
|----------|---------------|-----------------------|
| triangle | [Triangle][2] | The triangle to draw. |

> **Returns** - `void`

### DrawTriangle(in Vector, in Vector, in Vector)

Draw a triangle outline to the current surface.

```cs
public void DrawTriangle(in Vector a, in Vector b, in Vector c)
```

| Name | Type        | Summary           |
|------|-------------|-------------------|
| a    | [Vector][3] | The first point.  |
| b    | [Vector][3] | The second point. |
| c    | [Vector][3] | The third point.  |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../Triangle.md
[3]: ../Vector.md
