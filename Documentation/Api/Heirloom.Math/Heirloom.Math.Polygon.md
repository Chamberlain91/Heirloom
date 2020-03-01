# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Polygon (Class)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](Heirloom.Math.IShape.md)</small>  
<small>`DefaultMemberAttribute`</small>

Represents a simple polygon.

| Properties                       | Summary                                                                                                                                           |
|----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| [Item](#ITE8B5A2F95)             |                                                                                                                                                   |
| [Vertices](#VER648B0F21)         | Gets a read-only view of this polygon's vertices.                                                                                                 |
| [Count](#COU73CA0BBB)            | Gets the number of vertices in this polygon.                                                                                                      |
| [Area](#ARE9F5286F)              | Gets the area of the polygon.                                                                                                                     |
| [Center](#CEN7CD91D4B)           | Gets the center (point mean) of the polygon.                                                                                                      |
| [Centroid](#CENE921BA8E)         | Gets the centroid (area weighted) of the polygon.                                                                                                 |
| [Bounds](#BOUBCFE829)            | Gets the bounding rectangle of this polygon.                                                                                                      |
| [IsConvex](#ISC654C4E97)         | Gets a value determining if this polygon is convex (in clockwise ordering).                                                                       |
| [ConvexPartitions](#CON8A0F44BE) | Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original. |

| Methods                              | Summary                                                                                |
|--------------------------------------|----------------------------------------------------------------------------------------|
| [Clear](#CLE4538C554)                |                                                                                        |
| [Add](#ADD26190842)                  |                                                                                        |
| [Insert](#INSEF0217B3)               |                                                                                        |
| [RemoveAt](#REMC3C2E61D)             |                                                                                        |
| [GetClosestPoint](#GETDAC09B5B)      | Gets the nearest point on the polygon to the specified point.                          |
| [Contains](#CON33387C1A)             | Determines if the specified point is contained by this polygon.                        |
| [Overlaps](#OVE450AB809)             | Checks for an overlap between this polygon and another shape.                          |
| [Overlaps](#OVE5BEF9A70)             | Determines if this polygon overlaps the specified rectangle.                           |
| [Overlaps](#OVEE125CFD7)             | Determines if this polygon overlaps the specified circle.                              |
| [Overlaps](#OVEB6714E43)             | Determines if this polygon overlaps the specified triangle.                            |
| [Overlaps](#OVE89F258A7)             | Determines if this polygon overlaps the specified triangle.                            |
| [Project](#PRODD6295AA)              | Project this polygon onto the specified axis.                                          |
| [Raycast](#RAYACE7FDBA)              | Checks if a ray intersects this polygon.                                               |
| [Raycast](#RAY575B4078)              | Checks if a ray intersects this polygon and outputs information on the contact point.  |
| [Triangulate](#TRI3315CB9A)          | Decompose this polygon into triangles.                                                 |
| [CreateFromShape](#CRE36F297CD)      | Constructs a polygon representation of the specified shape.                            |
| [CreateFromShape](#CREB147874F)      | Constructs a polygon representation of the specified triangle.                         |
| [CreateFromShape](#CRE77B4EFDC)      | Constructs a polygon representation of the specified rectangle.                        |
| [CreateFromShape](#CREDF0CEABB)      | Constructs a polygon representation of the specified circle.                           |
| [CreateConvexHull](#CRE4AA9EF2F)     | Constructs a convex polygon representing the convex hull of the specified point cloud. |
| [CreateRegularPolygon](#CRE7F099341) | Construct a regular polygon.                                                           |
| [CreateRegularPolygon](#CRE641015A5) | Construct a regular polygon.                                                           |
| [CreateStar](#CRE93929A76)           | Creates a polygon shaped like a standard 5 point star centered on the origin.          |
| [CreateStar](#CREB647B390)           | Creates a polygon shaped like a standard 5 point star.                                 |
| [CreateStar](#CRE56D02DA0)           | Creates a polygon shaped like a star centered on the origin.                           |
| [CreateStar](#CRE47D7F7BA)           | Creates a polygon shaped like a star.                                                  |
| [CreateStar](#CRE684E0EBD)           | Creates a polygon shaped like a star.                                                  |

### Constructors

#### Polygon()

Constructs a new empty polygon.

#### Polygon(IEnumerable\<Vector> points)

Constructs a new simple polygon (assumes points are defined clockwise and describe non-crossing edges).

### Properties

#### <a name="ITE8B5A2F95"></a>Item : [Vector](Heirloom.Math.Vector.md)


#### <a name="VER648B0F21"></a>Vertices : IReadOnlyList\<Vector>

<small>`Read Only`</small>

Gets a read-only view of this polygon's vertices.

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of vertices in this polygon.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of the polygon.

#### <a name="CEN7CD91D4B"></a>Center : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the center (point mean) of the polygon.

#### <a name="CENE921BA8E"></a>Centroid : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the centroid (area weighted) of the polygon.

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](Heirloom.Math.Rectangle.md)

<small>`Read Only`</small>

Gets the bounding rectangle of this polygon.

#### <a name="ISC654C4E97"></a>IsConvex : bool

<small>`Read Only`</small>

Gets a value determining if this polygon is convex (in clockwise ordering).

#### <a name="CON8A0F44BE"></a>ConvexPartitions : IReadOnlyList\<IReadOnlyList\<Vector>>

<small>`Read Only`</small>

Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original.

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

#### <a name="ADD26190842"></a>Add([Vector](Heirloom.Math.Vector.md) item) : void


#### <a name="INSEF0217B3"></a>Insert(int index, [Vector](Heirloom.Math.Vector.md) item) : void


#### <a name="REMC3C2E61D"></a>RemoveAt(int index) : void


#### <a name="GETDAC09B5B"></a>GetClosestPoint(in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)

Gets the nearest point on the polygon to the specified point.


#### <a name="CON33387C1A"></a>Contains(in [Vector](Heirloom.Math.Vector.md) point) : bool

Determines if the specified point is contained by this polygon.


#### <a name="OVE450AB809"></a>Overlaps([IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Virtual`</small>

Checks for an overlap between this polygon and another shape.


#### <a name="OVE5BEF9A70"></a>Overlaps(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : bool

Determines if this polygon overlaps the specified rectangle.


#### <a name="OVEE125CFD7"></a>Overlaps(in [Circle](Heirloom.Math.Circle.md) circle) : bool

Determines if this polygon overlaps the specified circle.


#### <a name="OVEB6714E43"></a>Overlaps(in [Triangle](Heirloom.Math.Triangle.md) triangle) : bool

Determines if this polygon overlaps the specified triangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool

Determines if this polygon overlaps the specified triangle.


#### <a name="PRODD6295AA"></a>Project(in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)

Project this polygon onto the specified axis.


#### <a name="RAYACE7FDBA"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray) : bool

Checks if a ray intersects this polygon.


#### <a name="RAY575B4078"></a>Raycast(in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) hit) : bool

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="TRI3315CB9A"></a>Triangulate() : IEnumerable\<Triangle>

Decompose this polygon into triangles.

#### <a name="CRE36F297CD"></a>CreateFromShape([IShape](Heirloom.Math.IShape.md) shape) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified shape.


#### <a name="CREB147874F"></a>CreateFromShape(in [Triangle](Heirloom.Math.Triangle.md) triangle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified triangle.


#### <a name="CRE77B4EFDC"></a>CreateFromShape(in [Rectangle](Heirloom.Math.Rectangle.md) rectangle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified rectangle.


#### <a name="CREDF0CEABB"></a>CreateFromShape(in [Circle](Heirloom.Math.Circle.md) circle) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a polygon representation of the specified circle.


#### <a name="CRE4AA9EF2F"></a>CreateConvexHull(IEnumerable\<Vector> points) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Constructs a convex polygon representing the convex hull of the specified point cloud.


#### <a name="CRE7F099341"></a>CreateRegularPolygon([Vector](Heirloom.Math.Vector.md) center, int segments, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Construct a regular polygon.


#### <a name="CRE641015A5"></a>CreateRegularPolygon(int segments, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Construct a regular polygon.


#### <a name="CRE93929A76"></a>CreateStar(float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star centered on the origin.


#### <a name="CREB647B390"></a>CreateStar([Vector](Heirloom.Math.Vector.md) center, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star.


#### <a name="CRE56D02DA0"></a>CreateStar(int numPoints, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star centered on the origin.


#### <a name="CRE47D7F7BA"></a>CreateStar([Vector](Heirloom.Math.Vector.md) center, int numPoints, float radius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star.


#### <a name="CRE684E0EBD"></a>CreateStar([Vector](Heirloom.Math.Vector.md) center, int numPoints, float innerRadius, float outerRadius) : [Polygon](Heirloom.Math.Polygon.md)
<small>`Static`</small>

Creates a polygon shaped like a star.


