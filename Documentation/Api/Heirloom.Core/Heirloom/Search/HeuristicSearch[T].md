# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search.HeuristicSearch\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Search][1]

### HeuristicSearch<T>(T, T, Func<T, IEnumerable<T>>, Search.CostFunction<T>)

```cs
public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost)
```

| Name          | Type                         | Summary |
|---------------|------------------------------|---------|
| start         | `T`                          |         |
| goal          | `T`                          |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>`  |         |
| cost          | [Search.CostFunction\<T>][2] |         |

> **Returns** - `IList\<T>`

### HeuristicSearch<T>(T, T, Func<T, IEnumerable<T>>, Search.CostFunction<T>, Search.CostFunction<T>)

```cs
public static IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost, Search.CostFunction<T> heuristic)
```

| Name          | Type                         | Summary |
|---------------|------------------------------|---------|
| start         | `T`                          |         |
| goal          | `T`                          |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>`  |         |
| cost          | [Search.CostFunction\<T>][2] |         |
| heuristic     | [Search.CostFunction\<T>][2] |         |

> **Returns** - `IList\<T>`

### HeuristicSearch<T>(T, Func<T, bool>, Func<T, IEnumerable<T>>, Search.CostFunction<T>, Search.HeuristicFunction<T>)

```cs
public static IList<T> HeuristicSearch<T>(T start, Func<T, bool> targetPredicate, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost, Search.HeuristicFunction<T> heuristic)
```

| Name            | Type                              | Summary |
|-----------------|-----------------------------------|---------|
| start           | `T`                               |         |
| targetPredicate | `Func\<T, bool>`                  |         |
| getSuccessors   | `Func\<T, IEnumerable\<T>>`       |         |
| cost            | [Search.CostFunction\<T>][2]      |         |
| heuristic       | [Search.HeuristicFunction\<T>][3] |         |

> **Returns** - `IList\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../Search.md
[2]: ../Search.CostFunction[T].md
[3]: ../Search.HeuristicFunction[T].md
