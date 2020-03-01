# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IGraphEdge\<TKey> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  

An edge between two vertices.

| Properties          | Summary                            |
|---------------------|------------------------------------|
| [Source](#SOUR993F) | The name/key of the source vertex. |
| [Target](#TARGDEB1) | The name/key of the target vertex. |
| [Weight](#WEIGE098) | The cost/weight of this edge.      |

| Methods                  | Summary                |
|--------------------------|------------------------|
| [GetOtherKey](#GETO2E82) | Returns the other key. |

### Properties

#### <a name="SOUR993F"></a> Source : TKey

<small>`Read Only`</small>

The name/key of the source vertex.

#### <a name="TARGDEB1"></a> Target : TKey

<small>`Read Only`</small>

The name/key of the target vertex.

#### <a name="WEIGE098"></a> Weight : float


The cost/weight of this edge.

### Methods

#### <a name="GETO2FB4"></a> GetOtherKey(TKey key) : TKey
<small>`Abstract`</small>

Returns the other key.

<small>**key**: <param name="key"> Must be either <see cref="P:Heirloom.Collections.IGraphEdge`1.Source" /> or <see cref="P:Heirloom.Collections.IGraphEdge`1.Target" />. </param></small>  

