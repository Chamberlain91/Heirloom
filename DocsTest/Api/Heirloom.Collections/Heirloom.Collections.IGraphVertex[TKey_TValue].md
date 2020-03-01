# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IGraphVertex\<TKey|TValue> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  

A vertex representing a node on a graph.

| Properties                 | Summary                                                                       |
|----------------------------|-------------------------------------------------------------------------------|
| [Key](#KEY5F78)            | The name/key of this vertex.                                                  |
| [Value](#VALU829B)         | The data/value of this vertex.                                                |
| [IncomingEdges](#INCO2F61) | The list incoming edges in a directed graph ( no edges when undirected ).     |
| [Edges](#EDGE6DC4)         | The list of outgoing edges in a directed graph ( all edges when undirected ). |

### Properties

#### <a name="KEY5F78"></a> Key : TKey

<small>`Read Only`</small>

The name/key of this vertex.

#### <a name="VALU829B"></a> Value : TValue


The data/value of this vertex.

#### <a name="INCO2F61"></a> IncomingEdges : IReadOnlyList\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The list incoming edges in a directed graph ( no edges when undirected ).

#### <a name="EDGE6DC4"></a> Edges : IReadOnlyList\<IGraphEdge\<TKey>>

<small>`Read Only`</small>

The list of outgoing edges in a directed graph ( all edges when undirected ).

