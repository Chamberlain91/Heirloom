# Heirloom.Collections

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections](../heirloom.collections/heirloom.collections.md)</small>  

## Heap\<T> (Class)
<small>**Namespace**: Heirloom.Collections</sub></small>  
<small>**Interfaces**: [IHeap\<T>](heirloom.collections.iheap[t].md), [IReadOnlyHeap\<T>](heirloom.collections.ireadonlyheap[t].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

Represents a heap data structure. Allows the insertion and removal of items by priority.

The heap always acts like a min-heap but inverts the result of comparison for max heaps.

| Properties | Summary |
|------------|---------|
| [Comparer](#COM45265051) | The comparer used by this heap. |
| [Count](#COU73CA0BBB) | Gets the number of elements contained in the heap. |
| [Type](#TYP233312DE) | Which kind of heap is this? |

| Methods | Summary |
|---------|---------|
| [Add](#ADD9453EEA5) | Adds an item to the heap. |
| [AddRange](#ADD964BA200) | Adds multiple items to the heap. |
| [Peek](#PEE599BAF94) | Gets the next item in the heap to be removed. |
| [Remove](#REMF63FEEE5) | Removes and returns the next priority item in the heap. |
| [Remove](#REM291D149A) | Removes a specific item from the heap. |
| [Update](#UPD9BB09A13) | Alerts the heap to update the position the element within the heap. |
| [Contains](#CON50B6A9F) |  |
| [ToArray](#TOA1C8FFB1) | Clones the heap, and returns an array of the elements in priority ordering. |
| [GetEnumerator](#GETDDD17E2E) | Enumerates the values in the heap (in no particular order) |

### Constructors

#### Heap([HeapType](heirloom.collections.heaptype.md) type = 1)

Constructs a new heap that optionally sorts by maximum or minimum comparisons.

#### Heap(Comparison\<T> comparison, [HeapType](heirloom.collections.heaptype.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with a custom comparison function.

#### Heap(Comparer\<T> comparer, [HeapType](heirloom.collections.heaptype.md) type)

Constructs a new heap that optionally sorts by maximum or minimum comparisons with an instance of a custom comparer.

#### Heap([Heap\<T>](heirloom.collections.heap[t].md) heap)

### Properties

#### <a name="COM45265051"></a>Comparer : Comparer\<T>

<small>`Read Only`</small>

The comparer used by this heap.

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of elements contained in the heap.

#### <a name="TYP233312DE"></a>Type : [HeapType](heirloom.collections.heaptype.md)

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

