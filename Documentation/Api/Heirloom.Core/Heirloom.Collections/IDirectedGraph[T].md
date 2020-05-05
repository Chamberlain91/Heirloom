# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IDirectedGraph\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

An interface that represents a graph.

```cs
public interface IDirectedGraph<T> : IEnumerable<T>, IEnumerable
```

### Inherits

IEnumerable\<T>, IEnumerable

### Properties

[ArcCount][1], [Arcs][2], [VertexCount][3], [Vertices][4]

### Methods

[AddArc][5], [AddVertex][6], [Clear][7], [ContainsArc][8], [ContainsVertex][9], [FindPath][10], [GetArcWeight][11], [GetPredecessors][12], [GetSuccessors][13], [RemoveArc][14], [RemoveVertex][15], [SetArcWeight][16], [Traverse][17]

## Properties

#### Instance

| Name             | Type                            | Summary                                                          |
|------------------|---------------------------------|------------------------------------------------------------------|
| [ArcCount][1]    | `int`                           | Gets the number of arcs in the directed graph.                   |
| [Arcs][2]        | `IEnumerable<ValueTuple<T, T>>` | Gets a collection containing the arcs in the directed graph.     |
| [VertexCount][3] | `int`                           | Gets the number of vertices in the directed graph.               |
| [Vertices][4]    | `IEnumerable<T>`                | Gets a collection containing the vertices in the directed graph. |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                                                |
|---------------------------------|--------------------|------------------------------------------------------------------------|
| [AddArc(T, T, float)][5]        | `void`             | Inserts a new arc into the directed graph.                             |
| [AddVertex(T)][6]               | `void`             | Inserts a vertex into the directed graph.                              |
| [Clear()][7]                    | `void`             | Clears the directed graph. Removing all vertices and arcs.             |
| [ContainsArc(T, T)][8]          | `bool`             | Determines if the directed graph contains the specified arc.           |
| [ContainsVertex(T)][9]          | `bool`             | Determines if the directed graph contains the specified vertex.        |
| [FindPath(T, T, Heurist...][10] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [FindPath(T, Func<T, bo...][10] | `IReadOnlyList<T>` | Attempts to finds a path between `start` and `goal` vertices using ... |
| [GetArcWeight(T, T)][11]        | `float`            | Gets the weight of some arc.                                           |
| [GetPredecessors(T)][12]        | `IEnumerable<T>`   | Gets the predecessors (incoming neighbor) vertices.                    |
| [GetSuccessors(T)][13]          | `IEnumerable<T>`   | Gets the successor (outgoing neighbor) vertices.                       |
| [RemoveArc(T, T)][14]           | `bool`             | Removes an arc from the directed graph.                                |
| [RemoveVertex(T)][15]           | `bool`             | Removes a vertex from the directed graph.                              |
| [SetArcWeight(T, T, float)][16] | `void`             | Sets the weight of some arc.                                           |
| [Traverse(T, TraversalM...][17] | `IEnumerable<T>`   | Traverses the graph by the specified method.                           |

[0]: ../../Heirloom.Core.md
[1]: IDirectedGraph[T]/ArcCount.md
[2]: IDirectedGraph[T]/Arcs.md
[3]: IDirectedGraph[T]/VertexCount.md
[4]: IDirectedGraph[T]/Vertices.md
[5]: IDirectedGraph[T]/AddArc.md
[6]: IDirectedGraph[T]/AddVertex.md
[7]: IDirectedGraph[T]/Clear.md
[8]: IDirectedGraph[T]/ContainsArc.md
[9]: IDirectedGraph[T]/ContainsVertex.md
[10]: IDirectedGraph[T]/FindPath.md
[11]: IDirectedGraph[T]/GetArcWeight.md
[12]: IDirectedGraph[T]/GetPredecessors.md
[13]: IDirectedGraph[T]/GetSuccessors.md
[14]: IDirectedGraph[T]/RemoveArc.md
[15]: IDirectedGraph[T]/RemoveVertex.md
[16]: IDirectedGraph[T]/SetArcWeight.md
[17]: IDirectedGraph[T]/Traverse.md
