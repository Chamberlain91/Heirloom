# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Collision.Collide (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Collision][1]

### Collide(in IShape, in IShape, out CollisionData)

Perform collision detection between two shapes. Both shapes are assumed to already be in the same space.

```cs
public static bool Collide(in IShape s1, in IShape s2, out CollisionData result)
```

| Name   | Type               | Summary                                                                |
|--------|--------------------|------------------------------------------------------------------------|
| s1     | [IShape][2]        | The first shape.                                                       |
| s2     | [IShape][2]        | The second shape.                                                      |
| result | [CollisionData][3] | This value is populated with collision data upon a successful colli... |

> **Returns** - `bool` - True, if a collision was detected.

Note: [Polygon][4] are assumed to be convex.

### Collide(in Circle, in Circle, out CollisionData)

Perform collision detection between two circles. Both shapes are assumed to already be in the same space.

```cs
public static bool Collide(in Circle c1, in Circle c2, out CollisionData result)
```

| Name   | Type               | Summary                                                                |
|--------|--------------------|------------------------------------------------------------------------|
| c1     | [Circle][5]        | The first shape.                                                       |
| c2     | [Circle][5]        | The second shape.                                                      |
| result | [CollisionData][3] | This value is populated with collision data upon a successful colli... |

> **Returns** - `bool` - True, if a collision was detected.

### Collide(in Circle, Polygon, out CollisionData)

Perform collision detection between a circle and a convex polygon. Both shapes are assumed to already be in the same space.

```cs
public static bool Collide(in Circle c1, Polygon p2, out CollisionData result)
```

| Name   | Type               | Summary                                                                |
|--------|--------------------|------------------------------------------------------------------------|
| c1     | [Circle][5]        | The first shape.                                                       |
| p2     | [Polygon][4]       | The second shape.                                                      |
| result | [CollisionData][3] | This value is populated with collision data upon a successful colli... |

> **Returns** - `bool` - True, if a collision was detected.

### Collide(in Polygon, Circle, out CollisionData)

Perform collision detection between a convex polygon and a circle. Both shapes are assumed to already be in the same space.

```cs
public static bool Collide(in Polygon p1, Circle c2, out CollisionData result)
```

| Name   | Type               | Summary                                                                |
|--------|--------------------|------------------------------------------------------------------------|
| p1     | [Polygon][4]       | The first shape.                                                       |
| c2     | [Circle][5]        | The second shape.                                                      |
| result | [CollisionData][3] | This value is populated with collision data upon a successful colli... |

> **Returns** - `bool` - True, if a collision was detected.

### Collide(in Polygon, Polygon, out CollisionData)

Perform collision detection between two convex polygons. Both shapes are assumed to already be in the same space.

```cs
public static bool Collide(in Polygon p1, Polygon p2, out CollisionData result)
```

| Name   | Type               | Summary                                                                |
|--------|--------------------|------------------------------------------------------------------------|
| p1     | [Polygon][4]       | The first shape.                                                       |
| p2     | [Polygon][4]       | The second shape.                                                      |
| result | [CollisionData][3] | This value is populated with collision data upon a successful colli... |

> **Returns** - `bool` - True, if a collision was detected.

[0]: ../../../Heirloom.Core.md
[1]: ../Collision.md
[2]: ../IShape.md
[3]: ../CollisionData.md
[4]: ../Polygon.md
[5]: ../Circle.md
