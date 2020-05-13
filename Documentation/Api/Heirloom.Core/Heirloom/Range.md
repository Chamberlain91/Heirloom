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

[Contains][6], [Deconstruct][7], [Equals][8], [GetHashCode][9], [Include][10], [Intersect][11], [Overlaps][12], [Rescale][13], [Set][14], [ToString][15], [Union][16]

### Static Fields

[Indeterminate][17], [Infinite][18], [Zero][19]

### Static Methods

[FromValues][20]

## Fields

#### Instance

| Name     | Type    | Summary                         |
|----------|---------|---------------------------------|
| [Max][2] | `float` | The maximum value in the range. |
| [Min][1] | `float` | The minimum value in the range. |

#### Static

| Name                | Type        | Summary                                                                |
|---------------------|-------------|------------------------------------------------------------------------|
| [Indeterminate][17] | [Range][21] | Range from float.PositiveInfinity to float.NegativeInfinity useful ... |
| [Infinite][18]      | [Range][21] | Range from float.NegativeInfinity to float.PositiveInfinity (the wi... |
| [Zero][19]          | [Range][21] | Zero width range centered on zero.                                     |

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
| [Equals(object)][8]             | `bool`      | Compares this range for equality with another object.            |
| [Equals(Range)][8]              | `bool`      | Compares this range for equality with another range.             |
| [GetHashCode()][9]              | `int`       | Returns the hash code for this range.                            |
| [Include(in float)][10]         | `void`      | Mutate this range (by expansion) to include the specified value. |
| [Include(in Range)][10]         | `void`      | Mutate this range (by expansion) to include the specified range. |
| [Intersect(in Range)][11]       | [Range][21] | Computes the intersection of this range with another.            |
| [Overlaps(in Range)][12]        | `bool`      | Determines if this range overlaps another range.                 |
| [Rescale(in float, in f...][13] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Rescale(in float, in R...][13] | `float`     | Scales `x` from input domain (this range) to output range.       |
| [Set(float, float)][14]         | `void`      | Sets the components of this range.                               |
| [ToString()][15]                | `string`    | Returns the string representation of this Range .                |
| [Union(in Range)][16]           | [Range][21] | Computes the union of this range with another.                   |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromValues(IEnumerable...][20] | [Range][21] | Creates an instance of Range from the extreme values of the given c... |

## Operators

| Name                            | Return Type                | Summary                             |
|---------------------------------|----------------------------|-------------------------------------|
| [Equality(Range, Range)][22]    | `bool`                     | Compares two ranges for equality.   |
| [Explicit(Vector)][23]          | [Range][21]                |                                     |
| [Explicit(Range)][23]           | [Vector][24]               |                                     |
| [Implicit(Range)][25]           | `ValueTuple<float, float>` |                                     |
| [Implicit(ValueTuple<fl...][25] | [Range][21]                |                                     |
| [Inequality(Range, Range)][26]  | `bool`                     | Compares two ranges for inequality. |

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
[11]: Range/Intersect.md
[12]: Range/Overlaps.md
[13]: Range/Rescale.md
[14]: Range/Set.md
[15]: Range/ToString.md
[16]: Range/Union.md
[17]: Range/Indeterminate.md
[18]: Range/Infinite.md
[19]: Range/Zero.md
[20]: Range/FromValues.md
[21]: Range.md
[22]: Range/op_Equality.md
[23]: Range/op_Explicit.md
[24]: Vector.md
[25]: Range/op_Implicit.md
[26]: Range/op_Inequality.md
