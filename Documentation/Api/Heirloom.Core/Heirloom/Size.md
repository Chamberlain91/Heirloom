# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Size

> **Namespace**: [Heirloom][0]  

```cs
public struct Size : IEquatable<Size>, IComparable<Size>
```

### Inherits

IEquatable\<Size>, IComparable\<Size>

#### Fields

[Width][1], [Height][2]

#### Properties

[Area][3], [Aspect][4], [Indexer][5]

#### Methods

[Set][6], [Deconstruct][7], [CompareTo][8]

#### Static Fields

[Infinite][9], [Max][10], [Zero][11], [One][12]

## Fields

| Name          | Summary                                  |
|---------------|------------------------------------------|
| [Width][1]    | The width (horizontal size measure).     |
| [Height][2]   | The height (vertical size measure).      |
| [Infinite][9] | An infinite size.                        |
| [Max][10]     | The maximum representable size possible. |
| [Zero][11]    | A 0x0 size.                              |
| [One][12]     | A 1x1 size.                              |

## Properties

| Name         | Summary                                                            |
|--------------|--------------------------------------------------------------------|
| [Area][3]    | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | Gets the aspect ratio of this size.                                |
| [Indexer][5] |                                                                    |

## Methods

| Name             | Summary                           |
|------------------|-----------------------------------|
| [Set][6]         | Sets the components of this size. |
| [Deconstruct][7] |                                   |
| [CompareTo][8]   |                                   |

[0]: ../../Heirloom.Core.md
[1]: Size/Width.md
[2]: Size/Height.md
[3]: Size/Area.md
[4]: Size/Aspect.md
[5]: Size/Indexer.md
[6]: Size/Set.md
[7]: Size/Deconstruct.md
[8]: Size/CompareTo.md
[9]: Size/Infinite.md
[10]: Size/Max.md
[11]: Size/Zero.md
[12]: Size/One.md
