# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Rectangle (Struct)

> **Namespace**: [Heirloom][0]

Represents a rectangle, defined by the top left corner position and size.

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

[Contains][21], [Deconstruct][22], [GetNearestPoint][23], [Include][24], [Inflate][25], [Offset][26], [Overlaps][27], [Project][28], [Raycast][29], [Set][30], [ToPolygon][31], [Transform][32]

### Static Properties

[Infinite][33], [InvertedInfinite][34], [One][35], [Zero][36]

### Static Methods

[FromPoints][37], [Inflate][25], [Merge][38], [Offset][26], [Transform][32]

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
| [BottomLeft][8]  | [Vector][39] | Gets the bottom left corner of this rectangle.                         |
| [BottomRight][9] | [Vector][39] | Gets the bottom right corner of this rectangle.                        |
| [Center][10]     | [Vector][39] | Gets or sets the center position of this rectangle.                    |
| [IsValid][11]    | `bool`       | Determines if the values of this rectangle are considered to be val... |
| [Left][12]       | `float`      | Gets the left extent of this rectangle.                                |
| [Max][13]        | [Vector][39] | Gets the maximum corner of this rectangle.                             |
| [Min][14]        | [Vector][39] | Gets the minimum corner of this rectangle.                             |
| [Position][15]   | [Vector][39] | Gets or sets the position of this rectangle.                           |
| [Right][16]      | `float`      | Gets the right extent of this rectangle.                               |
| [Size][17]       | [Size][40]   | Gets or sets the size of this rectangle.                               |
| [Top][18]        | `float`      | Gets the top extent of this rectangle.                                 |
| [TopLeft][19]    | [Vector][39] | Gets the top left corner of this rectangle.                            |
| [TopRight][20]   | [Vector][39] | Gets the top right corner of this rectangle.                           |

#### Static

| Name                   | Type            | Summary                                                                |
|------------------------|-----------------|------------------------------------------------------------------------|
| [Infinite][33]         | [Rectangle][41] | A rectangle that spans the entire 2D plane.                            |
| [InvertedInfinite][34] | [Rectangle][41] | A rectangle that spans the entire 2D plane (but inverted, with min ... |
| [One][35]              | [Rectangle][41] | A 1x1 rectangle that is positioned at the origin.                      |
| [Zero][36]             | [Rectangle][41] | A 0x0 rectangle that is positioned at the origin.                      |

## Methods

#### Instance

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [Contains(in Vector)][21]       | `bool`          | Determines if this rectangle contains the given point?                 |
| [Contains(in Rectangle)][21]    | `bool`          | Determines if this rectangle contains another rectangle?               |
| [Deconstruct(out float,...][22] | `void`          | Deconstructs this rectangle into consituent components.                |
| [Deconstruct(out Vector...][22] | `void`          | Deconstructs this rectangle into consituent parts.                     |
| [GetNearestPoint(in Vec...][23] | [Vector][39]    | Returns the nearest point on the rectangle to the given point.         |
| [Include(Vector)][24]           | `void`          | Mutates this rectangle to accommodate the given point.                 |
| [Include(in Rectangle)][24]     | `void`          | Mutates this rectangle to accommodate the given rectangle.             |
| [Inflate(float)][25]            | `void`          | Expands (or shrinks) the rectangle by a factor on both axis.           |
| [Inflate(float, float)][25]     | `void`          | Expands (or shrinks) the rectangle by a factor on each axis.           |
| [Offset(float, float)][26]      | `void`          | Translates this rectangle.                                             |
| [Offset(Vector)][26]            | `void`          | Translates this rectangle.                                             |
| [Overlaps(IShape)][27]          | `bool`          | Determines if this rectangle overlaps another shape.                   |
| [Overlaps(in Circle)][27]       | `bool`          | Determines if this rectangle overlaps the specified circle.            |
| [Overlaps(in Triangle)][27]     | `bool`          | Determines if this rectangle overlaps the specified triangle.          |
| [Overlaps(in Rectangle)][27]    | `bool`          | Determines if this rectangle overlaps another rectangle.               |
| [Overlaps(IReadOnlyList...][27] | `bool`          | Determines if this rectangle overlaps the specified convex polygon.    |
| [Overlaps(Polygon)][27]         | `bool`          | Determines if this rectangle overlaps the specified simple polygon.    |
| [Project(in Vector)][28]        | [Range][42]     | Project this rectangle onto the specified axis.                        |
| [Raycast(in Ray)][29]           | `bool`          | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Raycast(in Ray, out Ra...][29] | `bool`          | Peforms a raycast onto this circle, returning true upon intersection.  |
| [Set(float, float, floa...][30] | `void`          | Sets the components of this rectangle.                                 |
| [ToPolygon()][31]               | [Polygon][43]   | Create a polygon from this rectangle.                                  |
| [Transform(in Matrix)][32]      | [Rectangle][41] | Transforms the four corners of this rectangle and updates itself to... |

#### Static

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [FromPoints(params Vect...][37] | [Rectangle][41] |                                                                        |
| [FromPoints(IEnumerable...][37] | [Rectangle][41] | Computes the bounding rectangle of the given set of points.            |
| [Inflate(Rectangle, float)][25] | [Rectangle][41] | Expands (or shrinks) the input rectangle by a factor on both axis.     |
| [Inflate(Rectangle, flo...][25] | [Rectangle][41] | Expands (or shrinks) the input rectangle by a factor on each axis.     |
| [Merge(in Rectangle, in...][38] | [Rectangle][41] | Merges the given rectangles into one potentially larger rectangle.     |
| [Merge(params Rectangle[])][38] | [Rectangle][41] |                                                                        |
| [Offset(Rectangle, floa...][26] | [Rectangle][41] | Copies and translates the given rectangle.                             |
| [Offset(Rectangle, Vector)][26] | [Rectangle][41] | Copies and translates the given rectangle.                             |
| [Transform(Rectangle, i...][32] | [Rectangle][41] | Transforms the four corners of this rectangle and returns the bound... |

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
[23]: Rectangle/GetNearestPoint.md
[24]: Rectangle/Include.md
[25]: Rectangle/Inflate.md
[26]: Rectangle/Offset.md
[27]: Rectangle/Overlaps.md
[28]: Rectangle/Project.md
[29]: Rectangle/Raycast.md
[30]: Rectangle/Set.md
[31]: Rectangle/ToPolygon.md
[32]: Rectangle/Transform.md
[33]: Rectangle/Infinite.md
[34]: Rectangle/InvertedInfinite.md
[35]: Rectangle/One.md
[36]: Rectangle/Zero.md
[37]: Rectangle/FromPoints.md
[38]: Rectangle/Merge.md
[39]: Vector.md
[40]: Size.md
[41]: Rectangle.md
[42]: Range.md
[43]: ../Heirloom.Geometry/Polygon.md
