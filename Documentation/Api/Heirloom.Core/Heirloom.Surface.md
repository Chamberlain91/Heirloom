# Surface

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a surface a [Graphics][1] object can draw on.

```cs
public sealed class Surface : ImageSource
```

--------------------------------------------------------------------------------

**Inherits**: [ImageSource][2]

**Properties**: [Size][3], [Multisample][4], [IsScreenBound][5]

**Static Properties**: [MaxSupportedMultisampleQuality][6]

--------------------------------------------------------------------------------

## Properties

| Name                                | Summary                                                            |
|-------------------------------------|--------------------------------------------------------------------|
| [Size][3]                           | Gets size of the surface in pixels.                                |
| [Multisample][4]                    | Gets the multisampling quality set on this surface.                |
| [IsScreenBound][5]                  | Determines if this surface is attached to a screen (ie, a window). |
| [MaxSupportedMultisampleQuality][6] | Gets the max multisample quality supported on this system.         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Graphics.md
[2]: Heirloom.ImageSource.md
[3]: Heirloom.Surface.Size.md
[4]: Heirloom.Surface.Multisample.md
[5]: Heirloom.Surface.IsScreenBound.md
[6]: Heirloom.Surface.MaxSupportedMultisampleQuality.md
