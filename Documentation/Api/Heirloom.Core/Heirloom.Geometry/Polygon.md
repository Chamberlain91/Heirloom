# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon (Class)

> **Namespace**: [Heirloom.Geometry][0]

Represents a simple polygon.

```cs
public class Polygon : IShape
```

### Inherits

[IShape][1]

### Properties

[Area][2], [Bounds][3], [Center][4], [Centroid][5], [ConvexPartitions][6], [IsConvex][7], [Normals][8], [Vertices][9]

### Methods

[Add][10], [Clear][11], [Contains][12], [GetNearestPoint][13], [Insert][14], [Overlaps][15], [Project][16], [Raycast][17], [RemoveAt][18], [Triangulate][19]

### Static Methods

[CreateConvexHull][20], [CreateFromShape][21]

## Properties

#### Instance

| Name                  | Type                                   | Summary                                                                |
|-----------------------|----------------------------------------|------------------------------------------------------------------------|
| [Area][2]             | `float`                                | Gets the area of the polygon.                                          |
| [Bounds][3]           | [Rectangle][22]                        | Gets the bounding rectangle of this polygon.                           |
| [Center][4]           | [Vector][23]                           | Gets the center (point mean) of the polygon.                           |
| [Centroid][5]         | [Vector][23]                           | Gets the centroid (area weighted) of the polygon.                      |
| [ConvexPartitions][6] | `IReadOnlyList<IReadOnlyList<Vector>>` | Gets the list of convex partitions. If this polygon is already conv... |
| [IsConvex][7]         | `bool`                                 | Gets a value determining if this polygon is convex (in clockwise or... |
| [Normals][8]          | `IReadOnlyList<Vector>`                | Gets a read-only view of the polygon's normals.                        |
| [Vertices][9]         | `IReadOnlyList<Vector>`                | Gets a read-only view of the polygon's vertices.                       |

## Methods

#### Instance

| Name                            | Return Type             | Summary                                                                |
|---------------------------------|-------------------------|------------------------------------------------------------------------|
| [Add(Vector)][10]               | `void`                  | Adds a vertex to the end of polygon's vertex list.                     |
| [Clear()][11]                   | `void`                  | Removes all vertices from the polygon.                                 |
| [Contains(in Vector)][12]       | `bool`                  | Determines if the specified point is contained by this polygon.        |
| [GetNearestPoint(in Vec...][13] | [Vector][23]            | Gets the nearest point on the polygon to the specified point.          |
| [Insert(int, Vector)][14]       | `void`                  | Inserts a vertex into the polygon's vertex list.                       |
| [Overlaps(IShape)][15]          | `bool`                  | Checks for an overlap between this polygon and another shape.          |
| [Overlaps(in Rectangle)][15]    | `bool`                  | Determines if this polygon overlaps the specified rectangle.           |
| [Overlaps(in Circle)][15]       | `bool`                  | Determines if this polygon overlaps the specified circle.              |
| [Overlaps(in Triangle)][15]     | `bool`                  | Determines if this polygon overlaps the specified triangle.            |
| [Overlaps(IReadOnlyList...][15] | `bool`                  | Determines if this polygon overlaps the specified triangle.            |
| [Project(in Vector)][16]        | [Range][24]             | Project this polygon onto the specified axis.                          |
| [Raycast(in Ray)][17]           | `bool`                  | Checks if a ray intersects this polygon.                               |
| [Raycast(in Ray, out Ra...][17] | `bool`                  | Checks if a ray intersects this polygon and outputs information on ... |
| [RemoveAt(int)][18]             | `void`                  | Removes a vertex from the polygon's vertex list.                       |
| [Triangulate()][19]             | `IEnumerable<Triangle>` | Decompose this polygon into triangles.                                 |

#### Static

| Name                            | Return Type   | Summary                                                                |
|---------------------------------|---------------|------------------------------------------------------------------------|
| [CreateConvexHull(IEnum...][20] | [Polygon][25] | Constructs a convex polygon representing the convex hull of the spe... |
| [CreateFromShape(IShape)][21]   | [Polygon][25] | Constructs a polygon representation of the specified shape.            |
| [CreateFromShape(in Tri...][21] | [Polygon][25] | Constructs a polygon representation of the specified triangle.         |
| [CreateFromShape(in Rec...][21] | [Polygon][25] | Constructs a polygon representation of the specified rectangle.        |
| [CreateFromShape(in Cir...][21] | [Polygon][25] | Constructs a polygon representation of the specified circle.           |

[0]: ../../Heirloom.Core.md
[1]: IShape.md
[2]: Polygon/Area.md
[3]: Polygon/Bounds.md
[4]: Polygon/Center.md
[5]: Polygon/Centroid.md
[6]: Polygon/ConvexPartitions.md
[7]: Polygon/IsConvex.md
[8]: Polygon/Normals.md
[9]: Polygon/Vertices.md
[10]: Polygon/Add.md
[11]: Polygon/Clear.md
[12]: Polygon/Contains.md
[13]: Polygon/GetNearestPoint.md
[14]: Polygon/Insert.md
[15]: Polygon/Overlaps.md
[16]: Polygon/Project.md
[17]: Polygon/Raycast.md
[18]: Polygon/RemoveAt.md
[19]: Polygon/Triangulate.md
[20]: Polygon/CreateConvexHull.md
[21]: Polygon/CreateFromShape.md
[22]: ../Heirloom/Rectangle.md
[23]: ../Heirloom/Vector.md
[24]: ../Heirloom/Range.md
[25]: Polygon.md
