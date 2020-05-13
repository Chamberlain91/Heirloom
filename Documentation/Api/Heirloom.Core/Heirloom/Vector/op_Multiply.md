# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Vector.Multiply (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Vector][1]

### Multiply(Vector, Vector)

Performs the component-wise multiplication of two vectors.

```cs
public static Vector Multiply(Vector a, Vector b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][1] |         |
| b    | [Vector][1] |         |

> **Returns** - [Vector][1]

### Multiply(Vector, float)

Performs the scaling of a vector.

```cs
public static Vector Multiply(Vector a, float b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][1] |         |
| b    | `float`     |         |

> **Returns** - [Vector][1]

### Multiply(float, Vector)

Performs the scaling of a vector.

```cs
public static Vector Multiply(float a, Vector b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | `float`     |         |
| b    | [Vector][1] |         |

> **Returns** - [Vector][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Vector.md
