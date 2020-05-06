# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Range (Struct)

> **Namespace**: [Heirloom][0]

Represents a range of single-precision floating point numbers from [Min][1] to [Max][2] .

```cs
public struct Range : IEquatable<Range>
```

### Inherits

IEquatable\<Range>

### Fields

[Max][2], [Min][1]

### Properties

[Average][3], [IsValid][4], [Size][5]

### Methods

[Contains][6], [Deconstruct][7], [Equals][8], [GetHashCode][9], [Include][10], [Overlaps][11], [Rescale][12], [Set][13], [ToString][14]

### Static Fields

[Indeterminate][15], [Infinite][16], [Zero][17]

## Fields

#### Instance

| Name     | Type    | Summary                         |
|----------|---------|---------------------------------|
| [Max][2] | `float` | The maximum value in the range. |
| [Min][1] | `float` | The minimum value in the range. |

#### Static

| Name                | Type        | Summary                                                                |
|---------------------|-------------|------------------------------------------------------------------------|
| [Indeterminate][15] | [Range][18] | Range from float.PositiveInfinity to float.NegativeInfinity useful ... |
| [Infinite][16]      | [Range][18] | Range from float.NegativeInfinity to float.PositiveInfinity (the wi... |
| [Zero][17]          | [Range][18] | Zero width range centered on zero.                                     |

## Properties

#### Instance

| Name         | Type    | Summary                                                                |
|--------------|---------|------------------------------------------------------------------------|
| [Average][3] | `float` | Gets the mean of Min and Max .                                         |
| [IsValid][4] | `bool`  | Gets a value that determines if the range is valid (ie, Max &gt;= M... |
| [Size][5]    | `float` | Gets the size of the range.                                            |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                          |
|---------------------------------|-------------|------------------------------------------------------------------|
| [Contains(in float)][6]         | `bool`      | Determines if this range contains the specified value.           |
| [Deconstruct(out float,...][7]  | `void`      |                                                                  |
| [Equals(object)][8]             | `bool`      |                                                                  |
| [Equals(Range)][8]              | `bool`      |                                                                  |
| [GetHashCode()][9]              | `int`       |                                                                  |
| [Include(in float)][10]         | `void`      | Mutate this range (by expansion) to include the specified value. |
| [Include(in Range)][10]         | `void`      | Mutate this range (by expansion) to include the specified range. |
| [Overlaps(in Range)][11]        | `bool`      | Determines if this range overlaps another range.                 |
| [Rescale(in float, in f...][12] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Rescale(in float, in R...][12] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Set(float, float)][13]         | `void`      | Sets the components of this range.                               |
| [ToString()][14]                | `string`    |                                                                  |

[0]: ../../Heirloom.Core.md
[1]: Range/Min.md
[2]: Range/Max.md
[3]: Range/Average.md
[4]: Range/IsValid.md
[5]: Range/Size.md
[6]: Range/Contains.md
[7]: Range/Deconstruct.md
[8]: Range/Equals.md
[9]: Range/GetHashCode.md
[10]: Range/Include.md
[11]: Range/Overlaps.md
[12]: Range/Rescale.md
[13]: Range/Set.md
[14]: Range/ToString.md
[15]: Range/Indeterminate.md
[16]: Range/Infinite.md
[17]: Range/Zero.md
[18]: Range.md
