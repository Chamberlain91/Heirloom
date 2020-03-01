# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Range (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Range></small>  

Represents a range of single-precision floating point numbers from `Heirloom.Math.Range.Min` to `Heirloom.Math.Range.Max`.

| Fields                     | Summary                                                                                                                              |
|----------------------------|--------------------------------------------------------------------------------------------------------------------------------------|
| [Min](#MINBF9E)            | The minimum value in the range.                                                                                                      |
| [Max](#MAXD4DA)            | The maximum value in the range.                                                                                                      |
| [Infinite](#INFIDABE)      | Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).                         |
| [Indeterminate](#INDE4A5E) | Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds. |
| [Zero](#ZEROC7D5)          | Zero width range centered on zero.                                                                                                   |

| Properties           | Summary                                                                                                                                                       |
|----------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average](#AVER2099) | Gets the mean of `Heirloom.Math.Range.Min` and `Heirloom.Math.Range.Max`.                                                                                     |
| [Size](#SIZE9C93)    | Gets the size of the range.                                                                                                                                   |
| [IsValid](#ISVAE38F) | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.Range.Max" /> &gt;= \<see cref="F:Heirloom.Math.Range.Min" />\</c>). |

| Methods                  | Summary                                                          |
|--------------------------|------------------------------------------------------------------|
| [Contains](#CONTD0AE)    | Determines if this range contains the specified value.           |
| [Overlaps](#OVER7F2D)    | Determines if this range overlaps another range.                 |
| [Include](#INCL2EBA)     | Mutate this range (by expansion) to include the specified value. |
| [Include](#INCL2EBA)     | Mutate this range (by expansion) to include the specified range. |
| [Rescale](#RESC8C92)     | Scales `x` from input domain (this range) to output range.       |
| [Rescale](#RESC8C92)     | Scales `x` from input domain (this range) to output range.       |
| [Deconstruct](#DECOC188) |                                                                  |

### Fields

#### <a name="MINBF9E"></a> Min : float

The minimum value in the range.

#### <a name="MAXD4DA"></a> Max : float

The maximum value in the range.

#### <a name="INFIDABE"></a> Infinite : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).

#### <a name="INDE4A5E"></a> Indeterminate : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds.

#### <a name="ZEROC7D5"></a> Zero : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Zero width range centered on zero.

#### <a name="INFIDABE"></a> Infinite : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).

#### <a name="INDE4A5E"></a> Indeterminate : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds.

#### <a name="ZEROC7D5"></a> Zero : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Zero width range centered on zero.

### Constructors

#### Range(float min, float max)

### Properties

#### <a name="AVER2099"></a> Average : float

<small>`Read Only`</small>

Gets the mean of `Heirloom.Math.Range.Min` and `Heirloom.Math.Range.Max`.

#### <a name="SIZE9C93"></a> Size : float

<small>`Read Only`</small>

Gets the size of the range.

#### <a name="ISVAE38F"></a> IsValid : bool

<small>`Read Only`</small>

Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.Range.Max" /> &gt;= \<see cref="F:Heirloom.Math.Range.Min" />\</c>).

### Methods

#### <a name="CONT86E7"></a> Contains(in float x) : bool

Determines if this range contains the specified value.


#### <a name="OVER16A0"></a> Overlaps(in [Range](Heirloom.Math.Range.md) other) : bool

Determines if this range overlaps another range.


#### <a name="INCL1CBF"></a> Include(in float val) : void

Mutate this range (by expansion) to include the specified value.


#### <a name="INCL5B67"></a> Include(in [Range](Heirloom.Math.Range.md) range) : void

Mutate this range (by expansion) to include the specified range.


#### <a name="RESC23D7"></a> Rescale(in float x, in float outMin, in float outMax) : float

Scales `x` from input domain (this range) to output range.


#### <a name="RESCF696"></a> Rescale(in float x, in [Range](Heirloom.Math.Range.md) outRange) : float

Scales `x` from input domain (this range) to output range.


#### <a name="DECOBB63"></a> Deconstruct(out float min, out float max) : void


