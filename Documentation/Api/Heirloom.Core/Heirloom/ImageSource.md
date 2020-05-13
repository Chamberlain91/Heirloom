# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ImageSource (Class)

> **Namespace**: [Heirloom][0]

An abstract representation of an image.

```cs
public abstract class ImageSource
```

**See Also:** [Surface][1], [Image][2]

### Properties

[Height][3], [Interpolation][4], [Origin][5], [Repeat][6], [Size][7], [Width][8]

## Properties

#### Instance

| Name               | Type                   | Summary                                                         |
|--------------------|------------------------|-----------------------------------------------------------------|
| [Height][3]        | `int`                  | The height of the image (in pixels).                            |
| [Interpolation][4] | [InterpolationMode][9] | Interpolation mode.                                             |
| [Origin][5]        | [IntVector][10]        | The offset used to 'center' the image around a non-zero origin. |
| [Repeat][6]        | [RepeatMode][11]       | Repeat mode.                                                    |
| [Size][7]          | [IntSize][12]          | The size of this image.                                         |
| [Width][8]         | `int`                  | The width of the image (in pixels).                             |

[0]: ../../Heirloom.Core.md
[1]: Surface.md
[2]: Image.md
[3]: ImageSource/Height.md
[4]: ImageSource/Interpolation.md
[5]: ImageSource/Origin.md
[6]: ImageSource/Repeat.md
[7]: ImageSource/Size.md
[8]: ImageSource/Width.md
[9]: InterpolationMode.md
[10]: IntVector.md
[11]: RepeatMode.md
[12]: IntSize.md
