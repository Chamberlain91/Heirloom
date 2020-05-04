# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search (Class)

> **Namespace**: [Heirloom][0]

```cs
public static class Search
```

### Static Methods

[BreadthFirst\<T>][1], [DepthFirst\<T>][2], [DetectCyclicGraph\<T>][3], [HeuristicSearch\<T>][4]

## Methods

| Name                           | Return Type         | Summary                                      |
|--------------------------------|---------------------|----------------------------------------------|
| [BreadthFirst<T>(T, Fun...][1] | `IEnumerable\<T>`   |                                              |
| [DepthFirst<T>(T, Func<...][2] | `IEnumerable\<T>`   |                                              |
| [DetectCyclicGraph<T>(I...][3] | `bool`              | Determines if the specified graph is cyclic. |
| [DetectCyclicGraph<T>(T...][3] | `bool`              |                                              |
| [HeuristicSearch<T>(T, ...][4] | `IReadOnlyList\<T>` |                                              |
| [HeuristicSearch<T>(T, ...][4] | `IReadOnlyList\<T>` |                                              |

[0]: ../../Heirloom.Core.md
[1]: Search/BreadthFirst[T].md
[2]: Search/DepthFirst[T].md
[3]: Search/DetectCyclicGraph[T].md
[4]: Search/HeuristicSearch[T].md
