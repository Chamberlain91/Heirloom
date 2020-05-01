# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.DecomposeConvexIndices (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [PolygonTools][1]

### DecomposeConvexIndices(IReadOnlyList<Vector>)

Converts a simple polygon into one or more convex polygons enumerated by indices of the original polygon.

```cs
public static IEnumerable<IReadOnlyList<int>> DecomposeConvexIndices(IReadOnlyList<Vector> points)
```

`IteratorStateMachineAttribute`

| Name   | Type                     | Summary |
|--------|--------------------------|---------|
| points | `IReadOnlyList\<Vector>` |         |

> **Returns** - `IEnumerable\<IReadOnlyList\<int>>`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
