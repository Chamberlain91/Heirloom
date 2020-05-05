# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GridUtilities.GetNeighborCoordinates (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [GridUtilities][1]

### GetNeighborCoordinates(IntVector, GridNeighborType)

Gets neighboring grid coordinates relative to the specified input coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates(IntVector co, GridNeighborType neighborType)
```

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| co           | [IntVector][2]        |         |
| neighborType | [GridNeighborType][3] |         |

> **Returns** - `IEnumerable<IntVector>`

### GetNeighborCoordinates(int, int, GridNeighborType)

Gets neighboring grid coordinates relative to the specified input coordinates.

```cs
public static IEnumerable<IntVector> GetNeighborCoordinates(int x, int y, GridNeighborType neighborType)
```

| Name         | Type                  | Summary |
|--------------|-----------------------|---------|
| x            | `int`                 |         |
| y            | `int`                 |         |
| neighborType | [GridNeighborType][3] |         |

> **Returns** - `IEnumerable<IntVector>`

[0]: ../../../Heirloom.Core.md
[1]: ../GridUtilities.md
[2]: ../../Heirloom/IntVector.md
[3]: ../GridNeighborType.md
