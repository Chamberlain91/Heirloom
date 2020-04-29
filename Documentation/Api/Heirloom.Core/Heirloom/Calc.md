# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc (Class)

> **Namespace**: [Heirloom][0]

Math operations for `float` and a other data types including `int` .   
 Includes a few genric utility functions such as [Swap\<T>][1]

```cs
public static class Calc
```

### Constants

[Epsilon][2], [HalfPi][3], [Pi][4], [ToDegree][5], [ToRadians][6], [TwoPi][7]

### Static Fields

[Perlin][8], [Random][9], [Simplex][10]

### Static Methods

[Abs][11], [Acos][12], [AngleLerp][13], [Asin][14], [Atan][15], [Atan2][16], [Between][17], [Ceil][18], [Clamp][19], [Cos][20], [CosineInterpolation][21], [DeadZone][22], [Distance][23], [Factorial][24], [Floor][25], [Fraction][26], [IsBetween][27], [IsPowerOfTwo][28], [Lerp][29], [Log][30], [LowerPowerOfTwo][31], [Max][32], [Max\<T>][33], [Min][34], [Min\<T>][35], [NearEquals][36], [NearestPowerOfTwo][37], [NearZero][38], [Order\<T>][39], [Osc][40], [Pow][41], [Rescale][42], [Round][43], [Sign][44], [Sin][45], [SmootherStep][46], [SmoothStep][47], [Sqrt][48], [Swap\<T>][1], [Tan][49], [UpperPowerOfTwo][50], [Wrap][51]

## Fields

| Name           | Type               | Summary                                                                |
|----------------|--------------------|------------------------------------------------------------------------|
| [Epsilon][2]   | `float`            | A small number almost considered zero, greatly differs from float.E... |
| [HalfPi][3]    | `float`            |                                                                        |
| [Perlin][8]    | [PerlinNoise][52]  | A static instance of the PerlinNoise for convenience.                  |
| [Pi][4]        | `float`            |                                                                        |
| [Random][9]    | `Random`           | A static instance of the Random for convenience.                       |
| [Simplex][10]  | [SimplexNoise][53] | A static instance of the SimplexNoise for convenience.                 |
| [ToDegree][5]  | `float`            | 180.0 / Pi                                                             |
| [ToRadians][6] | `float`            | Pi / 180.0                                                             |
| [TwoPi][7]     | `float`            |                                                                        |

