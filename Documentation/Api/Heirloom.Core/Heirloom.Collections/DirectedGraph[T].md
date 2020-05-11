# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## DirectedGraph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

A directed graph implemented using adjacency lists.

```cs
public sealed class DirectedGraph<T> : IDirectedGraph<T>
```

### Inherits

[IDirectedGraph\<T>][1]

### Properties

[ArcCount][2], [Arcs][3], [VertexCount][4], [Vertices][5]

### Methods

[AddArc][6], [AddVertex][7], [Clear][8], [ContainsArc][9], [ContainsVertex][10], [FindPath][11], [GetArcWeight][12], [GetPredecessors][13], [GetSuccessors][14], [RemoveArc][15], [RemoveVertex][16], [SetArcWeight][17], [Traverse][18]

## Properties

#### Instance

| Name             | Type                            | Summary                                                          |
|------------------|---------------------------------|------------------------------------------------------------------|
| [ArcCount][2]    | `int`                           | Gets the number of arcs in the directed graph.                   |
| [Arcs][3]        | `IEnumerable<ValueTuple<T, T>>` | Gets a collection containing the arcs in the directed graph.     |
| [VertexCount][4] | `int`                           | Gets the number of vertices in the directed graph.               |
| [Vertices][5]    | `IEnumerable<T>`                | Gets a collection containing the vertices in the directed graph. |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                                                |
|---------------------------------|--------------------|------------------------------------------------------------------------|
| [AddArc(T, T, float)][6]        | `void`             | Inserts a new arc into the directed graph.                             |
| [AddVertex(T)][7]               | `void`             | Inserts a vertex into the directed graph.                              |
| [Clear()][8]                    | `void`             | Clears the directed graph. Removing all vertices and arcs.             |
| [ContainsArc(T, T)][9]          | `bool`             | Determines if the directed graph contains the specified arc.           |
| [ContainsVertex(T)][10]         | `bool`             | Determines if the directed graph contains the specified vertex.        |
| [FindPath(T, T, Heurist...][11] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [FindPath(T, Func<T, bo...][11] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [GetArcWeight(T, T)][12]        | `float`            | Gets the weight of some arc.                                           |
| [GetPredecessors(T)][13]        | `IEnumerable<T>`   | Gets the predecessors (incoming neighbor) vertices.                    |
| [GetSuccessors(T)][14]          | `IEnumerable<T>`   | Gets the successor (outgoing neighbor) vertices.                       |
| [RemoveArc(T, T)][15]           | `bool`             | Removes an arc from the directed graph.                                |
| [RemoveVertex(T)][16]           | `bool`             | Removes a vertex from the directed graph.                              |
| [SetArcWeight(T, T, float)][17] | `void`             | Sets the weight of some arc.                                           |
| [Traverse(T, TraversalM...][18] | `IEnumerable<T>`   | Traverses the graph by the specified method.                           |

[0]: ../../Heirloom.Core.md
[1]: IDirectedGraph[T].md
[2]: DirectedGraph[T]/ArcCount.md
[3]: DirectedGraph[T]/Arcs.md
[4]: DirectedGraph[T]/VertexCount.md
[5]: DirectedGraph[T]/Vertices.md
[6]: DirectedGraph[T]/AddArc.md
[7]: DirectedGraph[T]/AddVertex.md
[8]: DirectedGraph[T]/Clear.md
[9]: DirectedGraph[T]/ContainsArc.md
[10]: DirectedGraph[T]/ContainsVertex.md
[11]: DirectedGraph[T]/FindPath.md
[12]: DirectedGraph[T]/GetArcWeight.md
[13]: DirectedGraph[T]/GetPredecessors.md
[14]: DirectedGraph[T]/GetSuccessors.md
[15]: DirectedGraph[T]/RemoveArc.md
[16]: DirectedGraph[T]/RemoveVertex.md
[17]: DirectedGraph[T]/SetArcWeight.md
[18]: DirectedGraph[T]/Traverse.md
