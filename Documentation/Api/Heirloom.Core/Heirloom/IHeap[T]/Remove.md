# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IHeap\<T>.Remove (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [IHeap\<T>][1]

### Remove(T)

Removes a specific item from the heap.

```cs
public abstract bool Remove(T item)
```

| Name | Type | Summary |
|------|------|---------|
| item | `T`  |         |

> **Returns** - `bool`

### Remove()

Removes and returns the next priority item in the heap.

```cs
public abstract T Remove()
```

> **Returns** - `T`

[0]: ../../../Heirloom.Core.md
[1]: ../IHeap[T].md
