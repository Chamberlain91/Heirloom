# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc.SmoothStep (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Calc][1]

### SmoothStep(float, float, float)

Computes the smooth-step of `x` between `min` and `max` .

```cs
public static float SmoothStep(float x, float min, float max)
```

| Name | Type    | Summary         |
|------|---------|-----------------|
| x    | `float` | Some number.    |
| min  | `float` | The lower edge. |
| max  | `float` | The upper edge. |

> **Returns** - `float` - The smoothstep of `x` .

### SmoothStep(float)

Computes smoothstep of a number. (Assumes `x` is in the range 0.0 to 1.0).

```cs
public static float SmoothStep(float x)
```

| Name | Type    | Summary      |
|------|---------|--------------|
| x    | `float` | Some number. |

> **Returns** - `float` - The smoothstep of `x` .

[0]: ../../../Heirloom.Core.md
[1]: ../Calc.md