## Methods

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Abs(double)][11]               | `double`    | Computes the absolute value of `x` .                                   |
| [Abs(float)][11]                | `float`     | Computes the absolute value of `x` .                                   |
| [Abs(int)][11]                  | `int`       | Computes the absolute value of `x` .                                   |
| [Abs(short)][11]                | `short`     | Computes the absolute value of `x` .                                   |
| [Abs(sbyte)][11]                | `sbyte`     | Computes the absolute value of `x` .                                   |
| [Acos(float)][12]               | `float`     | The inverse of the function cosine.                                    |
| [AngleLerp(double, doub...][13] | `double`    | Computes the linear interpolation of two angles across the shorter ... |
| [AngleLerp(float, float...][13] | `float`     | Computes the linear interpolation of two angles across the shorter ... |
| [Asin(float)][14]               | `float`     | Ihe inverse of the function sine.                                      |
| [Atan(float)][15]               | `float`     | The inverse of the tangent function.                                   |
| [Atan2(float, float)][16]       | `float`     | Computes the angle whose tangent is quotient to `x` and `y` .          |
| [Between(in float, in f...][17] | `float`     | Computes the interpolation factor (0.0 to 1.0) of `x` between `min`... |
| [Ceil(double)][18]              | `int`       | Computes the ceiling integer (rounding up) of the value `x` .          |
| [Ceil(float)][18]               | `int`       | Computes the ceiling integer (rounding up) of the value `x` .          |
| [Clamp(double, double, ...][19] | `double`    | Returns the value `x` clamped to the specified range.                  |
| [Clamp(float, float, fl...][19] | `float`     | Returns the value `x` clamped to the specified range.                  |
| [Clamp(int, int, int)][19]      | `int`       | Returns the value `x` clamped to the specified range.                  |
| [Clamp(uint, uint, uint)][19]   | `uint`      | Returns the value `x` clamped to the specified range.                  |
| [Clamp(short, short, sh...][19] | `short`     | Returns the value `x` clamped to the specified range.                  |
| [Clamp(ushort, ushort, ...][19] | `ushort`    | Returns the value `x` clamped to the specified range.                  |
| [Clamp(sbyte, sbyte, sb...][19] | `sbyte`     | Returns the value `x` clamped to the specified range.                  |
| [Clamp(byte, byte, byte)][19]   | ` byte`     | Returns the value `x` clamped to the specified range.                  |
| [Cos(float)][20]                | `float`     | The function cosine.                                                   |
| [CosineInterpolation(fl...][21] | `float`     | Computes a cosine based interpolation from `x1` to `x2` .              |
| [DeadZone(float, float,...][22] | `float`     | Returns `center` if `x` is within `spread` units of `center` otherw... |
| [Distance(float, float)][23]    | `float`     | Computes the distance between a pair of one-dimensional points.        |
| [Distance(float, float,...][23] | `float`     | Computes the distance between a pair of two-dimensional points.        |
| [Factorial(int)][24]            | `int`       | Computes the factorial of `x` .                                        |
| [Factorial(uint)][24]           | `uint`      | Computes the factorial of `x` .                                        |
| [Floor(double)][25]             | `int`       | Computes the floor integer (rounding down) of the value `x` .          |
| [Floor(float)][25]              | `int`       | Computes the floor integer (rounding down) of the value `x` .          |
| [Fraction(double)][26]          | `double`    | Compute the fractional (decimal) portion of the number `x` .           |
| [Fraction(float)][26]           | `float`     | Compute the fractional (decimal) portion of the number `x` .           |
| [IsBetween(in float, in...][27] | `bool`      | Computes the interpolation factor (0.0 to 1.0) of `x` between `min`... |
| [IsPowerOfTwo(ulong)][28]       | `bool`      | Determines if the given integer is a power of 2.                       |
| [IsPowerOfTwo(uint)][28]        | `bool`      | Determines if the given integer is a power of 2.                       |
| [IsPowerOfTwo(int)][28]         | `bool`      | Determines if the given integer is a power of 2.                       |
| [Lerp(byte, byte, float)][29]   | ` byte`     | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in sbyte, in sbyt...][29] | `sbyte`     | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in float, in floa...][29] | `float`     | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in double, in dou...][29] | `double`    | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in int, in int, i...][29] | `int`       | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in uint, in uint,...][29] | `uint`      | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in short, in shor...][29] | `short`     | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Lerp(in ushort, in ush...][29] | `ushort`    | Computes the linear interpolation from `x1` to `x2` by factor `t` .    |
| [Log(float)][30]                | `float`     | Computes the natural logarithm of `x` .                                |
| [Log(double)][30]               | `double`    | Computes the natural logarithm of `x` .                                |
| [Log(float, float)][30]         | `float`     | Computes the logarithm of `x` with base `b` .                          |
| [Log(double, double)][30]       | `double`    | Computes the logarithm of `x` with base `b` .                          |
| [LowerPowerOfTwo(uint)][31]     | `uint`      | Computes the lower power of 2 nearest to x.                            |
| [LowerPowerOfTwo(int)][31]      | `int`       | Computes the lower power of 2 nearest to x.                            |
| [Max(double, double)][32]       | `double`    | Returns the maximum value between `x` and `y` .                        |
| [Max(float, float)][32]         | `float`     | Returns the maximum value between `x` and `y` .                        |
| [Max(int, int)][32]             | `int`       | Returns the maximum value between `x` and `y` .                        |
| [Max(uint, uint)][32]           | `uint`      | Returns the maximum value between `x` and `y` .                        |
| [Max(short, short)][32]         | `short`     | Returns the maximum value between `x` and `y` .                        |
| [Max(ushort, ushort)][32]       | `ushort`    | Returns the maximum value between `x` and `y` .                        |
| [Max(sbyte, sbyte)][32]         | `sbyte`     | Returns the maximum value between `x` and `y` .                        |
| [Max(byte, byte)][32]           | ` byte`     | Returns the maximum value between `x` and `y` .                        |
| [Max(params int[])][32]         | `int`       |                                                                        |
| [Max(params float[])][32]       | `float`     |                                                                        |
| [Max<T>(params T[])][33]        | `T`         |                                                                        |
| [Max<T>(IList<T>)][33]          | `T`         | Finds the comparably maximum value from the set of value.              |
| [Min(double, double)][34]       | `double`    | Returns the minimum value between `x` and `y` .                        |
| [Min(float, float)][34]         | `float`     | Returns the minimum value between `x` and `y` .                        |
| [Min(int, int)][34]             | `int`       | Returns the minimum value between `x` and `y` .                        |
| [Min(uint, uint)][34]           | `uint`      | Returns the minimum value between `x` and `y` .                        |
| [Min(short, short)][34]         | `short`     | Returns the minimum value between `x` and `y` .                        |
| [Min(ushort, ushort)][34]       | `ushort`    | Returns the minimum value between `x` and `y` .                        |
| [Min(sbyte, sbyte)][34]         | `sbyte`     | Returns the minimum value between `x` and `y` .                        |
| [Min(byte, byte)][34]           | ` byte`     | Returns the minimum value between `x` and `y` .                        |
| [Min(params int[])][34]         | `int`       |                                                                        |
| [Min(params float[])][34]       | `float`     |                                                                        |
| [Min<T>(params T[])][35]        | `T`         |                                                                        |
| [Min<T>(IList<T>)][35]          | `T`         | Finds the comparably minimum value from the set of value.              |
| [NearEquals(double, dou...][36] | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearEquals(double, dou...][36] | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearEquals(float, float)][36]  | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearEquals(float, floa...][36] | `bool`      | Determines if the two values are nearly equal comparing equality wi... |
| [NearestPowerOfTwo(uint)][37]   | `uint`      | Computes the nearest power of 2 to a number. This is done by comput... |
| [NearestPowerOfTwo(int)][37]    | `int`       | Computes the nearest power of 2 to a number. This is done by comput... |
| [NearZero(double)][38]          | `bool`      | Determines if the value is nearly equal to zero by comparing equali... |
| [NearZero(float)][38]           | `bool`      | Determines if the value is nearly equal to zero by comparing equali... |
| [Order<T>(ref T, ref T)][39]    | `void`      | Orders the two given references so they are in comparable order.       |
| [Osc(float)][40]                | `float`     | The function sine mapped to [0.0, 1.0].                                |
| [Pow(float, float)][41]         | `float`     | Computes `x` raised to the power of `y` .                              |
| [Pow(double, double)][41]       | `double`    | Computes `x` raised to the power of `y` .                              |
| [Rescale(in float, in f...][42] | `float`     | Rescales a value with domain `min1` to `max1` to a new domain `min2... |
| [Rescale(in float, in R...][42] | `float`     | Rescales a value from the source domain a target domain.               |
| [Round(double)][43]             | `int`       | Computes the nearest integer of the value `x` .                        |
| [Round(float)][43]              | `int`       | Computes the nearest integer of the value `x` .                        |
| [Sign(double)][44]              | `int`       | Returns the sign of `x` as if compared against zero (-1, 0 or +1)      |
| [Sign(float)][44]               | `int`       | Returns the sign of `x` as if compared against zero (-1, 0 or +1)      |
| [Sign(int)][44]                 | `int`       | Returns the sign of `x` as if compared against zero (-1, 0 or +1)      |
| [Sign(short)][44]               | `int`       | Returns the sign of `x` as if compared against zero (-1, 0 or +1)      |
| [Sign(sbyte)][44]               | `int`       | Returns the sign of `x` as if compared against zero (-1, 0 or +1)      |
| [Sin(float)][45]                | `float`     | The function sine.                                                     |
| [SmootherStep(float, fl...][46] | `float`     | Computes the smoother smooth-step of `x` between `min` and `max` .     |
| [SmoothStep(float, floa...][47] | `float`     | Computes the smooth-step of `x` between `min` and `max` .              |
| [Sqrt(float)][48]               | `float`     | Computes the square root of `x` .                                      |
| [Sqrt(double)][48]              | `double`    | Computes the square root of `x` .                                      |
| [Swap<T>(ref T, ref T)][1]      | `void`      | Swaps two references.                                                  |
| [Swap<T>(IList<T>, int,...][1]  | `void`      | Swaps two positions within the given list.                             |
| [Tan(float)][49]                | `float`     | The tangent function.                                                  |
| [UpperPowerOfTwo(uint)][50]     | `uint`      | Computes the upper power of 2 nearest to x.                            |
| [UpperPowerOfTwo(int)][50]      | `int`       | Computes the upper power of 2 nearest to x.                            |
| [Wrap(int, int)][51]            | `int`       | Wraps (loops) a number within a zero to n range.                       |
| [Wrap(int, int, int)][51]       | `int`       | Wraps (loops) a number within a range.                                 |
| [Wrap(int, IntRange)][51]       | `int`       | Wraps (loops) a number within a range.                                 |
| [Wrap(float, float)][51]        | `float`     | Wraps (loops) a number within a zero to n range.                       |
| [Wrap(float, float, float)][51] | `float`     | Wraps (loops) a number within a range.                                 |
| [Wrap(float, Range)][51]        | `float`     | Wraps (loops) a number within a range.                                 |

[0]: ../../Heirloom.Core.md
[1]: Calc/Swap[T].md
[2]: Calc/Epsilon.md
[3]: Calc/HalfPi.md
[4]: Calc/Pi.md
[5]: Calc/ToDegree.md
[6]: Calc/ToRadians.md
[7]: Calc/TwoPi.md
[8]: Calc/Perlin.md
[9]: Calc/Random.md
[10]: Calc/Simplex.md
[11]: Calc/Abs.md
[12]: Calc/Acos.md
[13]: Calc/AngleLerp.md
[14]: Calc/Asin.md
[15]: Calc/Atan.md
[16]: Calc/Atan2.md
[17]: Calc/Between.md
[18]: Calc/Ceil.md
[19]: Calc/Clamp.md
[20]: Calc/Cos.md
[21]: Calc/CosineInterpolation.md
[22]: Calc/DeadZone.md
[23]: Calc/Distance.md
[24]: Calc/Factorial.md
[25]: Calc/Floor.md
[26]: Calc/Fraction.md
[27]: Calc/IsBetween.md
[28]: Calc/IsPowerOfTwo.md
[29]: Calc/Lerp.md
[30]: Calc/Log.md
[31]: Calc/LowerPowerOfTwo.md
[32]: Calc/Max.md
[33]: Calc/Max[T].md
[34]: Calc/Min.md
[35]: Calc/Min[T].md
[36]: Calc/NearEquals.md
[37]: Calc/NearestPowerOfTwo.md
[38]: Calc/NearZero.md
[39]: Calc/Order[T].md
[40]: Calc/Osc.md
[41]: Calc/Pow.md
[42]: Calc/Rescale.md
[43]: Calc/Round.md
[44]: Calc/Sign.md
[45]: Calc/Sin.md
[46]: Calc/SmootherStep.md
[47]: Calc/SmoothStep.md
[48]: Calc/Sqrt.md
[49]: Calc/Tan.md
[50]: Calc/UpperPowerOfTwo.md
[51]: Calc/Wrap.md
[52]: PerlinNoise.md
[53]: SimplexNoise.md
