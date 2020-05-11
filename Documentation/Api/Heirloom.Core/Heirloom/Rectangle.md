# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle (Struct)

> **Namespace**: [Heirloom][0]

```cs
public struct Rectangle : IShape, IEquatable<Rectangle>
```

### Inherits

[IShape][1], IEquatable\<Rectangle>

### Fields

[Height][2], [Width][3], [X][4], [Y][5]

### Properties

[Area][6], [Bottom][7], [BottomLeft][8], [BottomRight][9], [Center][10], [IsValid][11], [Left][12], [Max][13], [Min][14], [Position][15], [Right][16], [Size][17], [Top][18], [TopLeft][19], [TopRight][20]

### Methods

[Contains][21], [Deconstruct][22], [Equals][23], [GetHashCode][24], [GetNearestPoint][25], [Include][26], [Inflate][27], [Offset][28], [Overlaps][29], [Project][30], [Raycast][31], [Set][32], [ToPolygon][33], [ToString][34], [Transform][35]

### Static Properties

[Infinite][36], [InvertedInfinite][37], [One][38], [Zero][39]

### Static Methods

[FromPoints][40], [Inflate][27], [Merge][41], [Offset][28], [Transform][35]

## Fields

#### Instance

| Name        | Type    | Summary                             |
|-------------|---------|-------------------------------------|
| [Height][2] | `float` | The height of this rectangle.       |
| [Width][3]  | `float` | The width of this rectangle.        |
| [X][4]      | `float` | The x-coordinate of this rectangle. |
| [Y][5]      | `float` | The y-coordinate of this rectangle. |

## Properties

#### Instance

| Name             | Type         | Summary                                                                |
|------------------|--------------|------------------------------------------------------------------------|
| [Area][6]        | `float`      | Gets the area of this rectangle.                                       |
| [Bottom][7]      | `float`      | Gets the bottom extent of this rectangle.                              |
| [BottomLeft][8]  | [Vector][42] | Gets the bottom left corner of this rectangle.                         |
| [BottomRight][9] | [Vector][42] | Gets the bottom right corner of this rectangle.                        |
| [Center][10]     | [Vector][42] | Gets or sets the center position of this rectangle.                    |
| [IsValid][11]    | `bool`       | Determines if the values of this rectangle are considered to be val... |
| [Left][12]       | `float`      | Gets the left extent of this rectangle.                                |
| [Max][13]        | [Vector][42] | Gets the maximum corner of this rectangle.                             |
| [Min][14]        | [Vector][42] | Gets the minimum corner of this rectangle.                             |
| [Position][15]   | [Vector][42] | Gets or sets the position of this rectangle.                           |
| [Right][16]      | `float`      | Gets the right extent of this rectangle.                               |
| [Size][17]       | [Size][43]   | Gets or sets the size of this rectangle.                               |
| [Top][18]        | `float`      | Gets the top extent of this rectangle.                                 |
| [TopLeft][19]    | [Vector][42] | Gets the top left corner of this rectangle.                            |
| [TopRight][20]   | [Vector][42] | Gets the top right corner of this rectangle.                           |

#### Static

