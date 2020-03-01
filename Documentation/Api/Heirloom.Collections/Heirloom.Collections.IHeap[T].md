# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## IHeap\<T> (Interface)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IReadOnlyHeap\<T>](Heirloom.Collections.IReadOnlyHeap[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a heap data structure. Allowing the access and removal of items by a priority ordering.

| Methods                  | Summary                                                             |
|--------------------------|---------------------------------------------------------------------|
| [Add](#ADD9453EEA5)      | Adds an item to the heap.                                           |
| [AddRange](#ADD964BA200) | Adds multiple items to the heap.                                    |
| [Remove](#REM291D149A)   | Removes a specific item from the heap.                              |
| [Remove](#REMF63FEEE5)   | Removes and returns the next priority item in the heap.             |
| [Update](#UPD9BB09A13)   | Alerts the heap to update the position the element within the heap. |

### Methods

#### <a name="ADD9453EEA5"></a>Add(T item) : bool
<small>`Abstract`</small>

Adds an item to the heap.


#### <a name="ADD964BA200"></a>AddRange(IEnumerable\<T> items) : void
<small>`Abstract`</small>

Adds multiple items to the heap.


#### <a name="REM291D149A"></a>Remove(T item) : bool
<small>`Abstract`</small>

Removes a specific item from the heap.


#### <a name="REMF63FEEE5"></a>Remove() : T
<small>`Abstract`</small>

Removes and returns the next priority item in the heap.

#### <a name="UPD9BB09A13"></a>Update(T item) : void
<small>`Abstract`</small>

Alerts the heap to update the position the element within the heap.


