# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## CurveTools (Class)

> **Namespace**: [Heirloom.Geometry][0]

Utility function for computation with quadratic and cubic curves.

```cs
public sealed class CurveTools
```

### Static Methods

[ApproximateLength][1], [Interpolate][2], [InterpolateDerivative][3]

## Methods

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [ApproximateLength(Vect...][1] | `float`     | Computes the approximate arc length of the quadratic curve using li... |
| [ApproximateLength(Vect...][1] | `float`     | Computes the approximate arc length of the cubic curve using line s... |
| [ApproximateLength(Func...][1] | `float`     | Helper function to compute the approximate length of a curve using ... |
| [Interpolate(in Vector,...][2] | [Vector][4] | Computes the interpolated point on a quadratic curve defined by `a`... |
| [Interpolate(in Vector,...][2] | [Vector][4] | Computes the interpolated point on a cubic curve defined by `a` , `... |
| [InterpolateDerivative(...][3] | [Vector][4] | Computes the interpolated point on the derivative of a quadratic cu... |
| [InterpolateDerivative(...][3] | [Vector][4] | Computes the interpolated point on the derivative of a cubic curve ... |

[0]: ../../Heirloom.Core.md
[1]: CurveTools/ApproximateLength.md
[2]: CurveTools/Interpolate.md
[3]: CurveTools/InterpolateDerivative.md
[4]: ../Heirloom/Vector.md
