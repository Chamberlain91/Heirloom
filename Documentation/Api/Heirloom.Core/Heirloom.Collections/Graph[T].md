# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

An undirected graph implemented using adjacency lists.

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

| Name             | Type                            | Summary                                                 |
|------------------|---------------------------------|---------------------------------------------------------|
| [EdgeCount][2]   | `int`                           | Gets the number of edges in the graph.                  |
| [Edges][3]       | `IEnumerable<ValueTuple<T, T>>` | Gets a collection containing the edges in the graph.    |
| [VertexCount][4] | `int`                           | Gets the number of vertices in the graph.               |
| [Vertices][5]    | `IEnumerable<T>`                | Gets a collection containing the vertices in the graph. |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                                                |
|---------------------------------|--------------------|------------------------------------------------------------------------|
| [AddEdge(T, T, float)][6]       | `void`             | Inserts a new edge into the graph.                                     |
| [AddVertex(T)][7]               | `void`             | Inserts a vertex into the graph.                                       |
| [Clear()][8]                    | `void`             | Clears the graph. Removing all vertices and edges.                     |
| [ContainsEdge(T, T)][9]         | `bool`             | Determines if the graph contains the specified edge.                   |
| [ContainsVertex(T)][10]         | `bool`             | Determines if the graph contains the specified vertex.                 |
| [FindMinimumSpanningTree()][11] | [Graph\<T>][20]    | Finds and returns a minimum spanning tree.                             |
| [FindPath(T, T, Heurist...][12] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [FindPath(T, Func<T, bo...][12] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [GetEdgeWeight(T, T)][13]       | `float`            | Gets the weight of some edge.                                          |
| [GetEnumerator()][14]           | `IEnumerator<T>`   |                                                                        |
| [GetSuccessors(T)][15]          | `IEnumerable<T>`   | Gets the successor (outgoing neighbor) vertices.                       |
| [RemoveEdge(T, T)][16]          | `bool`             | Removes an edge from the graph.                                        |
| [RemoveVertex(T)][17]           | `bool`             | Removes a vertex from the graph.                                       |
| [SetEdgeWeight(T, T, fl...][18] | `void`             | Sets the weight of some edge.                                          |
| [Traverse(T, TraversalM...][19] | `IEnumerable<T>`   | Traverses the graph by the specified method.                           |

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
