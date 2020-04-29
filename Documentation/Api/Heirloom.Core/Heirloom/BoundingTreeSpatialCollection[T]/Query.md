# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## BoundingTreeSpatialCollection\<T>.Query (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [BoundingTreeSpatialCollection\<T>][1]

### Query(Vector)

Queries the spatial collection and returns the elements with bounds that overlap the specified point.

```cs
public IEnumerable<T> Query(Vector point)
```

`IteratorStateMachineAttribute`

| Name  | Type        | Summary |
|-------|-------------|---------|
| point | [Vector][2] |         |

> **Returns** - `IEnumerable\<T>`

### Query(IShape)

Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.

```cs
public IEnumerable<T> Query(IShape queryShape)
```

`IteratorStateMachineAttribute`

| Name       | Type        | Summary |
|------------|-------------|---------|
| queryShape | [IShape][3] |         |

> **Returns** - `IEnumerable\<T>`

### Query(Ray, float)

Queries the spatial collection and returns the elements with bounds that intersect the specified ray.

```cs
public IEnumerable<T> Query(Ray ray, float maxDistance = ∞)
```

`IteratorStateMachineAttribute`

| Name        | Type     | Summary |
|-------------|----------|---------|
| ray         | [Ray][4] |         |
| maxDistance | `float`  |         |

> **Returns** - `IEnumerable\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../BoundingTreeSpatialCollection[T].md
[2]: ../Vector.md
[3]: ../IShape.md
[4]: ../Ray.md
