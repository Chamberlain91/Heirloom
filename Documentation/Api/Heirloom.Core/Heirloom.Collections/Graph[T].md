# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

An undirected graph implemented using adjacency lists.

```cs
public sealed class Graph<T> : IGraph<T>
```

### Inherits

[IGraph\<T>][1]

### Properties

[EdgeCount][2], [Edges][3], [VertexCount][4], [Vertices][5]

### Methods

[AddEdge][6], [AddVertex][7], [Clear][8], [ContainsEdge][9], [ContainsVertex][10], [FindMinimumSpanningTree][11], [FindPath][12], [GetEdgeWeight][13], [GetNeighbors][14], [RemoveEdge][15], [RemoveVertex][16], [SetEdgeWeight][17], [Traverse][18]

## Properties

#### Instance

| Name             | Type                            | Summary                                   |
|------------------|---------------------------------|-------------------------------------------|
| [EdgeCount][2]   | `int`                           | Gets the number of edges in the graph.    |
| [Edges][3]       | `IEnumerable<ValueTuple<T, T>>` | Gets the edges in the graph.              |
| [VertexCount][4] | `int`                           | Gets the number of vertices in the graph. |
| [Vertices][5]    | `IEnumerable<T>`                | Gets the vertices in the graph.           |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                                                |
|---------------------------------|--------------------|------------------------------------------------------------------------|
| [AddEdge(T, T, float)][6]       | `void`             | Inserts a new edge into the graph.                                     |
| [AddVertex(T)][7]               | `void`             | Inserts a vertex into the graph.                                       |
| [Clear()][8]                    | `void`             | Clears the graph. Removing all vertices and edges.                     |
| [ContainsEdge(T, T)][9]         | `bool`             | Determines if the graph contains the specified edge.                   |
| [ContainsVertex(T)][10]         | `bool`             | Determines if the graph contains the specified vertex.                 |
| [FindMinimumSpanningTree()][11] | [Graph\<T>][19]    | Finds and returns a minimum spanning tree.                             |
| [FindPath(T, T, Heurist...][12] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [FindPath(T, Func<T, bo...][12] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [GetEdgeWeight(T, T)][13]       | `float`            | Gets the weight of some edge.                                          |
| [GetNeighbors(T)][14]           | `IEnumerable<T>`   | Gets the neighboring vertices.                                         |
| [RemoveEdge(T, T)][15]          | `bool`             | Removes an edge from the graph.                                        |
| [RemoveVertex(T)][16]           | `bool`             | Removes a vertex from the graph.                                       |
| [SetEdgeWeight(T, T, fl...][17] | `void`             | Sets the weight of some edge.                                          |
| [Traverse(T, TraversalM...][18] | `IEnumerable<T>`   | Traverses the graph by the specified method.                           |

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
[14]: Graph[T]/GetNeighbors.md
[15]: Graph[T]/RemoveEdge.md
[16]: Graph[T]/RemoveVertex.md
[17]: Graph[T]/SetEdgeWeight.md
[18]: Graph[T]/Traverse.md
[19]: Graph[T].md
