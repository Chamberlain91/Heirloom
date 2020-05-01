# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curves.QuadraticDerivative (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Curves][1]

### QuadraticDerivative(in Vector, in Vector, in Vector, in float)

Computes the interpolated point on the derivative of a quadratic curve defined by `a` , `b` , `c` .

```cs
public static Vector QuadraticDerivative(in Vector a, in Vector b, in Vector c, in float t)
```

| Name | Type        | Summary                                   |
|------|-------------|-------------------------------------------|
| a    | [Vector][2] | The curve starting point.                 |
| b    | [Vector][2] | The curve middle (handle).                |
| c    | [Vector][2] | The curve ending point.                   |
| t    | `float`     | The interpolation factor along the curve. |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Curves.md
[2]: ../../Heirloom/Vector.md
