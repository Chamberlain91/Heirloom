# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Statistics (Struct)

> **Namespace**: [Heirloom][0]

Represents statistics of some data.

```cs
public struct Statistics : IEquatable<Statistics>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<Statistics>

### Fields

[Average][1], [Deviation][2], [Range][3], [Variance][4]

### Methods

[Equals][5], [GetHashCode][6], [ToString][7]

### Static Methods

[Compute][8]

## Fields

#### Instance

| Name           | Type       | Summary                                                      |
|----------------|------------|--------------------------------------------------------------|
| [Average][1]   | `float`    | The average value. Also known as the mean or expected value. |
| [Deviation][2] | `float`    | The standard deviation.                                      |
| [Range][3]     | [Range][9] | The range of values.                                         |
| [Variance][4]  | `float`    | The variance value.                                          |

## Methods

#### Instance

| Name                    | Return Type | Summary |
|-------------------------|-------------|---------|
| [Equals(object)][5]     | `bool`      |         |
| [Equals(Statistics)][5] | `bool`      |         |
| [GetHashCode()][6]      | `int`       |         |
| [ToString()][7]         | `string`    |         |

#### Static

| Name                           | Return Type      | Summary                                                             |
|--------------------------------|------------------|---------------------------------------------------------------------|
| [Compute(IEnumerable<int>)][8] | [Statistics][10] | Computes new statistics from a collection of integers.              |
| [Compute(IEnumerable<fl...][8] | [Statistics][10] | Computes new statistics from a collection of numbers.               |
| [Compute(float, float, ...][8] | [Statistics][10] | Computes new statistics from a sum, squared sum, extents and count. |

## Operators

| Name                            | Return Type | Summary |
|---------------------------------|-------------|---------|
| [Equality(Statistics, S...][11] | `bool`      |         |
| [Inequality(Statistics,...][12] | `bool`      |         |

[0]: ../../Heirloom.Core.md
[1]: Statistics/Average.md
[2]: Statistics/Deviation.md
[3]: Statistics/Range.md
[4]: Statistics/Variance.md
[5]: Statistics/Equals.md
[6]: Statistics/GetHashCode.md
[7]: Statistics/ToString.md
[8]: Statistics/Compute.md
[9]: Range.md
[10]: Statistics.md
[11]: Statistics/op_Equality.md
[12]: Statistics/op_Inequality.md
