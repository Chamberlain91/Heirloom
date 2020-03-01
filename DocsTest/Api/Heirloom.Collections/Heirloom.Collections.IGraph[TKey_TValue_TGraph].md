# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IGraph\<TKey|TValue|TGraph> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Properties                       | Summary                                                   |
|----------------------------------|-----------------------------------------------------------|
| [IsUndirected](#ISUN7973)        | Is this graph undirected?                                 |
| [AllowSelfLoops](#ALLOB65A)      | Are self looping edges allowed?                           |
| [AllowNegativeWeight](#ALLO5C25) | Are edges allowed to have a negative weight?              |
| [VertexCount](#VERT996C)         | The number of vertices within this graph.                 |
| [EdgeCount](#EDGE7870)           | The number of edges within this graph.                    |
| [Edges](#EDGE6DC4)               | The edges contained within this graph.                    |
| [Vertices](#VERT648B)            | The vertices contained within this graph.                 |
| [Values](#VALUE51C)              | The data/values contained by the vertices in this graph.  |
| [Keys](#KEYS3D37)                | The names/keys used to lookup the vertices in this graph. |

| Methods                     | Summary                                                                |
|-----------------------------|------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)          | Removes all vertices and edges from the graph.                         |
| [ClearEdges](#CLEAE20C)     | Disconnects all edges from all vertices.                               |
| [ContainsVertex](#CONT8BA5) | Determine if a vertex with the given name/key exists within the graph. |
| [ContainsEdge](#CONT5181)   | Determine if an edge exists between two existing vertices.             |
| [ContainsValue](#CONTDC3C)  | Determines if the graph contains the value.                            |
| [AddVertex](#ADDVA40B)      | Adds a vertex to the graph.                                            |
| [RemoveVertex](#REMOCBD2)   | Removes a vertex from the graph.                                       |
| [GetVertex](#GETV659D)      | Returns the vertex with the given name/key.                            |
| [AddEdge](#ADDE814E)        | Connects two vertices by an edge in the graph.                         |
| [RemoveEdge](#REMO67FD)     | Removes an edge between two vertices in the graph.                     |
| [GetEdge](#GETEC1CA)        | Returns the edge between two vertices.                                 |

### Properties

#### <a name="ISUN7973"></a> IsUndirected : bool

<small>`Read Only`</small>

Is this graph undirected?

#### <a name="ALLOB65A"></a> AllowSelfLoops : bool

<small>`Read Only`</small>

Are self looping edges allowed?

#### <a name="ALLO5C25"></a> AllowNegativeWeight : bool

<small>`Read Only`</small>

Are edges allowed to have a negative weight?

#### <a name="VERT996C"></a> VertexCount : int

<small>`Read Only`</small>

The number of vertices within this graph.

#### <a name="EDGE7870"></a> EdgeCount : int

<small>`Read Only`</small>

The number of edges within this graph.

#### <a name="EDGE6DC4"></a> Edges : IEnumerable\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The edges contained within this graph.

#### <a name="VERT648B"></a> Vertices : IEnumerable\<IGraphVertex\<TKey|TValue>>

<small>`Read Only`</small>

The vertices contained within this graph.

#### <a name="VALUE51C"></a> Values : IEnumerable\<TValue>

<small>`Read Only`</small>

The data/values contained by the vertices in this graph.

#### <a name="KEYS3D37"></a> Keys : IEnumerable\<TKey>

<small>`Read Only`</small>

The names/keys used to lookup the vertices in this graph.

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Abstract`</small>

Removes all vertices and edges from the graph.

#### <a name="CLEAC3B6"></a> ClearEdges() : void
<small>`Abstract`</small>

Disconnects all edges from all vertices.

#### <a name="CONTEACA"></a> ContainsVertex(TKey key) : bool
<small>`Abstract`</small>

Determine if a vertex with the given name/key exists within the graph.

<small>**key**: <param name="key">The name/key of the vertex.</param></small>  

#### <a name="CONT79C2"></a> ContainsEdge(TKey keyA, TKey keyB) : bool
<small>`Abstract`</small>

Determine if an edge exists between two existing vertices.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

#### <a name="CONTEEF7"></a> ContainsValue(TValue value) : bool
<small>`Abstract`</small>

Determines if the graph contains the value.

<small>**value**: <param name="value">Some data/value contained by a vertex in the graph.</param></small>  

#### <a name="ADDV75B0"></a> AddVertex(TKey key, TValue value) : bool
<small>`Abstract`</small>

Adds a vertex to the graph.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  
<small>**value**: <param name="value">The data/value of the graph.</param></small>  

#### <a name="REMO489B"></a> RemoveVertex(TKey key) : bool
<small>`Abstract`</small>

Removes a vertex from the graph.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  

#### <a name="GETVFD3D"></a> GetVertex(TKey key) : [IGraphVertex\<TKey|TValue>](Heirloom.Collections.IGraphVertex[TKey_TValue].md)
<small>`Abstract`</small>

Returns the vertex with the given name/key.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  

#### <a name="ADDE9F66"></a> AddEdge(TKey keyA, TKey keyB, float weight) : bool
<small>`Abstract`</small>

Connects two vertices by an edge in the graph.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  
<small>**weight**: <param name="weight">The cost/weight of the edge.</param></small>  

#### <a name="REMODD74"></a> RemoveEdge(TKey keyA, TKey keyB) : bool
<small>`Abstract`</small>

Removes an edge between two vertices in the graph.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

#### <a name="GETE7B3D"></a> GetEdge(TKey keyA, TKey keyB) : [IGraphEdge\<TKey>](Heirloom.Collections.IGraphEdge[TKey].md)
<small>`Abstract`</small>

Returns the edge between two vertices.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

