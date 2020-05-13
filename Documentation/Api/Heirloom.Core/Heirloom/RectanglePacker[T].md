# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RectanglePacker\<T> (Class)

> **Namespace**: [Heirloom][0]

A utility object for packing rectangles into a larger container rectangle.

```cs
public sealed class RectanglePacker<T> : IRectanglePacker<T>
```

Useful for creating things like sprite sheets, font atlases, etc.

### Inherits

[IRectanglePacker\<T>][1]

### Properties

[Elements][2], [Size][3]

### Methods

[Clear][4], [Contains][5], [GetRectangle][6], [TryAdd][7], [TryGetRectangle][8]

## Properties

#### Instance

| Name          | Type             | Summary                                        |
|---------------|------------------|------------------------------------------------|
| [Elements][2] | `IEnumerable<T>` | Gets the elements packed into this collection. |
| [Size][3]     | [IntSize][9]     | Gets the size of the container rectangle.      |

## Methods

#### Instance

| Name                           | Return Type        | Summary                                                        |
|--------------------------------|--------------------|----------------------------------------------------------------|
| [Clear()][4]                   | `void`             | Removes all packed elements from this collection.              |
| [Contains(T)][5]               | `bool`             | Determines if this collection contains the specified element.  |
| [GetRectangle(T)][6]           | [IntRectangle][10] | Gets the packed rectangle of the specified element.            |
| [TryAdd(T, IntSize)][7]        | `bool`             | Attempts to add an element to the collection.                  |
| [TryGetRectangle(T, out...][8] | `bool`             | Attempts to get the packed rectangle of the specified element. |

[0]: ../../Heirloom.Core.md
[1]: IRectanglePacker[T].md
[2]: RectanglePacker[T]/Elements.md
[3]: RectanglePacker[T]/Size.md
[4]: RectanglePacker[T]/Clear.md
[5]: RectanglePacker[T]/Contains.md
[6]: RectanglePacker[T]/GetRectangle.md
[7]: RectanglePacker[T]/TryAdd.md
[8]: RectanglePacker[T]/TryGetRectangle.md
[9]: IntSize.md
[10]: IntRectangle.md
