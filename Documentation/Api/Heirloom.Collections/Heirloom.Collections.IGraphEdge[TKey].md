# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IGraphEdge\<TKey> (Interface)
<small>**Namespace**: Heirloom.Collections</small>  

An edge between two vertices.

| Properties             | Summary                            |
|------------------------|------------------------------------|
| [Source](#SOU993FEDB1) | The name/key of the source vertex. |
| [Target](#TARDEB13919) | The name/key of the target vertex. |
| [Weight](#WEIE098BAB2) | The cost/weight of this edge.      |

| Methods                     | Summary                |
|-----------------------------|------------------------|
| [GetOtherKey](#GET2FB41C95) | Returns the other key. |

### Properties

#### <a name="SOU993FEDB1"></a>Source : TKey

<small>`Read Only`</small>

The name/key of the source vertex.

#### <a name="TARDEB13919"></a>Target : TKey

<small>`Read Only`</small>

The name/key of the target vertex.

#### <a name="WEIE098BAB2"></a>Weight : float


The cost/weight of this edge.

### Methods

#### <a name="GET2FB41C95"></a>GetOtherKey(TKey key) : TKey
<small>`Abstract`</small>

Returns the other key.

<small>**key**: <param name="key"> Must be either <see cref="P:Heirloom.Collections.IGraphEdge`1.Source" /> or <see cref="P:Heirloom.Collections.IGraphEdge`1.Target" />. </param></small>  

