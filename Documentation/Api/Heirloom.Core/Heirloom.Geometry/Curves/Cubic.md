# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curves.Cubic (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Curves][1]

### Cubic(in Vector, in Vector, in Vector, in Vector, in float)

Computes the interpolated point on a cubic curve defined by `a` , `b` , `c` , `d` .

```cs
public static Vector Cubic(in Vector a, in Vector b, in Vector c, in Vector d, in float t)
```

| Name | Type        | Summary                                   |
|------|-------------|-------------------------------------------|
| a    | [Vector][2] | The curve's starting point.               |
| b    | [Vector][2] | The curve's first handle.                 |
| c    | [Vector][2] | The curve's second handle.                |
| d    | [Vector][2] | The curve's ending point.                 |
| t    | `float`     | The interpolation factor along the curve. |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Curves.md
[2]: ../../Heirloom/Vector.md
