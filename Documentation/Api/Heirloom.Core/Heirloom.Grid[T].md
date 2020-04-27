# Grid\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A finite grid (bounded by size) of values.

```cs
public sealed class Grid<T> : IFiniteGrid<T>, IGrid<T>, IReadOnlyGrid<T>
```

--------------------------------------------------------------------------------

**Inherits**: [IFiniteGrid\<T>][1], [IGrid\<T>][2], [IReadOnlyGrid\<T>][3]

**Properties**: [Width][4], [Height][5], [Item][6]

**Methods**: [Clear][7], [IsValidCoordinate][8]

--------------------------------------------------------------------------------

## Constructors

### Grid(int, int)

```cs
public Grid(int width, int height)
```

## Properties

| Name        | Summary                  |
|-------------|--------------------------|
| [Width][4]  | The width of this grid.  |
| [Height][5] | The height of this grid. |
| [Item][6]   |                          |
| [Item][6]   |                          |

## Methods

| Name                   | Summary                                                                       |
|------------------------|-------------------------------------------------------------------------------|
| [Clear][7]             | Sets all values in the grid to default for type \<typeparamref name="T" /> .  |
| [IsValidCoordinate][8] | Determines if the specified coordinate is a valid coordinate within the grid. |
| [IsValidCoordinate][8] | Determines if the specified coordinate is a valid coordinate within the grid. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IFiniteGrid[T].md
[2]: Heirloom.IGrid[T].md
[3]: Heirloom.IReadOnlyGrid[T].md
[4]: Heirloom.Grid[T].Width.md
[5]: Heirloom.Grid[T].Height.md
[6]: Heirloom.Grid[T].Item.md
[7]: Heirloom.Grid[T].Clear.md
[8]: Heirloom.Grid[T].IsValidCoordinate.md
