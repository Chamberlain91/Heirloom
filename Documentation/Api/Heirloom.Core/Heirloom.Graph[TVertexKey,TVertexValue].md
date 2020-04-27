# Graph\<TVertexKey, TVertexValue>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A configurable adjacency list based graph.

```cs
public class Graph<TVertexKey, TVertexValue> : IGraph<TVertexKey, TVertexValue, Graph<TVertexKey, TVertexValue>>
```

--------------------------------------------------------------------------------

**Inherits**: [IGraph\<TVertexKey, TVertexValue, Graph\<TVertexKey, TVertexValue>>][1]

**Properties**: [AllowNegativeWeight][2], [AllowSelfLoops][3], [IsUndirected][4], [Vertices][5], [Edges][6], [VertexCount][7], [EdgeCount][8], [Keys][9], [Values][10]

**Methods**: [Clear][11], [ClearEdges][12], [AddVertex][13], [AddEdge][14], [RemoveEdge][15], [RemoveVertex][16], [GetVertex][17], [GetEdge][18], [ContainsVertex][19], [ContainsEdge][20], [ContainsValue][21]

--------------------------------------------------------------------------------

## Properties

| Name                     | Summary                                                                            |
|--------------------------|------------------------------------------------------------------------------------|
| [AllowNegativeWeight][2] | Was this graph allowed to have negative edges weights?                             |
| [AllowSelfLoops][3]      | Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ). |
| [IsUndirected][4]        | Is this graph configured to have directed edges?                                   |
| [Vertices][5]            | An enumeration of all vertices within the graph.                                   |
| [Edges][6]               | An enumeration of all edges within the graph.                                      |
| [VertexCount][7]         | The number of vertices / elements stored in the graph.                             |
| [EdgeCount][8]           | The number of edges stored in the graph.                                           |
| [Keys][9]                | An enumeration of all the names/keys of the vertices in the graph.                 |
| [Values][10]             | An enumeration of all the elements stored in the vertices in the graph.            |

## Methods

| Name                 | Summary                                                                        |
|----------------------|--------------------------------------------------------------------------------|
| [Clear][11]          | Removes all vertices and edges from the graph.                                 |
| [ClearEdges][12]     | Removes all edges from the graph.                                              |
| [AddVertex][13]      | Adds a vertex to the given graph via the given name/key.                       |
| [AddEdge][14]        | Add an edge between two nodes in the graph.                                    |
| [RemoveEdge][15]     | Removes an edge between two vertices in the graph.                             |
| [RemoveVertex][16]   | Removes the given vertex from the graph ( also disconnects associated edges ). |
| [GetVertex][17]      | Gets a vertex identified with the given key.                                   |
| [GetEdge][18]        | Gets an edge in the graph.                                                     |
| [ContainsVertex][19] | Determines if this graph contains the element ( by name ) requested.           |
| [ContainsEdge][20]   | Determines if the graph contains an edge bewtween source and target vertices.  |
| [ContainsValue][21]  | Determines if this graph contains the element requested.                       |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IGraph[TVertexKey,TVertexValue,Graph[TVertexKey,TVertexValue]].md
[2]: Heirloom.Graph[TVertexKey,TVertexValue].AllowNegativeWeight.md
[3]: Heirloom.Graph[TVertexKey,TVertexValue].AllowSelfLoops.md
[4]: Heirloom.Graph[TVertexKey,TVertexValue].IsUndirected.md
[5]: Heirloom.Graph[TVertexKey,TVertexValue].Vertices.md
[6]: Heirloom.Graph[TVertexKey,TVertexValue].Edges.md
[7]: Heirloom.Graph[TVertexKey,TVertexValue].VertexCount.md
[8]: Heirloom.Graph[TVertexKey,TVertexValue].EdgeCount.md
[9]: Heirloom.Graph[TVertexKey,TVertexValue].Keys.md
[10]: Heirloom.Graph[TVertexKey,TVertexValue].Values.md
[11]: Heirloom.Graph[TVertexKey,TVertexValue].Clear.md
[12]: Heirloom.Graph[TVertexKey,TVertexValue].ClearEdges.md
[13]: Heirloom.Graph[TVertexKey,TVertexValue].AddVertex.md
[14]: Heirloom.Graph[TVertexKey,TVertexValue].AddEdge.md
[15]: Heirloom.Graph[TVertexKey,TVertexValue].RemoveEdge.md
[16]: Heirloom.Graph[TVertexKey,TVertexValue].RemoveVertex.md
[17]: Heirloom.Graph[TVertexKey,TVertexValue].GetVertex.md
[18]: Heirloom.Graph[TVertexKey,TVertexValue].GetEdge.md
[19]: Heirloom.Graph[TVertexKey,TVertexValue].ContainsVertex.md
[20]: Heirloom.Graph[TVertexKey,TVertexValue].ContainsEdge.md
[21]: Heirloom.Graph[TVertexKey,TVertexValue].ContainsValue.md
