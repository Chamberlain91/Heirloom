# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue>.RemoveVertex (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graph\<TVertexKey, TVertexValue>][1]

### RemoveVertex(TVertexKey)

Removes the given vertex from the graph ( also disconnects associated edges ).

```cs
public bool RemoveVertex(TVertexKey key)
```

| Name | Type         | Summary                               |
|------|--------------|---------------------------------------|
| key  | `TVertexKey` | Some name of a node within the graph. |

> **Returns** - `bool` - True, if the element existed and was removed.

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[TVertexKey,TVertexValue].md
