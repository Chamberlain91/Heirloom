# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Curves (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides utilities for working with Quadratic and Cubic curves.

| Methods                                           | Summary                                                                                           |
|---------------------------------------------------|---------------------------------------------------------------------------------------------------|
| [Quadratic](#QUADF5DA)                            | Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`.                    |
| [QuadraticDerivative](#QUAD6D2C)                  | Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`.  |
| [QuadraticApproximateLength](#QUADB62A)           | Computes the approximate arc length of the quadratic curve using line segments.                   |
| [QuadraticDerivativeApproximateLength](#QUADAE61) | Computes the approximate arc length of the derivative of a quadratic curve using line segments.   |
| [Cubic](#CUBI7230)                                | Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`.                   |
| [CubicDerivative](#CUBI976A)                      | Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`. |
| [CubicApproximateLength](#CUBI99EF)               | Computes the approximate arc length of the cubic curve using line segments.                       |
| [CubicDerivativeApproximateLength](#CUBI6D7A)     | Computes the approximate arc length of the cubic curve using line segments.                       |

### Methods

#### <a name="QUAD2689"></a> Quadratic(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="QUAD7521"></a> QuadraticDerivative(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on the derivative of a quadratic curve defined by `a`, `b`, `c`.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="QUAD7C69"></a> QuadraticApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : float
<small>`Static`</small>

Computes the approximate arc length of the quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  

#### <a name="QUADCE96"></a> QuadraticDerivativeApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c) : float
<small>`Static`</small>

Computes the approximate arc length of the derivative of a quadratic curve using line segments.

<small>**a**: <param name="a">The curve starting point.</param></small>  
<small>**b**: <param name="b">The curve middle (handle).</param></small>  
<small>**c**: <param name="c">The curve ending point.</param></small>  

#### <a name="CUBI236F"></a> Cubic(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="CUBI8C9C"></a> CubicDerivative(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d, in float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the interpolated point on the derivative of a cubic curve defined by `a`, `b`, `c`, `d`.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  
<small>**t**: <param name="t">The interpolation factor along the curve.</param></small>  

#### <a name="CUBI7333"></a> CubicApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d) : float
<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  

#### <a name="CUBIB892"></a> CubicDerivativeApproximateLength(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b, in [Vector](Heirloom.Math.Vector.md) c, in [Vector](Heirloom.Math.Vector.md) d) : float
<small>`Static`</small>

Computes the approximate arc length of the cubic curve using line segments.

<small>**a**: <param name="a">The curve's starting point.</param></small>  
<small>**b**: <param name="b">The curve's first handle.</param></small>  
<small>**c**: <param name="c">The curve's second handle.</param></small>  
<small>**d**: <param name="d">The curve's ending point.</param></small>  

