# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<T>.FindPath (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [Graph\<T>][1]

### FindPath(T, T, HeuristicCost<T>)

Attempts to finds a path between `start` and `goal` vertices using the specified `heuristic` .

```cs
public IReadOnlyList<T> FindPath(T start, T goal, HeuristicCost<T> heuristic)
```

| Name      | Type                   | Summary                                            |
|-----------|------------------------|----------------------------------------------------|
| start     | `T`                    | Some starting vertex.                              |
| goal      | `T`                    | Some goal vertex.                                  |
| heuristic | [HeuristicCost\<T>][2] | Some heuristic evaluation of the cost to the goal. |

> **Returns** - `IReadOnlyList<T>` - If exists, the path from `start` to `goal` , otherwise null.

### FindPath(T, Func<T, bool>, HeuristicCost<T>)

Attempts to finds a path between `start` until the first vertex to satisfy the `goalCondition` using the specified `heuristic` .

```cs
public IReadOnlyList<T> FindPath(T start, Func<T, bool> goalCondition, HeuristicCost<T> heuristic)
```

| Name          | Type                   | Summary                                            |
|---------------|------------------------|----------------------------------------------------|
| start         | `T`                    | Some starting vertex.                              |
| goalCondition | `Func<T, bool>`        | Some goal condition.                               |
| heuristic     | [HeuristicCost\<T>][2] | Some heuristic evaluation of the cost to the goal. |

> **Returns** - `IReadOnlyList<T>` - If exists, the path from `start` to the vertex that satisfied the `goalCondition` , otherwise null.

[0]: ../../../Heirloom.Core.md
[1]: ../Graph[T].md
[2]: ../../Heirloom/HeuristicCost[T].md
