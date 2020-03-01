# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Heap\<T> (Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IHeap\<T>](Heirloom.Collections.IHeap[T].md), [IReadOnlyHeap\<T>](Heirloom.Collections.IReadOnlyHeap[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a heap data structure. Allows the insertion and removal of items by priority.

The heap always acts like a min-heap but inverts the result of comparison for max heaps.

| Properties               | Summary                                            |
|--------------------------|----------------------------------------------------|
| [Comparer](#COM45265051) | The comparer used by this heap.                    |
| [Count](#COU73CA0BBB)    | Gets the number of elements contained in the heap. |
| [Type](#TYP233312DE)     | Which kind of heap is this?                        |

| Methods                       | Summary                                                                     |
|-------------------------------|-----------------------------------------------------------------------------|
| [Add](#ADDBCD0F225)           | Adds an item to the heap.                                                   |
| [AddRange](#ADD15375A3E)      | Adds multiple items to the heap.                                            |
| [Peek](#PEE52739267)          | Gets the next item in the heap to be removed.                               |
| [Remove](#REMF10744DE)        | Removes and returns the next priority item in the heap.                     |
| [Remove](#REMF10744DE)        | Removes a specific item from the heap.                                      |
| [Update](#UPDD1771A75)        | Alerts the heap to update the position the element within the heap.         |
| [Contains](#COND0AE797B)      |                                                                             |
| [ToArray](#TOAF17D74F8)       | Clones the heap, and returns an array of the elements in priority ordering. |
| [GetEnumerator](#GETF1F90828) | Enumerates the values in the heap (in no particular order)                  |

### Constructors

#### Heap([HeapType](Heirloom.Collections.HeapType.md) type = Min)

Constructs a new heap that optionally sorts by maximum or minimum comparisons.

#### Heap(Comparison\<T> comparison, [HeapType](Heirloom.Collections.HeapType.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with a custom comparison function.

#### Heap(Comparer\<T> comparer, [HeapType](Heirloom.Collections.HeapType.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with an instance of a custom comparer.

#### Heap([Heap\<T>](Heirloom.Collections.Heap[T].md) heap)

### Properties

#### <a name="COM45265051"></a>Comparer : Comparer\<T>

<small>`Read Only`</small>

The comparer used by this heap.

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of elements contained in the heap.

#### <a name="TYP233312DE"></a>Type : [HeapType](Heirloom.Collections.HeapType.md)

<small>`Read Only`</small>

Which kind of heap is this?

### Methods

#### <a name="ADD9453EEA5"></a>Add(T item) : bool
<small>`Virtual`</small>

Adds an item to the heap.


#### <a name="ADD964BA200"></a>AddRange(IEnumerable\<T> items) : void
<small>`Virtual`</small>

Adds multiple items to the heap.


#### <a name="PEE599BAF94"></a>Peek() : T
<small>`Virtual`</small>

Gets the next item in the heap to be removed.

#### <a name="REMF63FEEE5"></a>Remove() : T
<small>`Virtual`</small>

Removes and returns the next priority item in the heap.

#### <a name="REM291D149A"></a>Remove(T item) : bool
<small>`Virtual`</small>

Removes a specific item from the heap.


#### <a name="UPD9BB09A13"></a>Update(T item) : void
<small>`Virtual`</small>

Alerts the heap to update the position the element within the heap.


#### <a name="CON50B6A9F"></a>Contains(T item) : bool
<small>`Virtual`</small>


#### <a name="TOA1C8FFB1"></a>ToArray() : T

Clones the heap, and returns an array of the elements in priority ordering.

#### <a name="GETDDD17E2E"></a>GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

Enumerates the values in the heap (in no particular order)

