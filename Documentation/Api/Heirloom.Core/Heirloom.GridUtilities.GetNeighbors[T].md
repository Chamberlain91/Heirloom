# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## GridUtilities.GetNeighbors\<T>

> **Namespace**: [Heirloom][0]  
> **Type**: [GridUtilities][1]  

### GetNeighbors<T>(IGrid<T>, IntVector, GridNeighborType)

Gets the specified cell's neighbors.

```cs
public static IEnumerable<T> GetNeighbors<T>(IGrid<T> grid, IntVector co, GridNeighborType neighborType = Axis)
```

### GetNeighbors<T>(IGrid<T>, int, int, GridNeighborType)

Gets the specified cell's neighbors.

```cs
public static IEnumerable<T> GetNeighbors<T>(IGrid<T> grid, int x, int y, GridNeighborType neighborType = Axis)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.GridUtilities.md
