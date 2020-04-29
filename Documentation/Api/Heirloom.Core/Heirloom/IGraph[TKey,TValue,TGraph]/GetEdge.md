# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.GetEdge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### GetEdge(TKey, TKey)

Returns the edge between two vertices.

```cs
public abstract IGraphEdge<TKey> GetEdge(TKey keyA, TKey keyB)
```

| Name | Type   | Summary                           |
|------|--------|-----------------------------------|
| keyA | `TKey` | The name/key of the start vertex. |
| keyB | `TKey` | The name/key of the end vertex.   |

> **Returns** - [IGraphEdge\<TKey>][2] - A representation of the edge between the vertices.

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
[2]: ../IGraphEdge[TKey].md
