# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.CreateGridPattern (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### CreateGridPattern(IntSize, Color, int, int)

Create an image with a grid pattern.

```cs
public static Image CreateGridPattern(IntSize size, Color color, int cellSize, int borderWidth = 1)
```

| Name        | Type         | Summary                             |
|-------------|--------------|-------------------------------------|
| size        | [IntSize][2] | Size of the image in pixels.        |
| color       | [Color][3]   | Color to base the grid pattern on.  |
| cellSize    | `int`        | The size of a grid cell in pixels.  |
| borderWidth | `int`        | Size of the line between each cell. |

> **Returns** - [Image][1] - An image filled with the grid pattern.

### CreateGridPattern(int, int, Color, int, int)

Create an image with a grid pattern.

```cs
public static Image CreateGridPattern(int width, int height, Color color, int cellSize, int borderWidth = 1)
```

| Name        | Type       | Summary                             |
|-------------|------------|-------------------------------------|
| width       | `int`      | Width of the image in pixels.       |
| height      | `int`      | Height of the image in pixels.      |
| color       | [Color][3] | Color to base the grid pattern on.  |
| cellSize    | `int`      | The size of a grid cell in pixels.  |
| borderWidth | `int`      | Size of the line between each cell. |

> **Returns** - [Image][1] - An image filled with the grid pattern.

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntSize.md
[3]: ../Color.md
