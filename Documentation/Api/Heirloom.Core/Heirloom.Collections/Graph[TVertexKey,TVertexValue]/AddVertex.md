# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.AddVertex (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### AddVertex(TVertexKey, TVertexValue)

Adds a vertex to the given graph via the given name/key.

```cs
public bool AddVertex(TVertexKey key, TVertexValue element)
```

| Name    | Type           | Summary                               |
|---------|----------------|---------------------------------------|
| key     | `TVertexKey`   | The name/key to identify the element. |
| element | `TVertexValue` | Some element to store in the graph.   |

> **Returns** - `bool` - True, if the element could be added...?

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
