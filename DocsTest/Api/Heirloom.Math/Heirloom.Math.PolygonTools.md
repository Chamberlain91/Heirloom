# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## PolygonTools (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides several operations for polygons represented as a read-only list of vectors.

| Methods                                | Summary                                                                                                                               |
|----------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| [GetClosestPoint](#GET53DD1DC3)        | Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned. |
| [GetClosestPointOutline](#GET99348117) | Gets the closest point on the polygon outline to the specified point.                                                                 |
| [ContainsPoint](#CON881CB3C3)          | Assuming the polygon is convex, checks if the point is contained.                                                                     |
| [Overlaps](#OVE7F2D7C32)               | Tests if a (convex) polygon overlaps the specified shape.                                                                             |
| [Raycast](#RAY408E945F)                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast](#RAY408E945F)                | Checks if a ray intersects this polygon.                                                                                              |
| [Raycast](#RAY408E945F)                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Raycast](#RAY408E945F)                | Checks if a ray intersects this polygon and outputs information on the contact point.                                                 |
| [Project](#PROAD473221)                | Project a polygon onto the specified axis.                                                                                            |
| [DecomposeConvex](#DECD5C63F18)        | Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.                  |
| [DecomposeConvexIndices](#DEC1BF9D083) | Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.                             |
| [Triangulate](#TRI86AB1534)            | Decomposes a simple polygon into constituent triangles.                                                                               |
| [TriangulateIndices](#TRI1E4F7709)     | Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.                                 |
| [IsConvexVertex](#ISC39BC929B)         | Determines if the ith vertex is a convex (clockwise) vertex.                                                                          |
| [IsConvexPolygon](#ISC197C78B7)        | Determines if the polygon is considered convex (non-concave and oriented clockwise).                                                  |
| [IsConvexVertex](#ISC39BC929B)         | Determines if the vertex '`vCurr`' is convex (clockwise).                                                                             |
| [ComputeMetrics](#COM8EB5C2F0)         |                                                                                                                                       |
| [GetNormal](#GET3AF6BF49)              | Vector perpendicular to the i-th edge.                                                                                                |

### Methods

#### <a name="GETB151C47D"></a>GetClosestPoint(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned.


#### <a name="GETB6050D89"></a>GetClosestPointOutline(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) point) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on the polygon outline to the specified point.


#### <a name="CON45F666FA"></a>ContainsPoint(IReadOnlyList\<Vector> poly, in [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Assuming the polygon is convex, checks if the point is contained.


#### <a name="OVE3D4308CF"></a>Overlaps(IReadOnlyList\<Vector> polygon, [IShape](Heirloom.Math.IShape.md) shape) : bool
<small>`Static`</small>

Tests if a (convex) polygon overlaps the specified shape.


#### <a name="RAY30673858"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Ray](Heirloom.Math.Ray.md) ray) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAY848FA55D"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) direction) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon.


#### <a name="RAY406214DB"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Ray](Heirloom.Math.Ray.md) ray, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="RAY52A47FBA"></a>Raycast(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) origin, in [Vector](Heirloom.Math.Vector.md) direction, out [RayContact](Heirloom.Math.RayContact.md) contact) : bool
<small>`Static`</small>

Checks if a ray intersects this polygon and outputs information on the contact point.


#### <a name="PRO11EC2108"></a>Project(IReadOnlyList\<Vector> polygon, in [Vector](Heirloom.Math.Vector.md) axis) : [Range](Heirloom.Math.Range.md)
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


#### <a name="ISCB20F457E"></a>IsConvexVertex(in [Vector](Heirloom.Math.Vector.md) vPrev, in [Vector](Heirloom.Math.Vector.md) vCurr, in [Vector](Heirloom.Math.Vector.md) vNext) : bool
<small>`Static`</small>

Determines if the vertex '`vCurr`' is convex (clockwise).


#### <a name="COMDDEECE05"></a>ComputeMetrics(IReadOnlyList\<Vector> polygon, out float area, out [Vector](Heirloom.Math.Vector.md) center, out [Vector](Heirloom.Math.Vector.md) centroid) : void
<small>`Static`</small>


#### <a name="GET6318F2B1"></a>GetNormal(IReadOnlyList\<Vector> polygon, int i) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Vector perpendicular to the i-th edge.


