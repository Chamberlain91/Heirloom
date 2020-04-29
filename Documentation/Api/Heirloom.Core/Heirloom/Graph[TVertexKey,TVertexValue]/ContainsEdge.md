# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.ContainsEdge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### ContainsEdge(TVertexKey, TVertexKey)

Determines if the graph contains an edge bewtween source and target vertices.

```cs
public bool ContainsEdge(TVertexKey source, TVertexKey target)
```

| Name   | Type         | Summary                                      |
|--------|--------------|----------------------------------------------|
| source | `TVertexKey` | Some name of a source node within the graph. |
| target | `TVertexKey` | Some name of a target node within the graph. |

> **Returns** - `bool` - True, if the edge was contained.

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
