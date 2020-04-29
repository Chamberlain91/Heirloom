# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IFiniteGrid\<T> Interface

> **Namespace**: [Heirloom][0]  

A finite grid (bounded by [Width][1] and [Height][2] ).

```cs
public interface IFiniteGrid<T> : IGrid<T>, IReadOnlyGrid<T>
```

### Inherits

[IGrid\<T>][3], [IReadOnlyGrid\<T>][4]

#### Properties

[Width][1], [Height][2]

## Properties

| Name        | Summary                  |
|-------------|--------------------------|
| [Width][1]  | The width of this grid.  |
| [Height][2] | The height of this grid. |

[0]: ../../Heirloom.Core.md
[1]: IFiniteGrid[T]/Width.md
[2]: IFiniteGrid[T]/Height.md
[3]: IGrid[T].md
[4]: IReadOnlyGrid[T].md
