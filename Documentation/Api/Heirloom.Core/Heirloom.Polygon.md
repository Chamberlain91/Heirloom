# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Polygon

> **Namespace**: [Heirloom][0]  

Represents a simple polygon.

```cs
public class Polygon : IShape
```

### Inherits

[IShape][1]

#### Properties

[Item][2], [Vertices][3], [Count][4], [Area][5], [Center][6], [Centroid][7], [Bounds][8], [IsConvex][9], [ConvexPartitions][10]

#### Methods

[Clear][11], [Add][12], [Insert][13], [RemoveAt][14], [GetClosestPoint][15], [Contains][16], [Overlaps][17], [Project][18], [Raycast][19], [Triangulate][20]

#### Static Methods

[CreateFromShape][21], [CreateConvexHull][22], [CreateRegularPolygon][23], [CreateStar][24]

## Properties

| Name                   | Summary                                                                                                                                           |
|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| [Item][2]              |                                                                                                                                                   |
| [Vertices][3]          | Gets a read-only view of this polygon's vertices.                                                                                                 |
| [Count][4]             | Gets the number of vertices in this polygon.                                                                                                      |
| [Area][5]              | Gets the area of the polygon.                                                                                                                     |
| [Center][6]            | Gets the center (point mean) of the polygon.                                                                                                      |
| [Centroid][7]          | Gets the centroid (area weighted) of the polygon.                                                                                                 |
| [Bounds][8]            | Gets the bounding rectangle of this polygon.                                                                                                      |
| [IsConvex][9]          | Gets a value determining if this polygon is convex (in clockwise ordering).                                                                       |
| [ConvexPartitions][10] | Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original. |

## Methods

| Name                       | Summary                                                                                |
|----------------------------|----------------------------------------------------------------------------------------|
| [Clear][11]                |                                                                                        |
| [Add][12]                  |                                                                                        |
| [Insert][13]               |                                                                                        |
| [RemoveAt][14]             |                                                                                        |
| [GetClosestPoint][15]      | Gets the nearest point on the polygon to the specified point.                          |
| [Contains][16]             | Determines if the specified point is contained by this polygon.                        |
| [Overlaps][17]             | Checks for an overlap between this polygon and another shape.                          |
| [Overlaps][17]             | Determines if this polygon overlaps the specified rectangle.                           |
| [Overlaps][17]             | Determines if this polygon overlaps the specified circle.                              |
| [Overlaps][17]             | Determines if this polygon overlaps the specified triangle.                            |
| [Overlaps][17]             | Determines if this polygon overlaps the specified triangle.                            |
| [Project][18]              | Project this polygon onto the specified axis.                                          |
| [Raycast][19]              | Checks if a ray intersects this polygon.                                               |
| [Raycast][19]              | Checks if a ray intersects this polygon and outputs information on the contact point.  |
| [Triangulate][20]          | Decompose this polygon into triangles.                                                 |
| [CreateFromShape][21]      | Constructs a polygon representation of the specified shape.                            |
| [CreateFromShape][21]      | Constructs a polygon representation of the specified triangle.                         |
| [CreateFromShape][21]      | Constructs a polygon representation of the specified rectangle.                        |
| [CreateFromShape][21]      | Constructs a polygon representation of the specified circle.                           |
| [CreateConvexHull][22]     | Constructs a convex polygon representing the convex hull of the specified point cloud. |
| [CreateRegularPolygon][23] | Construct a regular polygon.                                                           |
| [CreateRegularPolygon][23] | Construct a regular polygon.                                                           |
| [CreateStar][24]           | Creates a polygon shaped like a standard 5 point star centered on the origin.          |
| [CreateStar][24]           | Creates a polygon shaped like a standard 5 point star.                                 |
| [CreateStar][24]           | Creates a polygon shaped like a star centered on the origin.                           |
| [CreateStar][24]           | Creates a polygon shaped like a star.                                                  |
| [CreateStar][24]           | Creates a polygon shaped like a star.                                                  |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IShape.md
[2]: Heirloom.Polygon.Item.md
[3]: Heirloom.Polygon.Vertices.md
[4]: Heirloom.Polygon.Count.md
[5]: Heirloom.Polygon.Area.md
[6]: Heirloom.Polygon.Center.md
[7]: Heirloom.Polygon.Centroid.md
[8]: Heirloom.Polygon.Bounds.md
[9]: Heirloom.Polygon.IsConvex.md
[10]: Heirloom.Polygon.ConvexPartitions.md
[11]: Heirloom.Polygon.Clear.md
[12]: Heirloom.Polygon.Add.md
[13]: Heirloom.Polygon.Insert.md
[14]: Heirloom.Polygon.RemoveAt.md
[15]: Heirloom.Polygon.GetClosestPoint.md
[16]: Heirloom.Polygon.Contains.md
[17]: Heirloom.Polygon.Overlaps.md
[18]: Heirloom.Polygon.Project.md
[19]: Heirloom.Polygon.Raycast.md
[20]: Heirloom.Polygon.Triangulate.md
[21]: Heirloom.Polygon.CreateFromShape.md
[22]: Heirloom.Polygon.CreateConvexHull.md
[23]: Heirloom.Polygon.CreateRegularPolygon.md
[24]: Heirloom.Polygon.CreateStar.md
