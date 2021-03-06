# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.MultiplyVector (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### MultiplyVector(in Vector)

Multiplies a vector against this matrix ignoring the translational components.

```cs
public Vector MultiplyVector(in Vector v)
```

| Name | Type        | Summary |
|------|-------------|---------|
| v    | [Vector][2] |         |

> **Returns** - [Vector][2]

### MultiplyVector(in Matrix, in Vector, ref Vector)

Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `r` .

```cs
public static void MultiplyVector(in Matrix a, in Vector v, ref Vector r)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| v    | [Vector][2] |         |
| r    | [Vector][2] |         |

> **Returns** - `void`

### MultiplyVector(in Matrix, in Vector)

Multiplies a vector and matrix together ignoring the translational components.

```cs
public static Vector MultiplyVector(in Matrix a, in Vector v)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| v    | [Vector][2] |         |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Vector.md
