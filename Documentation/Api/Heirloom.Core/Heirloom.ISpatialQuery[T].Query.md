# ISpatialQuery\<T>.Query

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [ISpatialQuery\<T>][1]  

--------------------------------------------------------------------------------

### Query(Vector)

Finds spatial elements that overlap the specified point.

```cs
public IEnumerable<T> Query(Vector point)
```

### Query(IShape)

Finds spatial elements that overlap the specified rectangle.

```cs
public IEnumerable<T> Query(IShape queryShape)
```

### Query(Ray, float)

Finds spatial elements that intersect the specified ray.

```cs
public IEnumerable<T> Query(Ray ray, float maxDistance = ∞)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.ISpatialQuery[T].md
