# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Polygon (Class)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md)</small>  

Represents a simple polygon.

| Properties                    | Summary                                                                                                                                           |
|-------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| [Item](#ITEM8B5A)             |                                                                                                                                                   |
| [Vertices](#VERT648B)         | Gets a read-only view of this polygon's vertices.                                                                                                 |
| [Count](#COUN73CA)            | Gets the number of vertices in this polygon.                                                                                                      |
| [Area](#AREA9F52)             | Gets the area of the polygon.                                                                                                                     |
| [Center](#CENT7CD9)           | Gets the center (point mean) of the polygon.                                                                                                      |
| [Centroid](#CENTE921)         | Gets the centroid (area weighted) of the polygon.                                                                                                 |
| [Bounds](#BOUNBCFE)           | Gets the bounding rectangle of this polygon.                                                                                                      |
| [IsConvex](#ISCO654C)         | Gets a value determining if this polygon is convex (in clockwise ordering).                                                                       |
| [ConvexPartitions](#CONV8A0F) | Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original. |

| Methods                           | Summary                                                                                |
|-----------------------------------|----------------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)                |                                                                                        |
| [Add](#ADDBCD0)                   |                                                                                        |
| [Insert](#INSEC7B1)               |                                                                                        |
| [RemoveAt](#REMO7EC3)             |                                                                                        |
| [GetClosestPoint](#GETC53DD)      | Gets the nearest point on the polygon to the specified point.                          |
| [Contains](#CONTD0AE)             | Determines if the specified point is contained by this polygon.                        |
| [Overlaps](#OVER7F2D)             | Checks for an overlap between this polygon and another shape.                          |
| [Overlaps](#OVER7F2D)             | Determines if this polygon overlaps the specified rectangle.                           |
| [Overlaps](#OVER7F2D)             | Determines if this polygon overlaps the specified circle.                              |
| [Overlaps](#OVER7F2D)             | Determines if this polygon overlaps the specified triangle.                            |
| [Overlaps](#OVER7F2D)             | Determines if this polygon overlaps the specified triangle.                            |
| [Project](#PROJAD47)              | Project this polygon onto the specified axis.                                          |
| [Raycast](#RAYC408E)              | Checks if a ray intersects this polygon.                                               |
| [Raycast](#RAYC408E)              | Checks if a ray intersects this polygon and outputs information on the contact point.  |
| [Triangulate](#TRIA86AB)          | Decompose this polygon into triangles.                                                 |
| [CreateFromShape](#CREABBB9)      | Constructs a polygon representation of the specified shape.                            |
| [CreateFromShape](#CREABBB9)      | Constructs a polygon representation of the specified triangle.                         |
| [CreateFromShape](#CREABBB9)      | Constructs a polygon representation of the specified rectangle.                        |
| [CreateFromShape](#CREABBB9)      | Constructs a polygon representation of the specified circle.                           |
| [CreateConvexHull](#CREAC184)     | Constructs a convex polygon representing the convex hull of the specified point cloud. |
| [CreateRegularPolygon](#CREAF803) | Construct a regular polygon.                                                           |
| [CreateRegularPolygon](#CREAF803) | Construct a regular polygon.                                                           |
| [CreateStar](#CREA60A3)           | Creates a polygon shaped like a standard 5 point star centered on the origin.          |
| [CreateStar](#CREA60A3)           | Creates a polygon shaped like a standard 5 point star.                                 |
| [CreateStar](#CREA60A3)           | Creates a polygon shaped like a star centered on the origin.                           |
| [CreateStar](#CREA60A3)           | Creates a polygon shaped like a star.                                                  |
| [CreateStar](#CREA60A3)           | Creates a polygon shaped like a star.                                                  |

### Constructors

#### Polygon()

Constructs a new empty polygon.

#### Polygon(IEnumerable\<Vector> points)

Constructs a new simple polygon (assumes points are defined clockwise and describe non-crossing edges).

### Properties

#### <a name="ITEM8B5A"></a> Item : [Vector](Heirloom.Math.Vector.md)


#### <a name="VERT648B"></a> Vertices : IReadOnlyList\<Vector>

<small>`Read Only`</small>

Gets a read-only view of this polygon's vertices.

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

Gets the number of vertices in this polygon.

#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of the polygon.

#### <a name="CENT7CD9"></a> Center : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the center (point mean) of the polygon.

#### <a name="CENTE921"></a> Centroid : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the centroid (area weighted) of the polygon.

#### <a name="BOUNBCFE"></a> Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this polygon.

#### <a name="ISCO654C"></a> IsConvex : bool

<small>`Read Only`</small>

Gets a value determining if this polygon is convex (in clockwise ordering).

#### <a name="CONV8A0F"></a> ConvexPartitions : IReadOnlyList\<IReadOnlyList\<Vector>>

<small>`Read Only`</small>

Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original.

### Methods

#### <a name="CLEA4538"></a> Clear() : void

#### <a name="ADD(2619"></a> Add([Vector](Heirloom.Math.Vector.md) item) : void


#### <a name="INSEEF02"></a> Insert(int index, [Vector](Heirloom.Math.Vector.md) item) : void


#### <a name="REMOC3C2"></a> RemoveAt(int index) : void


#### <a name="GETCDAC0"></a> GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the nearest point on the polygon to the specified point.


#### <a name="CONT3338"></a> Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if the specified point is contained by this polygon.


#### <a name="OVER450A"></a> Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Checks for an overlap between this polygon and another shape.


#### <a name="OVER5BEF"></a> Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this polygon overlaps the specified rectangle.


#### <a name="OVERE125"></a> Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this polygon overlaps the specified circle.


#### <a name="OVERB671"></a> Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this polygon overlaps the specified triangle.


#### <a name="OVER89F2"></a> Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this polygon overlaps the specified triangle.


#### <a name="PROJDD62"></a> Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this polygon onto the specified axis.


#### <a name="RAYCACE7"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Checks if a ray intersects this polygon.


#### <a name="RAYC575B"></a> Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) hit) : bool

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="TRIA3315"></a> Triangulate() : IEnumerable\<Triangle>

Decompose this polygon into triangles.

#### <a name="CREA36F2"></a> CreateFromShape([IShape](Heirloom.Math.IShape.md) shape) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified shape.


#### <a name="CREAB147"></a> CreateFromShape(in [Triangle](Heirloom.Math.Triangle.md) triangle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified triangle.


#### <a name="CREA77B4"></a> CreateFromShape(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified rectangle.


#### <a name="CREADF0C"></a> CreateFromShape(in [Circle](Heirloom.Math.Circle.md) circle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified circle.


#### <a name="CREA4AA9"></a> CreateConvexHull(IEnumerable\<Vector> points) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a convex polygon representing the convex hull of the specified point cloud.


#### <a name="CREA7F09"></a> CreateRegularPolygon([Vector](Heirloom.Math.Vector.md) center, int segments, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Construct a regular polygon.


#### <a name="CREA6410"></a> CreateRegularPolygon(int segments, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Construct a regular polygon.


#### <a name="CREA9392"></a> CreateStar(float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star centered on the origin.


#### <a name="CREAB647"></a> CreateStar([Vector](Heirloom.Math.Vector.md) center, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star.


#### <a name="CREA56D0"></a> CreateStar(int numPoints, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star centered on the origin.


#### <a name="CREA47D7"></a> CreateStar([Vector](Heirloom.Math.Vector.md) center, int numPoints, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star.


#### <a name="CREA684E"></a> CreateStar([Vector](Heirloom.Math.Vector.md) center, int numPoints, float innerRadius, float outerRadius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star.


