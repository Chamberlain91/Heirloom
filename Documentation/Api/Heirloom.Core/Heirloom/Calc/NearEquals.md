# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc.NearEquals (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Calc][1]

### NearEquals(float, float)

Determines if the two values are nearly equal comparing equality within a [Epsilon][2] threshold.

```cs
public static bool NearEquals(float x1, float x2)
```

| Name | Type    | Summary |
|------|---------|---------|
| x1   | `float` |         |
| x2   | `float` |         |

> **Returns** - `bool`

### NearEquals(float, float, float)

Determines if the two values are nearly equal comparing equality within a threshold.

```cs
public static bool NearEquals(float x1, float x2, float threshold)
```

| Name      | Type    | Summary |
|-----------|---------|---------|
| x1        | `float` |         |
| x2        | `float` |         |
| threshold | `float` |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Calc.md
[2]: Epsilon.md
