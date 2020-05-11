# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IFiniteGrid\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

A finite grid (bounded by [Width][1] and [Height][2] ).

```cs
public interface IFiniteGrid<T> : IGrid<T>, IReadOnlyGrid<T>
```

### Inherits

[IGrid\<T>][3], [IReadOnlyGrid\<T>][4]

### Properties

[Height][2], [Width][1]

### Methods

[Clear][5]

## Properties

#### Instance

| Name        | Type  | Summary                  |
|-------------|-------|--------------------------|
| [Height][2] | `int` | The height of this grid. |
| [Width][1]  | `int` | The width of this grid.  |

## Methods

#### Instance

| Name          | Return Type | Summary                              |
|---------------|-------------|--------------------------------------|
| [Clear(T)][5] | `void`      | Clear the entire grid to some value. |

[0]: ../../Heirloom.Core.md
[1]: IFiniteGrid[T]/Width.md
[2]: IFiniteGrid[T]/Height.md
[3]: IGrid[T].md
[4]: IReadOnlyGrid[T].md
[5]: IFiniteGrid[T]/Clear.md
