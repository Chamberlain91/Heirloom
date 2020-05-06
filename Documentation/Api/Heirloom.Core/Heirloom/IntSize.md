# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntSize (Struct)

> **Namespace**: [Heirloom][0]

Represents a size or dimensions defined with integer fields.

```cs
public struct IntSize : IEquatable<IntSize>, IComparable<IntSize>
```

### Inherits

IEquatable\<IntSize>, IComparable\<IntSize>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[CompareTo][6], [Deconstruct][7], [Equals][8], [GetHashCode][9], [Set][10], [ToString][11]

### Static Fields

[Max][12], [One][13], [Zero][14]

## Fields

#### Instance

| Name        | Type  | Summary                              |
|-------------|-------|--------------------------------------|
| [Height][1] | `int` | The height (vertical size measure).  |
| [Width][2]  | `int` | The width (horizontal size measure). |

#### Static

| Name       | Type          | Summary                                  |
|------------|---------------|------------------------------------------|
| [Max][12]  | [IntSize][15] | The maximum representable size possible. |
| [One][13]  | [IntSize][15] | A 1x1 size.                              |
| [Zero][14] | [IntSize][15] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `int`   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `int`   |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                           |
|--------------------------------|-------------|-----------------------------------|
| [CompareTo(IntSize)][6]        | `int`       |                                   |
| [Deconstruct(out int, o...][7] | `void`      |                                   |
| [Equals(object)][8]            | `bool`      |                                   |
| [Equals(IntSize)][8]           | `bool`      |                                   |
| [GetHashCode()][9]             | `int`       |                                   |
| [Set(int, int)][10]            | `void`      | Sets the components of this size. |
| [ToString()][11]               | `string`    |                                   |

[0]: ../../Heirloom.Core.md
[1]: IntSize/Height.md
[2]: IntSize/Width.md
[3]: IntSize/Area.md
[4]: IntSize/Aspect.md
[5]: IntSize/Indexer.md
[6]: IntSize/CompareTo.md
[7]: IntSize/Deconstruct.md
[8]: IntSize/Equals.md
[9]: IntSize/GetHashCode.md
[10]: IntSize/Set.md
[11]: IntSize/ToString.md
[12]: IntSize/Max.md
[13]: IntSize/One.md
[14]: IntSize/Zero.md
[15]: IntSize.md
