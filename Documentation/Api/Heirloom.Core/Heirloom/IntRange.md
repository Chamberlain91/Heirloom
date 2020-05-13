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

[Contains][6], [Deconstruct][7], [Equals][8], [GetEnumerator][9], [GetHashCode][10], [Include][11], [Intersect][12], [Overlaps][13], [Set][14], [ToString][15], [Union][16]

### Static Fields

[Huge][17], [Indeterminate][18], [Zero][19]

### Static Methods

[FromValues][20]

## Fields

#### Instance

| Name     | Type  | Summary                         |
|----------|-------|---------------------------------|
| [Max][2] | `int` | The maximum value in the range. |
| [Min][1] | `int` | The minimum value in the range. |

#### Static

| Name                | Type           | Summary                                                                |
|---------------------|----------------|------------------------------------------------------------------------|
| [Huge][17]          | [IntRange][21] | Range from int.MinValue to int.MaxValue (the widest possible range).   |
| [Indeterminate][18] | [IntRange][21] | Range from int.MaxValue to int.MinValue useful for an indeterminate... |
| [Zero][19]          | [IntRange][21] | Zero width range centered on zero.                                     |

## Properties

#### Instance

| Name         | Type   | Summary                                                                |
|--------------|--------|------------------------------------------------------------------------|
| [Average][3] | `int`  | Gets the mean of Min and Max .                                         |
| [IsValid][4] | `bool` | Gets a value that determines if the range is valid (ie, Max &gt;= M... |
| [Size][5]    | `int`  | Gets the size of the range.                                            |

## Methods

#### Instance

| Name                           | Return Type        | Summary                                                                |
|--------------------------------|--------------------|------------------------------------------------------------------------|
| [Contains(in int)][6]          | `bool`             | Determines if this range contains the specified value.                 |
| [Deconstruct(out int, o...][7] | `void`             | Deconstructs this IntRange into consituent parts.                      |
| [Equals(object)][8]            | `bool`             | Compares this range for equality with another object.                  |
| [Equals(IntRange)][8]          | `bool`             | Compares this range for equality with another range.                   |
| [GetEnumerator()][9]           | `IEnumerator<int>` | Returns an enumerator that will iterate over all integer values fro... |
| [GetHashCode()][10]            | `int`              | Returns the hash code for this range.                                  |
| [Include(int)][11]             | `void`             | Mutate this range (by expansion) to include the specified value.       |
| [Include(IntRange)][11]        | `void`             | Mutate this range (by expansion) to include the specified range.       |
| [Intersect(in IntRange)][12]   | [IntRange][21]     | Computes the intersection of this range with another.                  |
| [Overlaps(in IntRange)][13]    | `bool`             | Determines if this range overlaps another integer range.               |
| [Set(int, int)][14]            | `void`             | Sets the components of this range.                                     |
| [ToString()][15]               | `string`           | Returns the string representation of this IntRange .                   |
| [Union(in IntRange)][16]       | [IntRange][21]     | Computes the union of this range with another.                         |

#### Static

| Name                            | Return Type    | Summary                                                                |
|---------------------------------|----------------|------------------------------------------------------------------------|
| [FromValues(IEnumerable...][20] | [IntRange][21] | Creates an instance of IntRange from the extreme values of the give... |

## Operators

| Name                            | Return Type            | Summary                             |
|---------------------------------|------------------------|-------------------------------------|
| [Equality(IntRange, Int...][22] | `bool`                 | Compares two ranges for equality.   |
| [Explicit(IntVector)][23]       | [IntRange][21]         |                                     |
| [Explicit(IntRange)][23]        | [IntVector][24]        |                                     |
| [Implicit(IntRange)][25]        | [Range][26]            |                                     |
| [Implicit(IntRange)][25]        | `ValueTuple<int, int>` |                                     |
| [Implicit(ValueTuple<in...][25] | [IntRange][21]         |                                     |
| [Inequality(IntRange, I...][27] | `bool`                 | Compares two ranges for inequality. |

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
[12]: IntRange/Intersect.md
[13]: IntRange/Overlaps.md
[14]: IntRange/Set.md
[15]: IntRange/ToString.md
[16]: IntRange/Union.md
[17]: IntRange/Huge.md
[18]: IntRange/Indeterminate.md
[19]: IntRange/Zero.md
[20]: IntRange/FromValues.md
[21]: IntRange.md
[22]: IntRange/op_Equality.md
[23]: IntRange/op_Explicit.md
[24]: IntVector.md
[25]: IntRange/op_Implicit.md
[26]: Range.md
[27]: IntRange/op_Inequality.md
