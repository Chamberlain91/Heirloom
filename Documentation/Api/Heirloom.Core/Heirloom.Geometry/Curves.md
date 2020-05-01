# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Curves (Class)

> **Namespace**: [Heirloom.Geometry][0]

Provides utilities for working with Quadratic and Cubic curves.

```cs
public static class Curves
```

### Static Methods

[Cubic][1], [CubicApproximateLength][2], [CubicDerivative][3], [CubicDerivativeApproximateLength][4], [Quadratic][5], [QuadraticApproximateLength][6], [QuadraticDerivative][7], [QuadraticDerivativeApproximateLength][8]

## Methods

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [Cubic(in Vector, in Ve...][1] | [Vector][9] | Computes the interpolated point on a cubic curve defined by `a` , `... |
| [CubicApproximateLength...][2] | `float`     | Computes the approximate arc length of the cubic curve using line s... |
| [CubicDerivative(in Vec...][3] | [Vector][9] | Computes the interpolated point on the derivative of a cubic curve ... |
| [CubicDerivativeApproxi...][4] | `float`     | Computes the approximate arc length of the cubic curve using line s... |
| [Quadratic(in Vector, i...][5] | [Vector][9] | Computes the interpolated point on a quadratic curve defined by `a`... |
| [QuadraticApproximateLe...][6] | `float`     | Computes the approximate arc length of the quadratic curve using li... |
| [QuadraticDerivative(in...][7] | [Vector][9] | Computes the interpolated point on the derivative of a quadratic cu... |
| [QuadraticDerivativeApp...][8] | `float`     | Computes the approximate arc length of the derivative of a quadrati... |

[0]: ../../Heirloom.Core.md
[1]: Curves/Cubic.md
[2]: Curves/CubicApproximateLength.md
[3]: Curves/CubicDerivative.md
[4]: Curves/CubicDerivativeApproximateLength.md
[5]: Curves/Quadratic.md
[6]: Curves/QuadraticApproximateLength.md
[7]: Curves/QuadraticDerivative.md
[8]: Curves/QuadraticDerivativeApproximateLength.md
[9]: ../Heirloom/Vector.md
