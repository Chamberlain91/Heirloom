# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

An interface that represents a graph.

```cs
public interface IGraph<T> : IEnumerable<T>, IEnumerable
```

### Inherits

IEnumerable\<T>, IEnumerable

### Properties

[EdgeCount][1], [Edges][2], [VertexCount][3], [Vertices][4]

### Methods

[AddEdge][5], [AddVertex][6], [Clear][7], [ContainsEdge][8], [ContainsVertex][9], [FindMinimumSpanningTree][10], [FindPath][11], [GetEdgeWeight][12], [GetSuccessors][13], [RemoveEdge][14], [RemoveVertex][15], [SetEdgeWeight][16], [Traverse][17]

## Properties

#### Instance

| Name             | Type                            | Summary                                                 |
|------------------|---------------------------------|---------------------------------------------------------|
| [EdgeCount][1]   | `int`                           | Gets the number of edges in the graph.                  |
| [Edges][2]       | `IEnumerable<ValueTuple<T, T>>` | Gets a collection containing the edges in the graph.    |
| [VertexCount][3] | `int`                           | Gets the number of vertices in the graph.               |
| [Vertices][4]    | `IEnumerable<T>`                | Gets a collection containing the vertices in the graph. |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                                                |
|---------------------------------|--------------------|------------------------------------------------------------------------|
| [AddEdge(T, T, float)][5]       | `void`             | Inserts a new edge into the graph.                                     |
| [AddVertex(T)][6]               | `void`             | Inserts a vertex into the graph.                                       |
| [Clear()][7]                    | `void`             | Clears the graph. Removing all vertices and edges.                     |
| [ContainsEdge(T, T)][8]         | `bool`             | Determines if the graph contains the specified edge.                   |
| [ContainsVertex(T)][9]          | `bool`             | Determines if the graph contains the specified vertex.                 |
| [FindMinimumSpanningTree()][10] | [IGraph\<T>][18]   | Finds and returns a minimum spanning tree.                             |
| [FindPath(T, T, Heurist...][11] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [FindPath(T, Func<T, bo...][11] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [GetEdgeWeight(T, T)][12]       | `float`            | Gets the weight of some edge.                                          |
| [GetSuccessors(T)][13]          | `IEnumerable<T>`   | Gets the successor (outgoing neighbor) vertices.                       |
| [RemoveEdge(T, T)][14]          | `bool`             | Removes an edge from the graph.                                        |
| [RemoveVertex(T)][15]           | `bool`             | Removes a vertex from the graph.                                       |
| [SetEdgeWeight(T, T, fl...][16] | `void`             | Sets the weight of some edge.                                          |
| [Traverse(T, TraversalM...][17] | `IEnumerable<T>`   | Traverses the graph by the specified method.                           |

[0]: ../../Heirloom.Core.md
[1]: IGraph[T]/EdgeCount.md
[2]: IGraph[T]/Edges.md
[3]: IGraph[T]/VertexCount.md
[4]: IGraph[T]/Vertices.md
[5]: IGraph[T]/AddEdge.md
[6]: IGraph[T]/AddVertex.md
[7]: IGraph[T]/Clear.md
[8]: IGraph[T]/ContainsEdge.md
[9]: IGraph[T]/ContainsVertex.md
[10]: IGraph[T]/FindMinimumSpanningTree.md
[11]: IGraph[T]/FindPath.md
[12]: IGraph[T]/GetEdgeWeight.md
[13]: IGraph[T]/GetSuccessors.md
[14]: IGraph[T]/RemoveEdge.md
[15]: IGraph[T]/RemoveVertex.md
[16]: IGraph[T]/SetEdgeWeight.md
[17]: IGraph[T]/Traverse.md
[18]: IGraph[T].md
