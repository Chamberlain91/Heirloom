# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## PolygonTools.IsConvexVertex

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [PolygonTools][1]  

### IsConvexVertex(IReadOnlyList<Vector>, int)

Determines if the ith vertex is a convex (clockwise) vertex.

```cs
public static bool IsConvexVertex(IReadOnlyList<Vector> polygon, int i)
```

### IsConvexVertex(in Vector, in Vector, in Vector)

Determines if the vertex ' `vCurr` ' is convex (clockwise).

```cs
public static bool IsConvexVertex(in Vector vPrev, in Vector vCurr, in Vector vNext)
```

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
