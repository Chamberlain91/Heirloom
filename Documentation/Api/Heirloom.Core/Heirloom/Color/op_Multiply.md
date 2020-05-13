# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Color.Multiply (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Color][1]

### Multiply(Color, Color)

Performs a component-wise multiplication of two instances of [Color][1] .

```cs
public static Color Multiply(Color c1, Color c2)
```

| Name | Type       | Summary |
|------|------------|---------|
| c1   | [Color][1] |         |
| c2   | [Color][1] |         |

> **Returns** - [Color][1]

### Multiply(float, Color)

Performs a component-wise scale of a [Color][1] .

```cs
public static Color Multiply(float x, Color c2)
```

| Name | Type       | Summary |
|------|------------|---------|
| x    | `float`    |         |
| c2   | [Color][1] |         |

> **Returns** - [Color][1]

### Multiply(Color, float)

Performs a component-wise scale of a [Color][1] .

```cs
public static Color Multiply(Color c1, float x)
```

| Name | Type       | Summary |
|------|------------|---------|
| c1   | [Color][1] |         |
| x    | `float`    |         |

> **Returns** - [Color][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Color.md
