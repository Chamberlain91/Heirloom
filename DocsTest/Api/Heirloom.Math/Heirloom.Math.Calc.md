# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Calc (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Math operations for `System.Single` and a other data types including `System.Int32`.   
 Includes a few genric utility functions such as `Heirloom.Math.Calc.Swap``1(``0@,``0@)`

| Fields                 | Summary                                                                                 |
|------------------------|-----------------------------------------------------------------------------------------|
| [Random](#RANDE1E4)    | A static instance of the `Heirloom.Math.Calc.Random` for convenience.                   |
| [Perlin](#PERLAEEB)    | A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.   |
| [Simplex](#SIMP2F8D)   | A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience. |
| [Pi](#PIAA83)          | An approximation of the constant Pi (180 Degrees or Pi Radians). 3.14159265359...       |
| [TwoPi](#TWOP6693)     | Two times Pi. 360 Degrees in Radians. 6.28318530718...                                  |
| [HalfPi](#HALFD110)    | Half Pi. 90 Degrees in Radians. 0.5 * 3.141592653...                                    |
| [ToRadians](#TORA722D) | Pi / 180.0                                                                              |
| [ToDegree](#TODEE6B6)  | 180.0 / Pi                                                                              |
| [Epsilon](#EPSIB247)   | A small number almost considered zero, greatly differs from `System.Single.Epsilon`.    |

| Methods                          | Summary                                                                                                                                       |
|----------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [AngleLerp](#ANGLC5CF)           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [AngleLerp](#ANGLC5CF)           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [Between](#BETW1EFF)             | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.                                                                |
| [IsBetween](#ISBE589A)           | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.                                                                |
| [Rescale](#RESC8C92)             | Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2`.                                                               |
| [Rescale](#RESC8C92)             | Rescales a value from the source domain a target domain.                                                                                      |
| [CosineInterpolation](#COSI1808) | Computes a cosine based interpolation from `x1` to `x2`.                                                                                      |
| [SmoothStep](#SMOO5EC2)          | Computes the smooth-step of `x` between `min` and `max`.                                                                                      |
| [SmootherStep](#SMOO363D)        | Computes the smoother smooth-step of `x` between `min` and `max`.                                                                             |
| [NearestPowerOfTwo](#NEAR785D)   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [NearestPowerOfTwo](#NEAR785D)   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [LowerPowerOfTwo](#LOWEC50A)     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [LowerPowerOfTwo](#LOWEC50A)     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo](#UPPEEB7C)     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo](#UPPEEB7C)     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [IsPowerOfTwo](#ISPOCF21)        | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo](#ISPOCF21)        | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo](#ISPOCF21)        | Determines if the given integer is a power of 2.                                                                                              |
| [Swap<T>](#SWAPE745)             | Swaps two references.                                                                                                                         |
| [Swap<T>](#SWAPE745)             | Swaps two positions within the given list.                                                                                                    |
| [Order<T>](#ORDE797E)            | Orders the two given references so they are in comparable order.                                                                              |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRAP91C4)                | Wraps (loops) a number within a range.                                                                                                        |
| [DeadZone](#DEAD3137)            | Returns `center` if `x` is within `spread` units of `center` otherwise `x`.                                                                   |
| [Osc](#OSC6514)                  | The function sine mapped to [0.0, 1.0].                                                                                                       |
| [Sin](#SINBF9E)                  | The function sine.                                                                                                                            |
| [Asin](#ASIN6FB7)                | Ihe inverse of the function sine.                                                                                                             |
| [Cos](#COS4EDD)                  | The function cosine.                                                                                                                          |
| [Acos](#ACOSA697)                | The inverse of the function cosine.                                                                                                           |
| [Tan](#TAND4DA)                  | The tangent function.                                                                                                                         |
| [Atan](#ATANA363)                | The inverse of the tangent function.                                                                                                          |
| [Atan2](#ATANF732)               | Computes the angle whose tangent is quotient to `x` and `y`.                                                                                  |
| [Distance](#DIST3A36)            | Computes the distance between a pair of one-dimensional points.                                                                               |
| [Distance](#DIST3A36)            | Computes the distance between a pair of two-dimensional points.                                                                               |
| [Pow](#POW4EDD)                  | Computes `x` raised to the power of `y`.                                                                                                      |
| [Pow](#POW4EDD)                  | Computes `x` raised to the power of `y`.                                                                                                      |
| [Log](#LOG4EDD)                  | Computes the natural logarithm of `x`.                                                                                                        |
| [Log](#LOG4EDD)                  | Computes the natural logarithm of `x`.                                                                                                        |
| [Log](#LOG4EDD)                  | Computes the logarithm of `x` with base `b`.                                                                                                  |
| [Log](#LOG4EDD)                  | Computes the logarithm of `x` with base `b`.                                                                                                  |
| [Sqrt](#SQRTAD02)                | Computes the square root of `x`.                                                                                                              |
| [Sqrt](#SQRTAD02)                | Computes the square root of `x`.                                                                                                              |
| [Factorial](#FACT74CF)           | Computes the factorial of `x`.                                                                                                                |
| [Factorial](#FACT74CF)           | Computes the factorial of `x`.                                                                                                                |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSECE4)                  | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGNF27C)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction](#FRAC1E83)            | Compute the fractional (decimal) portion of the number `x`.                                                                                   |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSECE4)                  | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGNF27C)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction](#FRAC1E83)            | Compute the fractional (decimal) portion of the number `x`.                                                                                   |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSECE4)                  | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGNF27C)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSECE4)                  | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGNF27C)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSECE4)                  | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGNF27C)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MINBF9E)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXD4DA)                  | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAMC650)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MINBF9E)                  | Returns the minimum value in the given set of numbers.                                                                                        |
| [Max](#MAXD4DA)                  | Returns the maximum value in the given set of numbers.                                                                                        |
| [Min](#MINBF9E)                  | Returns the minimum value in the given set of numbers.                                                                                        |
| [Max](#MAXD4DA)                  | Returns the maximum value in the given set of numbers.                                                                                        |
| [Min<T>](#MIN<1EA6)              | Finds the comparably minimum value from the set of value.                                                                                     |
| [Min<T>](#MIN<1EA6)              | Finds the comparably minimum value from the set of value.                                                                                     |
| [Max<T>](#MAX<2275)              | Finds the comparably maximum value from the set of value.                                                                                     |
| [Max<T>](#MAX<2275)              | Finds the comparably maximum value from the set of value.                                                                                     |
| [Floor](#FLOO8101)               | Computes the floor integer (rounding down) of the value `x`.                                                                                  |
| [Ceil](#CEILAFCC)                | Computes the ceiling integer (rounding up) of the value `x`.                                                                                  |
| [Round](#ROUN73CA)               | Computes the nearest integer of the value `x`.                                                                                                |
| [Floor](#FLOO8101)               | Computes the floor integer (rounding down) of the value `x`.                                                                                  |
| [Ceil](#CEILAFCC)                | Computes the ceiling integer (rounding up) of the value `x`.                                                                                  |
| [Round](#ROUN73CA)               | Computes the nearest integer of the value `x`.                                                                                                |
| [NearEquals](#NEAR350D)          | Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                             |
| [NearEquals](#NEAR350D)          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero](#NEARB3B2)            | Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                        |
| [NearEquals](#NEAR350D)          | Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                             |
| [NearEquals](#NEAR350D)          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero](#NEARB3B2)            | Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                        |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERP252E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |

### Fields

#### <a name="RANDE1E4"></a> Random : Random
<small>`Read Only`</small>

A static instance of the `Heirloom.Math.Calc.Random` for convenience.

#### <a name="PERLAEEB"></a> Perlin : [PerlinNoise](Heirloom.Math.PerlinNoise.md)
<small>`Read Only`</small>

A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.

#### <a name="SIMP2F8D"></a> Simplex : [SimplexNoise](Heirloom.Math.SimplexNoise.md)
<small>`Read Only`</small>

A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience.

#### <a name="PIAA83"></a> Pi : float

An approximation of the constant Pi (180 Degrees or Pi Radians).   
 3.14159265359...

#### <a name="TWOP6693"></a> TwoPi : float

Two times Pi. 360 Degrees in Radians.   
 6.28318530718...

#### <a name="HALFD110"></a> HalfPi : float

Half Pi. 90 Degrees in Radians.   
 0.5 * 3.141592653...

#### <a name="TORA722D"></a> ToRadians : float

Pi / 180.0

#### <a name="TODEE6B6"></a> ToDegree : float

180.0 / Pi

#### <a name="EPSIB247"></a> Epsilon : float

A small number almost considered zero, greatly differs from `System.Single.Epsilon`.

#### <a name="RANDE1E4"></a> Random : Random
<small>`Static`, `Read Only`</small>

A static instance of the `Heirloom.Math.Calc.Random` for convenience.

#### <a name="PERLAEEB"></a> Perlin : [PerlinNoise](Heirloom.Math.PerlinNoise.md)
<small>`Static`, `Read Only`</small>

A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.

#### <a name="SIMP2F8D"></a> Simplex : [SimplexNoise](Heirloom.Math.SimplexNoise.md)
<small>`Static`, `Read Only`</small>

A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience.

#### <a name="PIAA83"></a> Pi : float
<small>`Static`</small>

An approximation of the constant Pi (180 Degrees or Pi Radians).   
 3.14159265359...

#### <a name="TWOP6693"></a> TwoPi : float
<small>`Static`</small>

Two times Pi. 360 Degrees in Radians.   
 6.28318530718...

#### <a name="HALFD110"></a> HalfPi : float
<small>`Static`</small>

Half Pi. 90 Degrees in Radians.   
 0.5 * 3.141592653...

#### <a name="TORA722D"></a> ToRadians : float
<small>`Static`</small>

Pi / 180.0

#### <a name="TODEE6B6"></a> ToDegree : float
<small>`Static`</small>

180.0 / Pi

#### <a name="EPSIB247"></a> Epsilon : float
<small>`Static`</small>

A small number almost considered zero, greatly differs from `System.Single.Epsilon`.

### Methods

#### <a name="LERP1E32"></a> Lerp(byte x1,  byte x2, float t) :  byte
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERP4C63"></a> Lerp(in sbyte x1, in sbyte x2, in float t) : sbyte
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="ANGL142C"></a> AngleLerp(double x1, double x2, double t) : double
<small>`Static`</small>

Computes the linear interpolation of two angles across the shorter distance.

<small>**x1**: <param name="x1">Start angle.</param></small>  
<small>**x2**: <param name="x2">End angle.</param></small>  
<small>**t**: <param name="t">Interpolation factor</param></small>  

#### <a name="ANGLB785"></a> AngleLerp(float x1, float x2, float t) : float
<small>`Static`</small>

Computes the linear interpolation of two angles across the shorter distance.

<small>**x1**: <param name="x1">Start angle.</param></small>  
<small>**x2**: <param name="x2">End angle.</param></small>  
<small>**t**: <param name="t">Interpolation factor</param></small>  

#### <a name="BETW198D"></a> Between(in float x, in float min, in float max) : float
<small>`Static`</small>

Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.


#### <a name="ISBED461"></a> IsBetween(in float x, in float min, in float max) : bool
<small>`Static`</small>

Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.


#### <a name="RESC24BC"></a> Rescale(in float x, in float min1, in float max1, in float min2, in float max2) : float
<small>`Static`</small>

Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2`.


#### <a name="RESC88F3"></a> Rescale(in float x, in [Range](Heirloom.Math.Range.md) src, in [Range](Heirloom.Math.Range.md) dst) : float
<small>`Static`</small>

Rescales a value from the source domain a target domain.


#### <a name="COSIA5D8"></a> CosineInterpolation(float x1, float x2, float t) : float
<small>`Static`</small>

Computes a cosine based interpolation from `x1` to `x2`.


#### <a name="SMOO4183"></a> SmoothStep(float min, float max, float x) : float
<small>`Static`</small>

Computes the smooth-step of `x` between `min` and `max`.


#### <a name="SMOOF6A0"></a> SmootherStep(float min, float max, float x) : float
<small>`Static`</small>

Computes the smoother smooth-step of `x` between `min` and `max`.


#### <a name="NEAR6678"></a> NearestPowerOfTwo(uint x) : uint
<small>`Static`</small>

Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="NEAR89EC"></a> NearestPowerOfTwo(int x) : int
<small>`Static`</small>

Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="LOWE9781"></a> LowerPowerOfTwo(uint x) : uint
<small>`Static`</small>

Computes the lower power of 2 nearest to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="LOWEEBAC"></a> LowerPowerOfTwo(int x) : int
<small>`Static`</small>

Computes the lower power of 2 nearest to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="UPPE6E59"></a> UpperPowerOfTwo(uint v) : uint
<small>`Static`</small>

Computes the upper power of 2 nearest to x.


#### <a name="UPPEE7C7"></a> UpperPowerOfTwo(int v) : int
<small>`Static`</small>

Computes the upper power of 2 nearest to x.


#### <a name="ISPO9095"></a> IsPowerOfTwo(ulong x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="ISPOF1B2"></a> IsPowerOfTwo(uint x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="ISPO1981"></a> IsPowerOfTwo(int x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="SWAPFD4B"></a> Swap<T>(ref T a, ref T b) : void
<small>`Static`</small>

Swaps two references.


#### <a name="SWAP3064"></a> Swap<T>(IList\<T> list, int a, int b) : void
<small>`Static`</small>

Swaps two positions within the given list.


#### <a name="ORDEE448"></a> Order<T>(ref T a, ref T b) : void
<small>`Static`</small>

Orders the two given references so they are in comparable order.


#### <a name="WRAPBCD6"></a> Wrap(int x, int n) : int
<small>`Static`</small>

Wraps (loops) a number within a zero to n range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**n**: <param name="n">Some upper bound from zero.</param></small>  

#### <a name="WRAPD3CA"></a> Wrap(int x, int min, int max) : int
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**min**: <param name="min">Some lower bound.</param></small>  
<small>**max**: <param name="max">Some upper bound.</param></small>  

#### <a name="WRAPB05E"></a> Wrap(int x, [IntRange](Heirloom.Math.IntRange.md) range) : int
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  

#### <a name="WRAP8FAF"></a> Wrap(float x, float n) : float
<small>`Static`</small>

Wraps (loops) a number within a zero to n range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**n**: <param name="n">Some upper bound from zero.</param></small>  

#### <a name="WRAPF354"></a> Wrap(float x, float min, float max) : float
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**min**: <param name="min">Some lower bound.</param></small>  
<small>**max**: <param name="max">Some upper bound.</param></small>  

#### <a name="WRAP1179"></a> Wrap(float x, [Range](Heirloom.Math.Range.md) range) : float
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  

#### <a name="DEAD59C9"></a> DeadZone(float x, float center, float spread) : float
<small>`Static`</small>

Returns `center` if `x` is within `spread` units of `center` otherwise `x`.


#### <a name="OSC(C703"></a> Osc(float x) : float
<small>`Static`</small>

The function sine mapped to [0.0, 1.0].


#### <a name="SIN(7B13"></a> Sin(float x) : float
<small>`Static`</small>

The function sine.


#### <a name="ASINB4E9"></a> Asin(float x) : float
<small>`Static`</small>

Ihe inverse of the function sine.


#### <a name="COS(637C"></a> Cos(float x) : float
<small>`Static`</small>

The function cosine.


#### <a name="ACOS3E5E"></a> Acos(float x) : float
<small>`Static`</small>

The inverse of the function cosine.


#### <a name="TAN(2AE1"></a> Tan(float x) : float
<small>`Static`</small>

The tangent function.


#### <a name="ATANE033"></a> Atan(float x) : float
<small>`Static`</small>

The inverse of the tangent function.


#### <a name="ATAN5850"></a> Atan2(float y, float x) : float
<small>`Static`</small>

Computes the angle whose tangent is quotient to `x` and `y`.


#### <a name="DISTED84"></a> Distance(float x1, float x2) : float
<small>`Static`</small>

Computes the distance between a pair of one-dimensional points.


#### <a name="DISTC594"></a> Distance(float x1, float y1, float x2, float y2) : float
<small>`Static`</small>

Computes the distance between a pair of two-dimensional points.


#### <a name="POW(6B12"></a> Pow(float x, float y) : float
<small>`Static`</small>

Computes `x` raised to the power of `y`.


#### <a name="POW(912A"></a> Pow(double x, double y) : double
<small>`Static`</small>

Computes `x` raised to the power of `y`.


#### <a name="LOG(FC92"></a> Log(float x) : float
<small>`Static`</small>

Computes the natural logarithm of `x`.


#### <a name="LOG(4650"></a> Log(double x) : double
<small>`Static`</small>

Computes the natural logarithm of `x`.


#### <a name="LOG(5B15"></a> Log(float x, float b) : float
<small>`Static`</small>

Computes the logarithm of `x` with base `b`.


#### <a name="LOG(2BFB"></a> Log(double x, double b) : double
<small>`Static`</small>

Computes the logarithm of `x` with base `b`.


#### <a name="SQRT996D"></a> Sqrt(float x) : float
<small>`Static`</small>

Computes the square root of `x`.


#### <a name="SQRT4A84"></a> Sqrt(double x) : double
<small>`Static`</small>

Computes the square root of `x`.


#### <a name="FACT871E"></a> Factorial(int x) : int
<small>`Static`</small>

Computes the factorial of `x`.


#### <a name="FACT6888"></a> Factorial(uint x) : uint
<small>`Static`</small>

Computes the factorial of `x`.


#### <a name="MIN(80FB"></a> Min(double x, double y) : double
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(5215"></a> Max(double x, double y) : double
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAM18B2"></a> Clamp(double x, double min, double max) : double
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS(1CB4"></a> Abs(double x) : double
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGNBB29"></a> Sign(double x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="FRACF6A6"></a> Fraction(double x) : double
<small>`Static`</small>

Compute the fractional (decimal) portion of the number `x`.


#### <a name="MIN(19D5"></a> Min(float x, float y) : float
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(3D06"></a> Max(float x, float y) : float
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAMF484"></a> Clamp(float x, float min, float max) : float
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS(1A29"></a> Abs(float x) : float
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGN971B"></a> Sign(float x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="FRACC290"></a> Fraction(float x) : float
<small>`Static`</small>

Compute the fractional (decimal) portion of the number `x`.


#### <a name="MIN(183B"></a> Min(int x, int y) : int
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(5223"></a> Max(int x, int y) : int
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAMF45D"></a> Clamp(int x, int min, int max) : int
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS(B887"></a> Abs(int x) : int
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGN586B"></a> Sign(int x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MIN(5446"></a> Min(uint x, uint y) : uint
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(6E70"></a> Max(uint x, uint y) : uint
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAM58FC"></a> Clamp(uint x, uint min, uint max) : uint
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MIN(D764"></a> Min(short x, short y) : short
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(5197"></a> Max(short x, short y) : short
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAM37C3"></a> Clamp(short x, short min, short max) : short
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS(E601"></a> Abs(short x) : short
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGN38E0"></a> Sign(short x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MIN(A647"></a> Min(ushort x, ushort y) : ushort
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(7EA5"></a> Max(ushort x, ushort y) : ushort
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAM248D"></a> Clamp(ushort x, ushort min, ushort max) : ushort
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MIN(16B1"></a> Min(sbyte x, sbyte y) : sbyte
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(C2F8"></a> Max(sbyte x, sbyte y) : sbyte
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAM4445"></a> Clamp(sbyte x, sbyte min, sbyte max) : sbyte
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS(BED4"></a> Abs(sbyte x) : sbyte
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGN7755"></a> Sign(sbyte x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MIN(85D4"></a> Min(byte x,  byte y) :  byte
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX(3B10"></a> Max(byte x,  byte y) :  byte
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAMFA07"></a> Clamp(byte x,  byte min,  byte max) :  byte
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MIN(F519"></a> Min(params int vals) : int
<small>`Static`</small>

Returns the minimum value in the given set of numbers.


#### <a name="MAX(8198"></a> Max(params int vals) : int
<small>`Static`</small>

Returns the maximum value in the given set of numbers.


#### <a name="MIN(2179"></a> Min(params float vals) : float
<small>`Static`</small>

Returns the minimum value in the given set of numbers.


#### <a name="MAX(864D"></a> Max(params float vals) : float
<small>`Static`</small>

Returns the maximum value in the given set of numbers.


#### <a name="MIN<2BBF"></a> Min<T>(params T vals) : T
<small>`Static`</small>

Finds the comparably minimum value from the set of value.


#### <a name="MIN<4058"></a> Min<T>(IList\<T> vals) : T
<small>`Static`</small>

Finds the comparably minimum value from the set of value.


#### <a name="MAX<29D3"></a> Max<T>(params T vals) : T
<small>`Static`</small>

Finds the comparably maximum value from the set of value.


#### <a name="MAX<E9A3"></a> Max<T>(IList\<T> vals) : T
<small>`Static`</small>

Finds the comparably maximum value from the set of value.


#### <a name="FLOO3657"></a> Floor(double x) : int
<small>`Static`</small>

Computes the floor integer (rounding down) of the value `x`.


#### <a name="CEIL3335"></a> Ceil(double x) : int
<small>`Static`</small>

Computes the ceiling integer (rounding up) of the value `x`.


#### <a name="ROUN922E"></a> Round(double x) : int
<small>`Static`</small>

Computes the nearest integer of the value `x`.


#### <a name="FLOOB207"></a> Floor(float x) : int
<small>`Static`</small>

Computes the floor integer (rounding down) of the value `x`.


#### <a name="CEIL69B7"></a> Ceil(float x) : int
<small>`Static`</small>

Computes the ceiling integer (rounding up) of the value `x`.


#### <a name="ROUND05B"></a> Round(float x) : int
<small>`Static`</small>

Computes the nearest integer of the value `x`.


#### <a name="NEAR95EA"></a> NearEquals(double x1, double x2) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEAR6E2C"></a> NearEquals(double x1, double x2, float threshold) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a threshold.


#### <a name="NEAR186B"></a> NearZero(double v) : bool
<small>`Static`</small>

Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEARB9BA"></a> NearEquals(float x1, float x2) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEARA94E"></a> NearEquals(float x1, float x2, float threshold) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a threshold.


#### <a name="NEAR5D5B"></a> NearZero(float v) : bool
<small>`Static`</small>

Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="LERP27CD"></a> Lerp(in float x1, in float x2, in float t) : float
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERPB99D"></a> Lerp(in double x1, in double x2, in double t) : double
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERPA070"></a> Lerp(in int x1, in int x2, in float t) : int
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERPB076"></a> Lerp(in uint x1, in uint x2, in float t) : uint
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERP1614"></a> Lerp(in short x1, in short x2, in float t) : short
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERP5F1F"></a> Lerp(in ushort x1, in ushort x2, in float t) : ushort
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


