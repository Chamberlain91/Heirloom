# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<T>.FindPath (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [IGraph\<T>][1]

### FindPath(T, T, HeuristicCost<T>)

```cs
public abstract IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic)
```

| Name      | Type                   | Summary |
|-----------|------------------------|---------|
| start     | `T`                    |         |
| goal      | `T`                    |         |
| heuristic | [HeuristicCost\<T>][2] |         |

> **Returns** - `IReadOnlyList\<T>`

### FindPath(T, Func<T, bool>, HeuristicCost<T>)

```cs
public abstract IReadOnlyList<T> FindPath(T start, Func<T, bool> goal, HeuristicCost<T> heuristic)
```

| Name      | Type                   | Summary |
|-----------|------------------------|---------|
| start     | `T`                    |         |
| goal      | `Func\<T, bool>`       |         |
| heuristic | [HeuristicCost\<T>][2] |         |

> **Returns** - `IReadOnlyList\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../IGraph[T].md
[2]: ../../Heirloom/HeuristicCost[T].md
