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

[Average][3], [Indexer][4], [IsValid][5], [Size][6]

### Methods

[Contains][7], [Deconstruct][8], [Include][9], [Overlaps][10], [Rescale][11], [Set][12]

### Static Fields

[Indeterminate][13], [Infinite][14], [Zero][15]

## Fields

#### Instance

| Name     | Type    | Summary                         |
|----------|---------|---------------------------------|
| [Max][2] | `float` | The maximum value in the range. |
| [Min][1] | `float` | The minimum value in the range. |

#### Static

| Name                | Type        | Summary                                                                |
|---------------------|-------------|------------------------------------------------------------------------|
| [Indeterminate][13] | [Range][16] | Range from float.PositiveInfinity to float.NegativeInfinity useful ... |
| [Infinite][14]      | [Range][16] | Range from float.NegativeInfinity to float.PositiveInfinity (the wi... |
| [Zero][15]          | [Range][16] | Zero width range centered on zero.                                     |

## Properties

#### Instance

| Name         | Type    | Summary                                                                |
|--------------|---------|------------------------------------------------------------------------|
| [Average][3] | `float` | Gets the mean of Min and Max .                                         |
| [Indexer][4] | `float` |                                                                        |
| [IsValid][5] | `bool`  | Gets a value that determines if the range is valid (ie, Max &gt;= M... |
| [Size][6]    | `float` | Gets the size of the range.                                            |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                          |
|---------------------------------|-------------|------------------------------------------------------------------|
| [Contains(in float)][7]         | `bool`      | Determines if this range contains the specified value.           |
| [Deconstruct(out float,...][8]  | `void`      |                                                                  |
| [Include(in float)][9]          | `void`      | Mutate this range (by expansion) to include the specified value. |
| [Include(in Range)][9]          | `void`      | Mutate this range (by expansion) to include the specified range. |
| [Overlaps(in Range)][10]        | `bool`      | Determines if this range overlaps another range.                 |
| [Rescale(in float, in f...][11] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Rescale(in float, in R...][11] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Set(float, float)][12]         | `void`      | Sets the components of this range.                               |

[0]: ../../Heirloom.Core.md
[1]: Range/Min.md
[2]: Range/Max.md
[3]: Range/Average.md
[4]: Range/Indexer.md
[5]: Range/IsValid.md
[6]: Range/Size.md
[7]: Range/Contains.md
[8]: Range/Deconstruct.md
[9]: Range/Include.md
[10]: Range/Overlaps.md
[11]: Range/Rescale.md
[12]: Range/Set.md
[13]: Range/Indeterminate.md
[14]: Range/Infinite.md
[15]: Range/Zero.md
[16]: Range.md
