# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## PolygonTools (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides several operations for polygons represented as a read-only list of vectors.

| Methods | Summary |
|---------|---------|
| [GetClosestPoint](#GETA1D099FD) | Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned. |
| [GetClosestPointOutline](#GETC29F13C9) | Gets the closest point on the polygon outline to the specified point. |
| [ContainsPoint](#CONE432BB9A) | Assuming the polygon is convex, checks if the point is contained. |
| [Overlaps](#OVE76FEACCF) | Tests if a (convex) polygon overlaps the specified shape. |
| [Raycast](#RAYA02AC878) | Checks if a ray intersects this polygon. |
| [Raycast](#RAY6A623E5D) | Checks if a ray intersects this polygon. |
| [Raycast](#RAY3927FAFB) | Checks if a ray intersects this polygon and outputs information on the contact point. |
| [Raycast](#RAYE3E5D1FA) | Checks if a ray intersects this polygon and outputs information on the contact point. |
| [Project](#PRODF61A2C8) | Project a polygon onto the specified axis. |
| [DecomposeConvex](#DECEF17F732) | Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it. |
| [DecomposeConvexIndices](#DEC97B32AE0) | Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon. |
| [Triangulate](#TRI22656356) | Decomposes a simple polygon into constituent triangles. |
| [TriangulateIndices](#TRI80731139) | Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon. |
| [IsConvexVertex](#ISC7B85044) | Determines if the ith vertex is a convex (clockwise) vertex. |
| [IsConvexPolygon](#ISC64A2C0E) | Determines if the polygon is considered convex (non-concave and oriented clockwise). |
| [IsConvexVertex](#ISC76C5FF9E) | Determines if the vertex '`vCurr`' is convex (clockwise). |
| [ComputeMetrics](#COM61446885) |  |
| [GetNormal](#GET8E9D5651) | Vector perpendicular to the i-th edge. |

### Methods

#### <a name="GETA1D099FD"></a>GetClosestPoint(IReadOnlyList\<Vector> polygon, in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned.


#### <a name="GETC29F13C9"></a>GetClosestPointOutline(IReadOnlyList\<Vector> polygon, in [Vector](heirloom.math.vector.md) point) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Gets the closest point on the polygon outline to the specified point.


#### <a name="CONE432BB9A"></a>ContainsPoint(IReadOnlyList\<Vector> poly, in [Vector](heirloom.math.vector.md) point) : bool

<small>`Static`</small>

Assuming the polygon is convex, checks if the point is contained.


#### <a name="OVE76FEACCF"></a>Overlaps(IReadOnlyList\<Vector> polygon, [IShape](heirloom.math.ishape.md) shape) : bool

<small>`Static`</small>

Tests if a (convex) polygon overlaps the specified shape.


#### <a name="RAYA02AC878"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Ray](heirloom.math.ray.md) ray) : bool

<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAY6A623E5D"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Vector](heirloom.math.vector.md) origin, in [Vector](heirloom.math.vector.md) direction) : bool

<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAY3927FAFB"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Ray](heirloom.math.ray.md) ray, out [RayContact](heirloom.math.raycontact.md) contact) : bool

<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="RAYE3E5D1FA"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Vector](heirloom.math.vector.md) origin, in [Vector](heirloom.math.vector.md) direction, out [RayContact](heirloom.math.raycontact.md) contact) : bool

<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="PRODF61A2C8"></a>Project(IReadOnlyList\<Vector> polygon, in [Vector](heirloom.math.vector.md) axis) : [Range](heirloom.math.range.md)

<small>`Static`</small>

Project a polygon onto the specified axis.


#### <a name="DECEF17F732"></a>DecomposeConvex(IReadOnlyList\<Vector> polygon) : IEnumerable\<Polygon>

<small>`Static`</small>

Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.


#### <a name="DEC97B32AE0"></a>DecomposeConvexIndices(IReadOnlyList\<Vector> points) : IEnumerable\<IReadOnlyList\<int>>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.


#### <a name="TRI22656356"></a>Triangulate(IReadOnlyList\<Vector> poylgon) : IEnumerable\<Triangle>

<small>`Static`</small>

Decomposes a simple polygon into constituent triangles.


#### <a name="TRI80731139"></a>TriangulateIndices(IEnumerable\<Vector> polygon) : IEnumerable\<ValueTuple\<int|int|int>>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.


#### <a name="ISC7B85044"></a>IsConvexVertex(IReadOnlyList\<Vector> polygon, int i) : bool

<small>`Static`</small>

Determines if the ith vertex is a convex (clockwise) vertex.


#### <a name="ISC64A2C0E"></a>IsConvexPolygon(IReadOnlyList\<Vector> polygon) : bool

<small>`Static`</small>

Determines if the polygon is considered convex (non-concave and oriented clockwise).


#### <a name="ISC76C5FF9E"></a>IsConvexVertex(in [Vector](heirloom.math.vector.md) vPrev, in [Vector](heirloom.math.vector.md) vCurr, in [Vector](heirloom.math.vector.md) vNext) : bool

<small>`Static`</small>

Determines if the vertex '`vCurr`' is convex (clockwise).


#### <a name="COM61446885"></a>ComputeMetrics(IReadOnlyList\<Vector> polygon, out float area, out [Vector](heirloom.math.vector.md) center, out [Vector](heirloom.math.vector.md) centroid) : void

<small>`Static`</small>


#### <a name="GET8E9D5651"></a>GetNormal(IReadOnlyList\<Vector> polygon, int i) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Vector perpendicular to the i-th edge.


