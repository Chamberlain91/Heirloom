# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GridUtilities (Class)

> **Namespace**: [Heirloom.Collections][0]

Provides extra utilities for interacting with a grid.

```cs
public static class GridUtilities
```

`ExtensionAttribute`

### Static Methods

[GetNeighborCoordinates][1], [GetNeighborCoordinates\<T>][2], [GetNeighbors\<T>][3]

## Methods

| Name                           | Return Type               | Summary                                                                |
|--------------------------------|---------------------------|------------------------------------------------------------------------|
| [GetNeighborCoordinates...][1] | `IEnumerable\<IntVector>` | Gets neighboring grid coordinates relative to the specified input c... |
| [GetNeighborCoordinates...][1] | `IEnumerable\<IntVector>` | Gets neighboring grid coordinates relative to the specified input c... |
| [GetNeighborCoordinates...][2] | `IEnumerable\<IntVector>` | Gets the specified cell's neighbor coordinates.                        |
| [GetNeighborCoordinates...][2] | `IEnumerable\<IntVector>` | Gets the specified cell's neighbor coordinates.                        |
| [GetNeighbors<T>(IGrid<...][3] | `IEnumerable\<T>`         | Gets the specified cell's neighbors.                                   |
| [GetNeighbors<T>(IGrid<...][3] | `IEnumerable\<T>`         | Gets the specified cell's neighbors.                                   |

[0]: ../../Heirloom.Core.md
[1]: GridUtilities/GetNeighborCoordinates.md
[2]: GridUtilities/GetNeighborCoordinates[T].md
[3]: GridUtilities/GetNeighbors[T].md
