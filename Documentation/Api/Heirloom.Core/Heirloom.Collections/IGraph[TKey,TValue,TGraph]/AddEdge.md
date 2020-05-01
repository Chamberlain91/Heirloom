# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.AddEdge (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### AddEdge(TKey, TKey, float)

Connects two vertices by an edge in the graph.

```cs
public abstract bool AddEdge(TKey keyA, TKey keyB, float weight)
```

| Name   | Type    | Summary                           |
|--------|---------|-----------------------------------|
| keyA   | `TKey`  | The name/key of the start vertex. |
| keyB   | `TKey`  | The name/key of the end vertex.   |
| weight | `float` | The cost/weight of the edge.      |

> **Returns** - `bool` - True, if the edge complies with !:AllowParallelEdges and vertices e...

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
