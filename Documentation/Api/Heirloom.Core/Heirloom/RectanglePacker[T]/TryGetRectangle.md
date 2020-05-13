# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RectanglePacker\<T>.TryGetRectangle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [RectanglePacker\<T>][1]

### TryGetRectangle(T, out IntRectangle)

Attempts to get the packed rectangle of the specified element.

```cs
public bool TryGetRectangle(T element, out IntRectangle rectangle)
```

| Name      | Type              | Summary                                                            |
|-----------|-------------------|--------------------------------------------------------------------|
| element   | `T`               | Some element potentially contained by this collection.             |
| rectangle | [IntRectangle][2] | Outputs the rectangle of the packed element, if call returns true. |

> **Returns** - `bool` - True if the element was contained by this collection.

[0]: ../../../Heirloom.Core.md
[1]: ../RectanglePacker[T].md
[2]: ../IntRectangle.md
