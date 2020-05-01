# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.AddEdge (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### AddEdge(TVertexKey, TVertexKey, float)

Add an edge between two nodes in the graph.

```cs
public bool AddEdge(TVertexKey source, TVertexKey target, float weight = 1)
```

| Name   | Type         | Summary                                                 |
|--------|--------------|---------------------------------------------------------|
| source | `TVertexKey` | Some name of a source node within the graph.            |
| target | `TVertexKey` | Some name of a target node within the graph.            |
| weight | `float`      | Some weight/cost to assign to the newly connected edge. |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
