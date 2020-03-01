# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## PolygonTools (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides several operations for polygons represented as a read-only list of vectors.

| Methods                             | Summary                                                                                                                               |
|-------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| [GetClosestPoint](#GETC53DD)        | Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned. |
| [GetClosestPointOutline](#GETC9934) | Gets the closest point on the polygon outline to the specified point.                                                                 |
| [ContainsPoint](#CONT881C)          | Assuming the polygon is convex, checks if the point is contained.                                                                     |
| [Overlaps](#OVER7F2D)               | Tests if a (convex) polygon overlaps the specified shape.                                                                             |
| [Raycast](#RAYC408E)                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast](#RAYC408E)                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast](#RAYC408E)                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Raycast](#RAYC408E)                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Project](#PROJAD47)                | Project a polygon onto the specified axis.                                                                                            |
| [DecomposeConvex](#DECOD5C6)        | Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.                  |
| [DecomposeConvexIndices](#DECO1BF9) | Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.                             |
| [Triangulate](#TRIA86AB)            | Decomposes a simple polygon into constituent triangles.                                                                               |
| [TriangulateIndices](#TRIA1E4F)     | Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.                                 |
| [IsConvexVertex](#ISCO39BC)         | Determines if the ith vertex is a convex (clockwise) vertex.                                                                          |
| [IsConvexPolygon](#ISCO197C)        | Determines if the polygon is considered convex (non-concave and oriented clockwise).                                                  |
| [IsConvexVertex](#ISCO39BC)         | Determines if the vertex '`vCurr`' is convex (clockwise).                                                                             |
| [ComputeMetrics](#COMP8EB5)         |                                                                                                                                       |
| [GetNormal](#GETN3AF6)              | Vector perpendicular to the i-th edge.                                                                                                |

### Methods

#### <a name="GETCB151"></a> GetClosestPoint(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned.


#### <a name="GETCB605"></a> GetClosestPointOutline(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on the polygon outline to the specified point.


#### <a name="CONT45F6"></a> ContainsPoint(IReadOnlyList\<Vector> poly, in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Assuming the polygon is convex, checks if the point is contained.


#### <a name="OVER3D43"></a> Overlaps(IReadOnlyList\<Vector> polygon, [IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Static`</small>

Tests if a (convex) polygon overlaps the specified shape.


#### <a name="RAYC3067"></a> Raycast(IReadOnlyList\<Vector> polygon, in [Ray](Heirloom.Math.Ray.md) ray) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAYC848F"></a> Raycast(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) direction) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAYC4062"></a> Raycast(IReadOnlyList\<Vector> polygon, in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="RAYC52A4"></a> Raycast(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) direction, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="PROJ11EC"></a> Project(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)
<small>`Static`</small>

Project a polygon onto the specified axis.


#### <a name="DECOEF17"></a> DecomposeConvex(IReadOnlyList\<Vector> polygon) : IEnumerable\<Polygon>
<small>`Static`</small>

Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.


#### <a name="DECO97B3"></a> DecomposeConvexIndices(IReadOnlyList\<Vector> points) : IEnumerable\<IReadOnlyList\<int>>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.


#### <a name="TRIA2265"></a> Triangulate(IReadOnlyList\<Vector> poylgon) : IEnumerable\<Triangle>
<small>`Static`</small>

Decomposes a simple polygon into constituent triangles.


#### <a name="TRIA8073"></a> TriangulateIndices(IEnumerable\<Vector> polygon) : IEnumerable\<ValueTuple\<int|int|int>>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.


#### <a name="ISCO7B85"></a> IsConvexVertex(IReadOnlyList\<Vector> polygon, int i) : bool
<small>`Static`</small>

Determines if the ith vertex is a convex (clockwise) vertex.


#### <a name="ISCO64A2"></a> IsConvexPolygon(IReadOnlyList\<Vector> polygon) : bool
<small>`Static`</small>

Determines if the polygon is considered convex (non-concave and oriented clockwise).


#### <a name="ISCOB20F"></a> IsConvexVertex(in [Vector](Heirloom.Math.Vector.md) vPrev, in [Vector](Heirloom.Math.Vector.md) vCurr, in [Vector](Heirloom.Math.Vector.md) vNext) : bool
<small>`Static`</small>

Determines if the vertex '`vCurr`' is convex (clockwise).


#### <a name="COMPDDEE"></a> ComputeMetrics(IReadOnlyList\<Vector> polygon, out float area, out [Vector](Heirloom.Math.Vector.md) center, out [Vector](Heirloom.Math.Vector.md) centroid) : void
<small>`Static`</small>


#### <a name="GETN6318"></a> GetNormal(IReadOnlyList\<Vector> polygon, int i) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Vector perpendicular to the i-th edge.


