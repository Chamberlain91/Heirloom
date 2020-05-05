# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## DirectedGraph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

```cs
public sealed class DirectedGraph<T> : IDirectedGraph<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IDirectedGraph\<T>][1], IEnumerable\<T>, IEnumerable

### Properties

[ArcCount][2], [Arcs][3], [VertexCount][4], [Vertices][5]

### Methods

[AddArc][6], [AddVertex][7], [Clear][8], [ContainsArc][9], [ContainsVertex][10], [FindPath][11], [GetArcWeight][12], [GetEnumerator][13], [GetPredecessors][14], [GetSuccessors][15], [RemoveArc][16], [RemoveVertex][17], [SetArcWeight][18], [Traverse][19]

## Properties

#### Instance

| Name             | Type                              | Summary |
|------------------|-----------------------------------|---------|
| [ArcCount][2]    | `int`                             |         |
| [Arcs][3]        | `IEnumerable\<ValueTuple\<T, T>>` |         |
| [VertexCount][4] | `int`                             |         |
| [Vertices][5]    | `IEnumerable\<T>`                 |         |

## Methods

#### Instance

| Name                            | Return Type         | Summary |
|---------------------------------|---------------------|---------|
| [AddArc(T, T, float)][6]        | `void`              |         |
| [AddVertex(T)][7]               | `void`              |         |
| [Clear()][8]                    | `void`              |         |
| [ContainsArc(T, T)][9]          | `bool`              |         |
| [ContainsVertex(T)][10]         | `bool`              |         |
| [FindPath(T, T, Heurist...][11] | `IReadOnlyList\<T>` |         |
| [FindPath(T, Func<T, bo...][11] | `IReadOnlyList\<T>` |         |
| [GetArcWeight(T, T)][12]        | `float`             |         |
| [GetEnumerator()][13]           | `IEnumerator\<T>`   |         |
| [GetPredecessors(T)][14]        | `IEnumerable\<T>`   |         |
| [GetSuccessors(T)][15]          | `IEnumerable\<T>`   |         |
| [RemoveArc(T, T)][16]           | `bool`              |         |
| [RemoveVertex(T)][17]           | `bool`              |         |
| [SetArcWeight(T, T, float)][18] | `void`              |         |
| [Traverse(T, TraversalM...][19] | `IEnumerable\<T>`   |         |

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
[13]: DirectedGraph[T]/GetEnumerator.md
[14]: DirectedGraph[T]/GetPredecessors.md
[15]: DirectedGraph[T]/GetSuccessors.md
[16]: DirectedGraph[T]/RemoveArc.md
[17]: DirectedGraph[T]/RemoveVertex.md
[18]: DirectedGraph[T]/SetArcWeight.md
[19]: DirectedGraph[T]/Traverse.md
