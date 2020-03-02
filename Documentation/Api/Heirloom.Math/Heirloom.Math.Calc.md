# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Calc (Static Class)
<small>**Namespace**: Heirloom.Math</small>  

Math operations for float and a other data types including int.   
 Includes a few genric utility functions such as `Heirloom.Math.Calc.Swap``1(``0@,``0@)`

| Fields                    | Summary                                                                                 |
|---------------------------|-----------------------------------------------------------------------------------------|
| [Random](#RANE1E4B317)    | A static instance of the `Heirloom.Math.Calc.Random` for convenience.                   |
| [Perlin](#PERAEEBD0B8)    | A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.   |
| [Simplex](#SIM2F8D31EC)   | A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience. |
| [Pi](#PIAA83371)          | An approximation of the constant Pi (180 Degrees or Pi Radians). 3.14159265359...       |
| [TwoPi](#TWO66932461)     | Two times Pi. 360 Degrees in Radians. 6.28318530718...                                  |
| [HalfPi](#HALD110A7A8)    | Half Pi. 90 Degrees in Radians. 0.5 * 3.141592653...                                    |
| [ToRadians](#TOR722D3A31) | Pi / 180.0                                                                              |
| [ToDegree](#TODE6B6954F)  | 180.0 / Pi                                                                              |
| [Epsilon](#EPSB247C6F0)   | A small number almost considered zero, greatly differs from `System.Single.Epsilon`.    |

| Methods                             | Summary                                                                                                                                       |
|-------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| [Lerp](#LER1E32ABA1)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LER4C636FF9)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [AngleLerp](#ANG142CA73E)           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [AngleLerp](#ANGB785B7AC)           | Computes the linear interpolation of two angles across the shorter distance.                                                                  |
| [Between](#BET198DC48C)             | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.                                                                |
| [IsBetween](#ISBD461C5F6)           | Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.                                                                |
| [Rescale](#RES24BC2AB3)             | Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2`.                                                               |
| [Rescale](#RES88F32950)             | Rescales a value from the source domain a target domain.                                                                                      |
| [CosineInterpolation](#COSA5D83C23) | Computes a cosine based interpolation from `x1` to `x2`.                                                                                      |
| [SmoothStep](#SMO4183BCC1)          | Computes the smooth-step of `x` between `min` and `max`.                                                                                      |
| [SmootherStep](#SMOF6A04A40)        | Computes the smoother smooth-step of `x` between `min` and `max`.                                                                             |
| [NearestPowerOfTwo](#NEA66788BDF)   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [NearestPowerOfTwo](#NEA89ECBB2F)   | Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x. |
| [LowerPowerOfTwo](#LOW9781B988)     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [LowerPowerOfTwo](#LOWEBAC5A48)     | Computes the lower power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo](#UPP6E59B0D1)     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [UpperPowerOfTwo](#UPPE7C7FFD5)     | Computes the upper power of 2 nearest to x.                                                                                                   |
| [IsPowerOfTwo](#ISP9095B2A2)        | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo](#ISPF1B2047)         | Determines if the given integer is a power of 2.                                                                                              |
| [IsPowerOfTwo](#ISP19811CD6)        | Determines if the given integer is a power of 2.                                                                                              |
| [Swap<T>](#SWAFD4B5953)             | Swaps two references.                                                                                                                         |
| [Swap<T>](#SWA30647658)             | Swaps two positions within the given list.                                                                                                    |
| [Order<T>](#ORDE448E4D8)            | Orders the two given references so they are in comparable order.                                                                              |
| [Wrap](#WRABCD6B4CE)                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap](#WRAD3CAA971)                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRAB05E993)                 | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRA8FAF8DA9)                | Wraps (loops) a number within a zero to n range.                                                                                              |
| [Wrap](#WRAF3544011)                | Wraps (loops) a number within a range.                                                                                                        |
| [Wrap](#WRA1179C81F)                | Wraps (loops) a number within a range.                                                                                                        |
| [DeadZone](#DEA59C907BB)            | Returns `center` if `x` is within `spread` units of `center` otherwise `x`.                                                                   |
| [Osc](#OSCC703CDB2)                 | The function sine mapped to [0.0, 1.0].                                                                                                       |
| [Sin](#SIN7B139DD3)                 | The function sine.                                                                                                                            |
| [Asin](#ASIB4E9E3D6)                | Ihe inverse of the function sine.                                                                                                             |
| [Cos](#COS637C3FC2)                 | The function cosine.                                                                                                                          |
| [Acos](#ACO3E5E8CB1)                | The inverse of the function cosine.                                                                                                           |
| [Tan](#TAN2AE1A1A2)                 | The tangent function.                                                                                                                         |
| [Atan](#ATAE0337541)                | The inverse of the tangent function.                                                                                                          |
| [Atan2](#ATA5850F146)               | Computes the angle whose tangent is quotient to `x` and `y`.                                                                                  |
| [Distance](#DISED845EB)             | Computes the distance between a pair of one-dimensional points.                                                                               |
| [Distance](#DISC594BAAA)            | Computes the distance between a pair of two-dimensional points.                                                                               |
| [Pow](#POW6B12C656)                 | Computes `x` raised to the power of `y`.                                                                                                      |
| [Pow](#POW912ACCF7)                 | Computes `x` raised to the power of `y`.                                                                                                      |
| [Log](#LOGFC9271CB)                 | Computes the natural logarithm of `x`.                                                                                                        |
| [Log](#LOG46505B5F)                 | Computes the natural logarithm of `x`.                                                                                                        |
| [Log](#LOG5B15651D)                 | Computes the logarithm of `x` with base `b`.                                                                                                  |
| [Log](#LOG2BFB8F5C)                 | Computes the logarithm of `x` with base `b`.                                                                                                  |
| [Sqrt](#SQR996D4597)                | Computes the square root of `x`.                                                                                                              |
| [Sqrt](#SQR4A846713)                | Computes the square root of `x`.                                                                                                              |
| [Factorial](#FAC871EAF8)            | Computes the factorial of `x`.                                                                                                                |
| [Factorial](#FAC6888BE58)           | Computes the factorial of `x`.                                                                                                                |
| [Min](#MIN80FBF4A5)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX5215748B)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLA18B2C116)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABS1CB4F8D7)                 | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIGBB29DCDC)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction](#FRAF6A62D09)            | Compute the fractional (decimal) portion of the number `x`.                                                                                   |
| [Min](#MIN19D5C3C)                  | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX3D06836A)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAF484BE34)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABS1A291AFB)                 | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIG971B9727)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Fraction](#FRAC290B5D9)            | Compute the fractional (decimal) portion of the number `x`.                                                                                   |
| [Min](#MIN183B67DF)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX522369C9)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAF45DDA14)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSB887ECA7)                 | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIG586B56E6)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MIN5446703A)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX6E703F64)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLA58FC0476)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MIND7646754)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX51973356)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLA37C3A8B4)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSE601A6E3)                 | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIG38E0C2F9)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MINA647BDF7)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX7EA54539)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLA248D39D6)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MIN16B10631)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAXC2F8C3BB)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLA4445FDB4)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Abs](#ABSBED4D5AF)                 | Computes the absolute value of `x`.                                                                                                           |
| [Sign](#SIG77555888)                | Returns the sign of `x` as if compared against zero (-1, 0 or +1)                                                                             |
| [Min](#MIN85D4B86C)                 | Returns the minimum value between `x` and `y`.                                                                                                |
| [Max](#MAX3B1057CE)                 | Returns the maximum value between `x` and `y`.                                                                                                |
| [Clamp](#CLAFA076B5E)               | Returns the value `x` clamped to the specified range.                                                                                         |
| [Min](#MINF5193F1)                  |                                                                                                                                               |
| [Max](#MAX819873FB)                 |                                                                                                                                               |
| [Min](#MIN2179CF51)                 |                                                                                                                                               |
| [Max](#MAX864DE05B)                 |                                                                                                                                               |
| [Min<T>](#MIN2BBFC41F)              |                                                                                                                                               |
| [Min<T>](#MIN40586098)              | Finds the comparably minimum value from the set of value.                                                                                     |
| [Max<T>](#MAX29D3D31D)              |                                                                                                                                               |
| [Max<T>](#MAXE9A325EA)              | Finds the comparably maximum value from the set of value.                                                                                     |
| [Floor](#FLO3657C8DD)               | Computes the floor integer (rounding down) of the value `x`.                                                                                  |
| [Ceil](#CEI3335BBF4)                | Computes the ceiling integer (rounding up) of the value `x`.                                                                                  |
| [Round](#ROU922EA54F)               | Computes the nearest integer of the value `x`.                                                                                                |
| [Floor](#FLOB207416)                | Computes the floor integer (rounding down) of the value `x`.                                                                                  |
| [Ceil](#CEI69B78787)                | Computes the ceiling integer (rounding up) of the value `x`.                                                                                  |
| [Round](#ROUD05B43F8)               | Computes the nearest integer of the value `x`.                                                                                                |
| [NearEquals](#NEA95EA6ECD)          | Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                             |
| [NearEquals](#NEA6E2C607C)          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero](#NEA186BBBDE)            | Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                        |
| [NearEquals](#NEAB9BA714D)          | Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                             |
| [NearEquals](#NEAA94E237C)          | Determines if the two values are nearly equal comparing equality within a threshold.                                                          |
| [NearZero](#NEA5D5B8D89)            | Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.                        |
| [Lerp](#LER27CDA8B0)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERB99D5C4E)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERA070D99F)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LERB0760236)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LER161420F8)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |
| [Lerp](#LER5F1F5667)                | Computes the linear interpolation from `x1` to `x2` by factor `t`.                                                                            |

### Fields

#### <a name="RANE1E4B317"></a>Random : Random
<small>`Read Only`</small>

A static instance of the `Heirloom.Math.Calc.Random` for convenience.

#### <a name="PERAEEBD0B8"></a>Perlin : [PerlinNoise](Heirloom.Math.PerlinNoise.md)
<small>`Read Only`</small>

A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.

#### <a name="SIM2F8D31EC"></a>Simplex : [SimplexNoise](Heirloom.Math.SimplexNoise.md)
<small>`Read Only`</small>

A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience.

#### <a name="PIAA83371"></a>Pi : float

An approximation of the constant Pi (180 Degrees or Pi Radians).   
 3.14159265359...

#### <a name="TWO66932461"></a>TwoPi : float

Two times Pi. 360 Degrees in Radians.   
 6.28318530718...

#### <a name="HALD110A7A8"></a>HalfPi : float

Half Pi. 90 Degrees in Radians.   
 0.5 * 3.141592653...

#### <a name="TOR722D3A31"></a>ToRadians : float

Pi / 180.0

#### <a name="TODE6B6954F"></a>ToDegree : float

180.0 / Pi

#### <a name="EPSB247C6F0"></a>Epsilon : float

A small number almost considered zero, greatly differs from `System.Single.Epsilon`.

#### <a name="RANE1E4B317"></a>Random : Random
<small>`Static`, `Read Only`</small>

A static instance of the `Heirloom.Math.Calc.Random` for convenience.

#### <a name="PERAEEBD0B8"></a>Perlin : [PerlinNoise](Heirloom.Math.PerlinNoise.md)
<small>`Static`, `Read Only`</small>

A static instance of the [PerlinNoise](Heirloom.Math.PerlinNoise.md) for convenience.

#### <a name="SIM2F8D31EC"></a>Simplex : [SimplexNoise](Heirloom.Math.SimplexNoise.md)
<small>`Static`, `Read Only`</small>

A static instance of the [SimplexNoise](Heirloom.Math.SimplexNoise.md) for convenience.

#### <a name="PIAA83371"></a>Pi : float
<small>`Static`</small>

An approximation of the constant Pi (180 Degrees or Pi Radians).   
 3.14159265359...

#### <a name="TWO66932461"></a>TwoPi : float
<small>`Static`</small>

Two times Pi. 360 Degrees in Radians.   
 6.28318530718...

#### <a name="HALD110A7A8"></a>HalfPi : float
<small>`Static`</small>

Half Pi. 90 Degrees in Radians.   
 0.5 * 3.141592653...

#### <a name="TOR722D3A31"></a>ToRadians : float
<small>`Static`</small>

Pi / 180.0

#### <a name="TODE6B6954F"></a>ToDegree : float
<small>`Static`</small>

180.0 / Pi

#### <a name="EPSB247C6F0"></a>Epsilon : float
<small>`Static`</small>

A small number almost considered zero, greatly differs from `System.Single.Epsilon`.

### Methods

#### <a name="LER1E32ABA1"></a>Lerp(byte x1,  byte x2, float t) :  byte
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LER4C636FF9"></a>Lerp(in sbyte x1, in sbyte x2, in float t) : sbyte
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="ANG142CA73E"></a>AngleLerp(double x1, double x2, double t) : double
<small>`Static`</small>

Computes the linear interpolation of two angles across the shorter distance.

<small>**x1**: <param name="x1">Start angle.</param></small>  
<small>**x2**: <param name="x2">End angle.</param></small>  
<small>**t**: <param name="t">Interpolation factor</param></small>  

#### <a name="ANGB785B7AC"></a>AngleLerp(float x1, float x2, float t) : float
<small>`Static`</small>

Computes the linear interpolation of two angles across the shorter distance.

<small>**x1**: <param name="x1">Start angle.</param></small>  
<small>**x2**: <param name="x2">End angle.</param></small>  
<small>**t**: <param name="t">Interpolation factor</param></small>  

#### <a name="BET198DC48C"></a>Between(in float x, in float min, in float max) : float
<small>`Static`</small>

Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.


#### <a name="ISBD461C5F6"></a>IsBetween(in float x, in float min, in float max) : bool
<small>`Static`</small>

Computes the interpolation factor (0.0 to 1.0) of `x` between `min` and `max`.


#### <a name="RES24BC2AB3"></a>Rescale(in float x, in float min1, in float max1, in float min2, in float max2) : float
<small>`Static`</small>

Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2`.


#### <a name="RES88F32950"></a>Rescale(in float x, in [Range](Heirloom.Math.Range.md) src, in [Range](Heirloom.Math.Range.md) dst) : float
<small>`Static`</small>

Rescales a value from the source domain a target domain.


#### <a name="COSA5D83C23"></a>CosineInterpolation(float x1, float x2, float t) : float
<small>`Static`</small>

Computes a cosine based interpolation from `x1` to `x2`.


#### <a name="SMO4183BCC1"></a>SmoothStep(float min, float max, float x) : float
<small>`Static`</small>

Computes the smooth-step of `x` between `min` and `max`.


#### <a name="SMOF6A04A40"></a>SmootherStep(float min, float max, float x) : float
<small>`Static`</small>

Computes the smoother smooth-step of `x` between `min` and `max`.


#### <a name="NEA66788BDF"></a>NearestPowerOfTwo(uint x) : uint
<small>`Static`</small>

Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="NEA89ECBB2F"></a>NearestPowerOfTwo(int x) : int
<small>`Static`</small>

Computes the nearest power of 2 to a number. This is done by computing both lower and upper power of 2, and then comparing the distance to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="LOW9781B988"></a>LowerPowerOfTwo(uint x) : uint
<small>`Static`</small>

Computes the lower power of 2 nearest to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="LOWEBAC5A48"></a>LowerPowerOfTwo(int x) : int
<small>`Static`</small>

Computes the lower power of 2 nearest to x.

<small>**x**: <param name="x"> Some unsigned integer </param></small>  

#### <a name="UPP6E59B0D1"></a>UpperPowerOfTwo(uint v) : uint
<small>`Static`</small>

Computes the upper power of 2 nearest to x.


#### <a name="UPPE7C7FFD5"></a>UpperPowerOfTwo(int v) : int
<small>`Static`</small>

Computes the upper power of 2 nearest to x.


#### <a name="ISP9095B2A2"></a>IsPowerOfTwo(ulong x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="ISPF1B2047"></a>IsPowerOfTwo(uint x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="ISP19811CD6"></a>IsPowerOfTwo(int x) : bool
<small>`Static`</small>

Determines if the given integer is a power of 2.


#### <a name="SWAFD4B5953"></a>Swap<T>(ref T a, ref T b) : void
<small>`Static`</small>

Swaps two references.


#### <a name="SWA30647658"></a>Swap<T>(IList\<T> list, int a, int b) : void
<small>`Static`</small>

Swaps two positions within the given list.


#### <a name="ORDE448E4D8"></a>Order<T>(ref T a, ref T b) : void
<small>`Static`</small>

Orders the two given references so they are in comparable order.


#### <a name="WRABCD6B4CE"></a>Wrap(int x, int n) : int
<small>`Static`</small>

Wraps (loops) a number within a zero to n range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**n**: <param name="n">Some upper bound from zero.</param></small>  

#### <a name="WRAD3CAA971"></a>Wrap(int x, int min, int max) : int
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**min**: <param name="min">Some lower bound.</param></small>  
<small>**max**: <param name="max">Some upper bound.</param></small>  

#### <a name="WRAB05E993"></a>Wrap(int x, [IntRange](Heirloom.Math.IntRange.md) range) : int
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  

#### <a name="WRA8FAF8DA9"></a>Wrap(float x, float n) : float
<small>`Static`</small>

Wraps (loops) a number within a zero to n range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**n**: <param name="n">Some upper bound from zero.</param></small>  

#### <a name="WRAF3544011"></a>Wrap(float x, float min, float max) : float
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  
<small>**min**: <param name="min">Some lower bound.</param></small>  
<small>**max**: <param name="max">Some upper bound.</param></small>  

#### <a name="WRA1179C81F"></a>Wrap(float x, [Range](Heirloom.Math.Range.md) range) : float
<small>`Static`</small>

Wraps (loops) a number within a range.

<small>**x**: <param name="x">Some value to wrap.</param></small>  

#### <a name="DEA59C907BB"></a>DeadZone(float x, float center, float spread) : float
<small>`Static`</small>

Returns `center` if `x` is within `spread` units of `center` otherwise `x`.


#### <a name="OSCC703CDB2"></a>Osc(float x) : float
<small>`Static`</small>

The function sine mapped to [0.0, 1.0].


#### <a name="SIN7B139DD3"></a>Sin(float x) : float
<small>`Static`</small>

The function sine.


#### <a name="ASIB4E9E3D6"></a>Asin(float x) : float
<small>`Static`</small>

Ihe inverse of the function sine.


#### <a name="COS637C3FC2"></a>Cos(float x) : float
<small>`Static`</small>

The function cosine.


#### <a name="ACO3E5E8CB1"></a>Acos(float x) : float
<small>`Static`</small>

The inverse of the function cosine.


#### <a name="TAN2AE1A1A2"></a>Tan(float x) : float
<small>`Static`</small>

The tangent function.


#### <a name="ATAE0337541"></a>Atan(float x) : float
<small>`Static`</small>

The inverse of the tangent function.


#### <a name="ATA5850F146"></a>Atan2(float y, float x) : float
<small>`Static`</small>

Computes the angle whose tangent is quotient to `x` and `y`.


#### <a name="DISED845EB"></a>Distance(float x1, float x2) : float
<small>`Static`</small>

Computes the distance between a pair of one-dimensional points.


#### <a name="DISC594BAAA"></a>Distance(float x1, float y1, float x2, float y2) : float
<small>`Static`</small>

Computes the distance between a pair of two-dimensional points.


#### <a name="POW6B12C656"></a>Pow(float x, float y) : float
<small>`Static`</small>

Computes `x` raised to the power of `y`.


#### <a name="POW912ACCF7"></a>Pow(double x, double y) : double
<small>`Static`</small>

Computes `x` raised to the power of `y`.


#### <a name="LOGFC9271CB"></a>Log(float x) : float
<small>`Static`</small>

Computes the natural logarithm of `x`.


#### <a name="LOG46505B5F"></a>Log(double x) : double
<small>`Static`</small>

Computes the natural logarithm of `x`.


#### <a name="LOG5B15651D"></a>Log(float x, float b) : float
<small>`Static`</small>

Computes the logarithm of `x` with base `b`.


#### <a name="LOG2BFB8F5C"></a>Log(double x, double b) : double
<small>`Static`</small>

Computes the logarithm of `x` with base `b`.


#### <a name="SQR996D4597"></a>Sqrt(float x) : float
<small>`Static`</small>

Computes the square root of `x`.


#### <a name="SQR4A846713"></a>Sqrt(double x) : double
<small>`Static`</small>

Computes the square root of `x`.


#### <a name="FAC871EAF8"></a>Factorial(int x) : int
<small>`Static`</small>

Computes the factorial of `x`.


#### <a name="FAC6888BE58"></a>Factorial(uint x) : uint
<small>`Static`</small>

Computes the factorial of `x`.


#### <a name="MIN80FBF4A5"></a>Min(double x, double y) : double
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX5215748B"></a>Max(double x, double y) : double
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLA18B2C116"></a>Clamp(double x, double min, double max) : double
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS1CB4F8D7"></a>Abs(double x) : double
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIGBB29DCDC"></a>Sign(double x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="FRAF6A62D09"></a>Fraction(double x) : double
<small>`Static`</small>

Compute the fractional (decimal) portion of the number `x`.


#### <a name="MIN19D5C3C"></a>Min(float x, float y) : float
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX3D06836A"></a>Max(float x, float y) : float
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAF484BE34"></a>Clamp(float x, float min, float max) : float
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABS1A291AFB"></a>Abs(float x) : float
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIG971B9727"></a>Sign(float x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="FRAC290B5D9"></a>Fraction(float x) : float
<small>`Static`</small>

Compute the fractional (decimal) portion of the number `x`.


#### <a name="MIN183B67DF"></a>Min(int x, int y) : int
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX522369C9"></a>Max(int x, int y) : int
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAF45DDA14"></a>Clamp(int x, int min, int max) : int
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABSB887ECA7"></a>Abs(int x) : int
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIG586B56E6"></a>Sign(int x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MIN5446703A"></a>Min(uint x, uint y) : uint
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX6E703F64"></a>Max(uint x, uint y) : uint
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLA58FC0476"></a>Clamp(uint x, uint min, uint max) : uint
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MIND7646754"></a>Min(short x, short y) : short
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX51973356"></a>Max(short x, short y) : short
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLA37C3A8B4"></a>Clamp(short x, short min, short max) : short
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABSE601A6E3"></a>Abs(short x) : short
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIG38E0C2F9"></a>Sign(short x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MINA647BDF7"></a>Min(ushort x, ushort y) : ushort
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX7EA54539"></a>Max(ushort x, ushort y) : ushort
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLA248D39D6"></a>Clamp(ushort x, ushort min, ushort max) : ushort
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MIN16B10631"></a>Min(sbyte x, sbyte y) : sbyte
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAXC2F8C3BB"></a>Max(sbyte x, sbyte y) : sbyte
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLA4445FDB4"></a>Clamp(sbyte x, sbyte min, sbyte max) : sbyte
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="ABSBED4D5AF"></a>Abs(sbyte x) : sbyte
<small>`Static`</small>

Computes the absolute value of `x`.


#### <a name="SIG77555888"></a>Sign(sbyte x) : int
<small>`Static`</small>

Returns the sign of `x` as if compared against zero (-1, 0 or +1)


#### <a name="MIN85D4B86C"></a>Min(byte x,  byte y) :  byte
<small>`Static`</small>

Returns the minimum value between `x` and `y`.


#### <a name="MAX3B1057CE"></a>Max(byte x,  byte y) :  byte
<small>`Static`</small>

Returns the maximum value between `x` and `y`.


#### <a name="CLAFA076B5E"></a>Clamp(byte x,  byte min,  byte max) :  byte
<small>`Static`</small>

Returns the value `x` clamped to the specified range.


#### <a name="MINF5193F1"></a>Min(params int vals) : int
<small>`Static`</small>


#### <a name="MAX819873FB"></a>Max(params int vals) : int
<small>`Static`</small>


#### <a name="MIN2179CF51"></a>Min(params float vals) : float
<small>`Static`</small>


#### <a name="MAX864DE05B"></a>Max(params float vals) : float
<small>`Static`</small>


#### <a name="MIN2BBFC41F"></a>Min<T>(params T vals) : T
<small>`Static`</small>


#### <a name="MIN40586098"></a>Min<T>(IList\<T> vals) : T
<small>`Static`</small>

Finds the comparably minimum value from the set of value.


#### <a name="MAX29D3D31D"></a>Max<T>(params T vals) : T
<small>`Static`</small>


#### <a name="MAXE9A325EA"></a>Max<T>(IList\<T> vals) : T
<small>`Static`</small>

Finds the comparably maximum value from the set of value.


#### <a name="FLO3657C8DD"></a>Floor(double x) : int
<small>`Static`</small>

Computes the floor integer (rounding down) of the value `x`.


#### <a name="CEI3335BBF4"></a>Ceil(double x) : int
<small>`Static`</small>

Computes the ceiling integer (rounding up) of the value `x`.


#### <a name="ROU922EA54F"></a>Round(double x) : int
<small>`Static`</small>

Computes the nearest integer of the value `x`.


#### <a name="FLOB207416"></a>Floor(float x) : int
<small>`Static`</small>

Computes the floor integer (rounding down) of the value `x`.


#### <a name="CEI69B78787"></a>Ceil(float x) : int
<small>`Static`</small>

Computes the ceiling integer (rounding up) of the value `x`.


#### <a name="ROUD05B43F8"></a>Round(float x) : int
<small>`Static`</small>

Computes the nearest integer of the value `x`.


#### <a name="NEA95EA6ECD"></a>NearEquals(double x1, double x2) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEA6E2C607C"></a>NearEquals(double x1, double x2, float threshold) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a threshold.


#### <a name="NEA186BBBDE"></a>NearZero(double v) : bool
<small>`Static`</small>

Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEAB9BA714D"></a>NearEquals(float x1, float x2) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="NEAA94E237C"></a>NearEquals(float x1, float x2, float threshold) : bool
<small>`Static`</small>

Determines if the two values are nearly equal comparing equality within a threshold.


#### <a name="NEA5D5B8D89"></a>NearZero(float v) : bool
<small>`Static`</small>

Determines if the value is nearly equal to zero by comparing equality within a `Heirloom.Math.Calc.Epsilon` threshold.


#### <a name="LER27CDA8B0"></a>Lerp(in float x1, in float x2, in float t) : float
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERB99D5C4E"></a>Lerp(in double x1, in double x2, in double t) : double
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERA070D99F"></a>Lerp(in int x1, in int x2, in float t) : int
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LERB0760236"></a>Lerp(in uint x1, in uint x2, in float t) : uint
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LER161420F8"></a>Lerp(in short x1, in short x2, in float t) : short
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


#### <a name="LER5F1F5667"></a>Lerp(in ushort x1, in ushort x2, in float t) : ushort
<small>`Static`</small>

Computes the linear interpolation from `x1` to `x2` by factor `t`.


