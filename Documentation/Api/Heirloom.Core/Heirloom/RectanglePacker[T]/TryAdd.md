# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RectanglePacker\<T>.TryAdd (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [RectanglePacker\<T>][1]

### TryAdd(T, IntSize)

Attempts to add an element to the collection.

```cs
public bool TryAdd(T element, IntSize itemSize)
```

| Name     | Type         | Summary                  |
|----------|--------------|--------------------------|
| element  | `T`          | The element to pack.     |
| itemSize | [IntSize][2] | The size of the element. |

> **Returns** - `bool` - Will return false if the item was not packed successfully or already existed, otherwise true.

[0]: ../../../Heirloom.Core.md
[1]: ../RectanglePacker[T].md
[2]: ../IntSize.md
