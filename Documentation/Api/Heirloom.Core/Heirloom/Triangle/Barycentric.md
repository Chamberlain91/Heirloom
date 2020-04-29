# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Triangle.Barycentric (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Triangle][1]

### Barycentric(in Vector, out float, out float, out float)

Computes the barycentric coefficients of the point `p` within the triangle.

```cs
public void Barycentric(in Vector p, out float u, out float v, out float w)
```

| Name | Type        | Summary |
|------|-------------|---------|
| p    | [Vector][2] |         |
| u    | `float`     |         |
| v    | `float`     |         |
| w    | `float`     |         |

> **Returns** - `void`

### Barycentric(in Vector, in Vector, in Vector, in Vector, out float, out float, out float)

Computes the barycentric coefficients of the point `p` within the triangle `a` , `b` , `c` .

```cs
public static void Barycentric(in Vector p, in Vector a, in Vector b, in Vector c, out float u, out float v, out float w)
```

| Name | Type        | Summary |
|------|-------------|---------|
| p    | [Vector][2] |         |
| a    | [Vector][2] |         |
| b    | [Vector][2] |         |
| c    | [Vector][2] |         |
| u    | `float`     |         |
| v    | `float`     |         |
| w    | `float`     |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Triangle.md
[2]: ../Vector.md
