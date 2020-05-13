# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.SetPixel (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### SetPixel(int, int, in ColorBytes)

Sets the color of a pixel at the specified coordinate.

```cs
public void SetPixel(int x, int y, in ColorBytes color)
```

| Name  | Type            | Summary                           |
|-------|-----------------|-----------------------------------|
| x     | `int`           | The x-coordinate of the pixel.    |
| y     | `int`           | The y-coordinate of the pixel.    |
| color | [ColorBytes][2] | The color to assign to the pixel. |

> **Returns** - `void`

### SetPixel(IntVector, in ColorBytes)

Sets the color of a pixel at the specified coordinate.

```cs
public void SetPixel(IntVector co, in ColorBytes color)
```

| Name  | Type            | Summary                           |
|-------|-----------------|-----------------------------------|
| co    | [IntVector][3]  | The coordinate of the pixel.      |
| color | [ColorBytes][2] | The color to assign to the pixel. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../ColorBytes.md
[3]: ../IntVector.md
