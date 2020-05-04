# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## DirectedGraph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

```cs
public sealed class DirectedGraph<T> : IGraph<T>
```

### Inherits

[IGraph\<T>][1]

### Properties

[EdgeCount][2], [Edges][3], [IsDirected][4], [VertexCount][5], [Vertices][6]

### Methods

[Add][7], [Clear][8], [Connect][9], [Contains][10], [Disconnect][11], [FindPath][12], [GetSuccessors][13], [GetWeight][14], [IsConnected][15], [Remove][16], [SetWeight][17], [Traverse][18]

## Properties

#### Instance

| Name             | Type                              | Summary |
|------------------|-----------------------------------|---------|
| [EdgeCount][2]   | `int`                             |         |
| [Edges][3]       | `IEnumerable\<ValueTuple\<T, T>>` |         |
| [IsDirected][4]  | `bool`                            |         |
| [VertexCount][5] | `int`                             |         |
| [Vertices][6]    | `IEnumerable\<T>`                 |         |

## Methods

#### Instance

| Name                            | Return Type         | Summary |
|---------------------------------|---------------------|---------|
| [Add(T)][7]                     | `void`              |         |
| [Clear()][8]                    | `void`              |         |
| [Connect(T, T, float)][9]       | `void`              |         |
| [Contains(T)][10]               | `bool`              |         |
| [Disconnect(T, T)][11]          | `void`              |         |
| [FindPath(T, T, Heurist...][12] | `IReadOnlyList\<T>` |         |
| [FindPath(T, Func<T, bo...][12] | `IReadOnlyList\<T>` |         |
| [GetSuccessors(T)][13]          | `IEnumerable\<T>`   |         |
| [GetWeight(T, T)][14]           | `float`             |         |
| [IsConnected(T, T)][15]         | `bool`              |         |
| [Remove(T)][16]                 | `bool`              |         |
| [SetWeight(T, T, float)][17]    | `void`              |         |
| [Traverse(T, TraversalM...][18] | `IEnumerable\<T>`   |         |

[0]: ../../Heirloom.Core.md
[1]: IGraph[T].md
[2]: DirectedGraph[T]/EdgeCount.md
[3]: DirectedGraph[T]/Edges.md
[4]: DirectedGraph[T]/IsDirected.md
[5]: DirectedGraph[T]/VertexCount.md
[6]: DirectedGraph[T]/Vertices.md
[7]: DirectedGraph[T]/Add.md
[8]: DirectedGraph[T]/Clear.md
[9]: DirectedGraph[T]/Connect.md
[10]: DirectedGraph[T]/Contains.md
[11]: DirectedGraph[T]/Disconnect.md
[12]: DirectedGraph[T]/FindPath.md
[13]: DirectedGraph[T]/GetSuccessors.md
[14]: DirectedGraph[T]/GetWeight.md
[15]: DirectedGraph[T]/IsConnected.md
[16]: DirectedGraph[T]/Remove.md
[17]: DirectedGraph[T]/SetWeight.md
[18]: DirectedGraph[T]/Traverse.md
