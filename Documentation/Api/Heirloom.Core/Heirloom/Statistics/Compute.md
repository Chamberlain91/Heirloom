# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Statistics.Compute (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Statistics][1]

### Compute(IEnumerable<int>)

Computes new statistics from a collection of integers.

```cs
public static Statistics Compute(IEnumerable<int> values)
```

| Name   | Type                | Summary |
|--------|---------------------|---------|
| values | `IEnumerable\<int>` |         |

> **Returns** - [Statistics][1]

### Compute(IEnumerable<float>)

Computes new statistics from a collection of numbers.

```cs
public static Statistics Compute(IEnumerable<float> values)
```

| Name   | Type                  | Summary |
|--------|-----------------------|---------|
| values | `IEnumerable\<float>` |         |

> **Returns** - [Statistics][1]

### Compute(float, float, Range, int)

Computes new statistics from a sum, squared sum, extents and count.

```cs
public static Statistics Compute(float sum, float squareSum, Range range, int count)
```

| Name      | Type       | Summary |
|-----------|------------|---------|
| sum       | `float`    |         |
| squareSum | `float`    |         |
| range     | [Range][2] |         |
| count     | `int`      |         |

> **Returns** - [Statistics][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Statistics.md
[2]: ../Range.md
