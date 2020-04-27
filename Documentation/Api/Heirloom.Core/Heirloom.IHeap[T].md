# IHeap\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a heap data structure. Allowing the access and removal of items by a priority ordering.

```cs
public abstract interface IHeap<T> : IReadOnlyHeap<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [IReadOnlyHeap\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Methods**: [Add][2], [AddRange][3], [Remove][4], [Update][5]

--------------------------------------------------------------------------------

## Methods

| Name          | Summary                                                             |
|---------------|---------------------------------------------------------------------|
| [Add][2]      | Adds an item to the heap.                                           |
| [AddRange][3] | Adds multiple items to the heap.                                    |
| [Remove][4]   | Removes a specific item from the heap.                              |
| [Remove][4]   | Removes and returns the next priority item in the heap.             |
| [Update][5]   | Alerts the heap to update the position the element within the heap. |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IReadOnlyHeap[T].md
[2]: Heirloom.IHeap[T].Add.md
[3]: Heirloom.IHeap[T].AddRange.md
[4]: Heirloom.IHeap[T].Remove.md
[5]: Heirloom.IHeap[T].Update.md
