# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.Inverse (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### Inverse(in Matrix)

Computes the inverse of this matrix.

```cs
public static Matrix Inverse(in Matrix a)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |

> **Returns** - [Matrix][1]

### Inverse(in Matrix, ref Matrix)

Computes the inverse of the matrix and stores the resulting matrix into `dest` .

```cs
public static void Inverse(in Matrix a, ref Matrix dest)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Matrix][1] |         |
| dest | [Matrix][1] |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
