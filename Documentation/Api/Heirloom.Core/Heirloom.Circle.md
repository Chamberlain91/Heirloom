# Circle

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a circle via center position and radius.

```cs
public struct Circle : IShape, IEquatable<Circle>
```

--------------------------------------------------------------------------------

**Inherits**: [IShape][1], IEquatable\<Circle>

**Fields**: [Position][2], [Radius][3]

**Properties**: [Area][4], [Bounds][5]

**Methods**: [Set][6], [ToPolygon][7], [GetClosestPoint][8], [Contains][9], [Overlaps][10], [Project][11], [Raycast][12]

--------------------------------------------------------------------------------

## Fields

| Name          | Summary                            |
|---------------|------------------------------------|
| [Position][2] | The center position of the circle. |
| [Radius][3]   | The radius of the circle.          |

## Properties

| Name        | Summary                                     |
|-------------|---------------------------------------------|
| [Area][4]   | Gets the area of the circle.                |
| [Bounds][5] | Gets the bounding rectangle of this circle. |

## Methods

| Name                 | Summary                                                               |
|----------------------|-----------------------------------------------------------------------|
| [Set][6]             | Sets the components of this circle.                                   |
| [ToPolygon][7]       | Create a polygon from this rectangle.                                 |
| [GetClosestPoint][8] | Gets the nearest point on the circle to the specified point.          |
| [Contains][9]        | Determines if the specified point is contained by the circle.         |
| [Contains][9]        | Determines if this circle contains another circle.                    |
| [Overlaps][10]       | Determines if this circle overlaps another shape.                     |
| [Overlaps][10]       | Determines if this circle overlaps another circle.                    |
| [Overlaps][10]       | Determines if this circle overlaps the specified rectangle.           |
| [Overlaps][10]       | Determines if this circle overlaps the specified triangle.            |
| [Overlaps][10]       | Determines if this circle overlaps the specified simple polygon.      |
| [Overlaps][10]       | Determines if this circle overlaps the specified convex polygon.      |
| [Project][11]        | Project this circle onto the specified axis.                          |
| [Raycast][12]        | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast][12]        | Peforms a raycast onto this circle, returning true upon intersection. |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IShape.md
[2]: Heirloom.Circle.Position.md
[3]: Heirloom.Circle.Radius.md
[4]: Heirloom.Circle.Area.md
[5]: Heirloom.Circle.Bounds.md
[6]: Heirloom.Circle.Set.md
[7]: Heirloom.Circle.ToPolygon.md
[8]: Heirloom.Circle.GetClosestPoint.md
[9]: Heirloom.Circle.Contains.md
[10]: Heirloom.Circle.Overlaps.md
[11]: Heirloom.Circle.Project.md
[12]: Heirloom.Circle.Raycast.md
