# Rectangle

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

```cs
public struct Rectangle : IShape, IEquatable<Rectangle>
```

--------------------------------------------------------------------------------

**Inherits**: [IShape][1], IEquatable\<Rectangle>

**Fields**: [X][2], [Y][3], [Width][4], [Height][5]

**Properties**: [Area][6], [Size][7], [Position][8], [Center][9], [Min][10], [Max][11], [Left][12], [Top][13], [Right][14], [Bottom][15], [TopLeft][16], [BottomLeft][17], [BottomRight][18], [TopRight][19], [IsValid][20]

**Methods**: [Set][21], [ToPolygon][22], [Offset][23], [Transform][24], [Include][25], [Inflate][26], [GetClosestPoint][27], [Contains][28], [Overlaps][29], [Project][30], [Raycast][31], [Deconstruct][32]

**Static Properties**: [InvertedInfinite][33], [Infinite][34], [One][35], [Zero][36]

**Static Methods**: [Offset][23], [Transform][24], [Merge][37], [Inflate][26], [FromPoints][38]

--------------------------------------------------------------------------------

## Constructors

### Rectangle(float, float, float, float)

```cs
public Rectangle(float x, float y, float width, float height)
```

### Rectangle(Vector, Size)

```cs
public Rectangle(Vector position, Size size)
```

### Rectangle(Vector, Vector)

```cs
public Rectangle(Vector min, Vector max)
```

## Fields

| Name        | Summary                             |
|-------------|-------------------------------------|
| [X][2]      | The x-coordinate of this rectangle. |
| [Y][3]      | The y-coordinate of this rectangle. |
| [Width][4]  | The width of this rectangle.        |
| [Height][5] | The height of this rectangle.       |

## Properties

| Name                   | Summary                                                                                                                                              |
|------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Area][6]              | Gets the area of this rectangle.                                                                                                                     |
| [Size][7]              | Gets or sets the size of this rectangle.                                                                                                             |
| [Position][8]          | Gets or sets the position of this rectangle.                                                                                                         |
| [Center][9]            | Gets or sets the center position of this rectangle.                                                                                                  |
| [Min][10]              | Gets the minimum corner of this rectangle.                                                                                                           |
| [Max][11]              | Gets the maximum corner of this rectangle.                                                                                                           |
| [Left][12]             | Gets the left extent of this rectangle.                                                                                                              |
| [Top][13]              | Gets the top extent of this rectangle.                                                                                                               |
| [Right][14]            | Gets the right extent of this rectangle.                                                                                                             |
| [Bottom][15]           | Gets the bottom extent of this rectangle.                                                                                                            |
| [TopLeft][16]          | Gets the top left corner of this rectangle.                                                                                                          |
| [BottomLeft][17]       | Gets the bottom left corner of this rectangle.                                                                                                       |
| [BottomRight][18]      | Gets the bottom right corner of this rectangle.                                                                                                      |
| [TopRight][19]         | Gets the top right corner of this rectangle.                                                                                                         |
| [IsValid][20]          | Determines if the values of this rectangle are considered to be valid or in other words that \<c>left &lt; right\</c> and \<c>top &lt; bottom\</c> . |
| [InvertedInfinite][33] | A rectangle that spans the entire 2D plane (but inverted, with min and max reversed).                                                                |
| [Infinite][34]         | A rectangle that spans the entire 2D plane.                                                                                                          |
| [One][35]              | A 1x1 rectangle that is positioned at the origin.                                                                                                    |
| [Zero][36]             | A 0x0 rectangle that is positioned at the origin.                                                                                                    |

## Methods

