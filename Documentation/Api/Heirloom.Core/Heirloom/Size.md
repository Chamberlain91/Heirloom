# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Size (Struct)

> **Namespace**: [Heirloom][0]

```cs
public struct Size : IEquatable<Size>, IComparable<Size>
```

### Inherits

IEquatable\<Size>, IComparable\<Size>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[CompareTo][6], [Deconstruct][7], [Equals][8], [GetHashCode][9], [Set][10], [ToString][11]

### Static Fields

[Infinite][12], [Max][13], [One][14], [Zero][15]

## Fields

#### Instance

| Name        | Type    | Summary                              |
|-------------|---------|--------------------------------------|
| [Height][1] | `float` | The height (vertical size measure).  |
| [Width][2]  | `float` | The width (horizontal size measure). |

#### Static

| Name           | Type       | Summary                                  |
|----------------|------------|------------------------------------------|
| [Infinite][12] | [Size][16] | An infinite size.                        |
| [Max][13]      | [Size][16] | The maximum representable size possible. |
| [One][14]      | [Size][16] | A 1x1 size.                              |
| [Zero][15]     | [Size][16] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `float` | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `float` |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                           |
|--------------------------------|-------------|-----------------------------------|
| [CompareTo(Size)][6]           | `int`       |                                   |
| [Deconstruct(out float,...][7] | `void`      |                                   |
| [Equals(object)][8]            | `bool`      |                                   |
| [Equals(Size)][8]              | `bool`      |                                   |
| [GetHashCode()][9]             | `int`       |                                   |
| [Set(float, float)][10]        | `void`      | Sets the components of this size. |
| [ToString()][11]               | `string`    |                                   |

[0]: ../../Heirloom.Core.md
[1]: Size/Height.md
[2]: Size/Width.md
[3]: Size/Area.md
[4]: Size/Aspect.md
[5]: Size/Indexer.md
[6]: Size/CompareTo.md
[7]: Size/Deconstruct.md
[8]: Size/Equals.md
[9]: Size/GetHashCode.md
[10]: Size/Set.md
[11]: Size/ToString.md
[12]: Size/Infinite.md
[13]: Size/Max.md
[14]: Size/One.md
[15]: Size/Zero.md
[16]: Size.md
