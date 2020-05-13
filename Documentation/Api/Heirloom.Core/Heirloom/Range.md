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

[Contains][6], [Deconstruct][7], [Include][8], [Intersect][9], [Overlaps][10], [Rescale][11], [Set][12], [Union][13]

### Static Fields

[Indeterminate][14], [Infinite][15], [Zero][16]

### Static Methods

[FromValues][17]

## Fields

#### Instance

| Name     | Type    | Summary                         |
|----------|---------|---------------------------------|
| [Max][2] | `float` | The maximum value in the range. |
| [Min][1] | `float` | The minimum value in the range. |

#### Static

| Name                | Type        | Summary                                                                |
|---------------------|-------------|------------------------------------------------------------------------|
| [Indeterminate][14] | [Range][18] | Range from float.PositiveInfinity to float.NegativeInfinity useful ... |
| [Infinite][15]      | [Range][18] | Range from float.NegativeInfinity to float.PositiveInfinity (the wi... |
| [Zero][16]          | [Range][18] | Zero width range centered on zero.                                     |

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
| [Deconstruct(out float,...][7]  | `void`      | Deconstructs this Range into consituent parts.                   |
| [Include(in float)][8]          | `void`      | Mutate this range (by expansion) to include the specified value. |
| [Include(in Range)][8]          | `void`      | Mutate this range (by expansion) to include the specified range. |
| [Intersect(in Range)][9]        | [Range][18] | Computes the intersection of this range with another.            |
| [Overlaps(in Range)][10]        | `bool`      | Determines if this range overlaps another range.                 |
| [Rescale(in float, in f...][11] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Rescale(in float, in R...][11] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Set(float, float)][12]         | `void`      | Sets the components of this range.                               |
| [Union(in Range)][13]           | [Range][18] | Computes the union of this range with another.                   |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromValues(IEnumerable...][17] | [Range][18] | Creates an instance of Range from the extreme values of the given c... |

[0]: ../../Heirloom.Core.md
[1]: Range/Min.md
[2]: Range/Max.md
[3]: Range/Average.md
[4]: Range/IsValid.md
[5]: Range/Size.md
[6]: Range/Contains.md
[7]: Range/Deconstruct.md
[8]: Range/Include.md
[9]: Range/Intersect.md
[10]: Range/Overlaps.md
[11]: Range/Rescale.md
[12]: Range/Set.md
[13]: Range/Union.md
[14]: Range/Indeterminate.md
[15]: Range/Infinite.md
[16]: Range/Zero.md
[17]: Range/FromValues.md
[18]: Range.md
