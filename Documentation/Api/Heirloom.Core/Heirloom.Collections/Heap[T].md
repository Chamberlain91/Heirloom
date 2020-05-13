# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heap\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

Represents a heap data structure. Allows the insertion and removal of items by priority.

```cs
public class Heap<T> : IHeap<T>, IReadOnlyHeap<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

The heap always acts like a min-heap but inverts the result of comparison for max heaps.

### Inherits

[IHeap\<T>][1], [IReadOnlyHeap\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Properties

[Comparer][3], [Count][4], [Type][5]

### Methods

[Add][6], [AddRange][7], [Contains][8], [GetEnumerator][9], [Peek][10], [Remove][11], [ToArray][12], [Update][13]

## Properties

#### Instance

| Name          | Type           | Summary                                            |
|---------------|----------------|----------------------------------------------------|
| [Comparer][3] | `Comparer<T>`  | The comparer used by this heap.                    |
| [Count][4]    | `int`          | Gets the number of elements contained in the heap. |
| [Type][5]     | [HeapType][14] | Which kind of heap is this?                        |

## Methods

#### Instance

| Name                          | Return Type      | Summary                                                                |
|-------------------------------|------------------|------------------------------------------------------------------------|
| [Add(T)][6]                   | `bool`           | Adds an item to the heap.                                              |
| [AddRange(IEnumerable<T>)][7] | `void`           | Adds multiple items to the heap.                                       |
| [Contains(T)][8]              | `bool`           | Determines whether the Heap<T> contains the specified item.            |
| [GetEnumerator()][9]          | `IEnumerator<T>` | Enumerates the values in the heap (in no particular order)             |
| [Peek()][10]                  | `T`              | Gets the next item in the heap to be removed.                          |
| [Remove()][11]                | `T`              | Removes and returns the next priority item in the heap.                |
| [Remove(T)][11]               | `bool`           | Removes a specific item from the heap.                                 |
| [ToArray()][12]               | `T[]`            | Clones the heap, and returns an array of the elements in priority o... |
| [Update(T)][13]               | `void`           | Alerts the heap to update the position the element within the heap.    |

[0]: ../../Heirloom.Core.md
[1]: IHeap[T].md
[2]: IReadOnlyHeap[T].md
[3]: Heap[T]/Comparer.md
[4]: Heap[T]/Count.md
[5]: Heap[T]/Type.md
[6]: Heap[T]/Add.md
[7]: Heap[T]/AddRange.md
[8]: Heap[T]/Contains.md
[9]: Heap[T]/GetEnumerator.md
[10]: Heap[T]/Peek.md
[11]: Heap[T]/Remove.md
[12]: Heap[T]/ToArray.md
[13]: Heap[T]/Update.md
[14]: HeapType.md
