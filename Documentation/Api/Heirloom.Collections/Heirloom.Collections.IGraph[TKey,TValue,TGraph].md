# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IGraph\<TKey, TValue, TGraph> (Interface)
<small>**Namespace**: Heirloom.Collections</small>  

| Properties                          | Summary                                                   |
|-------------------------------------|-----------------------------------------------------------|
| [IsUndirected](#ISU79739BDD)        | Is this graph undirected?                                 |
| [AllowSelfLoops](#ALLB65AF4B0)      | Are self looping edges allowed?                           |
| [AllowNegativeWeight](#ALL5C2502C8) | Are edges allowed to have a negative weight?              |
| [VertexCount](#VER996CD387)         | The number of vertices within this graph.                 |
| [EdgeCount](#EDG78704F9E)           | The number of edges within this graph.                    |
| [Edges](#EDG6DC48328)               | The edges contained within this graph.                    |
| [Vertices](#VER648B0F21)            | The vertices contained within this graph.                 |
| [Values](#VALE51C03E4)              | The data/values contained by the vertices in this graph.  |
| [Keys](#KEY3D37EC76)                | The names/keys used to lookup the vertices in this graph. |

| Methods                        | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Clear](#CLE4538C554)          | Removes all vertices and edges from the graph.                         |
| [ClearEdges](#CLEC3B6A45C)     | Disconnects all edges from all vertices.                               |
| [ContainsVertex](#CONEACA2150) | Determine if a vertex with the given name/key exists within the graph. |
| [ContainsEdge](#CON79C21D1A)   | Determine if an edge exists between two existing vertices.             |
| [ContainsValue](#CONEEF76259)  | Determines if the graph contains the value.                            |
| [AddVertex](#ADD75B09E92)      | Adds a vertex to the graph.                                            |
| [RemoveVertex](#REM489B3DD3)   | Removes a vertex from the graph.                                       |
| [GetVertex](#GET6E0FDD9F)      | Returns the vertex with the given name/key.                            |
| [AddEdge](#ADD9F66EC1A)        | Connects two vertices by an edge in the graph.                         |
| [RemoveEdge](#REMDD74603F)     | Removes an edge between two vertices in the graph.                     |
| [GetEdge](#GET7B3DE88F)        | Returns the edge between two vertices.                                 |

### Properties

#### <a name="ISU79739BDD"></a>IsUndirected : bool

<small>`Read Only`</small>

Is this graph undirected?

#### <a name="ALLB65AF4B0"></a>AllowSelfLoops : bool

<small>`Read Only`</small>

Are self looping edges allowed?

#### <a name="ALL5C2502C8"></a>AllowNegativeWeight : bool

<small>`Read Only`</small>

Are edges allowed to have a negative weight?

#### <a name="VER996CD387"></a>VertexCount : int

<small>`Read Only`</small>

The number of vertices within this graph.

#### <a name="EDG78704F9E"></a>EdgeCount : int

<small>`Read Only`</small>

The number of edges within this graph.

#### <a name="EDG6DC48328"></a>Edges : IEnumerable\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The edges contained within this graph.

#### <a name="VER648B0F21"></a>Vertices : IEnumerable\<IGraphVertex\<TKey, TValue>>

<small>`Read Only`</small>

The vertices contained within this graph.

#### <a name="VALE51C03E4"></a>Values : IEnumerable\<TValue>

<small>`Read Only`</small>

The data/values contained by the vertices in this graph.

#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<TKey>

<small>`Read Only`</small>

The names/keys used to lookup the vertices in this graph.

### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Abstract`</small>

Removes all vertices and edges from the graph.

#### <a name="CLEC3B6A45C"></a>ClearEdges() : void
<small>`Abstract`</small>

Disconnects all edges from all vertices.

#### <a name="CONEACA2150"></a>ContainsVertex(TKey key) : bool
<small>`Abstract`</small>

Determine if a vertex with the given name/key exists within the graph.

<small>**key**: <param name="key">The name/key of the vertex.</param></small>  

#### <a name="CON79C21D1A"></a>ContainsEdge(TKey keyA, TKey keyB) : bool
<small>`Abstract`</small>

Determine if an edge exists between two existing vertices.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

#### <a name="CONEEF76259"></a>ContainsValue(TValue value) : bool
<small>`Abstract`</small>

Determines if the graph contains the value.

<small>**value**: <param name="value">Some data/value contained by a vertex in the graph.</param></small>  

#### <a name="ADD75B09E92"></a>AddVertex(TKey key, TValue value) : bool
<small>`Abstract`</small>

Adds a vertex to the graph.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  
<small>**value**: <param name="value">The data/value of the graph.</param></small>  

#### <a name="REM489B3DD3"></a>RemoveVertex(TKey key) : bool
<small>`Abstract`</small>

Removes a vertex from the graph.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  

#### <a name="GET6E0FDD9F"></a>GetVertex(TKey key) : [IGraphVertex\<TKey, TValue>](Heirloom.Collections.IGraphVertex[TKey,TValue].md)
<small>`Abstract`</small>

Returns the vertex with the given name/key.

<small>**key**: <param name="key">The name/key of a vertex.</param></small>  

#### <a name="ADD9F66EC1A"></a>AddEdge(TKey keyA, TKey keyB, float weight) : bool
<small>`Abstract`</small>

Connects two vertices by an edge in the graph.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  
<small>**weight**: <param name="weight">The cost/weight of the edge.</param></small>  

#### <a name="REMDD74603F"></a>RemoveEdge(TKey keyA, TKey keyB) : bool
<small>`Abstract`</small>

Removes an edge between two vertices in the graph.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

#### <a name="GET7B3DE88F"></a>GetEdge(TKey keyA, TKey keyB) : [IGraphEdge\<TKey>](Heirloom.Collections.IGraphEdge[TKey].md)
<small>`Abstract`</small>

Returns the edge between two vertices.

<small>**keyA**: <param name="keyA">The name/key of the start vertex.</param></small>  
<small>**keyB**: <param name="keyB">The name/key of the end vertex.</param></small>  

