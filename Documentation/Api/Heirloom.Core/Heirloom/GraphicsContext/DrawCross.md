# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.DrawCross (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### DrawCross(in Vector, float, float)

Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.

```cs
public void DrawCross(in Vector center, float size = 3, float width = 1)
```

| Name   | Type        | Summary                                             |
|--------|-------------|-----------------------------------------------------|
| center | [Vector][2] | The position of the cross.                          |
| size   | `float`     | Size in screen pixels (not world space).            |
| width  | `float`     | Width of the lines screen pixels (not world space). |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../Vector.md
