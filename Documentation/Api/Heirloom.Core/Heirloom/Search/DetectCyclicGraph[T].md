# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search.DetectCyclicGraph\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Search][1]

### DetectCyclicGraph<T>(IGraph<T>, T)

Determines if the specified graph is cyclic.

```cs
public static bool DetectCyclicGraph<T>(IGraph<T> graph, T start)
```

| Name  | Type            | Summary           |
|-------|-----------------|-------------------|
| graph | [IGraph\<T>][2] | Some graph.       |
| start | `T`             | Starting element. |

> **Returns** - `bool` - True if the graph is determined to be cycle free, otherwise false.

### DetectCyclicGraph<T>(T, Func<T, IEnumerable<T>>)

```cs
public static bool DetectCyclicGraph<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
```

| Name          | Type                      | Summary |
|---------------|---------------------------|---------|
| start         | `T`                       |         |
| getSuccessors | `Func<T, IEnumerable<T>>` |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Search.md
[2]: ../../Heirloom.Collections/IGraph[T].md
