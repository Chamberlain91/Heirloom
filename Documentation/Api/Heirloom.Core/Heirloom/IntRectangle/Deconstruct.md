# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle.Deconstruct (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IntRectangle][1]

### Deconstruct(out int, out int, out int, out int)

Deconstructs this rectangle into consituent components.

```cs
public void Deconstruct(out int x, out int y, out int w, out int h)
```

| Name | Type  | Summary                          |
|------|-------|----------------------------------|
| x    | `int` | The x position of the rectangle. |
| y    | `int` | The y position of the rectangle. |
| w    | `int` | The width of the rectangle.      |
| h    | `int` | The height of the rectangle.     |

> **Returns** - `void`

### Deconstruct(out IntVector, out IntSize)

Deconstructs this rectangle into consituent parts.

```cs
public void Deconstruct(out IntVector position, out IntSize size)
```

| Name     | Type           | Summary                        |
|----------|----------------|--------------------------------|
| position | [IntVector][2] | The position of the rectangle. |
| size     | [IntSize][3]   | The size of the rectangle.     |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../IntRectangle.md
[2]: ../IntVector.md
[3]: ../IntSize.md
