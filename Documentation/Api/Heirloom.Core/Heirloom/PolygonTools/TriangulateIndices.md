# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PolygonTools.TriangulateIndices (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [PolygonTools][1]

### TriangulateIndices(IEnumerable<Vector>)

Decomposes a simple polygon into constituent triangles enumerated by indices of the original polygon.

```cs
public static IEnumerable<ValueTuple<int, int, int>> TriangulateIndices(IEnumerable<Vector> polygon)
```

`IteratorStateMachineAttribute`

| Name    | Type                   | Summary |
|---------|------------------------|---------|
| polygon | `IEnumerable\<Vector>` |         |

> **Returns** - `IEnumerable\<ValueTuple\<int, int, int>>`

[0]: ../../../Heirloom.Core.md
[1]: ../PolygonTools.md
