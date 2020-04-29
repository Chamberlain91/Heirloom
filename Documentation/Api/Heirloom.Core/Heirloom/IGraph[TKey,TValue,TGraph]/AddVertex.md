# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph>.AddVertex (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IGraph\<TKey, TValue, TGraph>][1]

### AddVertex(TKey, TValue)

Adds a vertex to the graph.

```cs
public abstract bool AddVertex(TKey key, TValue value)
```

| Name  | Type     | Summary                      |
|-------|----------|------------------------------|
| key   | `TKey`   | The name/key of a vertex.    |
| value | `TValue` | The data/value of the graph. |

> **Returns** - `bool` - True, if the name/key was unique and the vertex was added.

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[TKey,TValue,TGraph].md
