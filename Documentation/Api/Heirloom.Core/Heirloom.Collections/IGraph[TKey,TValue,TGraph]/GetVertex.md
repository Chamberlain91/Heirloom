# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.GetVertex (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### GetVertex(TKey)

Returns the vertex with the given name/key.

```cs
public abstract IGraphVertex<TKey, TValue> GetVertex(TKey key)
```

| Name | Type   | Summary                   |
|------|--------|---------------------------|
| key  | `TKey` | The name/key of a vertex. |

> **Returns** - [IGraphVertex\<TKey, TValue>][2] - A representation of the vertex with the given name.

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
[2]: ../IGraphVertex[TKey,TValue].md
