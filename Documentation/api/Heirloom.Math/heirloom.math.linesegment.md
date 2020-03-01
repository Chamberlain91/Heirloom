# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## LineSegment (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<LineSegment></small>  

Represents a line segment defined by two [Vector](heirloom.math.vector.md).

| Fields | Summary |
|-------|---------|
| [A](#ACDCAB7DD) | The first end-point. |
| [B](#BCDCAB7E0) | The second end-point. |
| [IntersectionTolerance](#INTB65AA64A) | A value to adjust the intersection tolerance to compensate for floating-point error. |

| Methods | Summary |
|---------|---------|
| [GetPoint](#GETD7A846C2) | Gets a intermediate point along the line segment. |
| [Intersects](#INT4F7FBFA6) | Checks if this line segment intersects another. |
| [Intersects](#INTE44EFDD5) | Checks if this line segment intersects another. |
| [GetClosestPoint](#GETC1F2FAE0) | Gets the closest point on the line segment to the specified point. |
| [Intersects](#INT55C58DCE) | Checks if two line segments intersect. |
| [Intersects](#INT67351FE9) | Checks if two line segments intersect. |
| [Intersects](#INT5939302B) | Checks if two line segments intersect. |
| [Intersects](#INT117F0930) | Checks if two line segments intersect. |
| [GetClosestPoint](#GET7BFCC877) | Gets the closest point on a line segment to the specified point. |
| [ClosestPoint](#CLO3D13C606) | Gets the closest point on a line segment to the specified point. |

### Fields

#### <a name="ACDCAB7DD"></a>A : [Vector](heirloom.math.vector.md)

The first end-point.

#### <a name="BCDCAB7E0"></a>B : [Vector](heirloom.math.vector.md)

The second end-point.

#### <a name="INTB65AA64A"></a>IntersectionTolerance : float

A value to adjust the intersection tolerance to compensate for floating-point error.

#### <a name="INTB65AA64A"></a>IntersectionTolerance : float
<small>`Static`</small>

A value to adjust the intersection tolerance to compensate for floating-point error.

### Constructors

#### LineSegment([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b)

### Methods

#### <a name="GETD7A846C2"></a>GetPoint(float t) : [Vector](heirloom.math.vector.md)


Gets a intermediate point along the line segment.


#### <a name="INT4F7FBFA6"></a>Intersects([LineSegment](heirloom.math.linesegment.md) other) : bool


Checks if this line segment intersects another.


#### <a name="INTE44EFDD5"></a>Intersects([LineSegment](heirloom.math.linesegment.md) other, out [Vector](heirloom.math.vector.md) point) : bool


Checks if this line segment intersects another.


#### <a name="GETC1F2FAE0"></a>GetClosestPoint([Vector](heirloom.math.vector.md) p) : [Vector](heirloom.math.vector.md)


Gets the closest point on the line segment to the specified point.


#### <a name="INT55C58DCE"></a>Intersects([LineSegment](heirloom.math.linesegment.md) s1, [LineSegment](heirloom.math.linesegment.md) s2) : bool

<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INT67351FE9"></a>Intersects([LineSegment](heirloom.math.linesegment.md) s1, [LineSegment](heirloom.math.linesegment.md) s2, out [Vector](heirloom.math.vector.md) point) : bool

<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INT5939302B"></a>Intersects([Vector](heirloom.math.vector.md) p1, [Vector](heirloom.math.vector.md) p2, [Vector](heirloom.math.vector.md) q1, [Vector](heirloom.math.vector.md) q2) : bool

<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INT117F0930"></a>Intersects([Vector](heirloom.math.vector.md) p1, [Vector](heirloom.math.vector.md) p2, [Vector](heirloom.math.vector.md) p3, [Vector](heirloom.math.vector.md) p4, out [Vector](heirloom.math.vector.md) point) : bool

<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="GET7BFCC877"></a>GetClosestPoint([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b, [Vector](heirloom.math.vector.md) p) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


#### <a name="CLO3D13C606"></a>ClosestPoint([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b, [Vector](heirloom.math.vector.md) p, out float distance) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