| Name                   | Type            | Summary                                                                |
|------------------------|-----------------|------------------------------------------------------------------------|
| [Infinite][36]         | [Rectangle][44] | A rectangle that spans the entire 2D plane.                            |
| [InvertedInfinite][37] | [Rectangle][44] | A rectangle that spans the entire 2D plane (but inverted, with min ... |
| [One][38]              | [Rectangle][44] | A 1x1 rectangle that is positioned at the origin.                      |
| [Zero][39]             | [Rectangle][44] | A 0x0 rectangle that is positioned at the origin.                      |

## Methods

#### Instance

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [Contains(in Vector)][21]       | `bool`          | Determines if this rectangle contains the given point?                 |
| [Contains(in Rectangle)][21]    | `bool`          | Determines if this rectangle contains another rectangle?               |
| [Deconstruct(out float,...][22] | `void`          |                                                                        |
| [Deconstruct(out Vector...][22] | `void`          |                                                                        |
| [Equals(object)][23]            | `bool`          |                                                                        |
| [Equals(Rectangle)][23]         | `bool`          |                                                                        |
| [GetHashCode()][24]             | `int`           |                                                                        |
| [GetNearestPoint(in Vec...][25] | [Vector][42]    | Returns the nearest point on the rectangle to the given point.         |
| [Include(Vector)][26]           | `void`          | Mutates this rectangle to accommodate the given point.                 |
| [Include(in Rectangle)][26]     | `void`          | Mutates this rectangle to accommodate the given rectangle.             |
| [Inflate(float)][27]            | `void`          | Expands (or shrinks) the rectangle by a factor on both axis.           |
| [Inflate(float, float)][27]     | `void`          | Expands (or shrinks) the rectangle by a factor on each axis.           |
| [Offset(float, float)][28]      | `void`          | Translates this rectangle.                                             |
| [Offset(Vector)][28]            | `void`          | Translates this rectangle.                                             |
| [Overlaps(IShape)][29]          | `bool`          | Determines if this rectangle overlaps another shape.                   |
| [Overlaps(in Circle)][29]       | `bool`          | Determines if this rectangle overlaps the specified circle.            |
| [Overlaps(in Triangle)][29]     | `bool`          | Determines if this rectangle overlaps the specified triangle.          |
| [Overlaps(in Rectangle)][29]    | `bool`          | Determines if this rectangle overlaps another rectangle.               |
| [Overlaps(IReadOnlyList...][29] | `bool`          | Determines if this rectangle overlaps the specified convex polygon.    |
| [Overlaps(Polygon)][29]         | `bool`          | Determines if this rectangle overlaps the specified simple polygon.    |
| [Project(in Vector)][30]        | [Range][45]     | Project this rectangle onto the specified axis.                        |
| [Raycast(in Ray)][31]           | `bool`          | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Raycast(in Ray, out Ra...][31] | `bool`          | Peforms a raycast onto this circle, returning true upon intersection.  |
| [Set(float, float, floa...][32] | `void`          | Sets the components of this rectangle.                                 |
| [ToPolygon()][33]               | [Polygon][46]   | Create a polygon from this rectangle.                                  |
| [ToString()][34]                | `string`        |                                                                        |
| [Transform(in Matrix)][35]      | [Rectangle][44] | Transforms the four corners of this rectangle and updates itself to... |

#### Static

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [FromPoints(params Vect...][40] | [Rectangle][44] |                                                                        |
| [FromPoints(IEnumerable...][40] | [Rectangle][44] | Computes the bounding rectangle of the given set of points.            |
| [Inflate(Rectangle, float)][27] | [Rectangle][44] | Expands (or shrinks) the input rectangle by a factor on both axis.     |
| [Inflate(Rectangle, flo...][27] | [Rectangle][44] | Expands (or shrinks) the input rectangle by a factor on each axis.     |
| [Merge(in Rectangle, in...][41] | [Rectangle][44] | Merges the given rectangles into one potentially larger rectangle.     |
| [Merge(params Rectangle[])][41] | [Rectangle][44] |                                                                        |
| [Offset(Rectangle, floa...][28] | [Rectangle][44] | Copies and translates the given rectangle.                             |
| [Offset(Rectangle, Vector)][28] | [Rectangle][44] | Copies and translates the given rectangle.                             |
| [Transform(Rectangle, i...][35] | [Rectangle][44] | Transforms the four corners of this rectangle and returns the bound... |

[0]: ../../Heirloom.Core.md
[1]: ../Heirloom.Geometry/IShape.md
[2]: Rectangle/Height.md
[3]: Rectangle/Width.md
[4]: Rectangle/X.md
[5]: Rectangle/Y.md
[6]: Rectangle/Area.md
[7]: Rectangle/Bottom.md
[8]: Rectangle/BottomLeft.md
[9]: Rectangle/BottomRight.md
[10]: Rectangle/Center.md
[11]: Rectangle/IsValid.md
[12]: Rectangle/Left.md
[13]: Rectangle/Max.md
[14]: Rectangle/Min.md
[15]: Rectangle/Position.md
[16]: Rectangle/Right.md
[17]: Rectangle/Size.md
[18]: Rectangle/Top.md
[19]: Rectangle/TopLeft.md
[20]: Rectangle/TopRight.md
[21]: Rectangle/Contains.md
[22]: Rectangle/Deconstruct.md
[23]: Rectangle/Equals.md
[24]: Rectangle/GetHashCode.md
[25]: Rectangle/GetNearestPoint.md
[26]: Rectangle/Include.md
[27]: Rectangle/Inflate.md
[28]: Rectangle/Offset.md
[29]: Rectangle/Overlaps.md
[30]: Rectangle/Project.md
[31]: Rectangle/Raycast.md
[32]: Rectangle/Set.md
[33]: Rectangle/ToPolygon.md
[34]: Rectangle/ToString.md
[35]: Rectangle/Transform.md
[36]: Rectangle/Infinite.md
[37]: Rectangle/InvertedInfinite.md
[38]: Rectangle/One.md
[39]: Rectangle/Zero.md
[40]: Rectangle/FromPoints.md
[41]: Rectangle/Merge.md
[42]: Vector.md
[43]: Size.md
[44]: Rectangle.md
[45]: Range.md
[46]: ../Heirloom.Geometry/Polygon.md
