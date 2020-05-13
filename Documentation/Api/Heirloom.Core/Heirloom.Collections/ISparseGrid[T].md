# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ISparseGrid\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

A sparse 2D grid of values.

```cs
public interface ISparseGrid<T> : IReadOnlySparseGrid<T>, IReadOnlyGrid<T>, IGrid<T>
```

### Inherits

[IReadOnlySparseGrid\<T>][1], [IReadOnlyGrid\<T>][2], [IGrid\<T>][3]

### Methods

[Remove][4]

## Methods

#### Instance

| Name                        | Return Type | Summary                                                   |
|-----------------------------|-------------|-----------------------------------------------------------|
| [Remove(in int, in int)][4] | `void`      | Clears the assigned valueon this cell of the sparse grid. |
| [Remove(in IntVector)][4]   | `void`      | Clears the assigned valueon this cell of the sparse grid. |

[0]: ../../Heirloom.Core.md
[1]: IReadOnlySparseGrid[T].md
[2]: IReadOnlyGrid[T].md
[3]: IGrid[T].md
[4]: ISparseGrid[T]/Remove.md
