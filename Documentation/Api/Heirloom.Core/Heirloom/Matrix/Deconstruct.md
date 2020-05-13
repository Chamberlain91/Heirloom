# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix.Deconstruct (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Matrix][1]

### Deconstruct(out Vector, out float, out Vector)

Deconstructs this [Matrix][1] into affine components.

```cs
public void Deconstruct(out Vector position, out float rotation, out Vector scale)
```

| Name     | Type        | Summary                                          |
|----------|-------------|--------------------------------------------------|
| position | [Vector][2] | Outputs the position or translational component. |
| rotation | `float`     | Outputs the rotational component.                |
| scale    | [Vector][2] | Outputs the scalar component.                    |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Matrix.md
[2]: ../Vector.md
