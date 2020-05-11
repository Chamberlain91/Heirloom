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

[CompareTo][6], [Deconstruct][7], [Set][8]

### Static Fields

[Infinite][9], [Max][10], [One][11], [Zero][12]

## Fields

#### Instance

| Name        | Type    | Summary                              |
|-------------|---------|--------------------------------------|
| [Height][1] | `float` | The height (vertical size measure).  |
| [Width][2]  | `float` | The width (horizontal size measure). |

#### Static

| Name          | Type       | Summary                                  |
|---------------|------------|------------------------------------------|
| [Infinite][9] | [Size][13] | An infinite size.                        |
| [Max][10]     | [Size][13] | The maximum representable size possible. |
| [One][11]     | [Size][13] | A 1x1 size.                              |
| [Zero][12]    | [Size][13] | A 0x0 size.                              |

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
| [Set(float, float)][8]         | `void`      | Sets the components of this size. |

[0]: ../../Heirloom.Core.md
[1]: Size/Height.md
[2]: Size/Width.md
[3]: Size/Area.md
[4]: Size/Aspect.md
[5]: Size/Indexer.md
[6]: Size/CompareTo.md
[7]: Size/Deconstruct.md
[8]: Size/Set.md
[9]: Size/Infinite.md
[10]: Size/Max.md
[11]: Size/One.md
[12]: Size/Zero.md
[13]: Size.md
