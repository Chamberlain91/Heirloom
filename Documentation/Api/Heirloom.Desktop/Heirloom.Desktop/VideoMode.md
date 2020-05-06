# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## VideoMode (Struct)

> **Namespace**: [Heirloom.Desktop][0]

Represents a video mode a [Monitor][1] can be in.

```cs
public struct VideoMode : IEquatable<VideoMode>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<VideoMode>

### Fields

[BlueBits][2], [GreenBits][3], [Height][4], [RedBits][5], [RefreshRate][6], [Width][7]

## Fields

#### Instance

| Name             | Type  | Summary                                                            |
|------------------|-------|--------------------------------------------------------------------|
| [BlueBits][2]    | `int` | The number of blue bits in the color space supported by the mode.  |
| [GreenBits][3]   | `int` | The number of green bits in the color space supported by the mode. |
| [Height][4]      | `int` | The height in pixels.                                              |
| [RedBits][5]     | `int` | The number of red bits in the color space supported by the mode.   |
| [RefreshRate][6] | `int` | The refresh rate (in hertz) of the mode.                           |
| [Width][7]       | `int` | The width in pixels.                                               |

[0]: ../../Heirloom.Desktop.md
[1]: Monitor.md
[2]: VideoMode/BlueBits.md
[3]: VideoMode/GreenBits.md
[4]: VideoMode/Height.md
[5]: VideoMode/RedBits.md
[6]: VideoMode/RefreshRate.md
[7]: VideoMode/Width.md
