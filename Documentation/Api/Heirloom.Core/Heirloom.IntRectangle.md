# IntRectangle

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a rectangle defined with integer coordinates.

```cs
public struct IntRectangle : IEquatable<IntRectangle>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<IntRectangle>

**Fields**: [X][1], [Y][2], [Width][3], [Height][4]

**Properties**: [Area][5], [Size][6], [Position][7], [Center][8], [Min][9], [Max][10], [Left][11], [Top][12], [Right][13], [Bottom][14], [TopLeft][15], [BottomLeft][16], [BottomRight][17], [TopRight][18], [IsValid][19]

**Methods**: [Set][20], [ToPolygon][21], [Offset][22], [Include][23], [Inflate][24], [ClosestPoint][25], [Contains][26], [Overlaps][27], [Deconstruct][28]

**Static Properties**: [InvertedInfinite][29], [Infinite][30], [One][31], [Zero][32]

**Static Methods**: [Offset][22], [Merge][33], [Inflate][24], [FromPoints][34]

--------------------------------------------------------------------------------

## Constructors

### IntRectangle(int, int, int, int)

```cs
public IntRectangle(int x, int y, int width, int height)
```

### IntRectangle(IntVector, IntSize)

```cs
public IntRectangle(IntVector position, IntSize size)
```

### IntRectangle(IntVector, IntVector)

```cs
public IntRectangle(IntVector min, IntVector max)
```

## Fields

| Name        | Summary                             |
|-------------|-------------------------------------|
| [X][1]      | The x-coordinate of this rectangle. |
| [Y][2]      | The y-coordinate of this rectangle. |
| [Width][3]  | The width of this rectangle.        |
| [Height][4] | The height of this rectangle.       |

## Properties

| Name                   | Summary                                                                                                                           |
|------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| [Area][5]              | Gets the area of this rectangle.                                                                                                  |
| [Size][6]              | Gets or sets the size of this rectangle.                                                                                          |
| [Position][7]          | Gets or sets the position of this rectangle.                                                                                      |
| [Center][8]            | Gets or sets the center position of this rectangle.                                                                               |
| [Min][9]               | Gets the minimum corner of this rectangle.                                                                                        |
| [Max][10]              | Gets the maximum corner of this rectangle.                                                                                        |
| [Left][11]             | Gets the left extent of this rectangle.                                                                                           |
| [Top][12]              | Gets the top extent of this rectangle.                                                                                            |
| [Right][13]            | Gets the right extent of this rectangle.                                                                                          |
| [Bottom][14]           | Gets the bottom extent of this rectangle.                                                                                         |
| [TopLeft][15]          | Gets the top left corner of this rectangle.                                                                                       |
| [BottomLeft][16]       | Gets the bottom left corner of this rectangle.                                                                                    |
| [BottomRight][17]      | Gets the bottom right corner of this rectangle.                                                                                   |
| [TopRight][18]         | Gets the top right corner of this rectangle.                                                                                      |
| [IsValid][19]          | Determines if the values of this rectangle are considered to be valid or in other words that left &lt; right and top &lt; bottom. |
| [InvertedInfinite][29] | A rectangle that spans the entire 2D integer plane (but inverted, with min and max reversed).                                     |
| [Infinite][30]         | A rectangle that spans the entire 2D integer plane.                                                                               |
| [One][31]              | A 1x1 rectangle that is positioned at the origin.                                                                                 |
| [Zero][32]             | A 0x0 rectangle that is positioned at the origin.                                                                                 |

## Methods

| Name               | Summary                                                            |
|--------------------|--------------------------------------------------------------------|
| [Set][20]          | Sets the components of this rectangle.                             |
| [ToPolygon][21]    | Create a polygon from this rectangle.                              |
| [Offset][22]       | Translates this rectangle.                                         |
| [Offset][22]       | Translates this rectangle.                                         |
| [Include][23]      | Mutates this rectangle to accommodate the given point.             |
| [Include][23]      | Mutates this rectangle to accommodate the given rectangle.         |
| [Inflate][24]      | Expands (or shrinks) the rectangle by a factor on both axis.       |
| [Inflate][24]      | Expands (or shrinks) the rectangle by a factor on each axis.       |
| [ClosestPoint][25] | Returns the nearest point on the rectangle to the given point.     |
| [Contains][26]     | Determines if this rectangle contains the given point?             |
| [Contains][26]     | Determines if this rectangle contains the given point?             |
| [Contains][26]     | Determines if this rectangle contains another rectangle?           |
| [Overlaps][27]     | Determines if this rectangle overlaps another rectangle.           |
| [Deconstruct][28]  |                                                                    |
| [Deconstruct][28]  |                                                                    |
| [Offset][22]       | Copies and translates the given rectangle.                         |
| [Offset][22]       | Copies and translates the given rectangle.                         |
| [Merge][33]        | Merges the given rectangles into one potentially larger rectangle. |
| [Merge][33]        |                                                                    |
| [Inflate][24]      | Expands (or shrinks) the input rectangle by a factor on both axis. |
| [Inflate][24]      | Expands (or shrinks) the input rectangle by a factor on each axis. |
| [FromPoints][34]   |                                                                    |
| [FromPoints][34]   | Computes the bounding rectangle of the given set of points.        |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IntRectangle.X.md
[2]: Heirloom.IntRectangle.Y.md
[3]: Heirloom.IntRectangle.Width.md
[4]: Heirloom.IntRectangle.Height.md
[5]: Heirloom.IntRectangle.Area.md
[6]: Heirloom.IntRectangle.Size.md
[7]: Heirloom.IntRectangle.Position.md
[8]: Heirloom.IntRectangle.Center.md
[9]: Heirloom.IntRectangle.Min.md
[10]: Heirloom.IntRectangle.Max.md
[11]: Heirloom.IntRectangle.Left.md
[12]: Heirloom.IntRectangle.Top.md
[13]: Heirloom.IntRectangle.Right.md
[14]: Heirloom.IntRectangle.Bottom.md
[15]: Heirloom.IntRectangle.TopLeft.md
[16]: Heirloom.IntRectangle.BottomLeft.md
[17]: Heirloom.IntRectangle.BottomRight.md
[18]: Heirloom.IntRectangle.TopRight.md
[19]: Heirloom.IntRectangle.IsValid.md
[20]: Heirloom.IntRectangle.Set.md
[21]: Heirloom.IntRectangle.ToPolygon.md
[22]: Heirloom.IntRectangle.Offset.md
[23]: Heirloom.IntRectangle.Include.md
[24]: Heirloom.IntRectangle.Inflate.md
[25]: Heirloom.IntRectangle.ClosestPoint.md
[26]: Heirloom.IntRectangle.Contains.md
[27]: Heirloom.IntRectangle.Overlaps.md
[28]: Heirloom.IntRectangle.Deconstruct.md
[29]: Heirloom.IntRectangle.InvertedInfinite.md
[30]: Heirloom.IntRectangle.Infinite.md
[31]: Heirloom.IntRectangle.One.md
[32]: Heirloom.IntRectangle.Zero.md
[33]: Heirloom.IntRectangle.Merge.md
[34]: Heirloom.IntRectangle.FromPoints.md
