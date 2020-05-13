# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Size (Struct)

> **Namespace**: [Heirloom][0]

Represents two dimensional size by a measure in each axis.

```cs
public struct Size : IEquatable<Size>
```

### Inherits

IEquatable\<Size>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[Deconstruct][6], [Set][7]

### Static Fields

[Infinite][8], [Max][9], [One][10], [Zero][11]

## Fields

#### Instance

| Name        | Type    | Summary                              |
|-------------|---------|--------------------------------------|
| [Height][1] | `float` | The height (vertical size measure).  |
| [Width][2]  | `float` | The width (horizontal size measure). |

#### Static

| Name          | Type       | Summary                                  |
|---------------|------------|------------------------------------------|
| [Infinite][8] | [Size][12] | An infinite size.                        |
| [Max][9]      | [Size][12] | The maximum representable size possible. |
| [One][10]     | [Size][12] | A 1x1 size.                              |
| [Zero][11]    | [Size][12] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `float` | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `float` |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                                            |
|--------------------------------|-------------|----------------------------------------------------|
| [Deconstruct(out float,...][6] | `void`      | Deconstructs this Size int constituent components. |
| [Set(float, float)][7]         | `void`      | Sets the components of this size.                  |

[0]: ../../Heirloom.Core.md
[1]: Size/Height.md
[2]: Size/Width.md
[3]: Size/Area.md
[4]: Size/Aspect.md
[5]: Size/Indexer.md
[6]: Size/Deconstruct.md
[7]: Size/Set.md
[8]: Size/Infinite.md
[9]: Size/Max.md
[10]: Size/One.md
[11]: Size/Zero.md
[12]: Size.md
