# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## UndirectedGraph\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

```cs
public sealed class UndirectedGraph<T> : IGraph<T>
```

### Inherits

[IGraph\<T>][1]

### Properties

[EdgeCount][2], [Edges][3], [IsDirected][4], [VertexCount][5], [Vertices][6]

### Methods

[Add][7], [Clear][8], [Connect][9], [Contains][10], [Disconnect][11], [FindMinimumSpanningTree][12], [FindPath][13], [GetSuccessors][14], [GetWeight][15], [IsConnected][16], [Remove][17], [SetWeight][18], [Traverse][19]

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

| Name                            | Return Type               | Summary                                               |
|---------------------------------|---------------------------|-------------------------------------------------------|
| [Add(T)][7]                     | `void`                    |                                                       |
| [Clear()][8]                    | `void`                    |                                                       |
| [Connect(T, T, float)][9]       | `void`                    |                                                       |
| [Contains(T)][10]               | `bool`                    |                                                       |
| [Disconnect(T, T)][11]          | `void`                    |                                                       |
| [FindMinimumSpanningTree()][12] | [UndirectedGraph\<T>][20] | Finds a minimum spanning tree using Prim's algorithm. |
| [FindPath(T, T, Heurist...][13] | `IReadOnlyList\<T>`       |                                                       |
| [FindPath(T, Func<T, bo...][13] | `IReadOnlyList\<T>`       |                                                       |
| [GetSuccessors(T)][14]          | `IEnumerable\<T>`         |                                                       |
| [GetWeight(T, T)][15]           | `float`                   |                                                       |
| [IsConnected(T, T)][16]         | `bool`                    |                                                       |
| [Remove(T)][17]                 | `bool`                    |                                                       |
| [SetWeight(T, T, float)][18]    | `void`                    |                                                       |
| [Traverse(T, TraversalM...][19] | `IEnumerable\<T>`         |                                                       |

[0]: ../../Heirloom.Core.md
[1]: IGraph[T].md
[2]: UndirectedGraph[T]/EdgeCount.md
[3]: UndirectedGraph[T]/Edges.md
[4]: UndirectedGraph[T]/IsDirected.md
[5]: UndirectedGraph[T]/VertexCount.md
[6]: UndirectedGraph[T]/Vertices.md
[7]: UndirectedGraph[T]/Add.md
[8]: UndirectedGraph[T]/Clear.md
[9]: UndirectedGraph[T]/Connect.md
[10]: UndirectedGraph[T]/Contains.md
[11]: UndirectedGraph[T]/Disconnect.md
[12]: UndirectedGraph[T]/FindMinimumSpanningTree.md
[13]: UndirectedGraph[T]/FindPath.md
[14]: UndirectedGraph[T]/GetSuccessors.md
[15]: UndirectedGraph[T]/GetWeight.md
[16]: UndirectedGraph[T]/IsConnected.md
[17]: UndirectedGraph[T]/Remove.md
[18]: UndirectedGraph[T]/SetWeight.md
[19]: UndirectedGraph[T]/Traverse.md
[20]: UndirectedGraph[T].md
