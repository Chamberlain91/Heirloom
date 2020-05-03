# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc.Wrap (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Calc][1]

### Wrap(int, int)

Wraps (loops) a number within a zero to n range.

```cs
public static int Wrap(int x, int n)
```

| Name | Type  | Summary                     |
|------|-------|-----------------------------|
| x    | `int` | Some value to wrap.         |
| n    | `int` | Some upper bound from zero. |

> **Returns** - `int`

### Wrap(int, int, int)

Wraps (loops) a number within a range.

```cs
public static int Wrap(int x, int min, int max)
```

| Name | Type  | Summary             |
|------|-------|---------------------|
| x    | `int` | Some value to wrap. |
| min  | `int` | Some lower bound.   |
| max  | `int` | Some upper bound.   |

> **Returns** - `int` - The resulting number contrained to the range in a loop.

### Wrap(int, IntRange)

Wraps (loops) a number within a range.

```cs
public static int Wrap(int x, IntRange range)
```

| Name  | Type          | Summary             |
|-------|---------------|---------------------|
| x     | `int`         | Some value to wrap. |
| range | [IntRange][2] | Some range.         |

> **Returns** - `int` - The resulting number contrained to the range in a loop.

### Wrap(float, float)

Wraps (loops) a number within a zero to n range.

```cs
public static float Wrap(float x, float n)
```

| Name | Type    | Summary                     |
|------|---------|-----------------------------|
| x    | `float` | Some value to wrap.         |
| n    | `float` | Some upper bound from zero. |

> **Returns** - `float`

### Wrap(float, float, float)

Wraps (loops) a number within a range.

```cs
public static float Wrap(float x, float min, float max)
```

| Name | Type    | Summary             |
|------|---------|---------------------|
| x    | `float` | Some value to wrap. |
| min  | `float` | Some lower bound.   |
| max  | `float` | Some upper bound.   |

> **Returns** - `float` - The resulting number contrained to the range in a loop.

### Wrap(float, Range)

Wraps (loops) a number within a range.

```cs
public static float Wrap(float x, Range range)
```

| Name  | Type       | Summary             |
|-------|------------|---------------------|
| x     | `float`    | Some value to wrap. |
| range | [Range][3] | Some range.         |

> **Returns** - `float` - The resulting number contrained to the range in a loop.

[0]: ../../../Heirloom.Core.md
[1]: ../Calc.md
[2]: ../IntRange.md
[3]: ../Range.md
