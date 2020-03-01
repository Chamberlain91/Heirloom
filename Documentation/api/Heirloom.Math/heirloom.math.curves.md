# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Curves (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides utilities for working with Quadratic and Cubic curves.

| Methods | Summary |
|---------|---------|
| [Quadratic](#QUA908AB227) | Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`. |
| [QuadraticDerivative](#QUADBB204BC) | Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`. |
| [QuadraticApproximateLength](#QUA7E1C2E4B) | Computes the approximate arc length of the quadratic curve using line segments. |
| [QuadraticDerivativeApproximateLength](#QUA901BAD34) | Computes the approximate arc length of the derivative of a quadratic curve using line segments. |
| [Cubic](#CUBE3AEFE25) | Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`. |
| [CubicDerivative](#CUB7F1CC10E) | Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`. |
| [CubicApproximateLength](#CUB4DADDB9D) | Computes the approximate arc length of the cubic curve using line segments. |
| [CubicDerivativeApproximateLength](#CUB398AFAB2) | Computes the approximate arc length of the cubic curve using line segments. |

### Methods

#### <a name="QUA908AB227"></a>Quadratic(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in float t) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param>  
</small>
<small>**b**: <param name="b">The curve middle (handle).</param>  
</small>
<small>**c**: <param name="c">The curve ending point.</param>  
</small>
<small>**t**: <param name="t">The interpolation factor along the curve.</param>  
</small>

#### <a name="QUADBB204BC"></a>QuadraticDerivative(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in float t) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param>  
</small>
<small>**b**: <param name="b">The curve middle (handle).</param>  
</small>
<small>**c**: <param name="c">The curve ending point.</param>  
</small>
<small>**t**: <param name="t">The interpolation factor along the curve.</param>  
</small>

#### <a name="QUA7E1C2E4B"></a>QuadraticApproximateLength(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c) : float

<small>`Static`</small>

Computes the approximate arc length of the quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param>  
</small>
<small>**b**: <param name="b">The curve middle (handle).</param>  
</small>
<small>**c**: <param name="c">The curve ending point.</param>  
</small>

#### <a name="QUA901BAD34"></a>QuadraticDerivativeApproximateLength(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c) : float

<small>`Static`</small>

Computes the approximate arc length of the derivative of a quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param>  
</small>
<small>**b**: <param name="b">The curve middle (handle).</param>  
</small>
<small>**c**: <param name="c">The curve ending point.</param>  
</small>

#### <a name="CUBE3AEFE25"></a>Cubic(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in [Vector](heirloom.math.vector.md) d, in float t) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param>  
</small>
<small>**b**: <param name="b">The curve's first handle.</param>  
</small>
<small>**c**: <param name="c">The curve's second handle.</param>  
</small>
<small>**d**: <param name="d">The curve's ending point.</param>  
</small>
<small>**t**: <param name="t">The interpolation factor along the curve.</param>  
</small>

#### <a name="CUB7F1CC10E"></a>CubicDerivative(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in [Vector](heirloom.math.vector.md) d, in float t) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param>  
</small>
<small>**b**: <param name="b">The curve's first handle.</param>  
</small>
<small>**c**: <param name="c">The curve's second handle.</param>  
</small>
<small>**d**: <param name="d">The curve's ending point.</param>  
</small>
<small>**t**: <param name="t">The interpolation factor along the curve.</param>  
</small>

#### <a name="CUB4DADDB9D"></a>CubicApproximateLength(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in [Vector](heirloom.math.vector.md) d) : float

<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param>  
</small>
<small>**b**: <param name="b">The curve's first handle.</param>  
</small>
<small>**c**: <param name="c">The curve's second handle.</param>  
</small>
<small>**d**: <param name="d">The curve's ending point.</param>  
</small>

#### <a name="CUB398AFAB2"></a>CubicDerivativeApproximateLength(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b, in [Vector](heirloom.math.vector.md) c, in [Vector](heirloom.math.vector.md) d) : float

<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param>  
</small>
<small>**b**: <param name="b">The curve's first handle.</param>  
</small>
<small>**c**: <param name="c">The curve's second handle.</param>  
</small>
<small>**d**: <param name="d">The curve's ending point.</param>  
</small>

