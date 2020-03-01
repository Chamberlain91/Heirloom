# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IReadOnlyHeap\<T> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a read-only view of a [Heap\<T>](Heirloom.Collections.Heap[T].md).

| Properties            | Summary                                     |
|-----------------------|---------------------------------------------|
| [Comparer](#COMP4526) | The comparer used to compare item priority. |

| Methods               | Summary                                       |
|-----------------------|-----------------------------------------------|
| [Contains](#CONTD0AE) | Does this heap contain the specified item?    |
| [Peek](#PEEK5273)     | Gets the next item in the heap to be removed. |

### Properties

#### <a name="COMP4526"></a> Comparer : Comparer\<T>

<small>`Read Only`</small>

The comparer used to compare item priority.

### Methods

#### <a name="CONT50B6"></a> Contains(T item) : bool
<small>`Abstract`</small>

Does this heap contain the specified item?

<small>**item**: <param name="item">Some object.</param></small>  

#### <a name="PEEK599B"></a> Peek() : T
<small>`Abstract`</small>

Gets the next item in the heap to be removed.

