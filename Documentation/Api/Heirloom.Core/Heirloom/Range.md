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

[Contains][6], [Deconstruct][7], [Include][8], [Overlaps][9], [Rescale][10], [Set][11]

### Static Fields

[Indeterminate][12], [Infinite][13], [Zero][14]

## Fields

#### Instance

| Name     | Type    | Summary                         |
|----------|---------|---------------------------------|
| [Max][2] | `float` | The maximum value in the range. |
| [Min][1] | `float` | The minimum value in the range. |

#### Static

| Name                | Type        | Summary                                                                |
|---------------------|-------------|------------------------------------------------------------------------|
| [Indeterminate][12] | [Range][15] | Range from float.PositiveInfinity to float.NegativeInfinity useful ... |
| [Infinite][13]      | [Range][15] | Range from float.NegativeInfinity to float.PositiveInfinity (the wi... |
| [Zero][14]          | [Range][15] | Zero width range centered on zero.                                     |

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
| [Include(in float)][8]          | `void`      | Mutate this range (by expansion) to include the specified value. |
| [Include(in Range)][8]          | `void`      | Mutate this range (by expansion) to include the specified range. |
| [Overlaps(in Range)][9]         | `bool`      | Determines if this range overlaps another range.                 |
| [Rescale(in float, in f...][10] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Rescale(in float, in R...][10] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Set(float, float)][11]         | `void`      | Sets the components of this range.                               |

[0]: ../../Heirloom.Core.md
[1]: Range/Min.md
[2]: Range/Max.md
[3]: Range/Average.md
[4]: Range/IsValid.md
[5]: Range/Size.md
[6]: Range/Contains.md
[7]: Range/Deconstruct.md
[8]: Range/Include.md
[9]: Range/Overlaps.md
[10]: Range/Rescale.md
[11]: Range/Set.md
[12]: Range/Indeterminate.md
[13]: Range/Infinite.md
[14]: Range/Zero.md
[15]: Range.md
