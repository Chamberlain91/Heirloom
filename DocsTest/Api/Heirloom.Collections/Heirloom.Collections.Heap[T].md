# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../Heirloom.Collections/Heirloom.Collections.md)</small>  

## Heap\<T> (Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IHeap\<T>](Heirloom.Collections.IHeap[T].md), [IReadOnlyHeap\<T>](Heirloom.Collections.IReadOnlyHeap[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a heap data structure. Allows the insertion and removal of items by priority.

The heap always acts like a min-heap but inverts the result of comparison for max heaps.

| Properties            | Summary                                            |
|-----------------------|----------------------------------------------------|
| [Comparer](#COMP4526) | The comparer used by this heap.                    |
| [Count](#COUN73CA)    | Gets the number of elements contained in the heap. |
| [Type](#TYPE2333)     | Which kind of heap is this?                        |

| Methods                    | Summary                                                                     |
|----------------------------|-----------------------------------------------------------------------------|
| [Add](#ADDBCD0)            | Adds an item to the heap.                                                   |
| [AddRange](#ADDR1537)      | Adds multiple items to the heap.                                            |
| [Peek](#PEEK5273)          | Gets the next item in the heap to be removed.                               |
| [Remove](#REMOF107)        | Removes and returns the next priority item in the heap.                     |
| [Remove](#REMOF107)        | Removes a specific item from the heap.                                      |
| [Update](#UPDAD177)        | Alerts the heap to update the position the element within the heap.         |
| [Contains](#CONTD0AE)      |                                                                             |
| [ToArray](#TOARF17D)       | Clones the heap, and returns an array of the elements in priority ordering. |
| [GetEnumerator](#GETEF1F9) | Enumerates the values in the heap (in no particular order)                  |

### Constructors

#### Heap([HeapType](Heirloom.Collections.HeapType.md) type = Min)

Constructs a new heap that optionally sorts by maximum or minimum comparisons.

#### Heap(Comparison\<T> comparison, [HeapType](Heirloom.Collections.HeapType.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with a custom comparison function.

#### Heap(Comparer\<T> comparer, [HeapType](Heirloom.Collections.HeapType.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with an instance of a custom comparer.

#### Heap([Heap\<T>](Heirloom.Collections.Heap[T].md) heap)

### Properties

#### <a name="COMP4526"></a> Comparer : Comparer\<T>

<small>`Read Only`</small>

The comparer used by this heap.

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

Gets the number of elements contained in the heap.

#### <a name="TYPE2333"></a> Type : [HeapType](Heirloom.Collections.HeapType.md)

<small>`Read Only`</small>

Which kind of heap is this?

### Methods

#### <a name="ADD(9453"></a> Add(T item) : bool
<small>`Virtual`</small>

Adds an item to the heap.


#### <a name="ADDR964B"></a> AddRange(IEnumerable\<T> items) : void
<small>`Virtual`</small>

Adds multiple items to the heap.


#### <a name="PEEK599B"></a> Peek() : T
<small>`Virtual`</small>

Gets the next item in the heap to be removed.

#### <a name="REMOF63F"></a> Remove() : T
<small>`Virtual`</small>

Removes and returns the next priority item in the heap.

#### <a name="REMO291D"></a> Remove(T item) : bool
<small>`Virtual`</small>

Removes a specific item from the heap.


#### <a name="UPDA9BB0"></a> Update(T item) : void
<small>`Virtual`</small>

Alerts the heap to update the position the element within the heap.


#### <a name="CONT50B6"></a> Contains(T item) : bool
<small>`Virtual`</small>


#### <a name="TOAR1C8F"></a> ToArray() : T

Clones the heap, and returns an array of the elements in priority ordering.

#### <a name="GETEDDD1"></a> GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

Enumerates the values in the heap (in no particular order)

