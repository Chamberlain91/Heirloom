# PolygonTools

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Provides several operations for polygons represented as a read-only list of vectors.

```cs
public static class PolygonTools
```

--------------------------------------------------------------------------------

**Static Methods**: [GetClosestPoint][1], [GetClosestPointOutline][2], [ContainsPoint][3], [Overlaps][4], [Raycast][5], [Project][6], [DecomposeConvex][7], [DecomposeConvexIndices][8], [Triangulate][9], [TriangulateIndices][10], [IsConvexVertex][11], [IsConvexPolygon][12], [ComputeMetrics][13], [GetNormal][14]

--------------------------------------------------------------------------------

## Methods

| Name                        | Summary                                                                                                                               |
|-----------------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| [GetClosestPoint][1]        | Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned. |
| [GetClosestPointOutline][2] | Gets the closest point on the polygon outline to the specified point.                                                                 |
| [ContainsPoint][3]          | Assuming the polygon is convex, checks if the point is contained.                                                                     |
| [Overlaps][4]               | Tests if a (convex) polygon overlaps the specified shape.                                                                             |
| [Raycast][5]                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast][5]                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast][5]                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Raycast][5]                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Project][6]                | Project a polygon onto the specified axis.                                                                                            |
| [DecomposeConvex][7]        | Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.                  |
| [DecomposeConvexIndices][8] | Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.                             |
| [Triangulate][9]            | Decomposes a simple polygon into constituent triangles.                                                                               |
| [TriangulateIndices][10]    | Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.                                 |
| [IsConvexVertex][11]        | Determines if the ith vertex is a convex (clockwise) vertex.                                                                          |
| [IsConvexPolygon][12]       | Determines if the polygon is considered convex (non-concave and oriented clockwise).                                                  |
| [IsConvexVertex][11]        | Determines if the vertex ' `vCurr` ' is convex (clockwise).                                                                           |
| [ComputeMetrics][13]        |                                                                                                                                       |
| [GetNormal][14]             | Vector perpendicular to the i-th edge.                                                                                                |

[0]: ../Heirloom.Core.md
[1]: Heirloom.PolygonTools.GetClosestPoint.md
[2]: Heirloom.PolygonTools.GetClosestPointOutline.md
[3]: Heirloom.PolygonTools.ContainsPoint.md
[4]: Heirloom.PolygonTools.Overlaps.md
[5]: Heirloom.PolygonTools.Raycast.md
[6]: Heirloom.PolygonTools.Project.md
[7]: Heirloom.PolygonTools.DecomposeConvex.md
[8]: Heirloom.PolygonTools.DecomposeConvexIndices.md
[9]: Heirloom.PolygonTools.Triangulate.md
[10]: Heirloom.PolygonTools.TriangulateIndices.md
[11]: Heirloom.PolygonTools.IsConvexVertex.md
[12]: Heirloom.PolygonTools.IsConvexPolygon.md
[13]: Heirloom.PolygonTools.ComputeMetrics.md
[14]: Heirloom.PolygonTools.GetNormal.md
