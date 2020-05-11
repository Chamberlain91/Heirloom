# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GridNeighborType (Enum)

> **Namespace**: [Heirloom.Collections][0]

Describes the choice of neighbors in a grid.

```cs
public enum GridNeighborType : IComparable, IFormattable, IConvertible
```

| Name     | Summary                                                               |
|----------|-----------------------------------------------------------------------|
| All      | All eight neiboring tiles (combines [Axis][1] and [Diagonal][2] ).    |
| Axis     | The four neighbors to north, east, south, west.                       |
| Diagonal | The four neighbors to north-east, south-east, south-west, north-west. |

[0]: ../../Heirloom.Core.md
[1]: GridNeighborType/Axis.md
[2]: GridNeighborType/Diagonal.md
