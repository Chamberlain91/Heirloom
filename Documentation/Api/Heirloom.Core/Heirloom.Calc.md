# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Calc

> **Namespace**: [Heirloom][0]  

Math operations for `float` and a other data types including `int` .   
 Includes a few genric utility functions such as [Swap\<T>][1]

```cs
public static class Calc
```

#### Static Fields

[Random][2], [Perlin][3], [Simplex][4], [Pi][5], [TwoPi][6], [HalfPi][7], [ToRadians][8], [ToDegree][9], [Epsilon][10]

#### Static Methods

[Lerp][11], [AngleLerp][12], [Between][13], [IsBetween][14], [Rescale][15], [CosineInterpolation][16], [SmoothStep][17], [SmootherStep][18], [NearestPowerOfTwo][19], [LowerPowerOfTwo][20], [UpperPowerOfTwo][21], [IsPowerOfTwo][22], [Swap\<T>][1], [Order\<T>][23], [Wrap][24], [DeadZone][25], [Osc][26], [Sin][27], [Asin][28], [Cos][29], [Acos][30], [Tan][31], [Atan][32], [Atan2][33], [Distance][34], [Pow][35], [Log][36], [Sqrt][37], [Factorial][38], [Min][39], [Max][40], [Clamp][41], [Abs][42], [Sign][43], [Fraction][44], [Min\<T>][45], [Max\<T>][46], [Floor][47], [Ceil][48], [Round][49], [NearEquals][50], [NearZero][51]

## Fields

| Name           | Summary                                                                           |
|----------------|-----------------------------------------------------------------------------------|
| [Random][2]    | A static instance of the [Random][2] for convenience.                             |
| [Perlin][3]    | A static instance of the [PerlinNoise][52] for convenience.                       |
| [Simplex][4]   | A static instance of the [SimplexNoise][53] for convenience.                      |
| [Pi][5]        | An approximation of the constant Pi (180 Degrees or Pi Radians). 3.14159265359... |
| [TwoPi][6]     | Two times Pi. 360 Degrees in Radians. 6.28318530718...                            |
| [HalfPi][7]    | Half Pi. 90 Degrees in Radians. 0.5 * 3.141592653...                              |
| [ToRadians][8] | Pi / 180.0                                                                        |
| [ToDegree][9]  | 180.0 / Pi                                                                        |
| [Epsilon][10]  | A small number almost considered zero, greatly differs from `float.Epsilon` .     |

## Methods

