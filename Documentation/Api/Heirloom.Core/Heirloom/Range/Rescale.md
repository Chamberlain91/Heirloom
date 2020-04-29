# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Range.Rescale (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Range][1]

### Rescale(in float, in float, in float)

Scales `x` from input domain (this range) to output range.

```cs
public float Rescale(in float x, in float outMin, in float outMax)
```

| Name   | Type    | Summary |
|--------|---------|---------|
| x      | `float` |         |
| outMin | `float` |         |
| outMax | `float` |         |

> **Returns** - `float`

### Rescale(in float, in Range)

Scales `x` from input domain (this range) to output range.

```cs
public float Rescale(in float x, in Range outRange)
```

| Name     | Type       | Summary |
|----------|------------|---------|
| x        | `float`    |         |
| outRange | [Range][1] |         |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Range.md
