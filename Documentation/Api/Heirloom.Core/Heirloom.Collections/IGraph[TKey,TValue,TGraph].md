# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraph\<TKey, TValue, TGraph> (Interface)

> **Namespace**: [Heirloom.Collections][0]

```cs
public interface IGraph<TKey, TValue, TGraph>
```

### Properties

[AllowNegativeWeight][1], [AllowSelfLoops][2], [EdgeCount][3], [Edges][4], [IsUndirected][5], [Keys][6], [Values][7], [VertexCount][8], [Vertices][9]

### Methods

[AddEdge][10], [AddVertex][11], [Clear][12], [ClearEdges][13], [ContainsEdge][14], [ContainsValue][15], [ContainsVertex][16], [GetEdge][17], [GetVertex][18], [RemoveEdge][19], [RemoveVertex][20]

## Properties

#### Instance

| Name                     | Type                                        | Summary                                                   |
|--------------------------|---------------------------------------------|-----------------------------------------------------------|
| [AllowNegativeWeight][1] | `bool`                                      | Are edges allowed to have a negative weight?              |
| [AllowSelfLoops][2]      | `bool`                                      | Are self looping edges allowed?                           |
| [EdgeCount][3]           | `int`                                       | The number of edges within this graph.                    |
| [Edges][4]               | `IEnumerable\<IGraphEdge\<TKey>>`           | The edges contained within this graph.                    |
| [IsUndirected][5]        | `bool`                                      | Is this graph undirected?                                 |
| [Keys][6]                | `IEnumerable\<TKey>`                        | The names/keys used to lookup the vertices in this graph. |
| [Values][7]              | `IEnumerable\<TValue>`                      | The data/values contained by the vertices in this graph.  |
| [VertexCount][8]         | `int`                                       | The number of vertices within this graph.                 |
| [Vertices][9]            | `IEnumerable\<IGraphVertex\<TKey, TValue>>` | The vertices contained within this graph.                 |

## Methods

#### Instance

| Name                            | Return Type                       | Summary                                                                |
|---------------------------------|-----------------------------------|------------------------------------------------------------------------|
| [AddEdge(TKey, TKey, fl...][10] | `bool`                            | Connects two vertices by an edge in the graph.                         |
| [AddVertex(TKey, TValue)][11]   | `bool`                            | Adds a vertex to the graph.                                            |
| [Clear()][12]                   | `void`                            | Removes all vertices and edges from the graph.                         |
| [ClearEdges()][13]              | `void`                            | Disconnects all edges from all vertices.                               |
| [ContainsEdge(TKey, TKey)][14]  | `bool`                            | Determine if an edge exists between two existing vertices.             |
| [ContainsValue(TValue)][15]     | `bool`                            | Determines if the graph contains the value.                            |
| [ContainsVertex(TKey)][16]      | `bool`                            | Determine if a vertex with the given name/key exists within the graph. |
| [GetEdge(TKey, TKey)][17]       | [IGraphEdge\<TKey>][21]           | Returns the edge between two vertices.                                 |
| [GetVertex(TKey)][18]           | [IGraphVertex\<TKey, TValue>][22] | Returns the vertex with the given name/key.                            |
| [RemoveEdge(TKey, TKey)][19]    | `bool`                            | Removes an edge between two vertices in the graph.                     |
| [RemoveVertex(TKey)][20]        | `bool`                            | Removes a vertex from the graph.                                       |

[0]: ../../Heirloom.Core.md
[1]: IGraph[TKey,TValue,TGraph]/AllowNegativeWeight.md
[2]: IGraph[TKey,TValue,TGraph]/AllowSelfLoops.md
[3]: IGraph[TKey,TValue,TGraph]/EdgeCount.md
[4]: IGraph[TKey,TValue,TGraph]/Edges.md
[5]: IGraph[TKey,TValue,TGraph]/IsUndirected.md
[6]: IGraph[TKey,TValue,TGraph]/Keys.md
[7]: IGraph[TKey,TValue,TGraph]/Values.md
[8]: IGraph[TKey,TValue,TGraph]/VertexCount.md
[9]: IGraph[TKey,TValue,TGraph]/Vertices.md
[10]: IGraph[TKey,TValue,TGraph]/AddEdge.md
[11]: IGraph[TKey,TValue,TGraph]/AddVertex.md
[12]: IGraph[TKey,TValue,TGraph]/Clear.md
[13]: IGraph[TKey,TValue,TGraph]/ClearEdges.md
[14]: IGraph[TKey,TValue,TGraph]/ContainsEdge.md
[15]: IGraph[TKey,TValue,TGraph]/ContainsValue.md
[16]: IGraph[TKey,TValue,TGraph]/ContainsVertex.md
[17]: IGraph[TKey,TValue,TGraph]/GetEdge.md
[18]: IGraph[TKey,TValue,TGraph]/GetVertex.md
[19]: IGraph[TKey,TValue,TGraph]/RemoveEdge.md
[20]: IGraph[TKey,TValue,TGraph]/RemoveVertex.md
[21]: IGraphEdge[TKey].md
[22]: IGraphVertex[TKey,TValue].md
