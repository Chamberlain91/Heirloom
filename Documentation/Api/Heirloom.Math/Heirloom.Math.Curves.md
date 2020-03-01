# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Curves (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides utilities for working with Quadratic and Cubic curves.

| Methods                                              | Summary                                                                                           |
|------------------------------------------------------|---------------------------------------------------------------------------------------------------|
| [Quadratic](#QUA26890E67)                            | Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`.                    |
| [QuadraticDerivative](#QUA752123C)                   | Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`.  |
| [QuadraticApproximateLength](#QUA7C69076B)           | Computes the approximate arc length of the quadratic curve using line segments.                   |
| [QuadraticDerivativeApproximateLength](#QUACE96FD14) | Computes the approximate arc length of the derivative of a quadratic curve using line segments.   |
| [Cubic](#CUB236F6845)                                | Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`.                   |
| [CubicDerivative](#CUB8C9CA66E)                      | Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`. |
| [CubicApproximateLength](#CUB7333FA1D)               | Computes the approximate arc length of the cubic curve using line segments.                       |
| [CubicDerivativeApproximateLength](#CUBB892D932)     | Computes the approximate arc length of the cubic curve using line segments.                       |

### Methods

#### <a name="QUA26890E67"></a>Quadratic(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="QUA752123C"></a>QuadraticDerivative(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="QUA7C69076B"></a>QuadraticApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : float
<small>`Static`</small>

Computes the approximate arc length of the quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  

#### <a name="QUACE96FD14"></a>QuadraticDerivativeApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : float
<small>`Static`</small>

Computes the approximate arc length of the derivative of a quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  

#### <a name="CUB236F6845"></a>Cubic(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="CUB8C9CA66E"></a>CubicDerivative(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="CUB7333FA1D"></a>CubicApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d) : float
<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  

#### <a name="CUBB892D932"></a>CubicDerivativeApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d) : float
<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  

