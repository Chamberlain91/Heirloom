# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search.HeuristicSearch\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Search][1]

### HeuristicSearch<T>(T, T, Func<T, IEnumerable<T>>, ActualCost<T>, HeuristicCost<T>)

```cs
public static IReadOnlyList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
```

| Name          | Type                        | Summary |
|---------------|-----------------------------|---------|
| start         | `T`                         |         |
| goal          | `T`                         |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>` |         |
| cost          | [ActualCost\<T>][2]         |         |
| heuristic     | [HeuristicCost\<T>][3]      |         |

> **Returns** - `IReadOnlyList\<T>`

### HeuristicSearch<T>(T, Func<T, bool>, Func<T, IEnumerable<T>>, ActualCost<T>, HeuristicCost<T>)

```cs
public static IReadOnlyList<T> HeuristicSearch<T>(T start, Func<T, bool> goalCondition, Func<T, IEnumerable<T>> getSuccessors, ActualCost<T> cost, HeuristicCost<T> heuristic)
```

| Name          | Type                        | Summary |
|---------------|-----------------------------|---------|
| start         | `T`                         |         |
| goalCondition | `Func\<T, bool>`            |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>` |         |
| cost          | [ActualCost\<T>][2]         |         |
| heuristic     | [HeuristicCost\<T>][3]      |         |

> **Returns** - `IReadOnlyList\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../Search.md
[2]: ../ActualCost[T].md
[3]: ../HeuristicCost[T].md
