# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

```cs
public sealed class Graph<T> : IGraph<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IGraph\<T>][1], IEnumerable\<T>, IEnumerable

### Properties

[EdgeCount][2], [Edges][3], [VertexCount][4], [Vertices][5]

### Methods

[AddEdge][6], [AddVertex][7], [Clear][8], [ContainsEdge][9], [ContainsVertex][10], [FindMinimumSpanningTree][11], [FindPath][12], [GetEdgeWeight][13], [GetEnumerator][14], [GetSuccessors][15], [RemoveEdge][16], [RemoveVertex][17], [SetEdgeWeight][18], [Traverse][19]

## Properties

#### Instance

| Name             | Type                              | Summary |
|------------------|-----------------------------------|---------|
| [EdgeCount][2]   | `int`                             |         |
| [Edges][3]       | `IEnumerable\<ValueTuple\<T, T>>` |         |
| [VertexCount][4] | `int`                             |         |
| [Vertices][5]    | `IEnumerable\<T>`                 |         |

## Methods

#### Instance

| Name                            | Return Type         | Summary |
|---------------------------------|---------------------|---------|
| [AddEdge(T, T, float)][6]       | `void`              |         |
| [AddVertex(T)][7]               | `void`              |         |
| [Clear()][8]                    | `void`              |         |
| [ContainsEdge(T, T)][9]         | `bool`              |         |
| [ContainsVertex(T)][10]         | `bool`              |         |
| [FindMinimumSpanningTree()][11] | [Graph\<T>][20]     |         |
| [FindPath(T, T, Heurist...][12] | `IReadOnlyList\<T>` |         |
| [FindPath(T, Func<T, bo...][12] | `IReadOnlyList\<T>` |         |
| [GetEdgeWeight(T, T)][13]       | `float`             |         |
| [GetEnumerator()][14]           | `IEnumerator\<T>`   |         |
| [GetSuccessors(T)][15]          | `IEnumerable\<T>`   |         |
| [RemoveEdge(T, T)][16]          | `bool`              |         |
| [RemoveVertex(T)][17]           | `bool`              |         |
| [SetEdgeWeight(T, T, fl...][18] | `void`              |         |
| [Traverse(T, TraversalM...][19] | `IEnumerable\<T>`   |         |

[0]: ../../Heirloom.Core.md
[1]: IGraph[T].md
[2]: Graph[T]/EdgeCount.md
[3]: Graph[T]/Edges.md
[4]: Graph[T]/VertexCount.md
[5]: Graph[T]/Vertices.md
[6]: Graph[T]/AddEdge.md
[7]: Graph[T]/AddVertex.md
[8]: Graph[T]/Clear.md
[9]: Graph[T]/ContainsEdge.md
[10]: Graph[T]/ContainsVertex.md
[11]: Graph[T]/FindMinimumSpanningTree.md
[12]: Graph[T]/FindPath.md
[13]: Graph[T]/GetEdgeWeight.md
[14]: Graph[T]/GetEnumerator.md
[15]: Graph[T]/GetSuccessors.md
[16]: Graph[T]/RemoveEdge.md
[17]: Graph[T]/RemoveVertex.md
[18]: Graph[T]/SetEdgeWeight.md
[19]: Graph[T]/Traverse.md
[20]: Graph[T].md
