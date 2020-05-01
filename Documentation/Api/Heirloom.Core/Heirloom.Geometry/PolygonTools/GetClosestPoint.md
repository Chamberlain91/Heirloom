# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.GetClosestPoint (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [PolygonTools][1]

### GetClosestPoint(IReadOnlyList<Vector>, in Vector)

Gets the closest point on the polygon to the specified point. If the point is contained by the polygon, the point itself is returned.

```cs
public static Vector GetClosestPoint(IReadOnlyList<Vector> polygon, in Vector point)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |
| point   | [Vector][2]              |         |

> **Returns** - [Vector][2]

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
[2]: ../../Heirloom/Vector.md
