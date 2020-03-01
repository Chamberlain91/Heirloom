# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## LineSegment (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<LineSegment></small>  

Represents a line segment defined by two [Vector](Heirloom.Math.Vector.md).

| Fields                             | Summary                                                                              |
|------------------------------------|--------------------------------------------------------------------------------------|
| [A](#ACDCA)                        | The first end-point.                                                                 |
| [B](#BCDCA)                        | The second end-point.                                                                |
| [IntersectionTolerance](#INTEB65A) | A value to adjust the intersection tolerance to compensate for floating-point error. |

| Methods                      | Summary                                                            |
|------------------------------|--------------------------------------------------------------------|
| [GetPoint](#GETP8949)        | Gets a intermediate point along the line segment.                  |
| [Intersects](#INTE62E2)      | Checks if this line segment intersects another.                    |
| [Intersects](#INTE62E2)      | Checks if this line segment intersects another.                    |
| [GetClosestPoint](#GETC53DD) | Gets the closest point on the line segment to the specified point. |
| [Intersects](#INTE62E2)      | Checks if two line segments intersect.                             |
| [Intersects](#INTE62E2)      | Checks if two line segments intersect.                             |
| [Intersects](#INTE62E2)      | Checks if two line segments intersect.                             |
| [Intersects](#INTE62E2)      | Checks if two line segments intersect.                             |
| [GetClosestPoint](#GETC53DD) | Gets the closest point on a line segment to the specified point.   |
| [ClosestPoint](#CLOSC5B5)    | Gets the closest point on a line segment to the specified point.   |

### Fields

#### <a name="ACDCA"></a> A : [Vector](Heirloom.Math.Vector.md)

The first end-point.

#### <a name="BCDCA"></a> B : [Vector](Heirloom.Math.Vector.md)

The second end-point.

#### <a name="INTEB65A"></a> IntersectionTolerance : float

A value to adjust the intersection tolerance to compensate for floating-point error.

#### <a name="INTEB65A"></a> IntersectionTolerance : float
<small>`Static`</small>

A value to adjust the intersection tolerance to compensate for floating-point error.

### Constructors

#### LineSegment([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b)

### Methods

#### <a name="GETPE0E1"></a> GetPoint(float t) : [Vector](Heirloom.Math.Vector.md)

Gets a intermediate point along the line segment.


#### <a name="INTE8009"></a> Intersects([LineSegment](Heirloom.Math.LineSegment.md) other) : bool

Checks if this line segment intersects another.


#### <a name="INTE61BB"></a> Intersects([LineSegment](Heirloom.Math.LineSegment.md) other, out [Vector](Heirloom.Math.Vector.md) point) : bool

Checks if this line segment intersects another.


#### <a name="GETCBC36"></a> GetClosestPoint([Vector](Heirloom.Math.Vector.md) p) : [Vector](Heirloom.Math.Vector.md)

Gets the closest point on the line segment to the specified point.


#### <a name="INTE2AE0"></a> Intersects([LineSegment](Heirloom.Math.LineSegment.md) s1, [LineSegment](Heirloom.Math.LineSegment.md) s2) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INTE97F1"></a> Intersects([LineSegment](Heirloom.Math.LineSegment.md) s1, [LineSegment](Heirloom.Math.LineSegment.md) s2, out [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INTEFA3A"></a> Intersects([Vector](Heirloom.Math.Vector.md) p1, [Vector](Heirloom.Math.Vector.md) p2, [Vector](Heirloom.Math.Vector.md) q1, [Vector](Heirloom.Math.Vector.md) q2) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INTED025"></a> Intersects([Vector](Heirloom.Math.Vector.md) p1, [Vector](Heirloom.Math.Vector.md) p2, [Vector](Heirloom.Math.Vector.md) p3, [Vector](Heirloom.Math.Vector.md) p4, out [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="GETC9422"></a> GetClosestPoint([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) p) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


#### <a name="CLOSB3D1"></a> ClosestPoint([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) p, out float distance) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


