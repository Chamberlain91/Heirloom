# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

```cs
public interface IGraph<T>
```

### Properties

[EdgeCount][1], [Edges][2], [IsDirected][3], [VertexCount][4], [Vertices][5]

### Methods

[Add][6], [Clear][7], [Connect][8], [Contains][9], [Disconnect][10], [FindMinimumSpanningTree][11], [FindPath][12], [GetSuccessors][13], [GetWeight][14], [IsConnected][15], [Remove][16], [SetWeight][17], [Traverse][18]

## Properties

#### Instance

| Name             | Type                              | Summary |
|------------------|-----------------------------------|---------|
| [EdgeCount][1]   | `int`                             |         |
| [Edges][2]       | `IEnumerable\<ValueTuple\<T, T>>` |         |
| [IsDirected][3]  | `bool`                            |         |
| [VertexCount][4] | `int`                             |         |
| [Vertices][5]    | `IEnumerable\<T>`                 |         |

## Methods

#### Instance

| Name                            | Return Type         | Summary |
|---------------------------------|---------------------|---------|
| [Add(T)][6]                     | `void`              |         |
| [Clear()][7]                    | `void`              |         |
| [Connect(T, T, float)][8]       | `void`              |         |
| [Contains(T)][9]                | `bool`              |         |
| [Disconnect(T, T)][10]          | `void`              |         |
| [FindMinimumSpanningTree()][11] | [IGraph\<T>][19]    |         |
| [FindPath(T, T, Heurist...][12] | `IReadOnlyList\<T>` |         |
| [FindPath(T, Func<T, bo...][12] | `IReadOnlyList\<T>` |         |
| [GetSuccessors(T)][13]          | `IEnumerable\<T>`   |         |
| [GetWeight(T, T)][14]           | `float`             |         |
| [IsConnected(T, T)][15]         | `bool`              |         |
| [Remove(T)][16]                 | `bool`              |         |
| [SetWeight(T, T, float)][17]    | `void`              |         |
| [Traverse(T, TraversalM...][18] | `IEnumerable\<T>`   |         |

[0]: ../../Heirloom.Core.md
[1]: IGraph[T]/EdgeCount.md
[2]: IGraph[T]/Edges.md
[3]: IGraph[T]/IsDirected.md
[4]: IGraph[T]/VertexCount.md
[5]: IGraph[T]/Vertices.md
[6]: IGraph[T]/Add.md
[7]: IGraph[T]/Clear.md
[8]: IGraph[T]/Connect.md
[9]: IGraph[T]/Contains.md
[10]: IGraph[T]/Disconnect.md
[11]: IGraph[T]/FindMinimumSpanningTree.md
[12]: IGraph[T]/FindPath.md
[13]: IGraph[T]/GetSuccessors.md
[14]: IGraph[T]/GetWeight.md
[15]: IGraph[T]/IsConnected.md
[16]: IGraph[T]/Remove.md
[17]: IGraph[T]/SetWeight.md
[18]: IGraph[T]/Traverse.md
[19]: IGraph[T].md
