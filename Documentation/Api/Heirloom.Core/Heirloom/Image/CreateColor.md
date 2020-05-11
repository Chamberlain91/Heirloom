# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.CreateColor (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### CreateColor(IntSize, Color)

Creates an image filled with a solid color.

```cs
public static Image CreateColor(IntSize size, Color color)
```

| Name  | Type         | Summary                       |
|-------|--------------|-------------------------------|
| size  | [IntSize][2] | Size of the image in pixels.  |
| color | [Color][3]   | Color to fill the image with. |

> **Returns** - [Image][1] - An image of only the specified color.

### CreateColor(int, int, Color)

Creates an image filled with a solid color.

```cs
public static Image CreateColor(int width, int height, Color color)
```

| Name   | Type       | Summary                        |
|--------|------------|--------------------------------|
| width  | `int`      | Width of the image in pixels.  |
| height | `int`      | Height of the image in pixels. |
| color  | [Color][3] | Color to fill the image with.  |

> **Returns** - [Image][1] - An image of only the specified color.

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntSize.md
[3]: ../Color.md
