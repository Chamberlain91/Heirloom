# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.ToHeap\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### ToHeap<T>(IEnumerable<T>, HeapType)

Constructs a new [Heap\<T>][2] from an `IEnumerable<T>`

```cs
public static Heap<T> ToHeap<T>(IEnumerable<T> items, HeapType type = Min)
```

`ExtensionAttribute`

| Name  | Type              | Summary |
|-------|-------------------|---------|
| items | `IEnumerable\<T>` |         |
| type  | [HeapType][3]     |         |

> **Returns** - [Heap\<T>][2]

### ToHeap<T>(IEnumerable<T>, Comparison<T>, HeapType)

Constructs a new [Heap\<T>][2] from an `IEnumerable<T>`

```cs
public static Heap<T> ToHeap<T>(IEnumerable<T> items, Comparison<T> comparison, HeapType type = Min)
```

`ExtensionAttribute`

| Name       | Type              | Summary |
|------------|-------------------|---------|
| items      | `IEnumerable\<T>` |         |
| comparison | `Comparison\<T>`  |         |
| type       | [HeapType][3]     |         |

> **Returns** - [Heap\<T>][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
[2]: ../Heap[T].md
[3]: ../HeapType.md
