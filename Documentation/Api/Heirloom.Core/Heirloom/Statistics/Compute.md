# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Statistics.Compute

> **Namespace**: [Heirloom][0]  
> **Type**: [Statistics][1]  

### Compute(IEnumerable<int>)

Computes new statistics from a collection of integers.

```cs
public static Statistics Compute(IEnumerable<int> values)
```

### Compute(IEnumerable<float>)

Computes new statistics from a collection of numbers.

```cs
public static Statistics Compute(IEnumerable<float> values)
```

### Compute(float, float, Range, int)

Computes new statistics from a sum, squared sum, extents and count.

```cs
public static Statistics Compute(float sum, float squareSum, Range range, int count)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Statistics.md
