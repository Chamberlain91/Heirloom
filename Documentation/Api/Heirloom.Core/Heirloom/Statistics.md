# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Statistics Struct

> **Namespace**: [Heirloom][0]  

Represents statistics of some data.

```cs
public struct Statistics : IEquatable<Statistics>
```

### Inherits

IEquatable\<Statistics>

#### Fields

[Average][1], [Variance][2], [Deviation][3], [Range][4]

#### Static Methods

[Compute][5]

## Fields

| Name           | Summary                                                      |
|----------------|--------------------------------------------------------------|
| [Average][1]   | The average value. Also known as the mean or expected value. |
| [Variance][2]  | The variance value.                                          |
| [Deviation][3] | The standard deviation.                                      |
| [Range][4]     | The range of values.                                         |

## Methods

| Name         | Summary                                                             |
|--------------|---------------------------------------------------------------------|
| [Compute][5] | Computes new statistics from a collection of integers.              |
| [Compute][5] | Computes new statistics from a collection of numbers.               |
| [Compute][5] | Computes new statistics from a sum, squared sum, extents and count. |

[0]: ../../Heirloom.Core.md
[1]: Statistics/Average.md
[2]: Statistics/Variance.md
[3]: Statistics/Deviation.md
[4]: Statistics/Range.md
[5]: Statistics/Compute.md
