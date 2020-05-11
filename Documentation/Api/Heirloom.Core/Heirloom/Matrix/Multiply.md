# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.Multiply (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### Multiply(in Vector)

Multiplies a vector against this matrix.

```cs
public Vector Multiply(in Vector v)
```

| Name | Type        | Summary |
|------|-------------|---------|
| v    | [Vector][2] |         |

> **Returns** - [Vector][2]

### Multiply(in Matrix, in Matrix, ref Matrix)

Multiply two matrices together and store the result in `dest` .

```cs
public static void Multiply(in Matrix a, in Matrix b, ref Matrix dest)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| b    | [Matrix][1] |         |
| dest | [Matrix][1] |         |

> **Returns** - `void`

### Multiply(in Matrix, in Matrix)

Multiply two matrices together.

```cs
public static Matrix Multiply(in Matrix a, in Matrix b)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| b    | [Matrix][1] |         |

> **Returns** - [Matrix][1]

### Multiply(in Matrix, in Vector, ref Vector)

Multiplies a vector and matrix together and stores the resulting vector into `dest` .

```cs
public static void Multiply(in Matrix a, in Vector v, ref Vector dest)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| v    | [Vector][2] |         |
| dest | [Vector][2] |         |

> **Returns** - `void`

### Multiply(in Matrix, in Vector)

Multiplies a vector and matrix together.

```cs
public static Vector Multiply(in Matrix a, in Vector v)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| v    | [Vector][2] |         |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Vector.md
