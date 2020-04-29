# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.NextFloat (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### NextFloat(Random)

Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.

```cs
public static float NextFloat(Random this)
```

`ExtensionAttribute`

| Name | Type     | Summary |
|------|----------|---------|
| this | `Random` |         |

> **Returns** - `float`

### NextFloat(Random, float, float)

Returns a random floating-point number that is within the specified range.

```cs
public static float NextFloat(Random this, float min, float max)
```

`ExtensionAttribute`

| Name | Type     | Summary |
|------|----------|---------|
| this | `Random` |         |
| min  | `float`  |         |
| max  | `float`  |         |

> **Returns** - `float`

### NextFloat(Random, Range)

Returns a random floating-point number that is within the specified range.

```cs
public static float NextFloat(Random this, Range range)
```

`ExtensionAttribute`

| Name  | Type       | Summary |
|-------|------------|---------|
| this  | `Random`   |         |
| range | [Range][2] |         |

> **Returns** - `float`

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
[2]: ../Range.md
