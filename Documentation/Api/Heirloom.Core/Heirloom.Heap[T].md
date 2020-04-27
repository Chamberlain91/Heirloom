# Heap\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a heap data structure. Allows the insertion and removal of items by priority.

```cs
public class Heap<T> : IHeap<T>, IReadOnlyHeap<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```


The heap always acts like a min-heap but inverts the result of comparison for max heaps.

--------------------------------------------------------------------------------

**Inherits**: [IHeap\<T>][1], [IReadOnlyHeap\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Properties**: [Comparer][3], [Count][4], [Type][5]

**Methods**: [Add][6], [AddRange][7], [Peek][8], [Remove][9], [Update][10], [Contains][11], [ToArray][12], [GetEnumerator][13]

--------------------------------------------------------------------------------

## Properties

| Name          | Summary                                            |
|---------------|----------------------------------------------------|
| [Comparer][3] | The comparer used by this heap.                    |
| [Count][4]    | Gets the number of elements contained in the heap. |
| [Type][5]     | Which kind of heap is this?                        |

## Methods

| Name                | Summary                                                                     |
|---------------------|-----------------------------------------------------------------------------|
| [Add][6]            | Adds an item to the heap.                                                   |
| [AddRange][7]       | Adds multiple items to the heap.                                            |
| [Peek][8]           | Gets the next item in the heap to be removed.                               |
| [Remove][9]         | Removes and returns the next priority item in the heap.                     |
| [Remove][9]         | Removes a specific item from the heap.                                      |
| [Update][10]        | Alerts the heap to update the position the element within the heap.         |
| [Contains][11]      |                                                                             |
| [ToArray][12]       | Clones the heap, and returns an array of the elements in priority ordering. |
| [GetEnumerator][13] | Enumerates the values in the heap (in no particular order)                  |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IHeap[T].md
[2]: Heirloom.IReadOnlyHeap[T].md
[3]: Heirloom.Heap[T].Comparer.md
[4]: Heirloom.Heap[T].Count.md
[5]: Heirloom.Heap[T].Type.md
[6]: Heirloom.Heap[T].Add.md
[7]: Heirloom.Heap[T].AddRange.md
[8]: Heirloom.Heap[T].Peek.md
[9]: Heirloom.Heap[T].Remove.md
[10]: Heirloom.Heap[T].Update.md
[11]: Heirloom.Heap[T].Contains.md
[12]: Heirloom.Heap[T].ToArray.md
[13]: Heirloom.Heap[T].GetEnumerator.md
