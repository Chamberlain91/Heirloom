# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Graph\<TVertexKey|TVertexValue> (Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IGraph\<TVertexKey|TVertexValue|Graph\<TVertexKey|TVertexValue>>](Heirloom.Collections.IGraph[TVertexKey_TVertexValue_Graph[TVertexKey_TVertexValue]].md)</small>  

A configurable adjacency list based graph.

| Properties                       | Summary                                                                            |
|----------------------------------|------------------------------------------------------------------------------------|
| [AllowNegativeWeight](#ALLO5C25) | Was this graph allowed to have negative edges weights?                             |
| [AllowSelfLoops](#ALLOB65A)      | Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ). |
| [IsUndirected](#ISUN7973)        | Is this graph configured to have directed edges?                                   |
| [Vertices](#VERT648B)            | An enumeration of all vertices within the graph.                                   |
| [Edges](#EDGE6DC4)               | An enumeration of all edges within the graph.                                      |
| [VertexCount](#VERT996C)         | The number of vertices / elements stored in the graph.                             |
| [EdgeCount](#EDGE7870)           | The number of edges stored in the graph.                                           |
| [Keys](#KEYS3D37)                | An enumeration of all the names/keys of the vertices in the graph.                 |
| [Values](#VALUE51C)              | An enumeration of all the elements stored in the vertices in the graph.            |

| Methods                     | Summary                                                                        |
|-----------------------------|--------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)          | Removes all vertices and edges from the graph.                                 |
| [ClearEdges](#CLEAE20C)     | Removes all edges from the graph.                                              |
| [AddVertex](#ADDVA40B)      | Adds a vertex to the given graph via the given name/key.                       |
| [AddEdge](#ADDE814E)        | Add an edge between two nodes in the graph.                                    |
| [RemoveEdge](#REMO67FD)     | Removes an edge between two vertices in the graph.                             |
| [RemoveVertex](#REMOCBD2)   | Removes the given vertex from the graph ( also disconnects associated edges ). |
| [GetVertex](#GETV659D)      | Gets a vertex identified with the given key.                                   |
| [GetEdge](#GETEC1CA)        | Gets an edge in the graph.                                                     |
| [ContainsVertex](#CONT8BA5) | Determines if this graph contains the element ( by name ) requested.           |
| [ContainsEdge](#CONT5181)   | Determines if the graph contains an edge bewtween source and target vertices.  |
| [ContainsValue](#CONTDC3C)  | Determines if this graph contains the element requested.                       |

### Constructors

#### Graph()

Creates a new directed weighted graph.

#### Graph(bool isUndirected, bool allowNegativeWeight = False, bool allowSelfLoops = False)

Creates a new graph with a custom configuration.

### Properties

#### <a name="ALLO5C25"></a> AllowNegativeWeight : bool

<small>`Read Only`</small>

Was this graph allowed to have negative edges weights?

#### <a name="ALLOB65A"></a> AllowSelfLoops : bool

<small>`Read Only`</small>

Was this graph allowed to have self connecting loops ( Ex, 'A' connected to 'A' ).

#### <a name="ISUN7973"></a> IsUndirected : bool

<small>`Read Only`</small>

Is this graph configured to have directed edges?

#### <a name="VERT648B"></a> Vertices : IEnumerable\<IGraphVertex\<TVertexKey|TVertexValue>>

<small>`Read Only`</small>

An enumeration of all vertices within the graph.

#### <a name="EDGE6DC4"></a> Edges : IEnumerable\<IGraphEdge\<TVertexKey>>

<small>`Read Only`</small>

An enumeration of all edges within the graph.

#### <a name="VERT996C"></a> VertexCount : int

<small>`Read Only`</small>

The number of vertices / elements stored in the graph.

#### <a name="EDGE7870"></a> EdgeCount : int

<small>`Read Only`</small>

The number of edges stored in the graph.

#### <a name="KEYS3D37"></a> Keys : IEnumerable\<TVertexKey>

<small>`Read Only`</small>

An enumeration of all the names/keys of the vertices in the graph.

#### <a name="VALUE51C"></a> Values : IEnumerable\<TVertexValue>

<small>`Read Only`</small>

An enumeration of all the elements stored in the vertices in the graph.

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

Removes all vertices and edges from the graph.

#### <a name="CLEAC3B6"></a> ClearEdges() : void
<small>`Virtual`</small>

Removes all edges from the graph.

#### <a name="ADDV5AA1"></a> AddVertex(TVertexKey key, TVertexValue element) : bool
<small>`Virtual`</small>

Adds a vertex to the given graph via the given name/key.

<small>**key**: <param name="key">The name/key to identify the element.</param></small>  
<small>**element**: <param name="element">Some element to store in the graph.</param></small>  

#### <a name="ADDEC1D4"></a> AddEdge(TVertexKey source, TVertexKey target, float weight = 1) : bool
<small>`Virtual`</small>

Add an edge between two nodes in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  
<small>**weight**: <param name="weight">Some weight/cost to assign to the newly connected edge.</param></small>  

#### <a name="REMO469A"></a> RemoveEdge(TVertexKey source, TVertexKey target) : bool
<small>`Virtual`</small>

Removes an edge between two vertices in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="REMO577B"></a> RemoveVertex(TVertexKey key) : bool
<small>`Virtual`</small>

Removes the given vertex from the graph ( also disconnects associated edges ).

<small>**key**: <param name="key">Some name of a node within the graph.</param></small>  

#### <a name="GETV58F3"></a> GetVertex(TVertexKey key) : [IGraphVertex\<TVertexKey|TVertexValue>](Heirloom.Collections.IGraphVertex[TVertexKey_TVertexValue].md)
<small>`Virtual`</small>

Gets a vertex identified with the given key.

<small>**key**: <param name="key"> Some known key in the graph. </param></small>  

#### <a name="GETE1423"></a> GetEdge(TVertexKey source, TVertexKey target) : [IGraphEdge\<TVertexKey>](Heirloom.Collections.IGraphEdge[TVertexKey].md)
<small>`Virtual`</small>

Gets an edge in the graph.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="CONTFF61"></a> ContainsVertex(TVertexKey key) : bool
<small>`Virtual`</small>

Determines if this graph contains the element ( by name ) requested.

<small>**key**: <param name="key">Some key/name of an element possibly within the graph.</param></small>  

#### <a name="CONT4B15"></a> ContainsEdge(TVertexKey source, TVertexKey target) : bool
<small>`Virtual`</small>

Determines if the graph contains an edge bewtween source and target vertices.

<small>**source**: <param name="source">Some name of a source node within the graph.</param></small>  
<small>**target**: <param name="target">Some name of a target node within the graph.</param></small>  

#### <a name="CONT35FD"></a> ContainsValue(TVertexValue value) : bool
<small>`Virtual`</small>

Determines if this graph contains the element requested.

<small>**value**: <param name="value">Some element possibly within the graph.</param></small>  

