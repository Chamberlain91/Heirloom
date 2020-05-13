# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools (Class)

> **Namespace**: [Heirloom.Geometry][0]

Provides several operations for polygons represented as a read-only list of vectors.

```cs
public static class PolygonTools
```

### Static Methods

[ComputeMetrics][1], [ContainsPoint][2], [DecomposeConvex][3], [DecomposeConvexIndices][4], [GetClosestPoint][5], [GetClosestPointOutline][6], [GetNormal][7], [IsConvexPolygon][8], [IsConvexVertex][9], [Overlaps][10], [Project][11], [Raycast][12], [Triangulate][13], [TriangulateIndices][14]

## Methods

| Name                            | Return Type                              | Summary                                                                |
|---------------------------------|------------------------------------------|------------------------------------------------------------------------|
| [ComputeMetrics(IReadOn...][1]  | `void`                                   | Computes general metrics about the specified polygon. Outputs the `... |
| [ContainsPoint(IReadOnl...][2]  | `bool`                                   | Assuming the polygon is convex, checks if the point is contained.      |
| [DecomposeConvex(IReadO...][3]  | `IEnumerable<Polygon>`                   | Converts a simple polygon into one or more convex polygons. If the ... |
| [DecomposeConvexIndices...][4]  | `IEnumerable<IReadOnlyList<int>>`        | Converts a simple polygon into one or more convex polygons enumerat... |
| [GetClosestPoint(IReadO...][5]  | [Vector][15]                             | Gets the closest point on the polygon to the specified point. If th... |
| [GetClosestPointOutline...][6]  | [Vector][15]                             | Gets the closest point on the polygon outline to the specified point.  |
| [GetNormal(IReadOnlyLis...][7]  | [Vector][15]                             | Vector perpendicular to the i-th edge.                                 |
| [IsConvexPolygon(IReadO...][8]  | `bool`                                   | Determines if the polygon is considered convex (non-concave and ori... |
| [IsConvexVertex(IReadOn...][9]  | `bool`                                   | Determines if the ith vertex is a convex (clockwise) vertex.           |
| [IsConvexVertex(in Vect...][9]  | `bool`                                   | Determines if the vertex ' `vCurr` ' is convex (clockwise).            |
| [Overlaps(IReadOnlyList...][10] | `bool`                                   | Tests if a (convex) polygon overlaps the specified shape.              |
| [Project(IReadOnlyList<...][11] | [Range][16]                              | Project a polygon onto the specified axis.                             |
| [Raycast(IReadOnlyList<...][12] | `bool`                                   | Checks if a ray intersects this polygon.                               |
| [Raycast(IReadOnlyList<...][12] | `bool`                                   | Checks if a ray intersects this polygon.                               |
| [Raycast(IReadOnlyList<...][12] | `bool`                                   | Checks if a ray intersects this polygon and outputs information on ... |
| [Raycast(IReadOnlyList<...][12] | `bool`                                   | Checks if a ray intersects this polygon and outputs information on ... |
| [Triangulate(IReadOnlyL...][13] | `IEnumerable<Triangle>`                  | Decomposes a simple polygon into constituent triangles.                |
| [TriangulateIndices(IEn...][14] | `IEnumerable<ValueTuple<int, int, int>>` | Decomposes a simple polygon into constituent triangles enumerated b... |

[0]: ../../Heirloom.Core.md
[1]: PolygonTools/ComputeMetrics.md
[2]: PolygonTools/ContainsPoint.md
[3]: PolygonTools/DecomposeConvex.md
[4]: PolygonTools/DecomposeConvexIndices.md
[5]: PolygonTools/GetClosestPoint.md
[6]: PolygonTools/GetClosestPointOutline.md
[7]: PolygonTools/GetNormal.md
[8]: PolygonTools/IsConvexPolygon.md
[9]: PolygonTools/IsConvexVertex.md
[10]: PolygonTools/Overlaps.md
[11]: PolygonTools/Project.md
[12]: PolygonTools/Raycast.md
[13]: PolygonTools/Triangulate.md
[14]: PolygonTools/TriangulateIndices.md
[15]: ../Heirloom/Vector.md
[16]: ../Heirloom/Range.md
