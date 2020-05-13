# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.GetPixel (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### GetPixel(int, int)

Gets the pixel at the specified coordinates.

```cs
public ColorBytes GetPixel(int x, int y)
```

| Name | Type  | Summary                        |
|------|-------|--------------------------------|
| x    | `int` | The x-coordinate of the pixel. |
| y    | `int` | The y-coordinate of the pixel. |

> **Returns** - [ColorBytes][2] - The color value at the specified coordinate.

### GetPixel(IntVector)

Gets the pixel at the specified coordinates.

```cs
public ColorBytes GetPixel(IntVector co)
```

| Name | Type           | Summary                      |
|------|----------------|------------------------------|
| co   | [IntVector][3] | The coordinate of the pixel. |

> **Returns** - [ColorBytes][2] - The color value at the specified coordinate.

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../ColorBytes.md
[3]: ../IntVector.md
