# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../heirloom.collections/heirloom.collections.md)</small>  

## IGraphVertex\<TKey|TValue> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  

A vertex representing a node on a graph.

| Properties | Summary |
|------------|---------|
| [Key](#KEY5F786897) | The name/key of this vertex. |
| [Value](#VAL829B10CF) | The data/value of this vertex. |
| [IncomingEdges](#INC2F618154) | The list incoming edges in a directed graph ( no edges when undirected ). |
| [Edges](#EDG6DC48328) | The list of outgoing edges in a directed graph ( all edges when undirected ). |

### Properties

#### <a name="KEY5F786897"></a>Key : TKey

<small>`Read Only`</small>

The name/key of this vertex.

#### <a name="VAL829B10CF"></a>Value : TValue


The data/value of this vertex.

#### <a name="INC2F618154"></a>IncomingEdges : IReadOnlyList\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The list incoming edges in a directed graph ( no edges when undirected ).

#### <a name="EDG6DC48328"></a>Edges : IReadOnlyList\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The list of outgoing edges in a directed graph ( all edges when undirected ).

