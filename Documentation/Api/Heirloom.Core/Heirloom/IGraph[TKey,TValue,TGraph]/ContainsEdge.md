# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.ContainsEdge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### ContainsEdge(TKey, TKey)

Determine if an edge exists between two existing vertices.

```cs
public abstract bool ContainsEdge(TKey keyA, TKey keyB)
```

| Name | Type   | Summary                           |
|------|--------|-----------------------------------|
| keyA | `TKey` | The name/key of the start vertex. |
| keyB | `TKey` | The name/key of the end vertex.   |

> **Returns** - `bool` - True, if the vertex exists.

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
