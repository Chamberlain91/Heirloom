# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IReadOnlySparseGrid\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

A sparse 2D grid of values.

```cs
public interface IReadOnlySparseGrid<T> : IReadOnlyGrid<T>
```

### Inherits

[IReadOnlyGrid\<T>][1]

### Properties

[Keys][2]

### Methods

[HasValue][3]

## Properties

#### Instance

| Name      | Type                     | Summary                                                   |
|-----------|--------------------------|-----------------------------------------------------------|
| [Keys][2] | `IEnumerable<IntVector>` | Gets a collection containing the keys of the sparse grid. |

## Methods

#### Instance

| Name                          | Return Type | Summary                                                             |
|-------------------------------|-------------|---------------------------------------------------------------------|
| [HasValue(in int, in int)][3] | `bool`      | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue(in IntVector)][3]   | `bool`      | Determines if a value has been set on this cell of the sparse grid. |

[0]: ../../Heirloom.Core.md
[1]: IReadOnlyGrid[T].md
[2]: IReadOnlySparseGrid[T]/Keys.md
[3]: IReadOnlySparseGrid[T]/HasValue.md
