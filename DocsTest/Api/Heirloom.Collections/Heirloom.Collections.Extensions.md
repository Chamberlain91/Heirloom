# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Extensions (Static Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  

| Methods                           | Summary                                                                                                         |
|-----------------------------------|-----------------------------------------------------------------------------------------------------------------|
| [ToHeap<T>](#TOHE22AF)            | Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1` |
| [ToHeap<T>](#TOHE22AF)            | Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1` |
| [IsAscendingOrder<T>](#ISAS91DF)  | Checks if the sequence is in ascending order (sequential equivalent items are considered in order).             |
| [IsDescendingOrder<T>](#ISDEDCEC) | Checks if the sequence is in descending order (sequential equivalent items are considered in order).            |

### Methods

#### <a name="TOHEC6F4"></a> ToHeap<T>(IEnumerable\<T> items, [HeapType](Heirloom.Collections.HeapType.md) type = Min) : [Heap\<T>](Heirloom.Collections.Heap[T].md)
<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="TOHE8547"></a> ToHeap<T>(IEnumerable\<T> items, Comparison\<T> comparison, [HeapType](Heirloom.Collections.HeapType.md) type = Min) : [Heap\<T>](Heirloom.Collections.Heap[T].md)
<small>`Static`, `ExtensionAttribute`</small>

Constructs a new [Heap\<T>](Heirloom.Collections.Heap[T].md) from an `System.Collections.Generic.IEnumerable`1`


#### <a name="ISAS1C30"></a> IsAscendingOrder<T>(IEnumerable\<T> seq) : bool
<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in ascending order (sequential equivalent items are considered in order).


#### <a name="ISDED260"></a> IsDescendingOrder<T>(IEnumerable\<T> seq) : bool
<small>`Static`, `ExtensionAttribute`</small>

Checks if the sequence is in descending order (sequential equivalent items are considered in order).


