# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Triangle.CreateCircumcircle (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Triangle][1]

### CreateCircumcircle(in Triangle)

Computes the circumcircle for the specified triangle.

```cs
public static Circle CreateCircumcircle(in Triangle tri)
```

| Name | Type          | Summary |
|------|---------------|---------|
| tri  | [Triangle][1] |         |

> **Returns** - [Circle][2]

### CreateCircumcircle(in Vector, in Vector, in Vector)

Computes the circumcircle for the specified triangle.

```cs
public static Circle CreateCircumcircle(in Vector a, in Vector b, in Vector c)
```

| Name | Type        | Summary |
|------|-------------|---------|
| a    | [Vector][3] |         |
| b    | [Vector][3] |         |
| c    | [Vector][3] |         |

> **Returns** - [Circle][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Triangle.md
[2]: ../Circle.md
[3]: ../../Heirloom/Vector.md
