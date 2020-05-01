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

[Area][2], [Bounds][3], [Center][4], [Centroid][5], [ConvexPartitions][6], [Count][7], [Indexer][8], [IsConvex][9], [Vertices][10]

### Methods

[Add][11], [Clear][12], [Contains][13], [GetClosestPoint][14], [Insert][15], [Overlaps][16], [Project][17], [Raycast][18], [RemoveAt][19], [Triangulate][20]

### Static Methods

[CreateConvexHull][21], [CreateFromShape][22], [CreateRegularPolygon][23], [CreateStar][24]

## Properties

#### Instance

| Name                  | Type                                     | Summary                                                                |
|-----------------------|------------------------------------------|------------------------------------------------------------------------|
| [Area][2]             | `float`                                  | Gets the area of the polygon.                                          |
| [Bounds][3]           | [Rectangle][25]                          | Gets the bounding rectangle of this polygon.                           |
| [Center][4]           | [Vector][26]                             | Gets the center (point mean) of the polygon.                           |
| [Centroid][5]         | [Vector][26]                             | Gets the centroid (area weighted) of the polygon.                      |
| [ConvexPartitions][6] | `IReadOnlyList\<IReadOnlyList\<Vector>>` | Gets the list of convex partitions. If this polygon is already conv... |
| [Count][7]            | `int`                                    | Gets the number of vertices in this polygon.                           |
| [Indexer][8]          | [Vector][26]                             |                                                                        |
| [IsConvex][9]         | `bool`                                   | Gets a value determining if this polygon is convex (in clockwise or... |
| [Vertices][10]        | `IReadOnlyList\<Vector>`                 | Gets a read-only view of this polygon's vertices.                      |

## Methods

#### Instance

| Name                            | Return Type              | Summary                                                                |
|---------------------------------|--------------------------|------------------------------------------------------------------------|
| [Add(Vector)][11]               | `void`                   |                                                                        |
| [Clear()][12]                   | `void`                   |                                                                        |
| [Contains(in Vector)][13]       | `bool`                   | Determines if the specified point is contained by this polygon.        |
| [GetClosestPoint(in Vec...][14] | [Vector][26]             | Gets the nearest point on the polygon to the specified point.          |
| [Insert(int, Vector)][15]       | `void`                   |                                                                        |
| [Overlaps(IShape)][16]          | `bool`                   | Checks for an overlap between this polygon and another shape.          |
| [Overlaps(in Rectangle)][16]    | `bool`                   | Determines if this polygon overlaps the specified rectangle.           |
| [Overlaps(in Circle)][16]       | `bool`                   | Determines if this polygon overlaps the specified circle.              |
| [Overlaps(in Triangle)][16]     | `bool`                   | Determines if this polygon overlaps the specified triangle.            |
| [Overlaps(IReadOnlyList...][16] | `bool`                   | Determines if this polygon overlaps the specified triangle.            |
| [Project(in Vector)][17]        | [Range][27]              | Project this polygon onto the specified axis.                          |
| [Raycast(in Ray)][18]           | `bool`                   | Checks if a ray intersects this polygon.                               |
| [Raycast(in Ray, out Ra...][18] | `bool`                   | Checks if a ray intersects this polygon and outputs information on ... |
| [RemoveAt(int)][19]             | `void`                   |                                                                        |
| [Triangulate()][20]             | `IEnumerable\<Triangle>` | Decompose this polygon into triangles.                                 |

#### Static

| Name                            | Return Type   | Summary                                                                |
|---------------------------------|---------------|------------------------------------------------------------------------|
| [CreateConvexHull(IEnum...][21] | [Polygon][28] | Constructs a convex polygon representing the convex hull of the spe... |
| [CreateFromShape(IShape)][22]   | [Polygon][28] | Constructs a polygon representation of the specified shape.            |
| [CreateFromShape(in Tri...][22] | [Polygon][28] | Constructs a polygon representation of the specified triangle.         |
| [CreateFromShape(in Rec...][22] | [Polygon][28] | Constructs a polygon representation of the specified rectangle.        |
| [CreateFromShape(in Cir...][22] | [Polygon][28] | Constructs a polygon representation of the specified circle.           |
| [CreateRegularPolygon(V...][23] | [Polygon][28] | Construct a regular polygon.                                           |
| [CreateRegularPolygon(i...][23] | [Polygon][28] | Construct a regular polygon.                                           |
| [CreateStar(float)][24]         | [Polygon][28] | Creates a polygon shaped like a standard 5 point star centered on t... |
| [CreateStar(Vector, float)][24] | [Polygon][28] | Creates a polygon shaped like a standard 5 point star.                 |
| [CreateStar(int, float)][24]    | [Polygon][28] | Creates a polygon shaped like a star centered on the origin.           |
| [CreateStar(Vector, int...][24] | [Polygon][28] | Creates a polygon shaped like a star.                                  |
| [CreateStar(Vector, int...][24] | [Polygon][28] | Creates a polygon shaped like a star.                                  |

[0]: ../../Heirloom.Core.md
[1]: IShape.md
[2]: Polygon/Area.md
[3]: Polygon/Bounds.md
[4]: Polygon/Center.md
[5]: Polygon/Centroid.md
[6]: Polygon/ConvexPartitions.md
[7]: Polygon/Count.md
[8]: Polygon/Indexer.md
[9]: Polygon/IsConvex.md
[10]: Polygon/Vertices.md
[11]: Polygon/Add.md
[12]: Polygon/Clear.md
[13]: Polygon/Contains.md
[14]: Polygon/GetClosestPoint.md
[15]: Polygon/Insert.md
[16]: Polygon/Overlaps.md
[17]: Polygon/Project.md
[18]: Polygon/Raycast.md
[19]: Polygon/RemoveAt.md
[20]: Polygon/Triangulate.md
[21]: Polygon/CreateConvexHull.md
[22]: Polygon/CreateFromShape.md
[23]: Polygon/CreateRegularPolygon.md
[24]: Polygon/CreateStar.md
[25]: ../Heirloom/Rectangle.md
[26]: ../Heirloom/Vector.md
[27]: ../Heirloom/Range.md
[28]: Polygon.md
