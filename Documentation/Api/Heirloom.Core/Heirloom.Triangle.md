# Triangle

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

```cs
public struct Triangle : IShape, IEquatable<Triangle>, IEnumerable<Vector>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [IShape][1], IEquatable\<Triangle>, IEnumerable\<Vector>, IEnumerable

**Fields**: [A][2], [B][3], [C][4]

**Properties**: [Bounds][5], [Area][6], [Centroid][7], [Item][8]

**Methods**: [Set][9], [ToPolygon][10], [GetClosestPoint][11], [Contains][12], [Overlaps][13], [Project][14], [Raycast][15], [Barycentric][16], [GetEdge][17], [Deconstruct][18], [GetEnumerator][19]

**Static Methods**: [ContainsPoint][20], [Barycentric][16], [CreateCircumcircle][21]

--------------------------------------------------------------------------------

## Fields

| Name   | Summary           |
|--------|-------------------|
| [A][2] | The first point.  |
| [B][3] | The second point. |
| [C][4] | The third point.  |

## Properties

| Name          | Summary                                              |
|---------------|------------------------------------------------------|
| [Bounds][5]   | Gets the bounds of this triangle.                    |
| [Area][6]     | Gets the area of this triangle.                      |
| [Centroid][7] | Gets the center of triangle (mean of corner points). |
| [Item][8]     |                                                      |

## Methods

| Name                     | Summary                                                                                      |
|--------------------------|----------------------------------------------------------------------------------------------|
| [Set][9]                 | Sets each point of the triangle.                                                             |
| [ToPolygon][10]          | Create a polygon from this triangle.                                                         |
| [GetClosestPoint][11]    | Gets the closest point on the triangle to the specified point.                               |
| [Contains][12]           | Determines if this triangle contains the specified point.                                    |
| [Overlaps][13]           | Determines if this triangle overlaps another shape.                                          |
| [Overlaps][13]           | Determines if this triangle overlaps the specified circle.                                   |
| [Overlaps][13]           | Determines if this triangle overlaps another triangle.                                       |
| [Overlaps][13]           | Determines if this triangle overlaps the specified rectangle.                                |
| [Overlaps][13]           | Determines if this triangle overlaps the specified convex polygon.                           |
| [Overlaps][13]           | Determines if this triangle overlaps the specified simple polygon.                           |
| [Project][14]            | Project this polygon onto the specified axis.                                                |
| [Raycast][15]            | Peforms a raycast onto this rectangle, returning true upon intersection.                     |
| [Raycast][15]            | Peforms a raycast onto this rectangle, returning true upon intersection.                     |
| [Barycentric][16]        | Computes the barycentric coefficients of the point `p` within the triangle.                  |
| [GetEdge][17]            |                                                                                              |
| [Deconstruct][18]        |                                                                                              |
| [GetEnumerator][19]      |                                                                                              |
| [ContainsPoint][20]      | Determines if the triangle defined by `a` , `b` , `c` contains the specified point.          |
| [Barycentric][16]        | Computes the barycentric coefficients of the point `p` within the triangle `a` , `b` , `c` . |
| [CreateCircumcircle][21] | Computes the circumcircle for the specified triangle.                                        |
| [CreateCircumcircle][21] | Computes the circumcircle for the specified triangle.                                        |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IShape.md
[2]: Heirloom.Triangle.A.md
[3]: Heirloom.Triangle.B.md
[4]: Heirloom.Triangle.C.md
[5]: Heirloom.Triangle.Bounds.md
[6]: Heirloom.Triangle.Area.md
[7]: Heirloom.Triangle.Centroid.md
[8]: Heirloom.Triangle.Item.md
[9]: Heirloom.Triangle.Set.md
[10]: Heirloom.Triangle.ToPolygon.md
[11]: Heirloom.Triangle.GetClosestPoint.md
[12]: Heirloom.Triangle.Contains.md
[13]: Heirloom.Triangle.Overlaps.md
[14]: Heirloom.Triangle.Project.md
[15]: Heirloom.Triangle.Raycast.md
[16]: Heirloom.Triangle.Barycentric.md
[17]: Heirloom.Triangle.GetEdge.md
[18]: Heirloom.Triangle.Deconstruct.md
[19]: Heirloom.Triangle.GetEnumerator.md
[20]: Heirloom.Triangle.ContainsPoint.md
[21]: Heirloom.Triangle.CreateCircumcircle.md
