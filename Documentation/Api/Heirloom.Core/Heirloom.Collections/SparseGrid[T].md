# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SparseGrid\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

An infinite, sparse grid of values.

```cs
public sealed class SparseGrid<T> : ISparseGrid<T>, IReadOnlySparseGrid<T>, IReadOnlyGrid<T>, IGrid<T>
```

### Inherits

[ISparseGrid\<T>][1], [IReadOnlySparseGrid\<T>][2], [IReadOnlyGrid\<T>][3], [IGrid\<T>][4]

### Properties

[Indexer][5], [Keys][6]

### Methods

[Clear][7], [ClearValue][8], [HasValue][9], [IsValidCoordinate][10]

## Properties

#### Instance

| Name         | Type                     | Summary |
|--------------|--------------------------|---------|
| [Indexer][5] | `T`                      |         |
| [Indexer][5] | `T`                      |         |
| [Keys][6]    | `IEnumerable<IntVector>` |         |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                             |
|---------------------------------|-------------|---------------------------------------------------------------------|
| [Clear()][7]                    | `void`      | Removes all values in the grid, marking everything as unoccupied.   |
| [ClearValue(in int, in ...][8]  | `void`      | Clears the assigned valueon this cell of the sparse grid.           |
| [ClearValue(in IntVector)][8]   | `void`      | Clears the assigned valueon this cell of the sparse grid.           |
| [HasValue(in int, in int)][9]   | `bool`      | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue(in IntVector)][9]     | `bool`      | Determines if a value has been set on this cell of the sparse grid. |
| [IsValidCoordinate(in i...][10] | `bool`      | Is the specified coordinate valid on this grid?                     |
| [IsValidCoordinate(in I...][10] | `bool`      | Is the specified coordinate valid on this grid?                     |

[0]: ../../Heirloom.Core.md
[1]: ISparseGrid[T].md
[2]: IReadOnlySparseGrid[T].md
[3]: IReadOnlyGrid[T].md
[4]: IGrid[T].md
[5]: SparseGrid[T]/Indexer.md
[6]: SparseGrid[T]/Keys.md
[7]: SparseGrid[T]/Clear.md
[8]: SparseGrid[T]/ClearValue.md
[9]: SparseGrid[T]/HasValue.md
[10]: SparseGrid[T]/IsValidCoordinate.md
