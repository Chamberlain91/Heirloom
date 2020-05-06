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

[Contains][6], [Deconstruct][7], [Equals][8], [GetEnumerator][9], [GetHashCode][10], [Include][11], [Overlaps][12], [Set][13], [ToString][14]

### Static Fields

[Huge][15], [Indeterminate][16], [Zero][17]

## Fields

#### Instance

| Name     | Type  | Summary                         |
|----------|-------|---------------------------------|
| [Max][2] | `int` | The maximum value in the range. |
| [Min][1] | `int` | The minimum value in the range. |

#### Static

| Name                | Type           | Summary                                                                |
|---------------------|----------------|------------------------------------------------------------------------|
| [Huge][15]          | [IntRange][18] | Range from int.MinValue to int.MaxValue (the widest possible range).   |
| [Indeterminate][16] | [IntRange][18] | Range from int.MaxValue to int.MinValue useful for an indeterminate... |
| [Zero][17]          | [IntRange][18] | Zero width range centered on zero.                                     |

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
| [Equals(object)][8]            | `bool`             |                                                                  |
| [Equals(IntRange)][8]          | `bool`             |                                                                  |
| [GetEnumerator()][9]           | `IEnumerator<int>` |                                                                  |
| [GetHashCode()][10]            | `int`              |                                                                  |
| [Include(int)][11]             | `void`             | Mutate this range (by expansion) to include the specified value. |
| [Include(IntRange)][11]        | `void`             | Mutate this range (by expansion) to include the specified range. |
| [Overlaps(in IntRange)][12]    | `bool`             | Determines if this range overlaps another integer range.         |
| [Set(int, int)][13]            | `void`             | Sets the components of this range.                               |
| [ToString()][14]               | `string`           |                                                                  |

[0]: ../../Heirloom.Core.md
[1]: IntRange/Min.md
[2]: IntRange/Max.md
[3]: IntRange/Average.md
[4]: IntRange/IsValid.md
[5]: IntRange/Size.md
[6]: IntRange/Contains.md
[7]: IntRange/Deconstruct.md
[8]: IntRange/Equals.md
[9]: IntRange/GetEnumerator.md
[10]: IntRange/GetHashCode.md
[11]: IntRange/Include.md
[12]: IntRange/Overlaps.md
[13]: IntRange/Set.md
[14]: IntRange/ToString.md
[15]: IntRange/Huge.md
[16]: IntRange/Indeterminate.md
[17]: IntRange/Zero.md
[18]: IntRange.md
