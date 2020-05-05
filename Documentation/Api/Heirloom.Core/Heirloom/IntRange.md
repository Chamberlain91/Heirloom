# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRange (Struct)

> **Namespace**: [Heirloom][0]

Represents a range of integers from [Min][1] to [Max][2] .

```cs
public struct IntRange : IEquatable<IntRange>, IEnumerable<int>, IEnumerable
```

### Inherits

IEquatable\<IntRange>, IEnumerable\<int>, IEnumerable

### Fields

[Max][2], [Min][1]

### Properties

[Average][3], [IsValid][4], [Size][5]

### Methods

[Contains][6], [Deconstruct][7], [GetEnumerator][8], [Include][9], [Overlaps][10], [Set][11]

### Static Fields

[Huge][12], [Indeterminate][13], [Zero][14]

## Fields

#### Instance

| Name     | Type  | Summary                         |
|----------|-------|---------------------------------|
| [Max][2] | `int` | The maximum value in the range. |
| [Min][1] | `int` | The minimum value in the range. |

#### Static

| Name                | Type           | Summary                                                                |
|---------------------|----------------|------------------------------------------------------------------------|
| [Huge][12]          | [IntRange][15] | Range from int.MinValue to int.MaxValue (the widest possible range).   |
| [Indeterminate][13] | [IntRange][15] | Range from int.MaxValue to int.MinValue useful for an indeterminate... |
| [Zero][14]          | [IntRange][15] | Zero width range centered on zero.                                     |

## Properties

#### Instance

| Name         | Type   | Summary                                                                |
|--------------|--------|------------------------------------------------------------------------|
| [Average][3] | `int`  | Gets the mean of Min and Max .                                         |
| [IsValid][4] | `bool` | Gets a value that determines if the range is valid (ie, Max &gt;= M... |
| [Size][5]    | `int`  | Gets the size of the range.                                            |

## Methods

#### Instance

| Name                           | Return Type        | Summary                                                          |
|--------------------------------|--------------------|------------------------------------------------------------------|
| [Contains(in int)][6]          | `bool`             | Determines if this range contains the specified value.           |
| [Deconstruct(out int, o...][7] | `void`             |                                                                  |
| [GetEnumerator()][8]           | `IEnumerator<int>` |                                                                  |
| [Include(int)][9]              | `void`             | Mutate this range (by expansion) to include the specified value. |
| [Include(IntRange)][9]         | `void`             | Mutate this range (by expansion) to include the specified range. |
| [Overlaps(in IntRange)][10]    | `bool`             | Determines if this range overlaps another integer range.         |
| [Set(int, int)][11]            | `void`             | Sets the components of this range.                               |

[0]: ../../Heirloom.Core.md
[1]: IntRange/Min.md
[2]: IntRange/Max.md
[3]: IntRange/Average.md
[4]: IntRange/IsValid.md
[5]: IntRange/Size.md
[6]: IntRange/Contains.md
[7]: IntRange/Deconstruct.md
[8]: IntRange/GetEnumerator.md
[9]: IntRange/Include.md
[10]: IntRange/Overlaps.md
[11]: IntRange/Set.md
[12]: IntRange/Huge.md
[13]: IntRange/Indeterminate.md
[14]: IntRange/Zero.md
[15]: IntRange.md
