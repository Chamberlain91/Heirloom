# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Range Struct

> **Namespace**: [Heirloom][0]  

Represents a range of single-precision floating point numbers from [Min][1] to [Max][2] .

```cs
public struct Range : IEquatable<Range>
```

### Inherits

IEquatable\<Range>

#### Fields

[Min][1], [Max][2]

#### Properties

[Average][3], [Size][4], [IsValid][5], [Indexer][6]

#### Methods

[Set][7], [Contains][8], [Overlaps][9], [Include][10], [Rescale][11], [Deconstruct][12]

#### Static Fields

[Infinite][13], [Indeterminate][14], [Zero][15]

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
| [Indexer][6] |                                                                                                                                                      |

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

[0]: ../../Heirloom.Core.md
[1]: Range/Min.md
[2]: Range/Max.md
[3]: Range/Average.md
[4]: Range/Size.md
[5]: Range/IsValid.md
[6]: Range/Indexer.md
[7]: Range/Set.md
[8]: Range/Contains.md
[9]: Range/Overlaps.md
[10]: Range/Include.md
[11]: Range/Rescale.md
[12]: Range/Deconstruct.md
[13]: Range/Infinite.md
[14]: Range/Indeterminate.md
[15]: Range/Zero.md