| Name                  | Summary                                                                                           |
|-----------------------|---------------------------------------------------------------------------------------------------|
| [Set][21]             | Sets the components of this rectangle.                                                            |
| [ToPolygon][22]       | Create a polygon from this rectangle.                                                             |
| [Offset][23]          | Translates this rectangle.                                                                        |
| [Offset][23]          | Translates this rectangle.                                                                        |
| [Transform][24]       | Transforms the four corners of this rectangle and updates itself to bound these points.           |
| [Include][25]         | Mutates this rectangle to accommodate the given point.                                            |
| [Include][25]         | Mutates this rectangle to accommodate the given rectangle.                                        |
| [Inflate][26]         | Expands (or shrinks) the rectangle by a factor on both axis.                                      |
| [Inflate][26]         | Expands (or shrinks) the rectangle by a factor on each axis.                                      |
| [GetClosestPoint][27] | Returns the nearest point on the rectangle to the given point.                                    |
| [Contains][28]        | Determines if this rectangle contains the given point?                                            |
| [Contains][28]        | Determines if this rectangle contains another rectangle?                                          |
| [Overlaps][29]        | Determines if this rectangle overlaps another shape.                                              |
| [Overlaps][29]        | Determines if this rectangle overlaps the specified circle.                                       |
| [Overlaps][29]        | Determines if this rectangle overlaps the specified triangle.                                     |
| [Overlaps][29]        | Determines if this rectangle overlaps another rectangle.                                          |
| [Overlaps][29]        | Determines if this rectangle overlaps the specified convex polygon.                               |
| [Overlaps][29]        | Determines if this rectangle overlaps the specified simple polygon.                               |
| [Project][30]         | Project this rectangle onto the specified axis.                                                   |
| [Raycast][31]         | Peforms a raycast onto this rectangle, returning true upon intersection.                          |
| [Raycast][31]         | Peforms a raycast onto this circle, returning true upon intersection.                             |
| [Deconstruct][32]     |                                                                                                   |
| [Deconstruct][32]     |                                                                                                   |
| [Offset][23]          | Copies and translates the given rectangle.                                                        |
| [Offset][23]          | Copies and translates the given rectangle.                                                        |
| [Transform][24]       | Transforms the four corners of this rectangle and returns the bounding rectangle of these points. |
| [Merge][37]           | Merges the given rectangles into one potentially larger rectangle.                                |
| [Merge][37]           |                                                                                                   |
| [Inflate][26]         | Expands (or shrinks) the input rectangle by a factor on both axis.                                |
| [Inflate][26]         | Expands (or shrinks) the input rectangle by a factor on each axis.                                |
| [FromPoints][38]      |                                                                                                   |
| [FromPoints][38]      | Computes the bounding rectangle of the given set of points.                                       |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IShape.md
[2]: Heirloom.Rectangle.X.md
[3]: Heirloom.Rectangle.Y.md
[4]: Heirloom.Rectangle.Width.md
[5]: Heirloom.Rectangle.Height.md
[6]: Heirloom.Rectangle.Area.md
[7]: Heirloom.Rectangle.Size.md
[8]: Heirloom.Rectangle.Position.md
[9]: Heirloom.Rectangle.Center.md
[10]: Heirloom.Rectangle.Min.md
[11]: Heirloom.Rectangle.Max.md
[12]: Heirloom.Rectangle.Left.md
[13]: Heirloom.Rectangle.Top.md
[14]: Heirloom.Rectangle.Right.md
[15]: Heirloom.Rectangle.Bottom.md
[16]: Heirloom.Rectangle.TopLeft.md
[17]: Heirloom.Rectangle.BottomLeft.md
[18]: Heirloom.Rectangle.BottomRight.md
[19]: Heirloom.Rectangle.TopRight.md
[20]: Heirloom.Rectangle.IsValid.md
[21]: Heirloom.Rectangle.Set.md
[22]: Heirloom.Rectangle.ToPolygon.md
[23]: Heirloom.Rectangle.Offset.md
[24]: Heirloom.Rectangle.Transform.md
[25]: Heirloom.Rectangle.Include.md
[26]: Heirloom.Rectangle.Inflate.md
[27]: Heirloom.Rectangle.GetClosestPoint.md
[28]: Heirloom.Rectangle.Contains.md
[29]: Heirloom.Rectangle.Overlaps.md
[30]: Heirloom.Rectangle.Project.md
[31]: Heirloom.Rectangle.Raycast.md
[32]: Heirloom.Rectangle.Deconstruct.md
[33]: Heirloom.Rectangle.InvertedInfinite.md
[34]: Heirloom.Rectangle.Infinite.md
[35]: Heirloom.Rectangle.One.md
[36]: Heirloom.Rectangle.Zero.md
[37]: Heirloom.Rectangle.Merge.md
[38]: Heirloom.Rectangle.FromPoints.md
