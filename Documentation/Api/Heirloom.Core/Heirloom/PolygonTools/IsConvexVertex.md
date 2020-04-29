# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.IsConvexVertex (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [PolygonTools][1]

### IsConvexVertex(IReadOnlyList<Vector>, int)

Determines if the ith vertex is a convex (clockwise) vertex.

```cs
public static bool IsConvexVertex(IReadOnlyList<Vector> polygon, int i)
```

| Name    | Type                     | Summary |
|---------|--------------------------|---------|
| polygon | `IReadOnlyList\<Vector>` |         |
| i       | `int`                    |         |

> **Returns** - `bool`

### IsConvexVertex(in Vector, in Vector, in Vector)

Determines if the vertex ' `vCurr` ' is convex (clockwise).

```cs
public static bool IsConvexVertex(in Vector vPrev, in Vector vCurr, in Vector vNext)
```

| Name  | Type        | Summary |
|-------|-------------|---------|
| vPrev | [Vector][2] |         |
| vCurr | [Vector][2] |         |
| vNext | [Vector][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
[2]: ../Vector.md
