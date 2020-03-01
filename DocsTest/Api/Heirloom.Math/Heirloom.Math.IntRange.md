# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntRange (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntRange>, IEnumerable\<int>, IEnumerable</small>  

Represents a range of integers from `Heirloom.Math.IntRange.Min` to `Heirloom.Math.IntRange.Max`.

| Fields                     | Summary                                                                                                            |
|----------------------------|--------------------------------------------------------------------------------------------------------------------|
| [Min](#MINBF9E)            | The minimum value in the range.                                                                                    |
| [Max](#MAXD4DA)            | The maximum value in the range.                                                                                    |
| [Huge](#HUGEA8FF)          | Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).                         |
| [Indeterminate](#INDE4A5E) | Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds. |
| [Zero](#ZEROC7D5)          | Zero width range centered on zero.                                                                                 |

| Properties           | Summary                                                                                                                                                             |
|----------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average](#AVER2099) | Gets the mean of `Heirloom.Math.IntRange.Min` and `Heirloom.Math.IntRange.Max`.                                                                                     |
| [Size](#SIZE9C93)    | Gets the size of the range.                                                                                                                                         |
| [IsValid](#ISVAE38F) | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.IntRange.Max" /> &gt;= \<see cref="F:Heirloom.Math.IntRange.Min" />\</c>). |

| Methods                    | Summary                                                          |
|----------------------------|------------------------------------------------------------------|
| [Contains](#CONTD0AE)      | Determines if this range contains the specified value.           |
| [Overlaps](#OVER7F2D)      | Determines if this range overlaps another integer range.         |
| [Include](#INCL2EBA)       | Mutate this range (by expansion) to include the specified value. |
| [Include](#INCL2EBA)       | Mutate this range (by expansion) to include the specified range. |
| [Deconstruct](#DECOC188)   |                                                                  |
| [GetEnumerator](#GETEF1F9) |                                                                  |

### Fields

#### <a name="MINBF9E"></a> Min : int

The minimum value in the range.

#### <a name="MAXD4DA"></a> Max : int

The maximum value in the range.

#### <a name="HUGEA8FF"></a> Huge : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).

#### <a name="INDE4A5E"></a> Indeterminate : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds.

#### <a name="ZEROC7D5"></a> Zero : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Zero width range centered on zero.

#### <a name="HUGEA8FF"></a> Huge : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).

#### <a name="INDE4A5E"></a> Indeterminate : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds.

#### <a name="ZEROC7D5"></a> Zero : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Zero width range centered on zero.

### Constructors

#### IntRange(int min, int max)

Constructs a new [IntRange](Heirloom.Math.IntRange.md).

### Properties

#### <a name="AVER2099"></a> Average : int

<small>`Read Only`</small>

Gets the mean of `Heirloom.Math.IntRange.Min` and `Heirloom.Math.IntRange.Max`.

#### <a name="SIZE9C93"></a> Size : int

<small>`Read Only`</small>

Gets the size of the range.

#### <a name="ISVAE38F"></a> IsValid : bool

<small>`Read Only`</small>

Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.IntRange.Max" /> &gt;= \<see cref="F:Heirloom.Math.IntRange.Min" />\</c>).

### Methods

#### <a name="CONT6D86"></a> Contains(in int x) : bool

Determines if this range contains the specified value.


#### <a name="OVER878B"></a> Overlaps(in [IntRange](Heirloom.Math.IntRange.md) other) : bool

Determines if this range overlaps another integer range.


#### <a name="INCLF9EE"></a> Include(int val) : void

Mutate this range (by expansion) to include the specified value.


#### <a name="INCL7608"></a> Include([IntRange](Heirloom.Math.IntRange.md) range) : void

Mutate this range (by expansion) to include the specified range.


#### <a name="DECOFF7E"></a> Deconstruct(out int min, out int max) : void


#### <a name="GETE32A4"></a> GetEnumerator() : IEnumerator\<int>
<small>`Virtual`</small>

