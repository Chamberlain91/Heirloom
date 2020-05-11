# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Collision (Class)

> **Namespace**: [Heirloom.Geometry][0]

Collision detection routines.

```cs
public static class Collision
```

### Static Methods

[Collide][1]

## Methods

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [Collide(in IShape, in ...][1] | `bool`      | Perform collision detection between two shapes. Both shapes are ass... |
| [Collide(in Circle, in ...][1] | `bool`      | Perform collision detection between two circles. Both shapes are as... |
| [Collide(in Circle, Pol...][1] | `bool`      | Perform collision detection between a circle and a convex polygon. ... |
| [Collide(in Polygon, Ci...][1] | `bool`      | Perform collision detection between a convex polygon and a circle. ... |
| [Collide(in Polygon, Po...][1] | `bool`      | Perform collision detection between two convex polygons. Both shape... |

[0]: ../../Heirloom.Core.md
[1]: Collision/Collide.md
