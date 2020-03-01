# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IReadOnlyHeap\<T> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a read-only view of a [Heap\<T>](Heirloom.Collections.Heap[T].md).

| Properties | Summary |
|------------|---------|
| [Comparer](#COM45265051) | The comparer used to compare item priority. |

| Methods | Summary |
|---------|---------|
| [Contains](#CON50B6A9F) | Does this heap contain the specified item? |
| [Peek](#PEE599BAF94) | Gets the next item in the heap to be removed. |

### Properties

#### <a name="COM45265051"></a>Comparer : Comparer\<T>

<small>`Read Only`</small>

The comparer used to compare item priority.

### Methods

#### <a name="CON50B6A9F"></a>Contains(T item) : bool

<small>`Abstract`</small>

Does this heap contain the specified item?

<small>**item**: <param name="item">Some object.</param>  
</small>

#### <a name="PEE599BAF94"></a>Peek() : T

<small>`Abstract`</small>

Gets the next item in the heap to be removed.

