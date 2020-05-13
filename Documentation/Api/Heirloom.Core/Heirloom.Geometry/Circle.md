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

[Contains][6], [Equals][7], [GetHashCode][8], [GetNearestPoint][9], [Overlaps][10], [Project][11], [Raycast][12], [Set][13], [ToPolygon][14], [ToString][15]

## Fields

#### Instance

| Name          | Type         | Summary                            |
|---------------|--------------|------------------------------------|
| [Position][2] | [Vector][16] | The center position of the circle. |
| [Radius][3]   | `float`      | The radius of the circle.          |

## Properties

#### Instance

| Name        | Type            | Summary                                     |
|-------------|-----------------|---------------------------------------------|
| [Area][4]   | `float`         | Gets the area of the circle.                |
| [Bounds][5] | [Rectangle][17] | Gets the bounding rectangle of this circle. |

## Methods

#### Instance

| Name                            | Return Type   | Summary                                                               |
|---------------------------------|---------------|-----------------------------------------------------------------------|
| [Contains(in Vector)][6]        | `bool`        | Determines if the specified point is contained by the circle.         |
| [Contains(in Circle)][6]        | `bool`        | Determines if this circle contains another circle.                    |
| [Equals(object)][7]             | `bool`        | Compares this Circle for equality with another object.                |
| [Equals(Circle)][7]             | `bool`        | Compares this Circle for equality with another Circle .               |
| [GetHashCode()][8]              | `int`         | Returns the hash code for this Circle .                               |
| [GetNearestPoint(in Vec...][9]  | [Vector][16]  | Gets the nearest point on the circle to the specified point.          |
| [Overlaps(IShape)][10]          | `bool`        | Determines if this circle overlaps another shape.                     |
| [Overlaps(in Circle)][10]       | `bool`        | Determines if this circle overlaps another circle.                    |
| [Overlaps(in Rectangle)][10]    | `bool`        | Determines if this circle overlaps the specified rectangle.           |
| [Overlaps(in Triangle)][10]     | `bool`        | Determines if this circle overlaps the specified triangle.            |
| [Overlaps(Polygon)][10]         | `bool`        | Determines if this circle overlaps the specified simple polygon.      |
| [Overlaps(IReadOnlyList...][10] | `bool`        | Determines if this circle overlaps the specified convex polygon.      |
| [Project(in Vector)][11]        | [Range][18]   | Project this circle onto the specified axis.                          |
| [Raycast(in Ray)][12]           | `bool`        | Peforms a raycast onto this circle, returning true upon intersection. |
| [Raycast(in Ray, out Ra...][12] | `bool`        | Peforms a raycast onto this circle, returning true upon intersection. |
| [Set(in Vector, float)][13]     | `void`        | Sets the components of this circle.                                   |
| [ToPolygon()][14]               | [Polygon][19] | Create a polygon from this rectangle.                                 |
| [ToString()][15]                | `string`      | Gets the string representation of this Circle .                       |

## Operators

| Name                            | Return Type | Summary                                          |
|---------------------------------|-------------|--------------------------------------------------|
| [Equality(Circle, Circle)][20]  | `bool`      | Compares two instances of Circle for equality.   |
| [Inequality(Circle, Cir...][21] | `bool`      | Compares two instances of Circle for inequality. |

[0]: ../../Heirloom.Core.md
[1]: IShape.md
[2]: Circle/Position.md
[3]: Circle/Radius.md
[4]: Circle/Area.md
[5]: Circle/Bounds.md
[6]: Circle/Contains.md
[7]: Circle/Equals.md
[8]: Circle/GetHashCode.md
[9]: Circle/GetNearestPoint.md
[10]: Circle/Overlaps.md
[11]: Circle/Project.md
[12]: Circle/Raycast.md
[13]: Circle/Set.md
[14]: Circle/ToPolygon.md
[15]: Circle/ToString.md
[16]: ../Heirloom/Vector.md
[17]: ../Heirloom/Rectangle.md
[18]: ../Heirloom/Range.md
[19]: Polygon.md
[20]: Circle/op_Equality.md
[21]: Circle/op_Inequality.md
