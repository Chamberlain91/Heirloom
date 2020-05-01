# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ISpatialQuery\<T>.Query (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [ISpatialQuery\<T>][1]

### Query(Vector)

Finds spatial elements that overlap the specified point.

```cs
public abstract IEnumerable<T> Query(Vector point)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| point | [Vector][2] |         |

> **Returns** - `IEnumerable\<T>`

### Query(IShape)

Finds spatial elements that overlap the specified rectangle.

```cs
public abstract IEnumerable<T> Query(IShape queryShape)
```

| Name       | Type        | Summary |
|------------|-------------|---------|
| queryShape | [IShape][3] |         |

> **Returns** - `IEnumerable\<T>`

### Query(Ray, float)

Finds spatial elements that intersect the specified ray.

```cs
public abstract IEnumerable<T> Query(Ray ray, float maxDistance = ∞)
```

| Name        | Type     | Summary |
|-------------|----------|---------|
| ray         | [Ray][4] |         |
| maxDistance | `float`  |         |

> **Returns** - `IEnumerable\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../ISpatialQuery[T].md
[2]: ../../Heirloom/Vector.md
[3]: ../../Heirloom.Geometry/IShape.md
[4]: ../../Heirloom.Geometry/Ray.md
