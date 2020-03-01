# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Graph\<TVertexKey|TVertexValue> (Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IGraph\<TVertexKey|TVertexValue|Graph\<TVertexKey|TVertexValue>>](Heirloom.Collections.IGraph[TVertexKey_TVertexValue_Graph[TVertexKey_TVertexValue]].md)</small>  

A configurable adjacency list based graph.

| Properties                          | Summary                                                                            |
|-------------------------------------|------------------------------------------------------------------------------------|
| [AllowNegativeWeight](#ALL5C2502C8) | Was this graph allowed to have negative edges weights?                             |
| [AllowSelfLoops](#ALLB65AF4B0)      | Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ). |
| [IsUndirected](#ISU79739BDD)        | Is this graph configured to have directed edges?                                   |
| [Vertices](#VER648B0F21)            | An enumeration of all vertices within the graph.                                   |
| [Edges](#EDG6DC48328)               | An enumeration of all edges within the graph.                                      |
| [VertexCount](#VER996CD387)         | The number of vertices / elements stored in the graph.                             |
| [EdgeCount](#EDG78704F9E)           | The number of edges stored in the graph.                                           |
| [Keys](#KEY3D37EC76)                | An enumeration of all the names/keys of the vertices in the graph.                 |
| [Values](#VALE51C03E4)              | An enumeration of all the elements stored in the vertices in the graph.            |

| Methods                        | Summary                                                                        |
|--------------------------------|--------------------------------------------------------------------------------|
| [Clear](#CLE3BB23EF9)          | Removes all vertices and edges from the graph.                                 |
| [ClearEdges](#CLEE20C634D)     | Removes all edges from the graph.                                              |
| [AddVertex](#ADDA40B49)        | Adds a vertex to the given graph via the given name/key.                       |
| [AddEdge](#ADD814EE3F0)        | Add an edge between two nodes in the graph.                                    |
| [RemoveEdge](#REM67FD8045)     | Removes an edge between two vertices in the graph.                             |
| [RemoveVertex](#REMCBD2C17C)   | Removes the given vertex from the graph ( also disconnects associated edges ). |
| [GetVertex](#GET659D2C24)      | Gets a vertex identified with the given key.                                   |
| [GetEdge](#GETC1CA31E1)        | Gets an edge in the graph.                                                     |
| [ContainsVertex](#CON8BA51147) | Determines if this graph contains the element ( by name ) requested.           |
| [ContainsEdge](#CON518111E)    | Determines if the graph contains an edge bewtween source and target vertices.  |
| [ContainsValue](#CONDC3C4948)  | Determines if this graph contains the element requested.                       |

### Constructors

#### Graph()

Creates a new directed weighted graph.

#### Graph(bool isUndirected, bool allowNegativeWeight = False, bool allowSelfLoops = False)

Creates a new graph with a custom configuration.

### Properties

#### <a name="ALL5C2502C8"></a>AllowNegativeWeight : bool

<small>`Read Only`</small>

Was this graph allowed to have negative edges weights?

#### <a name="ALLB65AF4B0"></a>AllowSelfLoops : bool

<small>`Read Only`</small>

Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ).

#### <a name="ISU79739BDD"></a>IsUndirected : bool

<small>`Read Only`</small>

Is this graph configured to have directed edges?

#### <a name="VER648B0F21"></a>Vertices : IEnumerable\<IGraphVertex\<TVertexKey|TVertexValue>>

<small>`Read Only`</small>

An enumeration of all vertices within the graph.

#### <a name="EDG6DC48328"></a>Edges : IEnumerable\<IGraphEdge\<TVertexKey>>

<small>`Read Only`</small>

An enumeration of all edges within the graph.

#### <a name="VER996CD387"></a>VertexCount : int

<small>`Read Only`</small>

The number of vertices / elements stored in the graph.

#### <a name="EDG78704F9E"></a>EdgeCount : int

<small>`Read Only`</small>

The number of edges stored in the graph.

#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<TVertexKey>

<small>`Read Only`</small>

An enumeration of all the names/keys of the vertices in the graph.

#### <a name="VALE51C03E4"></a>Values : IEnumerable\<TVertexValue>

<small>`Read Only`</small>

An enumeration of all the elements stored in the vertices in the graph.

### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Virtual`</small>

Removes all vertices and edges from the graph.

#### <a name="CLEC3B6A45C"></a>ClearEdges() : void
<small>`Virtual`</small>

Removes all edges from the graph.

#### <a name="ADD5AA17499"></a>AddVertex(TVertexKey key, TVertexValue element) : bool
<small>`Virtual`</small>

Adds a vertex to the given graph via the given name/key.

<small>**key**: <param name="key">The name/key to identify the element.</param></small>  
<small>**element**: <param name="element">Some element to store in the graph.</param></small>  

#### <a name="ADDC1D4A1D3"></a>AddEdge(TVertexKey source, TVertexKey target, float weight = 1) : bool
<small>`Virtual`</small>

Add an edge between two nodes in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  
<small>**weight**: <param name="weight">Some weight/cost to assign to the newly connected edge.</param></small>  

#### <a name="REM469A9FCE"></a>RemoveEdge(TVertexKey source, TVertexKey target) : bool
<small>`Virtual`</small>

Removes an edge between two vertices in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="REM577B1CC7"></a>RemoveVertex(TVertexKey key) : bool
<small>`Virtual`</small>

Removes the given vertex from the graph ( also disconnects associated edges ).

<small>**key**: <param name="key">Some name of a node within the graph.</param></small>  

#### <a name="GET58F3FBE0"></a>GetVertex(TVertexKey key) : [IGraphVertex\<TVertexKey|TVertexValue>](Heirloom.Collections.IGraphVertex[TVertexKey_TVertexValue].md)
<small>`Virtual`</small>

Gets a vertex identified with the given key.

<small>**key**: <param name="key"> Some known key in the graph. </param></small>  

#### <a name="GET142350CA"></a>GetEdge(TVertexKey source, TVertexKey target) : [IGraphEdge\<TVertexKey>](Heirloom.Collections.IGraphEdge[TVertexKey].md)
<small>`Virtual`</small>

Gets an edge in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="CONFF61E272"></a>ContainsVertex(TVertexKey key) : bool
<small>`Virtual`</small>

Determines if this graph contains the element ( by name ) requested.

<small>**key**: <param name="key">Some key/name of an element possibly within the graph.</param></small>  

#### <a name="CON4B1518D5"></a>ContainsEdge(TVertexKey source, TVertexKey target) : bool
<small>`Virtual`</small>

Determines if the graph contains an edge bewtween source and target vertices.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="CON35FD79FD"></a>ContainsValue(TVertexValue value) : bool
<small>`Virtual`</small>

Determines if this graph contains the element requested.

<small>**value**: <param name="value">Some element possibly within the graph.</param></small>  

