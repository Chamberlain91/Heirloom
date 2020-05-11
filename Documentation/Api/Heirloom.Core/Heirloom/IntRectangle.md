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

[ClosestPoint][20], [Contains][21], [Deconstruct][22], [Equals][23], [GetHashCode][24], [Include][25], [Inflate][26], [Offset][27], [Overlaps][28], [Set][29], [ToPolygon][30], [ToString][31]

### Static Properties

[Infinite][32], [InvertedInfinite][33], [One][34], [Zero][35]

### Static Methods

[FromPoints][36], [Inflate][26], [Merge][37], [Offset][27]

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
| [BottomLeft][7]  | [IntVector][38] | Gets the bottom left corner of this rectangle.                         |
| [BottomRight][8] | [IntVector][38] | Gets the bottom right corner of this rectangle.                        |
| [Center][9]      | [IntVector][38] | Gets or sets the center position of this rectangle.                    |
| [IsValid][10]    | `bool`          | Determines if the values of this rectangle are considered to be val... |
| [Left][11]       | `int`           | Gets the left extent of this rectangle.                                |
| [Max][12]        | [IntVector][38] | Gets the maximum corner of this rectangle.                             |
| [Min][13]        | [IntVector][38] | Gets the minimum corner of this rectangle.                             |
| [Position][14]   | [IntVector][38] | Gets or sets the position of this rectangle.                           |
| [Right][15]      | `int`           | Gets the right extent of this rectangle.                               |
| [Size][16]       | [IntSize][39]   | Gets or sets the size of this rectangle.                               |
| [Top][17]        | `int`           | Gets the top extent of this rectangle.                                 |
| [TopLeft][18]    | [IntVector][38] | Gets the top left corner of this rectangle.                            |
| [TopRight][19]   | [IntVector][38] | Gets the top right corner of this rectangle.                           |

#### Static

| Name                   | Type               | Summary                                                                |
|------------------------|--------------------|------------------------------------------------------------------------|
| [Infinite][32]         | [IntRectangle][40] | A rectangle that spans the entire 2D integer plane.                    |
| [InvertedInfinite][33] | [IntRectangle][40] | A rectangle that spans the entire 2D integer plane (but inverted, w... |
| [One][34]              | [IntRectangle][40] | A 1x1 rectangle that is positioned at the origin.                      |
| [Zero][35]             | [IntRectangle][40] | A 0x0 rectangle that is positioned at the origin.                      |

## Methods

#### Instance

| Name                            | Return Type     | Summary                                                        |
|---------------------------------|-----------------|----------------------------------------------------------------|
| [ClosestPoint(in IntVec...][20] | [IntVector][38] | Returns the nearest point on the rectangle to the given point. |
| [Contains(in Vector)][21]       | `bool`          | Determines if this rectangle contains the given point?         |
| [Contains(in IntVector)][21]    | `bool`          | Determines if this rectangle contains the given point?         |
| [Contains(in IntRectangle)][21] | `bool`          | Determines if this rectangle contains another rectangle?       |
| [Deconstruct(out int, o...][22] | `void`          |                                                                |
| [Deconstruct(out IntVec...][22] | `void`          |                                                                |
| [Equals(object)][23]            | `bool`          |                                                                |
| [Equals(IntRectangle)][23]      | `bool`          |                                                                |
| [GetHashCode()][24]             | `int`           |                                                                |
| [Include(IntVector)][25]        | `void`          | Mutates this rectangle to accommodate the given point.         |
| [Include(in IntRectangle)][25]  | `void`          | Mutates this rectangle to accommodate the given rectangle.     |
| [Inflate(int)][26]              | `void`          | Expands (or shrinks) the rectangle by a factor on both axis.   |
| [Inflate(int, int)][26]         | `void`          | Expands (or shrinks) the rectangle by a factor on each axis.   |
| [Offset(int, int)][27]          | `void`          | Translates this rectangle.                                     |
| [Offset(IntVector)][27]         | `void`          | Translates this rectangle.                                     |
| [Overlaps(IntRectangle)][28]    | `bool`          | Determines if this rectangle overlaps another rectangle.       |
| [Set(int, int, int, int)][29]   | `void`          | Sets the components of this rectangle.                         |
| [ToPolygon()][30]               | [Polygon][41]   | Create a polygon from this rectangle.                          |
| [ToString()][31]                | `string`        |                                                                |

#### Static

| Name                            | Return Type        | Summary                                                            |
|---------------------------------|--------------------|--------------------------------------------------------------------|
| [FromPoints(params IntV...][36] | [IntRectangle][40] |                                                                    |
| [FromPoints(IEnumerable...][36] | [IntRectangle][40] | Computes the bounding rectangle of the given set of points.        |
| [Inflate(IntRectangle, ...][26] | [IntRectangle][40] | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate(IntRectangle, ...][26] | [IntRectangle][40] | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [Merge(in IntRectangle,...][37] | [IntRectangle][40] | Merges the given rectangles into one potentially larger rectangle. |
| [Merge(params IntRectan...][37] | [IntRectangle][40] |                                                                    |
| [Offset(IntRectangle, i...][27] | [IntRectangle][40] | Copies and translates the given rectangle.                         |
| [Offset(IntRectangle, I...][27] | [IntRectangle][40] | Copies and translates the given rectangle.                         |

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
[23]: IntRectangle/Equals.md
[24]: IntRectangle/GetHashCode.md
[25]: IntRectangle/Include.md
[26]: IntRectangle/Inflate.md
[27]: IntRectangle/Offset.md
[28]: IntRectangle/Overlaps.md
[29]: IntRectangle/Set.md
[30]: IntRectangle/ToPolygon.md
[31]: IntRectangle/ToString.md
[32]: IntRectangle/Infinite.md
[33]: IntRectangle/InvertedInfinite.md
[34]: IntRectangle/One.md
[35]: IntRectangle/Zero.md
[36]: IntRectangle/FromPoints.md
[37]: IntRectangle/Merge.md
[38]: IntVector.md
[39]: IntSize.md
[40]: IntRectangle.md
[41]: ../Heirloom.Geometry/Polygon.md
