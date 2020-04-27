# Extensions.ToHeap\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Extensions][1]  

--------------------------------------------------------------------------------

### ToHeap<T>(IEnumerable<T>, HeapType)

Constructs a new [Heap\<T>][2] from an `IEnumerable<T>`

```cs
public Heap<T> ToHeap<T>(IEnumerable<T> items, HeapType type = Min)
```

### ToHeap<T>(IEnumerable<T>, Comparison<T>, HeapType)

Constructs a new [Heap\<T>][2] from an `IEnumerable<T>`

```cs
public Heap<T> ToHeap<T>(IEnumerable<T> items, Comparison<T> comparison, HeapType type = Min)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Extensions.md
[2]: Heirloom.Heap[T].md
