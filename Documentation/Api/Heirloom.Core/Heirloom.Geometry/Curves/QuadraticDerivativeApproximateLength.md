# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curves.QuadraticDerivativeApproximateLength (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Curves][1]

### QuadraticDerivativeApproximateLength(in Vector, in Vector, in Vector)

Computes the approximate arc length of the derivative of a quadratic curve using line segments.

```cs
public static float QuadraticDerivativeApproximateLength(in Vector a, in Vector b, in Vector c)
```

| Name | Type        | Summary                    |
|------|-------------|----------------------------|
| a    | [Vector][2] | The curve starting point.  |
| b    | [Vector][2] | The curve middle (handle). |
| c    | [Vector][2] | The curve ending point.    |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Curves.md
[2]: ../../Heirloom/Vector.md
