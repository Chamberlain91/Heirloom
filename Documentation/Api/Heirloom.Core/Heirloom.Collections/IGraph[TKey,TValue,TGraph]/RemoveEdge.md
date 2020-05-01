# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.RemoveEdge (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### RemoveEdge(TKey, TKey)

Removes an edge between two vertices in the graph.

```cs
public abstract bool RemoveEdge(TKey keyA, TKey keyB)
```

| Name | Type   | Summary                           |
|------|--------|-----------------------------------|
| keyA | `TKey` | The name/key of the start vertex. |
| keyB | `TKey` | The name/key of the end vertex.   |

> **Returns** - `bool` - True, if the edge existed and was removed. This may return true man...

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
