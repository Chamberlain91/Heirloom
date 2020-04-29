# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IReadOnlyHeap\<T> Interface

> **Namespace**: [Heirloom][0]  

Represents a read-only view of a [Heap\<T>][1] .

```cs
public interface IReadOnlyHeap<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

#### Properties

[Comparer][2]

#### Methods

[Contains][3], [Peek][4]

## Properties

| Name          | Summary                                     |
|---------------|---------------------------------------------|
| [Comparer][2] | The comparer used to compare item priority. |

## Methods

| Name          | Summary                                       |
|---------------|-----------------------------------------------|
| [Contains][3] | Does this heap contain the specified item?    |
| [Peek][4]     | Gets the next item in the heap to be removed. |

[0]: ../../Heirloom.Core.md
[1]: Heap[T].md
[2]: IReadOnlyHeap[T]/Comparer.md
[3]: IReadOnlyHeap[T]/Contains.md
[4]: IReadOnlyHeap[T]/Peek.md
