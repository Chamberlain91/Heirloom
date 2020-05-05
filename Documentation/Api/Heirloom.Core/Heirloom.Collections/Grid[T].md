# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Grid\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

A finite grid (bounded by size) of values.

```cs
public sealed class Grid<T> : IFiniteGrid<T>, IGrid<T>, IReadOnlyGrid<T>
```

### Inherits

[IFiniteGrid\<T>][1], [IGrid\<T>][2], [IReadOnlyGrid\<T>][3]

### Properties

[Height][4], [Indexer][5], [Width][6]

### Methods

[Clear][7], [IsValidCoordinate][8]

## Properties

#### Instance

| Name         | Type  | Summary                  |
|--------------|-------|--------------------------|
| [Height][4]  | `int` | The height of this grid. |
| [Indexer][5] | `T`   |                          |
| [Indexer][5] | `T`   |                          |
| [Width][6]   | `int` | The width of this grid.  |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [Clear()][7]                   | `void`      | Sets all values in the grid to default for type `T` .                  |
| [Clear(T)][7]                  | `void`      | Sets all values in the grid to some value of `T` .                     |
| [IsValidCoordinate(in i...][8] | `bool`      | Determines if the specified coordinate is a valid coordinate within... |
| [IsValidCoordinate(in I...][8] | `bool`      | Determines if the specified coordinate is a valid coordinate within... |

[0]: ../../Heirloom.Core.md
[1]: IFiniteGrid[T].md
[2]: IGrid[T].md
[3]: IReadOnlyGrid[T].md
[4]: Grid[T]/Height.md
[5]: Grid[T]/Indexer.md
[6]: Grid[T]/Width.md
[7]: Grid[T]/Clear.md
[8]: Grid[T]/IsValidCoordinate.md
