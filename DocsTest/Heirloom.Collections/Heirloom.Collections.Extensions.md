# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Extensions (Static Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Methods | Summary |
|---------|---------|
| [ToHeap<T>](#TOH5800195) | Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1` |
| [ToHeap<T>](#TOHB2C57287) | Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1` |
| [IsAscendingOrder<T>](#ISA1C30EC9B) | Checks if the sequence is in ascending order (sequential equivalent items are considered in order). |
| [IsDescendingOrder<T>](#ISDD260EDD7) | Checks if the sequence is in descending order (sequential equivalent items are considered in order). |

### Methods

#### <a name="TOH5800195"></a>ToHeap<T>(IEnumerable\<T> items, [HeapType](Heirloom.Collections.HeapType.md) type = 1) : [Heap\<T>](Heirloom.Collections.Heap[T].md)

<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="TOHB2C57287"></a>ToHeap<T>(IEnumerable\<T> items, Comparison\<T> comparison, [HeapType](Heirloom.Collections.HeapType.md) type = 1) : [Heap\<T>](Heirloom.Collections.Heap[T].md)

<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="ISA1C30EC9B"></a>IsAscendingOrder<T>(IEnumerable\<T> seq) : bool

<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in ascending order (sequential equivalent items are considered in order).


#### <a name="ISDD260EDD7"></a>IsDescendingOrder<T>(IEnumerable\<T> seq) : bool

<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in descending order (sequential equivalent items are considered in order).


