# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Curves

> **Namespace**: [Heirloom][0]  

Provides utilities for working with Quadratic and Cubic curves.

```cs
public static class Curves
```

#### Static Methods

[Quadratic][1], [QuadraticDerivative][2], [QuadraticApproximateLength][3], [QuadraticDerivativeApproximateLength][4], [Cubic][5], [CubicDerivative][6], [CubicApproximateLength][7], [CubicDerivativeApproximateLength][8]

## Methods

| Name                                      | Summary                                                                                               |
|-------------------------------------------|-------------------------------------------------------------------------------------------------------|
| [Quadratic][1]                            | Computes the interpolated point on a quadratic curve defined by `a` , `b` , `c` .                     |
| [QuadraticDerivative][2]                  | Computes the interpolated point on the derivative of a quadratic curve defined by `a` , `b` , `c` .   |
| [QuadraticApproximateLength][3]           | Computes the approximate arc length of the quadratic curve using line segments.                       |
| [QuadraticDerivativeApproximateLength][4] | Computes the approximate arc length of the derivative of a quadratic curve using line segments.       |
| [Cubic][5]                                | Computes the interpolated point on a cubic curve defined by `a` , `b` , `c` , `d` .                   |
| [CubicDerivative][6]                      | Computes the interpolated point on the derivative of a cubic curve defined by `a` , `b` , `c` , `d` . |
| [CubicApproximateLength][7]               | Computes the approximate arc length of the cubic curve using line segments.                           |
| [CubicDerivativeApproximateLength][8]     | Computes the approximate arc length of the cubic curve using line segments.                           |

[0]: ../../Heirloom.Core.md
[1]: Curves/Quadratic.md
[2]: Curves/QuadraticDerivative.md
[3]: Curves/QuadraticApproximateLength.md
[4]: Curves/QuadraticDerivativeApproximateLength.md
[5]: Curves/Cubic.md
[6]: Curves/CubicDerivative.md
[7]: Curves/CubicApproximateLength.md
[8]: Curves/CubicDerivativeApproximateLength.md
