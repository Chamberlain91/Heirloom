# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Surface (Class)

> **Namespace**: [Heirloom][0]

Represents a surface a [Graphics][1] object can draw on.

```cs
public sealed class Surface : ImageSource
```

### Inherits

[ImageSource][2]

### Properties

[IsScreenBound][3], [Multisample][4], [Size][5]

### Static Properties

[MaxSupportedMultisampleQuality][6]

## Properties

#### Instance

| Name               | Type                    | Summary                                                            |
|--------------------|-------------------------|--------------------------------------------------------------------|
| [IsScreenBound][3] | `bool`                  | Determines if this surface is attached to a screen (ie, a window). |
| [Multisample][4]   | [MultisampleQuality][7] | Gets the multisampling quality set on this surface.                |
| [Size][5]          | [IntSize][8]            | Gets size of the surface in pixels.                                |

#### Static

| Name                                | Type                    | Summary                                                    |
|-------------------------------------|-------------------------|------------------------------------------------------------|
| [MaxSupportedMultisampleQuality][6] | [MultisampleQuality][7] | Gets the max multisample quality supported on this system. |

[0]: ../../Heirloom.Core.md
[1]: Graphics.md
[2]: ImageSource.md
[3]: Surface/IsScreenBound.md
[4]: Surface/Multisample.md
[5]: Surface/Size.md
[6]: Surface/MaxSupportedMultisampleQuality.md
[7]: MultisampleQuality.md
[8]: IntSize.md
