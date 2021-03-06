# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## DirectedGraph\<T>.Traverse (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [DirectedGraph\<T>][1]

### Traverse(T, TraversalMethod)

Traverses the graph by the specified method.

```cs
public IEnumerable<T> Traverse(T start, TraversalMethod method)
```

| Name   | Type                 | Summary                      |
|--------|----------------------|------------------------------|
| start  | `T`                  | Some starting veretx.        |
| method | [TraversalMethod][2] | The desired traveral method. |

> **Returns** - `IEnumerable<T>` - A traveral of vertices in the graph.

[0]: ../../../Heirloom.Core.md
[1]: ../DirectedGraph[T].md
[2]: ../TraversalMethod.md
