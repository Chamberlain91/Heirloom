# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.GetEdge (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### GetEdge(TVertexKey, TVertexKey)

Gets an edge in the graph.

```cs
public IGraphEdge<TVertexKey> GetEdge(TVertexKey source, TVertexKey target)
```

| Name   | Type         | Summary                                      |
|--------|--------------|----------------------------------------------|
| source | `TVertexKey` | Some name of a source node within the graph. |
| target | `TVertexKey` | Some name of a target node within the graph. |

> **Returns** - [IGraphEdge\<TVertexKey>][2] - An edge representing the connection between source and target verti...

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
[2]: ../IGraphEdge[TVertexKey].md
