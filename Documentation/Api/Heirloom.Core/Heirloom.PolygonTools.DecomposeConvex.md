# PolygonTools.DecomposeConvex

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [PolygonTools][1]

--------------------------------------------------------------------------------

### DecomposeConvex(IReadOnlyList<Vector>)

Converts a simple polygon into one or more convex polygons. If the polygon is already convex, this simply clones it.

```cs
public IEnumerable<Polygon> DecomposeConvex(IReadOnlyList<Vector> polygon)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.PolygonTools.md
