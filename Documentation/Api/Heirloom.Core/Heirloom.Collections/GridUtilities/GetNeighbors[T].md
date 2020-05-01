# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GridUtilities.GetNeighbors\<T> (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [GridUtilities][1]

### GetNeighbors<T>(IGrid<T>, IntVector, GridNeighborType)

Gets the specified cell's neighbors.

```cs
public static IEnumerable<T> GetNeighbors<T>(IGrid<T> grid, IntVector co, GridNeighborType neighborType = Axis)
```

`ExtensionAttribute`

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| grid         | [IGrid\<T>][2]        |         |
| co           | [IntVector][3]        |         |
| neighborType | [GridNeighborType][4] |         |

> **Returns** - `IEnumerable\<T>`

### GetNeighbors<T>(IGrid<T>, int, int, GridNeighborType)

Gets the specified cell's neighbors.

```cs
public static IEnumerable<T> GetNeighbors<T>(IGrid<T> grid, int x, int y, GridNeighborType neighborType = Axis)
```

`IteratorStateMachineAttribute` `ExtensionAttribute`

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| grid         | [IGrid\<T>][2]        |         |
| x            | `int`                 |         |
| y            | `int`                 |         |
| neighborType | [GridNeighborType][4] |         |

> **Returns** - `IEnumerable\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../GridUtilities.md
[2]: ../IGrid[T].md
[3]: ../../Heirloom/IntVector.md
[4]: ../GridNeighborType.md
