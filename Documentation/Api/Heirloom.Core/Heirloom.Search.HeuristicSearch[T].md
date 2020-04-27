# Search.HeuristicSearch\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Search][1]  

--------------------------------------------------------------------------------

### HeuristicSearch<T>(T, T, Func<T, IEnumerable<T>>, Search.CostFunction<T>)

```cs
public IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost)
```

### HeuristicSearch<T>(T, T, Func<T, IEnumerable<T>>, Search.CostFunction<T>, Search.CostFunction<T>)

```cs
public IList<T> HeuristicSearch<T>(T start, T goal, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost, Search.CostFunction<T> heuristic)
```

### HeuristicSearch<T>(T, Func<T, bool>, Func<T, IEnumerable<T>>, Search.CostFunction<T>, Search.HeuristicFunction<T>)

```cs
public IList<T> HeuristicSearch<T>(T start, Func<T, bool> targetPredicate, Func<T, IEnumerable<T>> getSuccessors, Search.CostFunction<T> cost, Search.HeuristicFunction<T> heuristic)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Search.md
