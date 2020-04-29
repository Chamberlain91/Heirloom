# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curves.CubicDerivativeApproximateLength (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Curves][1]

### CubicDerivativeApproximateLength(in Vector, in Vector, in Vector, in Vector)

Computes the approximate arc length of the cubic curve using line segments.

```cs
public static float CubicDerivativeApproximateLength(in Vector a, in Vector b, in Vector c, in Vector d)
```

| Name | Type        | Summary                     |
|------|-------------|-----------------------------|
| a    | [Vector][2] | The curve's starting point. |
| b    | [Vector][2] | The curve's first handle.   |
| c    | [Vector][2] | The curve's second handle.  |
| d    | [Vector][2] | The curve's ending point.   |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Curves.md
[2]: ../Vector.md
