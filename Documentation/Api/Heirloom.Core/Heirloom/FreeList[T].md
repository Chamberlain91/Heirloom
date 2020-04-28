# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## FreeList\<T>

> **Namespace**: [Heirloom][0]  

A free list an allocation-centric data structure that allows insertion and removal of elements in O(1) time, but does not behave like a typical "list" data type.

```cs
public sealed class FreeList<T>
```

#### Properties

[Indexer][1], [Capacity][2], [Count][3]

#### Methods

[Clear][4], [Insert][5], [Remove][6], [Resize][7]

## Properties

| Name          | Summary                                                                           |
|---------------|-----------------------------------------------------------------------------------|
| [Indexer][1]  |                                                                                   |
| [Capacity][2] | Gets the total number of elements that can be stored in this [FreeList\\<T>][8] . |
| [Count][3]    | Gets the number of elements stored in this [FreeList\\<T>][8] .                   |

## Methods

| Name        | Summary                                                                                                                           |
|-------------|-----------------------------------------------------------------------------------------------------------------------------------|
| [Clear][4]  | Clears the free list, invalidating all indices and clearing element data.                                                         |
| [Insert][5] | Inserts an element into the free list and returns its index.                                                                      |
| [Remove][6] | Removes an element from the free list by an index returned by [Insert][5] . This index is not validated, you must be responsible. |
| [Resize][7] | Resize the free list with an increased capacity.                                                                                  |

[0]: ../../Heirloom.Core.md
[1]: FreeList[T]/Indexer.md
[2]: FreeList[T]/Capacity.md
[3]: FreeList[T]/Count.md
[4]: FreeList[T]/Clear.md
[5]: FreeList[T]/Insert.md
[6]: FreeList[T]/Remove.md
[7]: FreeList[T]/Resize.md
[8]: FreeList[T].md
