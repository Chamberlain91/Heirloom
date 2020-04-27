# IntSize

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a size or dimensions defined with integer fields.

```cs
public struct IntSize : IEquatable<IntSize>, IComparable<IntSize>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<IntSize>, IComparable\<IntSize>

**Fields**: [Width][1], [Height][2]

**Properties**: [Area][3], [Aspect][4], [Item][5]

**Methods**: [Set][6], [Deconstruct][7], [CompareTo][8]

**Static Fields**: [Max][9], [Zero][10], [One][11]

--------------------------------------------------------------------------------

## Constructors

### IntSize(int, int)

```cs
public IntSize(int width, int height)
```

## Fields

| Name        | Summary                                  |
|-------------|------------------------------------------|
| [Width][1]  | The width (horizontal size measure).     |
| [Height][2] | The height (vertical size measure).      |
| [Max][9]    | The maximum representable size possible. |
| [Zero][10]  | A 0x0 size.                              |
| [One][11]   | A 1x1 size.                              |

## Properties

| Name        | Summary                                                            |
|-------------|--------------------------------------------------------------------|
| [Area][3]   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4] | Gets the aspect ratio of this size.                                |
| [Item][5]   |                                                                    |

## Methods

| Name             | Summary                           |
|------------------|-----------------------------------|
| [Set][6]         | Sets the components of this size. |
| [Deconstruct][7] |                                   |
| [CompareTo][8]   |                                   |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IntSize.Width.md
[2]: Heirloom.IntSize.Height.md
[3]: Heirloom.IntSize.Area.md
[4]: Heirloom.IntSize.Aspect.md
[5]: Heirloom.IntSize.Item.md
[6]: Heirloom.IntSize.Set.md
[7]: Heirloom.IntSize.Deconstruct.md
[8]: Heirloom.IntSize.CompareTo.md
[9]: Heirloom.IntSize.Max.md
[10]: Heirloom.IntSize.Zero.md
[11]: Heirloom.IntSize.One.md
