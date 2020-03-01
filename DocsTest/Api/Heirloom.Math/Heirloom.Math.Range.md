# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Range (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Range></small>  

Represents a range of single-precision floating point numbers from `Heirloom.Math.Range.Min` to `Heirloom.Math.Range.Max`.

| Fields                        | Summary                                                                                                                              |
|-------------------------------|--------------------------------------------------------------------------------------------------------------------------------------|
| [Min](#MINBF9EF002)           | The minimum value in the range.                                                                                                      |
| [Max](#MAXD4DA94E4)           | The maximum value in the range.                                                                                                      |
| [Infinite](#INFDABEDF6)       | Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).                         |
| [Indeterminate](#IND4A5E782F) | Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds. |
| [Zero](#ZERC7D5C0B8)          | Zero width range centered on zero.                                                                                                   |

| Properties             | Summary                                                                                                                                                       |
|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average](#AVE2099683) | Gets the mean of `Heirloom.Math.Range.Min` and `Heirloom.Math.Range.Max`.                                                                                     |
| [Size](#SIZ9C9392F9)   | Gets the size of the range.                                                                                                                                   |
| [IsValid](#ISVE38FCA8) | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.Range.Max" /> &gt;= \<see cref="F:Heirloom.Math.Range.Min" />\</c>). |

| Methods                     | Summary                                                          |
|-----------------------------|------------------------------------------------------------------|
| [Contains](#COND0AE797B)    | Determines if this range contains the specified value.           |
| [Overlaps](#OVE7F2D7C32)    | Determines if this range overlaps another range.                 |
| [Include](#INC2EBA9B2E)     | Mutate this range (by expansion) to include the specified value. |
| [Include](#INC2EBA9B2E)     | Mutate this range (by expansion) to include the specified range. |
| [Rescale](#RES8C920C4B)     | Scales `x` from input domain (this range) to output range.       |
| [Rescale](#RES8C920C4B)     | Scales `x` from input domain (this range) to output range.       |
| [Deconstruct](#DECC1884FDA) |                                                                  |

### Fields

#### <a name="MINBF9EF002"></a>Min : float

The minimum value in the range.

#### <a name="MAXD4DA94E4"></a>Max : float

The maximum value in the range.

#### <a name="INFDABEDF6"></a>Infinite : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).

#### <a name="IND4A5E782F"></a>Indeterminate : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds.

#### <a name="ZERC7D5C0B8"></a>Zero : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

Zero width range centered on zero.

#### <a name="INFDABEDF6"></a>Infinite : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Range from `System.Single.NegativeInfinity` to `System.Single.PositiveInfinity` (the widest possible range).

#### <a name="IND4A5E782F"></a>Indeterminate : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Range from `System.Single.PositiveInfinity` to `System.Single.NegativeInfinity` useful for an indeterminate range to compute bounds.

#### <a name="ZERC7D5C0B8"></a>Zero : [Range](Heirloom.Math.Range.md)
<small>`Static`, `Read Only`</small>

Zero width range centered on zero.

### Constructors

#### Range(float min, float max)

### Properties

#### <a name="AVE2099683"></a>Average : float

<small>`Read Only`</small>

Gets the mean of `Heirloom.Math.Range.Min` and `Heirloom.Math.Range.Max`.

#### <a name="SIZ9C9392F9"></a>Size : float

<small>`Read Only`</small>

Gets the size of the range.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.Range.Max" /> &gt;= \<see cref="F:Heirloom.Math.Range.Min" />\</c>).

### Methods

#### <a name="CON86E7517D"></a>Contains(in float x) : bool

Determines if this range contains the specified value.


#### <a name="OVE16A02ECD"></a>Overlaps(in [Range](Heirloom.Math.Range.md) other) : bool

Determines if this range overlaps another range.


#### <a name="INC1CBF6203"></a>Include(in float val) : void

Mutate this range (by expansion) to include the specified value.


#### <a name="INC5B673B0E"></a>Include(in [Range](Heirloom.Math.Range.md) range) : void

Mutate this range (by expansion) to include the specified range.


#### <a name="RES23D760E5"></a>Rescale(in float x, in float outMin, in float outMax) : float

Scales `x` from input domain (this range) to output range.


#### <a name="RESF69634AC"></a>Rescale(in float x, in [Range](Heirloom.Math.Range.md) outRange) : float

Scales `x` from input domain (this range) to output range.


#### <a name="DECBB6397C9"></a>Deconstruct(out float min, out float max) : void


