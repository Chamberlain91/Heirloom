# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Polygon (Class)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: [IShape](heirloom.math.ishape.md)</small>  

Represents a simple polygon.

| Properties | Summary |
|------------|---------|
| [Item](#ITE8B5A2F95) |  |
| [Vertices](#VER648B0F21) | Gets a read-only view of this polygon's vertices. |
| [Count](#COU73CA0BBB) | Gets the number of vertices in this polygon. |
| [Area](#ARE9F5286F) | Gets the area of the polygon. |
| [Center](#CEN7CD91D4B) | Gets the center (point mean) of the polygon. |
| [Centroid](#CENE921BA8E) | Gets the centroid (area weighted) of the polygon. |
| [Bounds](#BOUBCFE829) | Gets the bounding rectangle of this polygon. |
| [IsConvex](#ISC654C4E97) | Gets a value determining if this polygon is convex (in clockwise ordering). |
| [ConvexPartitions](#CON8A0F44BE) | Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original. |

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) |  |
| [Add](#ADD97A131A2) |  |
| [Insert](#INSEF7C8513) |  |
| [RemoveAt](#REMC3C2E61D) |  |
| [GetClosestPoint](#GETCEB6999B) | Gets the nearest point on the polygon to the specified point. |
| [Contains](#CONE7A5727A) | Determines if the specified point is contained by this polygon. |
| [Overlaps](#OVEBC208089) | Checks for an overlap between this polygon and another shape. |
| [Overlaps](#OVED270B350) | Determines if this polygon overlaps the specified rectangle. |
| [Overlaps](#OVE1C4FD437) | Determines if this polygon overlaps the specified circle. |
| [Overlaps](#OVE30299463) | Determines if this polygon overlaps the specified triangle. |
| [Overlaps](#OVE89F258A7) | Determines if this polygon overlaps the specified triangle. |
| [Project](#PROEEB2942A) | Project this polygon onto the specified axis. |
| [Raycast](#RAYE998F35A) | Checks if a ray intersects this polygon. |
| [Raycast](#RAY92E37398) | Checks if a ray intersects this polygon and outputs information on the contact point. |
| [Triangulate](#TRI3315CB9A) | Decompose this polygon into triangles. |
| [CreateFromShape](#CRE856C02AD) | Constructs a polygon representation of the specified shape. |
| [CreateFromShape](#CRE31CF2E0F) | Constructs a polygon representation of the specified triangle. |
| [CreateFromShape](#CRE3CE8CA9C) | Constructs a polygon representation of the specified rectangle. |
| [CreateFromShape](#CREEE58DD7B) | Constructs a polygon representation of the specified circle. |
| [CreateConvexHull](#CRECD6184CF) | Constructs a convex polygon representing the convex hull of the specified point cloud. |
| [CreateRegularPolygon](#CRED5324341) | Construct a regular polygon. |
| [CreateRegularPolygon](#CREE3333145) | Construct a regular polygon. |
| [CreateStar](#CRE2ED4056) | Creates a polygon shaped like a standard 5 point star centered on the origin. |
| [CreateStar](#CRE6FFD5510) | Creates a polygon shaped like a standard 5 point star. |
| [CreateStar](#CREDBEA9900) | Creates a polygon shaped like a star centered on the origin. |
| [CreateStar](#CREC7638E3A) | Creates a polygon shaped like a star. |
| [CreateStar](#CREC7B763D) | Creates a polygon shaped like a star. |

### Constructors

#### Polygon()

Constructs a new empty polygon.

#### Polygon(IEnumerable\<Vector> points)

Constructs a new simple polygon (assumes points are defined clockwise and describe non-crossing edges).

### Properties

#### <a name="ITE8B5A2F95"></a>Item : [Vector](heirloom.math.vector.md)


#### <a name="VER648B0F21"></a>Vertices : IReadOnlyList\<Vector>

<small>`Read Only`</small>

Gets a read-only view of this polygon's vertices.

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of vertices in this polygon.

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of the polygon.

#### <a name="CEN7CD91D4B"></a>Center : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the center (point mean) of the polygon.

#### <a name="CENE921BA8E"></a>Centroid : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the centroid (area weighted) of the polygon.

#### <a name="BOUBCFE829"></a>Bounds : [Rectangle](heirloom.math.rectangle.md)

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


#### <a name="ADD97A131A2"></a>Add([Vector](heirloom.math.vector.md) item) : void



#### <a name="INSEF7C8513"></a>Insert(int index, [Vector](heirloom.math.vector.md) item) : void



#### <a name="REMC3C2E61D"></a>RemoveAt(int index) : void



#### <a name="GETCEB6999B"></a>GetClosestPoint(in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)


Gets the nearest point on the polygon to the specified point.


#### <a name="CONE7A5727A"></a>Contains(in [Vector](heirloom.math.vector.md) point) : bool


Determines if the specified point is contained by this polygon.


#### <a name="OVEBC208089"></a>Overlaps([IShape](heirloom.math.ishape.md) shape) : bool

<small>`Virtual`</small>

Checks for an overlap between this polygon and another shape.


#### <a name="OVED270B350"></a>Overlaps(in [Rectangle](heirloom.math.rectangle.md) rectangle) : bool


Determines if this polygon overlaps the specified rectangle.


#### <a name="OVE1C4FD437"></a>Overlaps(in [Circle](heirloom.math.circle.md) circle) : bool


Determines if this polygon overlaps the specified circle.


#### <a name="OVE30299463"></a>Overlaps(in [Triangle](heirloom.math.triangle.md) triangle) : bool


Determines if this polygon overlaps the specified triangle.


#### <a name="OVE89F258A7"></a>Overlaps(IReadOnlyList\<Vector> polygon) : bool


Determines if this polygon overlaps the specified triangle.


#### <a name="PROEEB2942A"></a>Project(in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)


Project this polygon onto the specified axis.


#### <a name="RAYE998F35A"></a>Raycast(in [Ray](heirloom.math.ray.md) ray) : bool


Checks if a ray intersects this polygon.


#### <a name="RAY92E37398"></a>Raycast(in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) hit) : bool


Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="TRI3315CB9A"></a>Triangulate() : IEnumerable\<Triangle>


Decompose this polygon into triangles.

#### <a name="CRE856C02AD"></a>CreateFromShape([IShape](heirloom.math.ishape.md) shape) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Constructs a polygon representation of the specified shape.


#### <a name="CRE31CF2E0F"></a>CreateFromShape(in [Triangle](heirloom.math.triangle.md) triangle) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Constructs a polygon representation of the specified triangle.


#### <a name="CRE3CE8CA9C"></a>CreateFromShape(in [Rectangle](heirloom.math.rectangle.md) rectangle) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Constructs a polygon representation of the specified rectangle.


#### <a name="CREEE58DD7B"></a>CreateFromShape(in [Circle](heirloom.math.circle.md) circle) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Constructs a polygon representation of the specified circle.


#### <a name="CRECD6184CF"></a>CreateConvexHull(IEnumerable\<Vector> points) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Constructs a convex polygon representing the convex hull of the specified point cloud.


#### <a name="CRED5324341"></a>CreateRegularPolygon([Vector](heirloom.math.vector.md) center, int segments, float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Construct a regular polygon.


#### <a name="CREE3333145"></a>CreateRegularPolygon(int segments, float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Construct a regular polygon.


#### <a name="CRE2ED4056"></a>CreateStar(float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star centered on the origin.


#### <a name="CRE6FFD5510"></a>CreateStar([Vector](heirloom.math.vector.md) center, float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Creates a polygon shaped like a standard 5 point star.


#### <a name="CREDBEA9900"></a>CreateStar(int numPoints, float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Creates a polygon shaped like a star centered on the origin.


#### <a name="CREC7638E3A"></a>CreateStar([Vector](heirloom.math.vector.md) center, int numPoints, float radius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Creates a polygon shaped like a star.


#### <a name="CREC7B763D"></a>CreateStar([Vector](heirloom.math.vector.md) center, int numPoints, float innerRadius, float outerRadius) : [Polygon](heirloom.math.polygon.md)

<small>`Static`</small>

Creates a polygon shaped like a star.


