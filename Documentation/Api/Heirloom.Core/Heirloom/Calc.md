# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc (Class)

> **Namespace**: [Heirloom][0]

Provides various math operations for `float` and `int` types including a few genric utility functions such as [Swap\<T>][1] or [Lerp][2] .

```cs
public static class Calc
```

`ExtensionAttribute`

### Constants

[Epsilon][3], [HalfPi][4], [Pi][5], [ToDegree][6], [ToRadians][7], [TwoPi][8]

### Static Fields

[Perlin][9], [Random][10], [Simplex][11]

### Static Methods

[Abs][12], [Acos][13], [Asin][14], [Atan][15], [Atan2][16], [Between][17], [Ceil][18], [Clamp][19], [Cos][20], [DeadZone][21], [Distance][22], [DistanceSquared][23], [Factorial][24], [Floor][25], [Fraction][26], [InverseSmoothStep][27], [IsBetween][28], [IsPowerOfTwo][29], [Lerp][2], [LerpAngle][30], [Log][31], [LowerPowerOfTwo][32], [ManhattanDistance][33], [Max][34], [Max\<T>][35], [Min][36], [Min\<T>][37], [NearEquals][38], [NearestPowerOfTwo][39], [NearZero][40], [Order\<T>][41], [Osc][42], [Pow][43], [Rescale][44], [Round][45], [Sign][46], [Sin][47], [SmoothStep][48], [Sqrt][49], [Swap\<T>][1], [Tan][50], [UpperPowerOfTwo][51], [Wrap][52]

## Fields

| Name           | Type               | Summary                                                                |
|----------------|--------------------|------------------------------------------------------------------------|
| [Epsilon][3]   | `float`            | A small number almost considered zero, greatly differs from float.E... |
| [HalfPi][4]    | `float`            |                                                                        |
| [Perlin][9]    | [PerlinNoise][53]  | A static instance of the PerlinNoise for convenience.                  |
| [Pi][5]        | `float`            |                                                                        |
| [Random][10]   | `Random`           | A static instance of the Random for convenience.                       |
| [Simplex][11]  | [SimplexNoise][54] | A static instance of the SimplexNoise for convenience.                 |
| [ToDegree][6]  | `float`            | 180.0 / Pi                                                             |
| [ToRadians][7] | `float`            | Pi / 180.0                                                             |
| [TwoPi][8]     | `float`            |                                                                        |

