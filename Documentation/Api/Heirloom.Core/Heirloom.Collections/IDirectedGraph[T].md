# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IDirectedGraph\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

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

| Name             | Type                              | Summary |
|------------------|-----------------------------------|---------|
| [ArcCount][1]    | `int`                             |         |
| [Arcs][2]        | `IEnumerable\<ValueTuple\<T, T>>` |         |
| [VertexCount][3] | `int`                             |         |
| [Vertices][4]    | `IEnumerable\<T>`                 |         |

## Methods

#### Instance

| Name                            | Return Type         | Summary |
|---------------------------------|---------------------|---------|
| [AddArc(T, T, float)][5]        | `void`              |         |
| [AddVertex(T)][6]               | `void`              |         |
| [Clear()][7]                    | `void`              |         |
| [ContainsArc(T, T)][8]          | `bool`              |         |
| [ContainsVertex(T)][9]          | `bool`              |         |
| [FindPath(T, T, Heurist...][10] | `IReadOnlyList\<T>` |         |
| [FindPath(T, Func<T, bo...][10] | `IReadOnlyList\<T>` |         |
| [GetArcWeight(T, T)][11]        | `float`             |         |
| [GetPredecessors(T)][12]        | `IEnumerable\<T>`   |         |
| [GetSuccessors(T)][13]          | `IEnumerable\<T>`   |         |
| [RemoveArc(T, T)][14]           | `bool`              |         |
| [RemoveVertex(T)][15]           | `bool`              |         |
| [SetArcWeight(T, T, float)][16] | `void`              |         |
| [Traverse(T, TraversalM...][17] | `IEnumerable\<T>`   |         |

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
