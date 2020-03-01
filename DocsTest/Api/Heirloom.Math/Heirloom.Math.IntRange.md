# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntRange (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntRange>, IEnumerable\<int>, IEnumerable</small>  

Represents a range of integers from `Heirloom.Math.IntRange.Min` to `Heirloom.Math.IntRange.Max`.

| Fields                        | Summary                                                                                                            |
|-------------------------------|--------------------------------------------------------------------------------------------------------------------|
| [Min](#MINBF9EF002)           | The minimum value in the range.                                                                                    |
| [Max](#MAXD4DA94E4)           | The maximum value in the range.                                                                                    |
| [Huge](#HUGA8FFCD53)          | Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).                         |
| [Indeterminate](#IND4A5E782F) | Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds. |
| [Zero](#ZERC7D5C0B8)          | Zero width range centered on zero.                                                                                 |

| Properties             | Summary                                                                                                                                                             |
|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average](#AVE2099683) | Gets the mean of `Heirloom.Math.IntRange.Min` and `Heirloom.Math.IntRange.Max`.                                                                                     |
| [Size](#SIZ9C9392F9)   | Gets the size of the range.                                                                                                                                         |
| [IsValid](#ISVE38FCA8) | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.IntRange.Max" /> &gt;= \<see cref="F:Heirloom.Math.IntRange.Min" />\</c>). |

| Methods                       | Summary                                                          |
|-------------------------------|------------------------------------------------------------------|
| [Contains](#COND0AE797B)      | Determines if this range contains the specified value.           |
| [Overlaps](#OVE7F2D7C32)      | Determines if this range overlaps another integer range.         |
| [Include](#INC2EBA9B2E)       | Mutate this range (by expansion) to include the specified value. |
| [Include](#INC2EBA9B2E)       | Mutate this range (by expansion) to include the specified range. |
| [Deconstruct](#DECC1884FDA)   |                                                                  |
| [GetEnumerator](#GETF1F90828) |                                                                  |

### Fields

#### <a name="MINBF9EF002"></a>Min : int

The minimum value in the range.

#### <a name="MAXD4DA94E4"></a>Max : int

The maximum value in the range.

#### <a name="HUGA8FFCD53"></a>Huge : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).

#### <a name="IND4A5E782F"></a>Indeterminate : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntRange](Heirloom.Math.IntRange.md)
<small>`Read Only`</small>

Zero width range centered on zero.

#### <a name="HUGA8FFCD53"></a>Huge : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Range from `System.Int32.MinValue` to `System.Int32.MaxValue` (the widest possible range).

#### <a name="IND4A5E782F"></a>Indeterminate : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Range from `System.Int32.MaxValue` to `System.Int32.MinValue` useful for an indeterminate range to compute bounds.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntRange](Heirloom.Math.IntRange.md)
<small>`Static`, `Read Only`</small>

Zero width range centered on zero.

### Constructors

#### IntRange(int min, int max)

Constructs a new [IntRange](Heirloom.Math.IntRange.md).

### Properties

#### <a name="AVE2099683"></a>Average : int

<small>`Read Only`</small>

Gets the mean of `Heirloom.Math.IntRange.Min` and `Heirloom.Math.IntRange.Max`.

#### <a name="SIZ9C9392F9"></a>Size : int

<small>`Read Only`</small>

Gets the size of the range.

#### <a name="ISVE38FCA8"></a>IsValid : bool

<small>`Read Only`</small>

Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Math.IntRange.Max" /> &gt;= \<see cref="F:Heirloom.Math.IntRange.Min" />\</c>).

### Methods

#### <a name="CON6D86EFB8"></a>Contains(in int x) : bool

Determines if this range contains the specified value.


#### <a name="OVE878BA259"></a>Overlaps(in [IntRange](Heirloom.Math.IntRange.md) other) : bool

Determines if this range overlaps another integer range.


#### <a name="INCF9EE613B"></a>Include(int val) : void

Mutate this range (by expansion) to include the specified value.


#### <a name="INC76080F77"></a>Include([IntRange](Heirloom.Math.IntRange.md) range) : void

Mutate this range (by expansion) to include the specified range.


#### <a name="DECFF7EBA59"></a>Deconstruct(out int min, out int max) : void


#### <a name="GET32A42BED"></a>GetEnumerator() : IEnumerator\<int>
<small>`Virtual`</small>

