# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## LineSegment (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: IEquatable\<LineSegment></small>  

Represents a line segment defined by two [Vector](Heirloom.Math.Vector.md).

| Fields                                | Summary                                                                              |
|---------------------------------------|--------------------------------------------------------------------------------------|
| [A](#ACDCAB7DD)                       | The first end-point.                                                                 |
| [B](#BCDCAB7E0)                       | The second end-point.                                                                |
| [IntersectionTolerance](#INTB65AA64A) | A value to adjust the intersection tolerance to compensate for floating-point error. |

| Methods                         | Summary                                                            |
|---------------------------------|--------------------------------------------------------------------|
| [GetPoint](#GETE0E17AE2)        | Gets a intermediate point along the line segment.                  |
| [Intersects](#INT800903E6)      | Checks if this line segment intersects another.                    |
| [Intersects](#INT61BB38B5)      | Checks if this line segment intersects another.                    |
| [GetClosestPoint](#GETBC363A60) | Gets the closest point on the line segment to the specified point. |
| [Intersects](#INT2AE0750E)      | Checks if two line segments intersect.                             |
| [Intersects](#INT97F198C9)      | Checks if two line segments intersect.                             |
| [Intersects](#INTFA3AEBEB)      | Checks if two line segments intersect.                             |
| [Intersects](#INTD0254790)      | Checks if two line segments intersect.                             |
| [GetClosestPoint](#GET942232F7) | Gets the closest point on a line segment to the specified point.   |
| [ClosestPoint](#CLOB3D1E9C6)    | Gets the closest point on a line segment to the specified point.   |

### Fields

#### <a name="ACDCAB7DD"></a>A : [Vector](Heirloom.Math.Vector.md)

The first end-point.

#### <a name="BCDCAB7E0"></a>B : [Vector](Heirloom.Math.Vector.md)

The second end-point.

#### <a name="INTB65AA64A"></a>IntersectionTolerance : float

A value to adjust the intersection tolerance to compensate for floating-point error.

#### <a name="INTB65AA64A"></a>IntersectionTolerance : float
<small>`Static`</small>

A value to adjust the intersection tolerance to compensate for floating-point error.

### Constructors

#### LineSegment([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b)

### Methods

#### <a name="GETE0E17AE2"></a>GetPoint(float t) : [Vector](Heirloom.Math.Vector.md)

Gets a intermediate point along the line segment.


#### <a name="INT800903E6"></a>Intersects([LineSegment](Heirloom.Math.LineSegment.md) other) : bool

Checks if this line segment intersects another.


#### <a name="INT61BB38B5"></a>Intersects([LineSegment](Heirloom.Math.LineSegment.md) other, out [Vector](Heirloom.Math.Vector.md) point) : bool

Checks if this line segment intersects another.


#### <a name="GETBC363A60"></a>GetClosestPoint([Vector](Heirloom.Math.Vector.md) p) : [Vector](Heirloom.Math.Vector.md)

Gets the closest point on the line segment to the specified point.


#### <a name="INT2AE0750E"></a>Intersects([LineSegment](Heirloom.Math.LineSegment.md) s1, [LineSegment](Heirloom.Math.LineSegment.md) s2) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INT97F198C9"></a>Intersects([LineSegment](Heirloom.Math.LineSegment.md) s1, [LineSegment](Heirloom.Math.LineSegment.md) s2, out [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INTFA3AEBEB"></a>Intersects([Vector](Heirloom.Math.Vector.md) p1, [Vector](Heirloom.Math.Vector.md) p2, [Vector](Heirloom.Math.Vector.md) q1, [Vector](Heirloom.Math.Vector.md) q2) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="INTD0254790"></a>Intersects([Vector](Heirloom.Math.Vector.md) p1, [Vector](Heirloom.Math.Vector.md) p2, [Vector](Heirloom.Math.Vector.md) p3, [Vector](Heirloom.Math.Vector.md) p4, out [Vector](Heirloom.Math.Vector.md) point) : bool
<small>`Static`</small>

Checks if two line segments intersect.


#### <a name="GET942232F7"></a>GetClosestPoint([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) p) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


#### <a name="CLOB3D1E9C6"></a>ClosestPoint([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b, [Vector](Heirloom.Math.Vector.md) p, out float distance) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Gets the closest point on a line segment to the specified point.


