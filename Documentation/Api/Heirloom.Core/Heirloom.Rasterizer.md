# Rasterizer

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Contains rasterization methods for iterating over pixel positions.

```cs
public static class Rasterizer
```


This is useful beyond drawing images. For example, in a city builder the [Rectangle][1] or [Line][2] commands can be used to compute positions to place tiles when drawing building plots or roads.

--------------------------------------------------------------------------------

**Static Methods**: [Rectangle][1], [RectangleOutline][3], [Circle][4], [CircleOutline][5], [Triangle][6], [Line][2], [HLine][7], [VLine][8]

--------------------------------------------------------------------------------

## Methods

| Name                  | Summary                                        |
|-----------------------|------------------------------------------------|
| [Rectangle][1]        | Rasterize a rectangular region.                |
| [Rectangle][1]        | Rasterize a rectangular region.                |
| [Rectangle][1]        | Rasterize a rectangular region.                |
| [Rectangle][1]        | Rasterize a rectangular region.                |
| [RectangleOutline][3] | Rasterize the outline of a rectangular region. |
| [Circle][4]           | Rasterizes a filled circle.                    |
| [CircleOutline][5]    | Rasterizes a circle outline.                   |
| [Triangle][6]         | Rasterize a triangle.                          |
| [Line][2]             | Rasterize along a line.                        |
| [Line][2]             | Rasterize along a line.                        |
| [Line][2]             | Rasterize along a line.                        |
| [Line][2]             | Rasterize along a line.                        |
| [HLine][7]            | Iterate over a perfectly horizontal line.      |
| [VLine][8]            | Iterate over a perfectly vertical line.        |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Rasterizer.Rectangle.md
[2]: Heirloom.Rasterizer.Line.md
[3]: Heirloom.Rasterizer.RectangleOutline.md
[4]: Heirloom.Rasterizer.Circle.md
[5]: Heirloom.Rasterizer.CircleOutline.md
[6]: Heirloom.Rasterizer.Triangle.md
[7]: Heirloom.Rasterizer.HLine.md
[8]: Heirloom.Rasterizer.VLine.md
