# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## GridUtilities.GetNeighborCoordinates\<T>

> **Namespace**: [Heirloom][0]  
> **Type**: [GridUtilities][1]  

### GetNeighborCoordinates<T>(IGrid<T>, int, int, GridNeighborType)

Gets the specified cell's neighbor coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates<T>(IGrid<T> grid, int x, int y, GridNeighborType neighborType = Axis)
```

### GetNeighborCoordinates<T>(IGrid<T>, IntVector, GridNeighborType)

Gets the specified cell's neighbor coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates<T>(IGrid<T> grid, IntVector co, GridNeighborType neighborType = Axis)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.GridUtilities.md
