# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Polygon.ConvexPartitions (Property)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [Polygon][1]

### ConvexPartitions

Gets the list of convex partitions. If this polygon is already convex, there is only one convex partition that maps one-to-one with the original.

```cs
public IReadOnlyList<IReadOnlyList<Vector>> ConvexPartitions { get; }
```

> **Returns**: `IReadOnlyList<IReadOnlyList<Vector>>`

[0]: ../../../Heirloom.Core.md
[1]: ../Polygon.md