| Name                      | Summary                                                                                                                                       |
|---------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [AngleLerp][12]           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [AngleLerp][12]           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [Between][13]             | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max` .                                                               |
| [IsBetween][14]           | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max` .                                                               |
| [Rescale][15]             | Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2` .                                                              |
| [Rescale][15]             | Rescales a value from the source domain a target domain.                                                                                      |
| [CosineInterpolation][16] | Computes a cosine based interpolation from `x1` to `x2` .                                                                                     |
| [SmoothStep][17]          | Computes the smooth-step of `x` between `min` and `max` .                                                                                     |
| [SmootherStep][18]        | Computes the smoother smooth-step of `x` between `min` and `max` .                                                                            |
| [NearestPowerOfTwo][19]   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [NearestPowerOfTwo][19]   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [LowerPowerOfTwo][20]     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [LowerPowerOfTwo][20]     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo][21]     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo][21]     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [IsPowerOfTwo][22]        | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo][22]        | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo][22]        | Determines if the given integer is a power of 2.                                                                                              |
| [Swap\<T>][1]             | Swaps two references.                                                                                                                         |
| [Swap\<T>][1]             | Swaps two positions within the given list.                                                                                                    |
| [Order\<T>][23]           | Orders the two given references so they are in comparable order.                                                                              |
| [Wrap][24]                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap][24]                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap][24]                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap][24]                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap][24]                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap][24]                | Wraps (loops) a number within a range.                                                                                                        |
| [DeadZone][25]            | Returns `center` if `x` is within `spread` units of `center` otherwise `x` .                                                                  |
| [Osc][26]                 | The function sine mapped to [0.0, 1.0].                                                                                                       |
| [Sin][27]                 | The function sine.                                                                                                                            |
| [Asin][28]                | Ihe inverse of the function sine.                                                                                                             |
| [Cos][29]                 | The function cosine.                                                                                                                          |
| [Acos][30]                | The inverse of the function cosine.                                                                                                           |
| [Tan][31]                 | The tangent function.                                                                                                                         |
| [Atan][32]                | The inverse of the tangent function.                                                                                                          |
| [Atan2][33]               | Computes the angle whose tangent is quotient to `x` and `y` .                                                                                 |
| [Distance][34]            | Computes the distance between a pair of one-dimensional points.                                                                               |
| [Distance][34]            | Computes the distance between a pair of two-dimensional points.                                                                               |
| [Pow][35]                 | Computes `x` raised to the power of `y` .                                                                                                     |
| [Pow][35]                 | Computes `x` raised to the power of `y` .                                                                                                     |
| [Log][36]                 | Computes the natural logarithm of `x` .                                                                                                       |
| [Log][36]                 | Computes the natural logarithm of `x` .                                                                                                       |
| [Log][36]                 | Computes the logarithm of `x` with base `b` .                                                                                                 |
| [Log][36]                 | Computes the logarithm of `x` with base `b` .                                                                                                 |
| [Sqrt][37]                | Computes the square root of `x` .                                                                                                             |
| [Sqrt][37]                | Computes the square root of `x` .                                                                                                             |
| [Factorial][38]           | Computes the factorial of `x` .                                                                                                               |
| [Factorial][38]           | Computes the factorial of `x` .                                                                                                               |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs][42]                 | Computes the absolute value of `x` .                                                                                                          |
| [Sign][43]                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction][44]            | Compute the fractional (decimal) portion of the number `x` .                                                                                  |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs][42]                 | Computes the absolute value of `x` .                                                                                                          |
| [Sign][43]                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction][44]            | Compute the fractional (decimal) portion of the number `x` .                                                                                  |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs][42]                 | Computes the absolute value of `x` .                                                                                                          |
| [Sign][43]                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs][42]                 | Computes the absolute value of `x` .                                                                                                          |
| [Sign][43]                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs][42]                 | Computes the absolute value of `x` .                                                                                                          |
| [Sign][43]                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min][39]                 | Returns the minimum value between `x` and `y` .                                                                                               |
| [Max][40]                 | Returns the maximum value between `x` and `y` .                                                                                               |
| [Clamp][41]               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min][39]                 |                                                                                                                                               |
| [Max][40]                 |                                                                                                                                               |
| [Min][39]                 |                                                                                                                                               |
| [Max][40]                 |                                                                                                                                               |
| [Min\<T>][45]             |                                                                                                                                               |
| [Min\<T>][45]             | Finds the comparably minimum value from the set of value.                                                                                     |
| [Max\<T>][46]             |                                                                                                                                               |
| [Max\<T>][46]             | Finds the comparably maximum value from the set of value.                                                                                     |
| [Floor][47]               | Computes the floor integer (rounding down) of the value `x` .                                                                                 |
| [Ceil][48]                | Computes the ceiling integer (rounding up) of the value `x` .                                                                                 |
| [Round][49]               | Computes the nearest integer of the value `x` .                                                                                               |
| [Floor][47]               | Computes the floor integer (rounding down) of the value `x` .                                                                                 |
| [Ceil][48]                | Computes the ceiling integer (rounding up) of the value `x` .                                                                                 |
| [Round][49]               | Computes the nearest integer of the value `x` .                                                                                               |
| [NearEquals][50]          | Determines if the two values are nearly equal comparing equality within a [Epsilon][10] threshold.                                            |
| [NearEquals][50]          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero][51]            | Determines if the value is nearly equal to zero by comparing equality within a [Epsilon][10] threshold.                                       |
| [NearEquals][50]          | Determines if the two values are nearly equal comparing equality within a [Epsilon][10] threshold.                                            |
| [NearEquals][50]          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero][51]            | Determines if the value is nearly equal to zero by comparing equality within a [Epsilon][10] threshold.                                       |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |
| [Lerp][11]                | Computes the linear interpolation from `x1` to `x2` by factor `t` .                                                                           |

[0]: ../Heirloom.Core.md
[1]: Heirloom.Calc.Swap[T].md
[2]: Heirloom.Calc.Random.md
[3]: Heirloom.Calc.Perlin.md
[4]: Heirloom.Calc.Simplex.md
[5]: Heirloom.Calc.Pi.md
[6]: Heirloom.Calc.TwoPi.md
[7]: Heirloom.Calc.HalfPi.md
[8]: Heirloom.Calc.ToRadians.md
[9]: Heirloom.Calc.ToDegree.md
[10]: Heirloom.Calc.Epsilon.md
[11]: Heirloom.Calc.Lerp.md
[12]: Heirloom.Calc.AngleLerp.md
[13]: Heirloom.Calc.Between.md
[14]: Heirloom.Calc.IsBetween.md
[15]: Heirloom.Calc.Rescale.md
[16]: Heirloom.Calc.CosineInterpolation.md
[17]: Heirloom.Calc.SmoothStep.md
[18]: Heirloom.Calc.SmootherStep.md
[19]: Heirloom.Calc.NearestPowerOfTwo.md
[20]: Heirloom.Calc.LowerPowerOfTwo.md
[21]: Heirloom.Calc.UpperPowerOfTwo.md
[22]: Heirloom.Calc.IsPowerOfTwo.md
[23]: Heirloom.Calc.Order[T].md
[24]: Heirloom.Calc.Wrap.md
[25]: Heirloom.Calc.DeadZone.md
[26]: Heirloom.Calc.Osc.md
[27]: Heirloom.Calc.Sin.md
[28]: Heirloom.Calc.Asin.md
[29]: Heirloom.Calc.Cos.md
[30]: Heirloom.Calc.Acos.md
[31]: Heirloom.Calc.Tan.md
[32]: Heirloom.Calc.Atan.md
[33]: Heirloom.Calc.Atan2.md
[34]: Heirloom.Calc.Distance.md
[35]: Heirloom.Calc.Pow.md
[36]: Heirloom.Calc.Log.md
[37]: Heirloom.Calc.Sqrt.md
[38]: Heirloom.Calc.Factorial.md
[39]: Heirloom.Calc.Min.md
[40]: Heirloom.Calc.Max.md
[41]: Heirloom.Calc.Clamp.md
[42]: Heirloom.Calc.Abs.md
[43]: Heirloom.Calc.Sign.md
[44]: Heirloom.Calc.Fraction.md
[45]: Heirloom.Calc.Min[T].md
[46]: Heirloom.Calc.Max[T].md
[47]: Heirloom.Calc.Floor.md
[48]: Heirloom.Calc.Ceil.md
[49]: Heirloom.Calc.Round.md
[50]: Heirloom.Calc.NearEquals.md
[51]: Heirloom.Calc.NearZero.md
[52]: Heirloom.PerlinNoise.md
[53]: Heirloom.SimplexNoise.md
