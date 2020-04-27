# Range

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a range of single-precision floating point numbers from [Min][1] to [Max][2] .

```cs
public struct Range : IEquatable<Range>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<Range>

**Fields**: [Min][1], [Max][2]

**Properties**: [Average][3], [Size][4], [IsValid][5], [Item][6]

**Methods**: [Set][7], [Contains][8], [Overlaps][9], [Include][10], [Rescale][11], [Deconstruct][12]

**Static Fields**: [Infinite][13], [Indeterminate][14], [Zero][15]

--------------------------------------------------------------------------------

## Constructors

### Range(float, float)

```cs
public Range(float min, float max)
```

## Fields

| Name                | Summary                                                                                                              |
|---------------------|----------------------------------------------------------------------------------------------------------------------|
| [Min][1]            | The minimum value in the range.                                                                                      |
| [Max][2]            | The maximum value in the range.                                                                                      |
| [Infinite][13]      | Range from `float.NegativeInfinity` to `float.PositiveInfinity` (the widest possible range).                         |
| [Indeterminate][14] | Range from `float.PositiveInfinity` to `float.NegativeInfinity` useful for an indeterminate range to compute bounds. |
| [Zero][15]          | Zero width range centered on zero.                                                                                   |

## Properties

| Name         | Summary                                                                                                                                              |
|--------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average][3] | Gets the mean of [Min][1] and [Max][2] .                                                                                                             |
| [Size][4]    | Gets the size of the range.                                                                                                                          |
| [IsValid][5] | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.Range.Max" /> &gt;= \<see cref="F:Heirloom.Range.Min" />\</c> ). |
| [Item][6]    |                                                                                                                                                      |

## Methods

| Name              | Summary                                                          |
|-------------------|------------------------------------------------------------------|
| [Set][7]          | Sets the components of this range.                               |
| [Contains][8]     | Determines if this range contains the specified value.           |
| [Overlaps][9]     | Determines if this range overlaps another range.                 |
| [Include][10]     | Mutate this range (by expansion) to include the specified value. |
| [Include][10]     | Mutate this range (by expansion) to include the specified range. |
| [Rescale][11]     | Scales `x` from input domain (this range) to output range.       |
| [Rescale][11]     | Scales `x` from input domain (this range) to output range.       |
| [Deconstruct][12] |                                                                  |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Range.Min.md
[2]: Heirloom.Range.Max.md
[3]: Heirloom.Range.Average.md
[4]: Heirloom.Range.Size.md
[5]: Heirloom.Range.IsValid.md
[6]: Heirloom.Range.Item.md
[7]: Heirloom.Range.Set.md
[8]: Heirloom.Range.Contains.md
[9]: Heirloom.Range.Overlaps.md
[10]: Heirloom.Range.Include.md
[11]: Heirloom.Range.Rescale.md
[12]: Heirloom.Range.Deconstruct.md
[13]: Heirloom.Range.Infinite.md
[14]: Heirloom.Range.Indeterminate.md
[15]: Heirloom.Range.Zero.md
