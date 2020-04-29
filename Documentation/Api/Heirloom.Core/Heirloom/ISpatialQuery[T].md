# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ISpatialQuery\<T> (Interface)

> **Namespace**: [Heirloom][0]

Provides methods for querying elements in 2D space.

```cs
public interface ISpatialQuery<T>
```

### Methods

[Query][1]

## Methods

#### Instance

| Name                   | Return Type       | Summary                                                      |
|------------------------|-------------------|--------------------------------------------------------------|
| [Query(Vector)][1]     | `IEnumerable\<T>` | Finds spatial elements that overlap the specified point.     |
| [Query(IShape)][1]     | `IEnumerable\<T>` | Finds spatial elements that overlap the specified rectangle. |
| [Query(Ray, float)][1] | `IEnumerable\<T>` | Finds spatial elements that intersect the specified ray.     |

[0]: ../../Heirloom.Core.md
[1]: ISpatialQuery[T]/Query.md
