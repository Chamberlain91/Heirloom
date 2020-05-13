# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.Multiply (Operator)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### Multiply(Matrix, Matrix)

Performs the multiplication of two matrices.

```cs
public static Matrix Multiply(Matrix a, Matrix b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| b    | [Matrix][1] |         |

> **Returns** - [Matrix][1]

### Multiply(Matrix, Vector)

Performs the multiplication of a matrix and vector, transforming the vector.

```cs
public static Vector Multiply(Matrix m, Vector v)
```

| Name | Type        | Summary |
|------|-------------|---------|
| m    | [Matrix][1] |         |
| v    | [Vector][2] |         |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Vector.md
