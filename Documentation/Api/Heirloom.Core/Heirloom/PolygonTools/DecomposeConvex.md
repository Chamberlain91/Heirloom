# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.DecomposeConvex (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [PolygonTools][1]

### DecomposeConvex(IReadOnlyList<Vector>)

Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.

```cs
public static IEnumerable<Polygon> DecomposeConvex(IReadOnlyList<Vector> polygon)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |

> **Returns** - `IEnumerable\<Polygon>`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
