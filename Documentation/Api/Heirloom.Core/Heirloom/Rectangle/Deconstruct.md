# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle.Deconstruct (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Rectangle][1]

### Deconstruct(out float, out float, out float, out float)

Deconstructs this rectangle into consituent components.

```cs
public void Deconstruct(out float x, out float y, out float w, out float h)
```

| Name | Type    | Summary                          |
|------|---------|----------------------------------|
| x    | `float` | The x position of the rectangle. |
| y    | `float` | The y position of the rectangle. |
| w    | `float` | The width of the rectangle.      |
| h    | `float` | The height of the rectangle.     |

> **Returns** - `void`

### Deconstruct(out Vector, out Size)

Deconstructs this rectangle into consituent parts.

```cs
public void Deconstruct(out Vector position, out Size size)
```

| Name     | Type        | Summary                        |
|----------|-------------|--------------------------------|
| position | [Vector][2] | The position of the rectangle. |
| size     | [Size][3]   | The size of the rectangle.     |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Rectangle.md
[2]: ../Vector.md
[3]: ../Size.md
