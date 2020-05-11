# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IHeap\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

Represents a heap data structure. Allowing the access and removal of items by a priority ordering.

```cs
public interface IHeap<T> : IReadOnlyHeap<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IReadOnlyHeap\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Methods

[Add][2], [AddRange][3], [Remove][4], [Update][5]

## Methods

#### Instance

| Name                          | Return Type | Summary                                                             |
|-------------------------------|-------------|---------------------------------------------------------------------|
| [Add(T)][2]                   | `bool`      | Adds an item to the heap.                                           |
| [AddRange(IEnumerable<T>)][3] | `void`      | Adds multiple items to the heap.                                    |
| [Remove(T)][4]                | `bool`      | Removes a specific item from the heap.                              |
| [Remove()][4]                 | `T`         | Removes and returns the next priority item in the heap.             |
| [Update(T)][5]                | `void`      | Alerts the heap to update the position the element within the heap. |

[0]: ../../Heirloom.Core.md
[1]: IReadOnlyHeap[T].md
[2]: IHeap[T]/Add.md
[3]: IHeap[T]/AddRange.md
[4]: IHeap[T]/Remove.md
[5]: IHeap[T]/Update.md
