# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntRectangle (Struct)

> **Namespace**: [Heirloom][0]

Represents a rectangle defined with integer coordinates.

```cs
public struct IntRectangle : IEquatable<IntRectangle>
```

### Inherits

IEquatable\<IntRectangle>

### Fields

[Height][1], [Width][2], [X][3], [Y][4]

### Properties

[Area][5], [Bottom][6], [BottomLeft][7], [BottomRight][8], [Center][9], [IsValid][10], [Left][11], [Max][12], [Min][13], [Position][14], [Right][15], [Size][16], [Top][17], [TopLeft][18], [TopRight][19]

### Methods

[ClosestPoint][20], [Contains][21], [Deconstruct][22], [Include][23], [Inflate][24], [Offset][25], [Overlaps][26], [Set][27], [ToPolygon][28]

### Static Properties

[Infinite][29], [InvertedInfinite][30], [One][31], [Zero][32]

### Static Methods

[FromPoints][33], [Inflate][24], [Merge][34], [Offset][25]

## Fields

#### Instance

| Name        | Type  | Summary                             |
|-------------|-------|-------------------------------------|
| [Height][1] | `int` | The height of this rectangle.       |
| [Width][2]  | `int` | The width of this rectangle.        |
| [X][3]      | `int` | The x-coordinate of this rectangle. |
| [Y][4]      | `int` | The y-coordinate of this rectangle. |

## Properties

#### Instance

| Name             | Type            | Summary                                                                |
|------------------|-----------------|------------------------------------------------------------------------|
| [Area][5]        | `int`           | Gets the area of this rectangle.                                       |
| [Bottom][6]      | `int`           | Gets the bottom extent of this rectangle.                              |
| [BottomLeft][7]  | [IntVector][35] | Gets the bottom left corner of this rectangle.                         |
| [BottomRight][8] | [IntVector][35] | Gets the bottom right corner of this rectangle.                        |
| [Center][9]      | [IntVector][35] | Gets or sets the center position of this rectangle.                    |
| [IsValid][10]    | `bool`          | Determines if the values of this rectangle are considered to be val... |
| [Left][11]       | `int`           | Gets the left extent of this rectangle.                                |
| [Max][12]        | [IntVector][35] | Gets the maximum corner of this rectangle.                             |
| [Min][13]        | [IntVector][35] | Gets the minimum corner of this rectangle.                             |
| [Position][14]   | [IntVector][35] | Gets or sets the position of this rectangle.                           |
| [Right][15]      | `int`           | Gets the right extent of this rectangle.                               |
| [Size][16]       | [IntSize][36]   | Gets or sets the size of this rectangle.                               |
| [Top][17]        | `int`           | Gets the top extent of this rectangle.                                 |
| [TopLeft][18]    | [IntVector][35] | Gets the top left corner of this rectangle.                            |
| [TopRight][19]   | [IntVector][35] | Gets the top right corner of this rectangle.                           |

#### Static

