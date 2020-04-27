# IReadOnlySpatialCollection\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A read-only view of a spatial collection to query elements in 2D space.

```cs
public abstract interface IReadOnlySpatialCollection<T> : ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [ISpatialQuery\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Methods**: [Contains][2]

--------------------------------------------------------------------------------

## Methods

| Name          | Summary                                                        |
|---------------|----------------------------------------------------------------|
| [Contains][2] | Determines if the specified element exists in this collection. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.ISpatialQuery[T].md
[2]: Heirloom.IReadOnlySpatialCollection[T].Contains.md
