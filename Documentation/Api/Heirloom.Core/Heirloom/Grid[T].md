# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Grid\<T> Class

> **Namespace**: [Heirloom][0]  

A finite grid (bounded by size) of values.

```cs
public sealed class Grid<T> : IFiniteGrid<T>, IGrid<T>, IReadOnlyGrid<T>
```

### Inherits

[IFiniteGrid\<T>][1], [IGrid\<T>][2], [IReadOnlyGrid\<T>][3]

#### Properties

[Width][4], [Height][5], [Indexer][6]

#### Methods

[Clear][7], [IsValidCoordinate][8]

## Properties

| Name         | Summary                  |
|--------------|--------------------------|
| [Width][4]   | The width of this grid.  |
| [Height][5]  | The height of this grid. |
| [Indexer][6] |                          |
| [Indexer][6] |                          |

## Methods

| Name                   | Summary                                                                       |
|------------------------|-------------------------------------------------------------------------------|
| [Clear][7]             | Sets all values in the grid to default for type \<typeparamref name="T" /> .  |
| [IsValidCoordinate][8] | Determines if the specified coordinate is a valid coordinate within the grid. |
| [IsValidCoordinate][8] | Determines if the specified coordinate is a valid coordinate within the grid. |

[0]: ../../Heirloom.Core.md
[1]: IFiniteGrid[T].md
[2]: IGrid[T].md
[3]: IReadOnlyGrid[T].md
[4]: Grid[T]/Width.md
[5]: Grid[T]/Height.md
[6]: Grid[T]/Indexer.md
[7]: Grid[T]/Clear.md
[8]: Grid[T]/IsValidCoordinate.md
