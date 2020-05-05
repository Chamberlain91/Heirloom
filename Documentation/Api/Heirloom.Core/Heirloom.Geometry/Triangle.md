# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Triangle (Struct)

> **Namespace**: [Heirloom.Geometry][0]

```cs
public struct Triangle : IShape, IEquatable<Triangle>, IEnumerable<Vector>, IEnumerable
```

### Inherits

[IShape][1], IEquatable\<Triangle>, IEnumerable\<Vector>, IEnumerable

### Fields

[A][2], [B][3], [C][4]

### Properties

[Area][5], [Bounds][6], [Centroid][7], [Indexer][8]

### Methods

[Barycentric][9], [Contains][10], [Deconstruct][11], [GetEdge][12], [GetEnumerator][13], [GetNearestPoint][14], [Overlaps][15], [Project][16], [Raycast][17], [Set][18], [ToPolygon][19]

### Static Methods

[Barycentric][9], [ContainsPoint][20], [CreateCircumcircle][21]

## Fields

#### Instance

| Name   | Type         | Summary           |
|--------|--------------|-------------------|
| [A][2] | [Vector][22] | The first point.  |
| [B][3] | [Vector][22] | The second point. |
| [C][4] | [Vector][22] | The third point.  |

## Properties

#### Instance

| Name          | Type            | Summary                                              |
|---------------|-----------------|------------------------------------------------------|
| [Area][5]     | `float`         | Gets the area of this triangle.                      |
| [Bounds][6]   | [Rectangle][23] | Gets the bounds of this triangle.                    |
| [Centroid][7] | [Vector][22]    | Gets the center of triangle (mean of corner points). |
| [Indexer][8]  | [Vector][22]    |                                                      |

## Methods

#### Instance

| Name                            | Return Type           | Summary                                                                |
|---------------------------------|-----------------------|------------------------------------------------------------------------|
| [Barycentric(in Vector,...][9]  | `void`                | Computes the barycentric coefficients of the point `p` within the t... |
| [Contains(in Vector)][10]       | `bool`                | Determines if this triangle contains the specified point.              |
| [Deconstruct(out Vector...][11] | `void`                |                                                                        |
| [GetEdge(int)][12]              | [LineSegment][24]     |                                                                        |
| [GetEnumerator()][13]           | `IEnumerator<Vector>` |                                                                        |
| [GetNearestPoint(in Vec...][14] | [Vector][22]          | Gets the closest point on the triangle to the specified point.         |
| [Overlaps(IShape)][15]          | `bool`                | Determines if this triangle overlaps another shape.                    |
| [Overlaps(in Circle)][15]       | `bool`                | Determines if this triangle overlaps the specified circle.             |
| [Overlaps(in Triangle)][15]     | `bool`                | Determines if this triangle overlaps another triangle.                 |
| [Overlaps(in Rectangle)][15]    | `bool`                | Determines if this triangle overlaps the specified rectangle.          |
| [Overlaps(IReadOnlyList...][15] | `bool`                | Determines if this triangle overlaps the specified convex polygon.     |
| [Overlaps(Polygon)][15]         | `bool`                | Determines if this triangle overlaps the specified simple polygon.     |
| [Project(in Vector)][16]        | [Range][25]           | Project this polygon onto the specified axis.                          |
| [Raycast(in Ray)][17]           | `bool`                | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Raycast(in Ray, out Ra...][17] | `bool`                | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Set(in Vector, in Vect...][18] | `void`                | Sets each point of the triangle.                                       |
| [ToPolygon()][19]               | [Polygon][26]         | Create a polygon from this triangle.                                   |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Barycentric(in Vector,...][9]  | `void`       | Computes the barycentric coefficients of the point `p` within the t... |
| [ContainsPoint(in Vecto...][20] | `bool`       | Determines if the triangle defined by `a` , `b` , `c` contains the ... |
| [CreateCircumcircle(in ...][21] | [Circle][27] | Computes the circumcircle for the specified triangle.                  |
| [CreateCircumcircle(in ...][21] | [Circle][27] | Computes the circumcircle for the specified triangle.                  |

[0]: ../../Heirloom.Core.md
[1]: IShape.md
[2]: Triangle/A.md
[3]: Triangle/B.md
[4]: Triangle/C.md
[5]: Triangle/Area.md
[6]: Triangle/Bounds.md
[7]: Triangle/Centroid.md
[8]: Triangle/Indexer.md
[9]: Triangle/Barycentric.md
[10]: Triangle/Contains.md
[11]: Triangle/Deconstruct.md
[12]: Triangle/GetEdge.md
[13]: Triangle/GetEnumerator.md
[14]: Triangle/GetNearestPoint.md
[15]: Triangle/Overlaps.md
[16]: Triangle/Project.md
[17]: Triangle/Raycast.md
[18]: Triangle/Set.md
[19]: Triangle/ToPolygon.md
[20]: Triangle/ContainsPoint.md
[21]: Triangle/CreateCircumcircle.md
[22]: ../Heirloom/Vector.md
[23]: ../Heirloom/Rectangle.md
[24]: LineSegment.md
[25]: ../Heirloom/Range.md
[26]: Polygon.md
[27]: Circle.md
