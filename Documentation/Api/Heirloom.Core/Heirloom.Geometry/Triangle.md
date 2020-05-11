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

[Barycentric][9], [Contains][10], [Deconstruct][11], [Equals][12], [GetEdge][13], [GetEnumerator][14], [GetHashCode][15], [GetNearestPoint][16], [Overlaps][17], [Project][18], [Raycast][19], [Set][20], [ToPolygon][21], [ToString][22]

### Static Methods

[Barycentric][9], [ContainsPoint][23], [CreateCircumcircle][24]

## Fields

#### Instance

| Name   | Type         | Summary           |
|--------|--------------|-------------------|
| [A][2] | [Vector][25] | The first point.  |
| [B][3] | [Vector][25] | The second point. |
| [C][4] | [Vector][25] | The third point.  |

## Properties

#### Instance

| Name          | Type            | Summary                                              |
|---------------|-----------------|------------------------------------------------------|
| [Area][5]     | `float`         | Gets the area of this triangle.                      |
| [Bounds][6]   | [Rectangle][26] | Gets the bounds of this triangle.                    |
| [Centroid][7] | [Vector][25]    | Gets the center of triangle (mean of corner points). |
| [Indexer][8]  | [Vector][25]    |                                                      |

## Methods

#### Instance

| Name                            | Return Type           | Summary                                                                |
|---------------------------------|-----------------------|------------------------------------------------------------------------|
| [Barycentric(in Vector,...][9]  | `void`                | Computes the barycentric coefficients of the point `p` within the t... |
| [Contains(in Vector)][10]       | `bool`                | Determines if this triangle contains the specified point.              |
| [Deconstruct(out Vector...][11] | `void`                |                                                                        |
| [Equals(object)][12]            | `bool`                |                                                                        |
| [Equals(Triangle)][12]          | `bool`                |                                                                        |
| [GetEdge(int)][13]              | [LineSegment][27]     |                                                                        |
| [GetEnumerator()][14]           | `IEnumerator<Vector>` |                                                                        |
| [GetHashCode()][15]             | `int`                 |                                                                        |
| [GetNearestPoint(in Vec...][16] | [Vector][25]          | Gets the closest point on the triangle to the specified point.         |
| [Overlaps(IShape)][17]          | `bool`                | Determines if this triangle overlaps another shape.                    |
| [Overlaps(in Circle)][17]       | `bool`                | Determines if this triangle overlaps the specified circle.             |
| [Overlaps(in Triangle)][17]     | `bool`                | Determines if this triangle overlaps another triangle.                 |
| [Overlaps(in Rectangle)][17]    | `bool`                | Determines if this triangle overlaps the specified rectangle.          |
| [Overlaps(IReadOnlyList...][17] | `bool`                | Determines if this triangle overlaps the specified convex polygon.     |
| [Overlaps(Polygon)][17]         | `bool`                | Determines if this triangle overlaps the specified simple polygon.     |
| [Project(in Vector)][18]        | [Range][28]           | Project this polygon onto the specified axis.                          |
| [Raycast(in Ray)][19]           | `bool`                | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Raycast(in Ray, out Ra...][19] | `bool`                | Peforms a raycast onto this rectangle, returning true upon intersec... |
| [Set(in Vector, in Vect...][20] | `void`                | Sets each point of the triangle.                                       |
| [ToPolygon()][21]               | [Polygon][29]         | Create a polygon from this triangle.                                   |
| [ToString()][22]                | `string`              |                                                                        |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Barycentric(in Vector,...][9]  | `void`       | Computes the barycentric coefficients of the point `p` within the t... |
| [ContainsPoint(in Vecto...][23] | `bool`       | Determines if the triangle defined by `a` , `b` , `c` contains the ... |
| [CreateCircumcircle(in ...][24] | [Circle][30] | Computes the circumcircle for the specified triangle.                  |
| [CreateCircumcircle(in ...][24] | [Circle][30] | Computes the circumcircle for the specified triangle.                  |

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
[12]: Triangle/Equals.md
[13]: Triangle/GetEdge.md
[14]: Triangle/GetEnumerator.md
[15]: Triangle/GetHashCode.md
[16]: Triangle/GetNearestPoint.md
[17]: Triangle/Overlaps.md
[18]: Triangle/Project.md
[19]: Triangle/Raycast.md
[20]: Triangle/Set.md
[21]: Triangle/ToPolygon.md
[22]: Triangle/ToString.md
[23]: Triangle/ContainsPoint.md
[24]: Triangle/CreateCircumcircle.md
[25]: ../Heirloom/Vector.md
[26]: ../Heirloom/Rectangle.md
[27]: LineSegment.md
[28]: ../Heirloom/Range.md
[29]: Polygon.md
[30]: Circle.md
