# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Circle (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Represents a circle via center position and radius.

```cs
public struct Circle : IShape, IEquatable<Circle>
```

### Inherits

[IShape][1], IEquatable\<Circle>

### Fields

[Position][2], [Radius][3]

### Properties

[Area][4], [Bounds][5]

### Methods

[Contains][6], [GetNearestPoint][7], [Overlaps][8], [Project][9], [Raycast][10], [Set][11], [ToPolygon][12]

## Fields

#### Instance

| Name          | Type         | Summary                            |
|---------------|--------------|------------------------------------|
| [Position][2] | [Vector][13] | The center position of the circle. |
| [Radius][3]   | `float`      | The radius of the circle.          |

## Properties

#### Instance

| Name        | Type            | Summary                                     |
|-------------|-----------------|---------------------------------------------|
| [Area][4]   | `float`         | Gets the area of the circle.                |
| [Bounds][5] | [Rectangle][14] | Gets the bounding rectangle of this circle. |

## Methods

#### Instance

| Name                            | Return Type   | Summary                                                               |
|---------------------------------|---------------|-----------------------------------------------------------------------|
| [Contains(in Vector)][6]        | `bool`        | Determines if the specified point is contained by the circle.         |
| [Contains(in Circle)][6]        | `bool`        | Determines if this circle contains another circle.                    |
| [GetNearestPoint(in Vec...][7]  | [Vector][13]  | Gets the nearest point on the circle to the specified point.          |
| [Overlaps(IShape)][8]           | `bool`        | Determines if this circle overlaps another shape.                     |
| [Overlaps(in Circle)][8]        | `bool`        | Determines if this circle overlaps another circle.                    |
| [Overlaps(in Rectangle)][8]     | `bool`        | Determines if this circle overlaps the specified rectangle.           |
| [Overlaps(in Triangle)][8]      | `bool`        | Determines if this circle overlaps the specified triangle.            |
| [Overlaps(Polygon)][8]          | `bool`        | Determines if this circle overlaps the specified simple polygon.      |
| [Overlaps(IReadOnlyList...][8]  | `bool`        | Determines if this circle overlaps the specified convex polygon.      |
| [Project(in Vector)][9]         | [Range][15]   | Project this circle onto the specified axis.                          |
| [Raycast(in Ray)][10]           | `bool`        | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast(in Ray, out Ra...][10] | `bool`        | Peforms a raycast onto this circle, returning true upon intersection. |
| [Set(in Vector, float)][11]     | `void`        | Sets the components of this circle.                                   |
| [ToPolygon()][12]               | [Polygon][16] | Create a polygon from this rectangle.                                 |

[0]: ../../Heirloom.Core.md
[1]: IShape.md
[2]: Circle/Position.md
[3]: Circle/Radius.md
[4]: Circle/Area.md
[5]: Circle/Bounds.md
[6]: Circle/Contains.md
[7]: Circle/GetNearestPoint.md
[8]: Circle/Overlaps.md
[9]: Circle/Project.md
[10]: Circle/Raycast.md
[11]: Circle/Set.md
[12]: Circle/ToPolygon.md
[13]: ../Heirloom/Vector.md
[14]: ../Heirloom/Rectangle.md
[15]: ../Heirloom/Range.md
[16]: Polygon.md
