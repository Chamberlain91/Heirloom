# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RectanglePacker\<T> (Class)

> **Namespace**: [Heirloom][0]

```cs
public class RectanglePacker<T> : IRectanglePacker<T>
```

### Inherits

[IRectanglePacker\<T>][1]

### Properties

[Elements][2], [Size][3]

### Methods

[Add][4], [Clear][5], [Contains][6], [GetRectangle][7], [TryGetRectangle][8]

## Properties

#### Instance

| Name          | Type             | Summary |
|---------------|------------------|---------|
| [Elements][2] | `IEnumerable<T>` |         |
| [Size][3]     | [IntSize][9]     |         |

## Methods

#### Instance

| Name                           | Return Type        | Summary |
|--------------------------------|--------------------|---------|
| [Add(T, IntSize)][4]           | `bool`             |         |
| [Clear()][5]                   | `void`             |         |
| [Contains(T)][6]               | `bool`             |         |
| [GetRectangle(T)][7]           | [IntRectangle][10] |         |
| [TryGetRectangle(T, out...][8] | `bool`             |         |

[0]: ../../Heirloom.Core.md
[1]: IRectanglePacker[T].md
[2]: RectanglePacker[T]/Elements.md
[3]: RectanglePacker[T]/Size.md
[4]: RectanglePacker[T]/Add.md
[5]: RectanglePacker[T]/Clear.md
[6]: RectanglePacker[T]/Contains.md
[7]: RectanglePacker[T]/GetRectangle.md
[8]: RectanglePacker[T]/TryGetRectangle.md
[9]: IntSize.md
[10]: IntRectangle.md
