# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Surface

> **Namespace**: [Heirloom][0]  

Represents a surface a [Graphics][1] object can draw on.

```cs
public sealed class Surface : ImageSource
```

### Inherits

[ImageSource][2]

#### Properties

[Size][3], [Multisample][4], [IsScreenBound][5]

#### Static Properties

[MaxSupportedMultisampleQuality][6]

## Properties

| Name                                | Summary                                                            |
|-------------------------------------|--------------------------------------------------------------------|
| [Size][3]                           | Gets size of the surface in pixels.                                |
| [Multisample][4]                    | Gets the multisampling quality set on this surface.                |
| [IsScreenBound][5]                  | Determines if this surface is attached to a screen (ie, a window). |
| [MaxSupportedMultisampleQuality][6] | Gets the max multisample quality supported on this system.         |

[0]: ../../Heirloom.Core.md
[1]: Graphics.md
[2]: ImageSource.md
[3]: Surface/Size.md
[4]: Surface/Multisample.md
[5]: Surface/IsScreenBound.md
[6]: Surface/MaxSupportedMultisampleQuality.md
