# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IReadOnlySpatialCollection\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

A read-only view of a spatial collection to query elements in 2D space.

```cs
public interface IReadOnlySpatialCollection<T> : ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ISpatialQuery\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Methods

[Contains][2]

## Methods

#### Instance

| Name                | Return Type | Summary                                                        |
|---------------------|-------------|----------------------------------------------------------------|
| [Contains(in T)][2] | `bool`      | Determines if the specified element exists in this collection. |

[0]: ../../Heirloom.Core.md
[1]: ISpatialQuery[T].md
[2]: IReadOnlySpatialCollection[T]/Contains.md
