# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## BoundingTreeSpatialCollection\<T>.Query

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [BoundingTreeSpatialCollection\<T>][1]  

### Query(Vector)

Queries the spatial collection and returns the elements with bounds that overlap the specified point.

```cs
public IEnumerable<T> Query(Vector point)
```

### Query(IShape)

Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.

```cs
public IEnumerable<T> Query(IShape queryShape)
```

### Query(Ray, float)

Queries the spatial collection and returns the elements with bounds that intersect the specified ray.

```cs
public IEnumerable<T> Query(Ray ray, float maxDistance = ∞)
```

[0]: ../../../Heirloom.Core.md
[1]: ../BoundingTreeSpatialCollection[T].md
