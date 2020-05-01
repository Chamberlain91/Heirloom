# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## FreeList\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

A free list an allocation-centric data structure that allows insertion and removal of elements in O(1) time, but does not behave like a typical "list" data type.

```cs
public sealed class FreeList<T>
```

### Properties

[Capacity][1], [Count][2], [Indexer][3]

### Methods

[Clear][4], [Insert][5], [Remove][6], [Resize][7]

## Properties

#### Instance

| Name          | Type  | Summary                                                                |
|---------------|-------|------------------------------------------------------------------------|
| [Capacity][1] | `int` | Gets the total number of elements that can be stored in this FreeLi... |
| [Count][2]    | `int` | Gets the number of elements stored in this FreeList<T> .               |
| [Indexer][3]  | `T`   |                                                                        |

## Methods

#### Instance

| Name             | Return Type | Summary                                                                |
|------------------|-------------|------------------------------------------------------------------------|
| [Clear()][4]     | `void`      | Clears the free list, invalidating all indices and clearing element... |
| [Insert(T)][5]   | `int`       | Inserts an element into the free list and returns its index.           |
| [Remove(int)][6] | `void`      | Removes an element from the free list by an index returned by Inser... |
| [Resize(int)][7] | `void`      | Resize the free list with an increased capacity.                       |

[0]: ../../Heirloom.Core.md
[1]: FreeList[T]/Capacity.md
[2]: FreeList[T]/Count.md
[3]: FreeList[T]/Indexer.md
[4]: FreeList[T]/Clear.md
[5]: FreeList[T]/Insert.md
[6]: FreeList[T]/Remove.md
[7]: FreeList[T]/Resize.md
