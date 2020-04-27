# Statistics.Compute

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Statistics][1]  

--------------------------------------------------------------------------------

### Compute(IEnumerable<int>)

Computes new statistics from a collection of integers.

```cs
public Statistics Compute(IEnumerable<int> values)
```

### Compute(IEnumerable<float>)

Computes new statistics from a collection of numbers.

```cs
public Statistics Compute(IEnumerable<float> values)
```

### Compute(float, float, Range, int)

Computes new statistics from a sum, squared sum, extents and count.

```cs
public Statistics Compute(float sum, float squareSum, Range range, int count)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Statistics.md
