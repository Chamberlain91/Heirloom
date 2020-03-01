# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../heirloom.collections/heirloom.collections.md)</small>  

## Extensions (Static Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Methods | Summary |
|---------|---------|
| [ToHeap<T>](#TOH59177695) | Constructs a new [Heap\<T>](heirloom.collections.heap[t].md) from an `System.Collections.Generic.IEnumerable`1` |
| [ToHeap<T>](#TOH7795B387) | Constructs a new [Heap\<T>](heirloom.collections.heap[t].md) from an `System.Collections.Generic.IEnumerable`1` |
| [IsAscendingOrder<T>](#ISA1C30EC9B) | Checks if the sequence is in ascending order (sequential equivalent items are considered in order). |
| [IsDescendingOrder<T>](#ISDD260EDD7) | Checks if the sequence is in descending order (sequential equivalent items are considered in order). |

### Methods

#### <a name="TOH59177695"></a>ToHeap<T>(IEnumerable\<T> items, [HeapType](heirloom.collections.heaptype.md) type = 1) : [Heap\<T>](heirloom.collections.heap[t].md)

<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](heirloom.collections.heap[t].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="TOH7795B387"></a>ToHeap<T>(IEnumerable\<T> items, Comparison\<T> comparison, [HeapType](heirloom.collections.heaptype.md) type = 1) : [Heap\<T>](heirloom.collections.heap[t].md)

<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](heirloom.collections.heap[t].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="ISA1C30EC9B"></a>IsAscendingOrder<T>(IEnumerable\<T> seq) : bool

<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in ascending order (sequential equivalent items are considered in order).


#### <a name="ISDD260EDD7"></a>IsDescendingOrder<T>(IEnumerable\<T> seq) : bool

<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in descending order (sequential equivalent items are considered in order).