| Name                   | Type               | Summary                                                                |
|------------------------|--------------------|------------------------------------------------------------------------|
| [Infinite][29]         | [IntRectangle][37] | A rectangle that spans the entire 2D integer plane.                    |
| [InvertedInfinite][30] | [IntRectangle][37] | A rectangle that spans the entire 2D integer plane (but inverted, w... |
| [One][31]              | [IntRectangle][37] | A 1x1 rectangle that is positioned at the origin.                      |
| [Zero][32]             | [IntRectangle][37] | A 0x0 rectangle that is positioned at the origin.                      |

## Methods

#### Instance

| Name                            | Return Type     | Summary                                                        |
|---------------------------------|-----------------|----------------------------------------------------------------|
| [ClosestPoint(in IntVec...][20] | [IntVector][35] | Returns the nearest point on the rectangle to the given point. |
| [Contains(in Vector)][21]       | `bool`          | Determines if this rectangle contains the given point?         |
| [Contains(in IntVector)][21]    | `bool`          | Determines if this rectangle contains the given point?         |
| [Contains(in IntRectangle)][21] | `bool`          | Determines if this rectangle contains another rectangle?       |
| [Deconstruct(out int, o...][22] | `void`          |                                                                |
| [Deconstruct(out IntVec...][22] | `void`          |                                                                |
| [Include(IntVector)][23]        | `void`          | Mutates this rectangle to accommodate the given point.         |
| [Include(in IntRectangle)][23]  | `void`          | Mutates this rectangle to accommodate the given rectangle.     |
| [Inflate(int)][24]              | `void`          | Expands (or shrinks) the rectangle by a factor on both axis.   |
| [Inflate(int, int)][24]         | `void`          | Expands (or shrinks) the rectangle by a factor on each axis.   |
| [Offset(int, int)][25]          | `void`          | Translates this rectangle.                                     |
| [Offset(IntVector)][25]         | `void`          | Translates this rectangle.                                     |
| [Overlaps(IntRectangle)][26]    | `bool`          | Determines if this rectangle overlaps another rectangle.       |
| [Set(int, int, int, int)][27]   | `void`          | Sets the components of this rectangle.                         |
| [ToPolygon()][28]               | [Polygon][38]   | Create a polygon from this rectangle.                          |

#### Static

| Name                            | Return Type        | Summary                                                            |
|---------------------------------|--------------------|--------------------------------------------------------------------|
| [FromPoints(params IntV...][33] | [IntRectangle][37] |                                                                    |
| [FromPoints(IEnumerable...][33] | [IntRectangle][37] | Computes the bounding rectangle of the given set of points.        |
| [Inflate(IntRectangle, ...][24] | [IntRectangle][37] | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate(IntRectangle, ...][24] | [IntRectangle][37] | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [Merge(in IntRectangle,...][34] | [IntRectangle][37] | Merges the given rectangles into one potentially larger rectangle. |
| [Merge(params IntRectan...][34] | [IntRectangle][37] |                                                                    |
| [Offset(IntRectangle, i...][25] | [IntRectangle][37] | Copies and translates the given rectangle.                         |
| [Offset(IntRectangle, I...][25] | [IntRectangle][37] | Copies and translates the given rectangle.                         |

[0]: ../../Heirloom.Core.md
[1]: IntRectangle/Height.md
[2]: IntRectangle/Width.md
[3]: IntRectangle/X.md
[4]: IntRectangle/Y.md
[5]: IntRectangle/Area.md
[6]: IntRectangle/Bottom.md
[7]: IntRectangle/BottomLeft.md
[8]: IntRectangle/BottomRight.md
[9]: IntRectangle/Center.md
[10]: IntRectangle/IsValid.md
[11]: IntRectangle/Left.md
[12]: IntRectangle/Max.md
[13]: IntRectangle/Min.md
[14]: IntRectangle/Position.md
[15]: IntRectangle/Right.md
[16]: IntRectangle/Size.md
[17]: IntRectangle/Top.md
[18]: IntRectangle/TopLeft.md
[19]: IntRectangle/TopRight.md
[20]: IntRectangle/ClosestPoint.md
[21]: IntRectangle/Contains.md
[22]: IntRectangle/Deconstruct.md
[23]: IntRectangle/Include.md
[24]: IntRectangle/Inflate.md
[25]: IntRectangle/Offset.md
[26]: IntRectangle/Overlaps.md
[27]: IntRectangle/Set.md
[28]: IntRectangle/ToPolygon.md
[29]: IntRectangle/Infinite.md
[30]: IntRectangle/InvertedInfinite.md
[31]: IntRectangle/One.md
[32]: IntRectangle/Zero.md
[33]: IntRectangle/FromPoints.md
[34]: IntRectangle/Merge.md
[35]: IntVector.md
[36]: IntSize.md
[37]: IntRectangle.md
[38]: Polygon.md