## Methods

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Abs(int)][12]                  | `int`       | Returns the absolute value of some number.                             |
| [Abs(float)][12]                | `float`     | Returns the absolute value of some number.                             |
| [Acos(float)][13]               | `float`     | Return the arccosine of the specified angle.                           |
| [Asin(float)][14]               | `float`     | Return the arcsine of the specified angle.                             |
| [Atan(float)][15]               | `float`     | Return the arctangent of the specified angle.                          |
| [Atan2(float, float)][16]       | `float`     | Computes the angle whose tangent is quotient to `x` and `y` .          |
| [Between(float, float, ...][17] | `float`     | Computes the interpolation factor (0.0 to 1.0) of `x` between `min`... |
| [Ceil(float)][18]               | `int`       | Computes the ceiling integer (rounding up) of the value `x` .          |
| [Clamp(int, int, int)][19]      | `int`       | Returns the value `x` clamped to the specified range.                  |
| [Clamp(float, float, fl...][19] | `float`     | Returns the value `x` clamped to the specified range.                  |
| [Cos(float)][20]                | `float`     | Return the cosine of the specified angle.                              |
| [DeadZone(float, float,...][21] | `float`     | Returns `center` if `x` is within `spread` units of `center` otherw... |
| [Distance(float, float)][22]    | `float`     | Computes the distance between a pair of one-dimensional points.        |
| [Distance(float, float,...][22] | `float`     | Computes the distance between a pair of two-dimensional points.        |
| [DistanceSquared(float,...][23] | `float`     | Computes the distance squared between a pair of two-dimensional poi... |
| [Factorial(int)][24]            | `int`       | Computes the factorial of `x` .                                        |
| [Factorial(uint)][24]           | `uint`      | Computes the factorial of `x` .                                        |
| [Floor(float)][25]              | `int`       | Computes the floor integer (rounding down) of the value `x` .          |
| [Fraction(float)][26]           | `float`     | Compute the fractional (decimal) portion of the number `x` .           |
| [InverseSmoothStep(float)][27]  | `float`     | Computes the inverse of SmoothStep .                                   |
| [IsBetween(float, float...][28] | `bool`      | Computes the interpolation factor (0.0 to 1.0) of `x` between `min`... |
| [IsPowerOfTwo(ulong)][29]       | `bool`      | Determines if the given integer is a power of 2.                       |
| [IsPowerOfTwo(uint)][29]        | `bool`      | Determines if the given integer is a power of 2.                       |
| [IsPowerOfTwo(int)][29]         | `bool`      | Determines if the given integer is a power of 2.                       |
| [Lerp(float, float, float)][2]  | `float`     | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [LerpAngle(float, float...][30] | `double`    | Computes the linear interpolation of two angles across the shorter ... |
| [Log(float)][31]                | `float`     | Computes the natural logarithm of `x` .                                |
| [Log(float, float)][31]         | `float`     | Computes the logarithm of `x` with base `b` .                          |
| [LowerPowerOfTwo(uint)][32]     | `uint`      | Computes the lower power of 2 nearest to x.                            |
| [LowerPowerOfTwo(int)][32]      | `int`       | Computes the lower power of 2 nearest to x.                            |
| [ManhattanDistance(floa...][33] | `float`     | Computes the manhattan-distance between a pair of two-dimensional p... |
| [Max(int, int)][34]             | `int`       | Returns the larger of two values.                                      |
| [Max(float, float)][34]         | `float`     | Returns the larger of two values.                                      |
| [Max(params int[])][34]         | `int`       |                                                                        |
| [Max(params float[])][34]       | `float`     |                                                                        |
| [Max<T>(params T[])][35]        | `T`         |                                                                        |
| [Min(int, int)][36]             | `int`       | Returns the smaller of two values.                                     |
| [Min(float, float)][36]         | `float`     | Returns the smaller of two values.                                     |
| [Min(params int[])][36]         | `int`       |                                                                        |
| [Min(params float[])][36]       | `float`     |                                                                        |
| [Min<T>(params T[])][37]        | `T`         |                                                                        |
| [NearEquals(float, float)][38]  | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearEquals(float, floa...][38] | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearestPowerOfTwo(uint)][39]   | `uint`      | Computes the nearest power of 2 to a number. This is done by comput... |
| [NearestPowerOfTwo(int)][39]    | `int`       | Computes the nearest power of 2 to a number. This is done by comput... |
| [NearZero(float)][40]           | `bool`      | Determines if the value is nearly equal to zero by comparing equali... |
| [Order<T>(ref T, ref T)][41]    | `void`      | Orders the two given references so they are in comparable order.       |
| [Osc(float)][42]                | `float`     | The function Sin mapped to [0.0, 1.0].                                 |
| [Pow(float, float)][43]         | `float`     | Computes `x` raised to the power of `y` .                              |
| [Rescale(float, float, ...][44] | `float`     | Rescales a value with domain `min1` to `max1` to a new domain `min2... |
| [Rescale(float, Range, ...][44] | `float`     | Rescales a value from the source domain a target domain.               |
| [Round(float)][45]              | `int`       | Computes the nearest integer of the value `x` .                        |
| [Sign(int)][46]                 | `int`       | Returns an integer that represents the sign of the specified number.   |
| [Sign(float)][46]               | `int`       | Returns an integer that represents the sign of the specified number.   |
| [Sin(float)][47]                | `float`     | Return the sine of the specified angle.                                |
| [SmoothStep(float, floa...][48] | `float`     | Computes the smooth-step of `x` between `min` and `max` .              |
| [SmoothStep(float)][48]         | `float`     | Computes smoothstep of a number. (Assumes `x` is in the range 0.0 t... |
| [Sqrt(float)][49]               | `float`     | Returns the absolute value of some number.                             |
| [Swap<T>(ref T, ref T)][1]      | `void`      | Swaps two references.                                                  |
| [Swap<T>(IList<T>, int,...][1]  | `void`      | Swaps two positions within the given list.                             |
| [Tan(float)][50]                | `float`     | Return the tangent of the specified angle.                             |
| [UpperPowerOfTwo(uint)][51]     | `uint`      | Computes the upper power of 2 nearest to x.                            |
| [UpperPowerOfTwo(int)][51]      | `int`       | Computes the upper power of 2 nearest to x.                            |
| [Wrap(int, int)][52]            | `int`       | Wraps (loops) a number within a zero to n range.                       |
| [Wrap(int, int, int)][52]       | `int`       | Wraps (loops) a number within a range.                                 |
| [Wrap(int, IntRange)][52]       | `int`       | Wraps (loops) a number within a range.                                 |
| [Wrap(float, float)][52]        | `float`     | Wraps (loops) a number within a zero to n range.                       |
| [Wrap(float, float, float)][52] | `float`     | Wraps (loops) a number within a range.                                 |
| [Wrap(float, Range)][52]        | `float`     | Wraps (loops) a number within a range.                                 |

[0]: ../../Heirloom.Core.md
[1]: Calc/Swap[T].md
[2]: Calc/Lerp.md
[3]: Calc/Epsilon.md
[4]: Calc/HalfPi.md
[5]: Calc/Pi.md
[6]: Calc/ToDegree.md
[7]: Calc/ToRadians.md
[8]: Calc/TwoPi.md
[9]: Calc/Perlin.md
[10]: Calc/Random.md
[11]: Calc/Simplex.md
[12]: Calc/Abs.md
[13]: Calc/Acos.md
[14]: Calc/Asin.md
[15]: Calc/Atan.md
[16]: Calc/Atan2.md
[17]: Calc/Between.md
[18]: Calc/Ceil.md
[19]: Calc/Clamp.md
[20]: Calc/Cos.md
[21]: Calc/DeadZone.md
[22]: Calc/Distance.md
[23]: Calc/DistanceSquared.md
[24]: Calc/Factorial.md
[25]: Calc/Floor.md
[26]: Calc/Fraction.md
[27]: Calc/InverseSmoothStep.md
[28]: Calc/IsBetween.md
[29]: Calc/IsPowerOfTwo.md
[30]: Calc/LerpAngle.md
[31]: Calc/Log.md
[32]: Calc/LowerPowerOfTwo.md
[33]: Calc/ManhattanDistance.md
[34]: Calc/Max.md
[35]: Calc/Max[T].md
[36]: Calc/Min.md
[37]: Calc/Min[T].md
[38]: Calc/NearEquals.md
[39]: Calc/NearestPowerOfTwo.md
[40]: Calc/NearZero.md
[41]: Calc/Order[T].md
[42]: Calc/Osc.md
[43]: Calc/Pow.md
[44]: Calc/Rescale.md
[45]: Calc/Round.md
[46]: Calc/Sign.md
[47]: Calc/Sin.md
[48]: Calc/SmoothStep.md
[49]: Calc/Sqrt.md
[50]: Calc/Tan.md
[51]: Calc/UpperPowerOfTwo.md
[52]: Calc/Wrap.md
[53]: PerlinNoise.md
[54]: SimplexNoise.md
