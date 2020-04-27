# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IntRange

> **Namespace**: [Heirloom][0]  

Represents a range of integers from [Min][1] to [Max][2] .

```cs
public struct IntRange : IEquatable<IntRange>, IEnumerable<int>, IEnumerable
```

### Inherits

IEquatable\<IntRange>, IEnumerable\<int>, IEnumerable

#### Fields

[Min][1], [Max][2]

#### Properties

[Average][3], [Size][4], [IsValid][5], [Item][6]

#### Methods

[Set][7], [Contains][8], [Overlaps][9], [Include][10], [Deconstruct][11], [GetEnumerator][12]

#### Static Fields

[Huge][13], [Indeterminate][14], [Zero][15]

## Fields

| Name                | Summary                                                                                          |
|---------------------|--------------------------------------------------------------------------------------------------|
| [Min][1]            | The minimum value in the range.                                                                  |
| [Max][2]            | The maximum value in the range.                                                                  |
| [Huge][13]          | Range from `int.MinValue` to `int.MaxValue` (the widest possible range).                         |
| [Indeterminate][14] | Range from `int.MaxValue` to `int.MinValue` useful for an indeterminate range to compute bounds. |
| [Zero][15]          | Zero width range centered on zero.                                                               |

## Properties

| Name         | Summary                                                                                                                                                    |
|--------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Average][3] | Gets the mean of [Min][1] and [Max][2] .                                                                                                                   |
| [Size][4]    | Gets the size of the range.                                                                                                                                |
| [IsValid][5] | Gets a value that determines if the range is valid (ie, \<c> \<see cref="F:Heirloom.IntRange.Max" /> &gt;= \<see cref="F:Heirloom.IntRange.Min" />\</c> ). |
| [Item][6]    |                                                                                                                                                            |

## Methods

| Name                | Summary                                                          |
|---------------------|------------------------------------------------------------------|
| [Set][7]            | Sets the components of this range.                               |
| [Contains][8]       | Determines if this range contains the specified value.           |
| [Overlaps][9]       | Determines if this range overlaps another integer range.         |
| [Include][10]       | Mutate this range (by expansion) to include the specified value. |
| [Include][10]       | Mutate this range (by expansion) to include the specified range. |
| [Deconstruct][11]   |                                                                  |
| [GetEnumerator][12] |                                                                  |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IntRange.Min.md
[2]: Heirloom.IntRange.Max.md
[3]: Heirloom.IntRange.Average.md
[4]: Heirloom.IntRange.Size.md
[5]: Heirloom.IntRange.IsValid.md
[6]: Heirloom.IntRange.Item.md
[7]: Heirloom.IntRange.Set.md
[8]: Heirloom.IntRange.Contains.md
[9]: Heirloom.IntRange.Overlaps.md
[10]: Heirloom.IntRange.Include.md
[11]: Heirloom.IntRange.Deconstruct.md
[12]: Heirloom.IntRange.GetEnumerator.md
[13]: Heirloom.IntRange.Huge.md
[14]: Heirloom.IntRange.Indeterminate.md
[15]: Heirloom.IntRange.Zero.md
