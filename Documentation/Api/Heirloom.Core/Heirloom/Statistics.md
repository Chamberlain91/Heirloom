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

### Static Methods

[Compute][5]

## Fields

#### Instance

| Name           | Type       | Summary                                                      |
|----------------|------------|--------------------------------------------------------------|
| [Average][1]   | `float`    | The average value. Also known as the mean or expected value. |
| [Deviation][2] | `float`    | The standard deviation.                                      |
| [Range][3]     | [Range][6] | The range of values.                                         |
| [Variance][4]  | `float`    | The variance value.                                          |

## Methods

| Name                           | Return Type     | Summary                                                             |
|--------------------------------|-----------------|---------------------------------------------------------------------|
| [Compute(IEnumerable<int>)][5] | [Statistics][7] | Computes new statistics from a collection of integers.              |
| [Compute(IEnumerable<fl...][5] | [Statistics][7] | Computes new statistics from a collection of numbers.               |
| [Compute(float, float, ...][5] | [Statistics][7] | Computes new statistics from a sum, squared sum, extents and count. |

[0]: ../../Heirloom.Core.md
[1]: Statistics/Average.md
[2]: Statistics/Deviation.md
[3]: Statistics/Range.md
[4]: Statistics/Variance.md
[5]: Statistics/Compute.md
[6]: Range.md
[7]: Statistics.md
