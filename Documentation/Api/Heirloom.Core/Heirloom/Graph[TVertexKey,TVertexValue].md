# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graph\<TVertexKey, TVertexValue> (Class)

> **Namespace**: [Heirloom][0]

A configurable adjacency list based graph.

```cs
public class Graph<TVertexKey, TVertexValue> : IGraph<TVertexKey, TVertexValue, Graph<TVertexKey, TVertexValue>>
```

### Inherits

[IGraph\<TVertexKey, TVertexValue, Graph\<TVertexKey, TVertexValue>>][1]

### Properties

[AllowNegativeWeight][2], [AllowSelfLoops][3], [EdgeCount][4], [Edges][5], [IsUndirected][6], [Keys][7], [Values][8], [VertexCount][9], [Vertices][10]

### Methods

[AddEdge][11], [AddVertex][12], [Clear][13], [ClearEdges][14], [ContainsEdge][15], [ContainsValue][16], [ContainsVertex][17], [GetEdge][18], [GetVertex][19], [RemoveEdge][20], [RemoveVertex][21]

## Properties

#### Instance

| Name                     | Type                                                    | Summary                                                                |
|--------------------------|---------------------------------------------------------|------------------------------------------------------------------------|
| [AllowNegativeWeight][2] | `bool`                                                  | Was this graph allowed to have negative edges weights?                 |
| [AllowSelfLoops][3]      | `bool`                                                  | Was this graph allowed to have self connecting loops ( Ex, 'A' conn... |
| [EdgeCount][4]           | `int`                                                   | The number of edges stored in the graph.                               |
| [Edges][5]               | `IEnumerable\<IGraphEdge\<TVertexKey>>`                 | An enumeration of all edges within the graph.                          |
| [IsUndirected][6]        | `bool`                                                  | Is this graph configured to have directed edges?                       |
| [Keys][7]                | `IEnumerable\<TVertexKey>`                              | An enumeration of all the names/keys of the vertices in the graph.     |
| [Values][8]              | `IEnumerable\<TVertexValue>`                            | An enumeration of all the elements stored in the vertices in the gr... |
| [VertexCount][9]         | `int`                                                   | The number of vertices / elements stored in the graph.                 |
| [Vertices][10]           | `IEnumerable\<IGraphVertex\<TVertexKey, TVertexValue>>` | An enumeration of all vertices within the graph.                       |

## Methods

#### Instance

| Name                            | Return Type                                   | Summary                                                                |
|---------------------------------|-----------------------------------------------|------------------------------------------------------------------------|
| [AddEdge(TVertexKey, TV...][11] | `bool`                                        | Add an edge between two nodes in the graph.                            |
| [AddVertex(TVertexKey, ...][12] | `bool`                                        | Adds a vertex to the given graph via the given name/key.               |
| [Clear()][13]                   | `void`                                        | Removes all vertices and edges from the graph.                         |
| [ClearEdges()][14]              | `void`                                        | Removes all edges from the graph.                                      |
| [ContainsEdge(TVertexKe...][15] | `bool`                                        | Determines if the graph contains an edge bewtween source and target... |
| [ContainsValue(TVertexV...][16] | `bool`                                        | Determines if this graph contains the element requested.               |
| [ContainsVertex(TVertex...][17] | `bool`                                        | Determines if this graph contains the element ( by name ) requested.   |
| [GetEdge(TVertexKey, TV...][18] | [IGraphEdge\<TVertexKey>][22]                 | Gets an edge in the graph.                                             |
| [GetVertex(TVertexKey)][19]     | [IGraphVertex\<TVertexKey, TVertexValue>][23] | Gets a vertex identified with the given key.                           |
| [RemoveEdge(TVertexKey,...][20] | `bool`                                        | Removes an edge between two vertices in the graph.                     |
| [RemoveVertex(TVertexKey)][21]  | `bool`                                        | Removes the given vertex from the graph ( also disconnects associat... |

[0]: ../../Heirloom.Core.md
[1]: IGraph[TVertexKey,TVertexValue,Graph[TVertexKey,TVertexValue]].md
[2]: Graph[TVertexKey,TVertexValue]/AllowNegativeWeight.md
[3]: Graph[TVertexKey,TVertexValue]/AllowSelfLoops.md
[4]: Graph[TVertexKey,TVertexValue]/EdgeCount.md
[5]: Graph[TVertexKey,TVertexValue]/Edges.md
[6]: Graph[TVertexKey,TVertexValue]/IsUndirected.md
[7]: Graph[TVertexKey,TVertexValue]/Keys.md
[8]: Graph[TVertexKey,TVertexValue]/Values.md
[9]: Graph[TVertexKey,TVertexValue]/VertexCount.md
[10]: Graph[TVertexKey,TVertexValue]/Vertices.md
[11]: Graph[TVertexKey,TVertexValue]/AddEdge.md
[12]: Graph[TVertexKey,TVertexValue]/AddVertex.md
[13]: Graph[TVertexKey,TVertexValue]/Clear.md
[14]: Graph[TVertexKey,TVertexValue]/ClearEdges.md
[15]: Graph[TVertexKey,TVertexValue]/ContainsEdge.md
[16]: Graph[TVertexKey,TVertexValue]/ContainsValue.md
[17]: Graph[TVertexKey,TVertexValue]/ContainsVertex.md
[18]: Graph[TVertexKey,TVertexValue]/GetEdge.md
[19]: Graph[TVertexKey,TVertexValue]/GetVertex.md
[20]: Graph[TVertexKey,TVertexValue]/RemoveEdge.md
[21]: Graph[TVertexKey,TVertexValue]/RemoveVertex.md
[22]: IGraphEdge[TVertexKey].md
[23]: IGraphVertex[TVertexKey,TVertexValue].md
