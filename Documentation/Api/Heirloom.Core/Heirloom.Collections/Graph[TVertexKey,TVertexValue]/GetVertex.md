# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.GetVertex (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### GetVertex(TVertexKey)

Gets a vertex identified with the given key.

```cs
public IGraphVertex<TVertexKey, TVertexValue> GetVertex(TVertexKey key)
```

| Name | Type         | Summary                      |
|------|--------------|------------------------------|
| key  | `TVertexKey` | Some known key in the graph. |

> **Returns** - [IGraphVertex\<TVertexKey, TVertexValue>][2] - An instance of the vertex created when a key-value pair was added t...

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
[2]: ../IGraphVertex[TVertexKey,TVertexValue].md
