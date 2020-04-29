# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GridUtilities.GetNeighborCoordinates\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GridUtilities][1]

### GetNeighborCoordinates<T>(IGrid<T>, int, int, GridNeighborType)

Gets the specified cell's neighbor coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates<T>(IGrid<T> grid, int x, int y, GridNeighborType neighborType = Axis)
```

`IteratorStateMachineAttribute` `ExtensionAttribute`

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| grid         | [IGrid\<T>][2]        |         |
| x            | `int`                 |         |
| y            | `int`                 |         |
| neighborType | [GridNeighborType][3] |         |

> **Returns** - `IEnumerable\<IntVector>`

### GetNeighborCoordinates<T>(IGrid<T>, IntVector, GridNeighborType)

Gets the specified cell's neighbor coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates<T>(IGrid<T> grid, IntVector co, GridNeighborType neighborType = Axis)
```

`ExtensionAttribute`

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| grid         | [IGrid\<T>][2]        |         |
| co           | [IntVector][4]        |         |
| neighborType | [GridNeighborType][3] |         |

> **Returns** - `IEnumerable\<IntVector>`

[0]: ../../../Heirloom.Core.md
[1]: ../GridUtilities.md
[2]: ../IGrid[T].md
[3]: ../GridNeighborType.md
[4]: ../IntVector.md
