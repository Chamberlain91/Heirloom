# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Vector.Division (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Vector][1]

### Division(Vector, Vector)

Performs the component-wise division of two vectors.

```cs
public static Vector Division(Vector a, Vector b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][1] |         |
| b    | [Vector][1] |         |

> **Returns** - [Vector][1]

### Division(float, Vector)

Performs the scaling of a vector via per-component division.

```cs
public static Vector Division(float a, Vector b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | `float`     |         |
| b    | [Vector][1] |         |

> **Returns** - [Vector][1]

### Division(Vector, float)

Performs the scaling of a vector via per-component division.

```cs
public static Vector Division(Vector a, float b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][1] |         |
| b    | `float`     |         |

> **Returns** - [Vector][1]

[0]: ../../../Heirloom.Core.md
[1]: ../Vector.md
