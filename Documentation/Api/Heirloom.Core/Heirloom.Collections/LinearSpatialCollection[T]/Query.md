# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LinearSpatialCollection\<T>.Query (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [LinearSpatialCollection\<T>][1]

### Query(IShape)

```cs
public IEnumerable<T> Query(IShape queryShape)
```

| Name       | Type        | Summary |
|------------|-------------|---------|
| queryShape | [IShape][2] |         |

> **Returns** - `IEnumerable<T>`

### Query(Vector)

```cs
public IEnumerable<T> Query(Vector point)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| point | [Vector][3] |         |

> **Returns** - `IEnumerable<T>`

### Query(Ray, float)

```cs
public IEnumerable<T> Query(Ray ray, float maxDistance = ∞)
```

| Name        | Type     | Summary |
|-------------|----------|---------|
| ray         | [Ray][4] |         |
| maxDistance | `float`  |         |

> **Returns** - `IEnumerable<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../LinearSpatialCollection[T].md
[2]: ../../Heirloom.Geometry/IShape.md
[3]: ../../Heirloom/Vector.md
[4]: ../../Heirloom.Geometry/Ray.md
