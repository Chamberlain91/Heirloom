# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc.Rescale (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Calc][1]

### Rescale(in float, in float, in float, in float, in float)

Rescales a value with domain `min1` to `max1` to a new domain `min2` to `max2` .

```cs
public static float Rescale(in float x, in float min1, in float max1, in float min2, in float max2)
```

| Name | Type    | Summary |
|------|---------|---------|
| x    | `float` |         |
| min1 | `float` |         |
| max1 | `float` |         |
| min2 | `float` |         |
| max2 | `float` |         |

> **Returns** - `float`

### Rescale(in float, in Range, in Range)

Rescales a value from the source domain a target domain.

```cs
public static float Rescale(in float x, in Range src, in Range dst)
```

| Name | Type       | Summary |
|------|------------|---------|
| x    | `float`    |         |
| src  | [Range][2] |         |
| dst  | [Range][2] |         |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Calc.md
[2]: ../Range.md
