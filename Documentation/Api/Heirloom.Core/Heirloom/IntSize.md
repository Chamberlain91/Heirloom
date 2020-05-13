# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntSize (Struct)

> **Namespace**: [Heirloom][0]

Represents two dimensional size by a measure in each axis.

```cs
public struct IntSize : IEquatable<IntSize>
```

### Inherits

IEquatable\<IntSize>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[Deconstruct][6], [Set][7]

### Static Fields

[Max][8], [One][9], [Zero][10]

## Fields

#### Instance

| Name        | Type  | Summary                              |
|-------------|-------|--------------------------------------|
| [Height][1] | `int` | The height (vertical size measure).  |
| [Width][2]  | `int` | The width (horizontal size measure). |

#### Static

| Name       | Type          | Summary                                  |
|------------|---------------|------------------------------------------|
| [Max][8]   | [IntSize][11] | The maximum representable size possible. |
| [One][9]   | [IntSize][11] | A 1x1 size.                              |
| [Zero][10] | [IntSize][11] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `int`   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `int`   |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                                               |
|--------------------------------|-------------|-------------------------------------------------------|
| [Deconstruct(out int, o...][6] | `void`      | Deconstructs this IntSize int constituent components. |
| [Set(int, int)][7]             | `void`      | Sets the components of this size.                     |

[0]: ../../Heirloom.Core.md
[1]: IntSize/Height.md
[2]: IntSize/Width.md
[3]: IntSize/Area.md
[4]: IntSize/Aspect.md
[5]: IntSize/Indexer.md
[6]: IntSize/Deconstruct.md
[7]: IntSize/Set.md
[8]: IntSize/Max.md
[9]: IntSize/One.md
[10]: IntSize/Zero.md
[11]: IntSize.md
