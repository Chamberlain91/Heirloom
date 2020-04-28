# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IGraph\<TKey, TValue, TGraph>

> **Namespace**: [Heirloom][0]  

```cs
public abstract interface IGraph<TKey, TValue, TGraph>
```

#### Properties

[IsUndirected][1], [AllowSelfLoops][2], [AllowNegativeWeight][3], [VertexCount][4], [EdgeCount][5], [Edges][6], [Vertices][7], [Values][8], [Keys][9]

#### Methods

[Clear][10], [ClearEdges][11], [ContainsVertex][12], [ContainsEdge][13], [ContainsValue][14], [AddVertex][15], [RemoveVertex][16], [GetVertex][17], [AddEdge][18], [RemoveEdge][19], [GetEdge][20]

## Properties

| Name                     | Summary                                                   |
|--------------------------|-----------------------------------------------------------|
| [IsUndirected][1]        | Is this graph undirected?                                 |
| [AllowSelfLoops][2]      | Are self looping edges allowed?                           |
| [AllowNegativeWeight][3] | Are edges allowed to have a negative weight?              |
| [VertexCount][4]         | The number of vertices within this graph.                 |
| [EdgeCount][5]           | The number of edges within this graph.                    |
| [Edges][6]               | The edges contained within this graph.                    |
| [Vertices][7]            | The vertices contained within this graph.                 |
| [Values][8]              | The data/values contained by the vertices in this graph.  |
| [Keys][9]                | The names/keys used to lookup the vertices in this graph. |

## Methods

| Name                 | Summary                                                                |
|----------------------|------------------------------------------------------------------------|
| [Clear][10]          | Removes all vertices and edges from the graph.                         |
| [ClearEdges][11]     | Disconnects all edges from all vertices.                               |
| [ContainsVertex][12] | Determine if a vertex with the given name/key exists within the graph. |
| [ContainsEdge][13]   | Determine if an edge exists between two existing vertices.             |
| [ContainsValue][14]  | Determines if the graph contains the value.                            |
| [AddVertex][15]      | Adds a vertex to the graph.                                            |
| [RemoveVertex][16]   | Removes a vertex from the graph.                                       |
| [GetVertex][17]      | Returns the vertex with the given name/key.                            |
| [AddEdge][18]        | Connects two vertices by an edge in the graph.                         |
| [RemoveEdge][19]     | Removes an edge between two vertices in the graph.                     |
| [GetEdge][20]        | Returns the edge between two vertices.                                 |

[0]: ../../Heirloom.Core.md
[1]: IGraph[TKey,TValue,TGraph]/IsUndirected.md
[2]: IGraph[TKey,TValue,TGraph]/AllowSelfLoops.md
[3]: IGraph[TKey,TValue,TGraph]/AllowNegativeWeight.md
[4]: IGraph[TKey,TValue,TGraph]/VertexCount.md
[5]: IGraph[TKey,TValue,TGraph]/EdgeCount.md
[6]: IGraph[TKey,TValue,TGraph]/Edges.md
[7]: IGraph[TKey,TValue,TGraph]/Vertices.md
[8]: IGraph[TKey,TValue,TGraph]/Values.md
[9]: IGraph[TKey,TValue,TGraph]/Keys.md
[10]: IGraph[TKey,TValue,TGraph]/Clear.md
[11]: IGraph[TKey,TValue,TGraph]/ClearEdges.md
[12]: IGraph[TKey,TValue,TGraph]/ContainsVertex.md
[13]: IGraph[TKey,TValue,TGraph]/ContainsEdge.md
[14]: IGraph[TKey,TValue,TGraph]/ContainsValue.md
[15]: IGraph[TKey,TValue,TGraph]/AddVertex.md
[16]: IGraph[TKey,TValue,TGraph]/RemoveVertex.md
[17]: IGraph[TKey,TValue,TGraph]/GetVertex.md
[18]: IGraph[TKey,TValue,TGraph]/AddEdge.md
[19]: IGraph[TKey,TValue,TGraph]/RemoveEdge.md
[20]: IGraph[TKey,TValue,TGraph]/GetEdge.md
