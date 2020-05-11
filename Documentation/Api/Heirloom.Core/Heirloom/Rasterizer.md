# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rasterizer (Class)

> **Namespace**: [Heirloom][0]

Contains rasterization methods for iterating over pixel positions.

```cs
public static class Rasterizer
```

This is useful beyond drawing images. For example, in a city builder the [Rectangle][1] or [Line][2] commands can be used to compute positions to place tiles when drawing building plots or roads.

### Static Methods

[Circle][3], [CircleOutline][4], [HLine][5], [Line][2], [Rectangle][1], [RectangleOutline][6], [Triangle][7], [VLine][8]

## Methods

| Name                           | Return Type              | Summary                                        |
|--------------------------------|--------------------------|------------------------------------------------|
| [Circle(int, int, int)][3]     | `IEnumerable<IntVector>` | Rasterizes a filled circle.                    |
| [CircleOutline(int, int...][4] | `IEnumerable<IntVector>` | Rasterizes a circle outline.                   |
| [HLine(int, int, int)][5]      | `IEnumerable<IntVector>` | Iterate over a perfectly horizontal line.      |
| [Line(IntVector, IntVec...][2] | `IEnumerable<IntVector>` | Rasterize along a line.                        |
| [Line(IntVector, IntVec...][2] | `IEnumerable<IntVector>` | Rasterize along a line.                        |
| [Line(IntVector, IntVec...][2] | `IEnumerable<IntVector>` | Rasterize along a line.                        |
| [Line(IntVector, IntVec...][2] | `IEnumerable<IntVector>` | Rasterize along a line.                        |
| [Rectangle(int, int, in...][1] | `IEnumerable<IntVector>` | Rasterize a rectangular region.                |
| [Rectangle(IntRectangle)][1]   | `IEnumerable<IntVector>` | Rasterize a rectangular region.                |
| [Rectangle(IntVector, I...][1] | `IEnumerable<IntVector>` | Rasterize a rectangular region.                |
| [Rectangle(IntSize)][1]        | `IEnumerable<IntVector>` | Rasterize a rectangular region.                |
| [RectangleOutline(int, ...][6] | `IEnumerable<IntVector>` | Rasterize the outline of a rectangular region. |
| [Triangle(IntVector, In...][7] | `IEnumerable<IntVector>` | Rasterize a triangle.                          |
| [VLine(int, int, int)][8]      | `IEnumerable<IntVector>` | Iterate over a perfectly vertical line.        |

[0]: ../../Heirloom.Core.md
[1]: Rasterizer/Rectangle.md
[2]: Rasterizer/Line.md
[3]: Rasterizer/Circle.md
[4]: Rasterizer/CircleOutline.md
[5]: Rasterizer/HLine.md
[6]: Rasterizer/RectangleOutline.md
[7]: Rasterizer/Triangle.md
[8]: Rasterizer/VLine.md
