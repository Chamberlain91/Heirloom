# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.CreateCheckerboardPattern (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### CreateCheckerboardPattern(IntSize, Color, int)

Create an image with checkerboard pattern.

```cs
public static Image CreateCheckerboardPattern(IntSize size, Color color, int cellSize = 16)
```

| Name     | Type         | Summary                                     |
|----------|--------------|---------------------------------------------|
| size     | [IntSize][2] | Size of the image in pixels.                |
| color    | [Color][3]   | Color to base the checkerboard pattern on.  |
| cellSize | `int`        | Size of each "checker" in the checkerboard. |

> **Returns** - [Image][1] - An image filled with the checkerboard pattern.

### CreateCheckerboardPattern(int, int, Color, int)

Create an image with checkerboard pattern.

```cs
public static Image CreateCheckerboardPattern(int width, int height, Color color, int cellSize = 16)
```

| Name     | Type       | Summary                                     |
|----------|------------|---------------------------------------------|
| width    | `int`      | Width of the image in pixels.               |
| height   | `int`      | Height of the image in pixels.              |
| color    | [Color][3] | Color to base the checkerboard pattern on.  |
| cellSize | `int`      | Size of each "checker" in the checkerboard. |

> **Returns** - [Image][1] - An image filled with the checkerboard pattern.

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntSize.md
[3]: ../Color.md
