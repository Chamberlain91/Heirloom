# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## GridNeighborType

> **Namespace**: [Heirloom][0]  

Describes the choice of neighbors in a grid.

```cs
public enum GridNeighborType : IComparable, IFormattable, IConvertible
```

| Name     | Summary                                                               |
|----------|-----------------------------------------------------------------------|
| Axis     | The four neighbors to north, east, south, west.                       |
| Diagonal | The four neighbors to north-east, south-east, south-west, north-west. |
| All      | All eight neiboring tiles (combines [Axis][1] and [Diagonal][2] ).    |
[0]: ../Heirloom.Core.md
[1]: Heirloom.GridNeighborType.Axis.md
[2]: Heirloom.GridNeighborType.Diagonal.md
