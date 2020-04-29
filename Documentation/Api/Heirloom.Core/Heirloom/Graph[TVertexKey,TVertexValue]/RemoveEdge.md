# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.RemoveEdge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### RemoveEdge(TVertexKey, TVertexKey)

Removes an edge between two vertices in the graph.

```cs
public bool RemoveEdge(TVertexKey source, TVertexKey target)
```

| Name   | Type         | Summary                                      |
|--------|--------------|----------------------------------------------|
| source | `TVertexKey` | Some name of a source node within the graph. |
| target | `TVertexKey` | Some name of a target node within the graph. |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
