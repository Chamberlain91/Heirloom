# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Surface (Class)

> **Namespace**: [Heirloom][0]

Represents a surface a [GraphicsContext][1] object can draw on.

```cs
public sealed class Surface : ImageSource
```

### Inherits

[ImageSource][2]

### Properties

[IsScreenBound][3], [Multisample][4], [Size][5], [SurfaceType][6]

### Static Properties

[MaxSupportedMultisampleQuality][7]

## Properties

#### Instance

| Name               | Type                    | Summary                                                            |
|--------------------|-------------------------|--------------------------------------------------------------------|
| [IsScreenBound][3] | `bool`                  | Determines if this surface is attached to a screen (ie, a window). |
| [Multisample][4]   | [MultisampleQuality][8] | Gets the multisampling quality set on this surface.                |
| [Size][5]          | [IntSize][9]            | Gets size of the surface in pixels.                                |
| [SurfaceType][6]   | [SurfaceType][10]       | Gets the type of this surface.                                     |

#### Static

| Name                                | Type                    | Summary                                                    |
|-------------------------------------|-------------------------|------------------------------------------------------------|
| [MaxSupportedMultisampleQuality][7] | [MultisampleQuality][8] | Gets the max multisample quality supported on this system. |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext.md
[2]: ImageSource.md
[3]: Surface/IsScreenBound.md
[4]: Surface/Multisample.md
[5]: Surface/Size.md
[6]: Surface/SurfaceType.md
[7]: Surface/MaxSupportedMultisampleQuality.md
[8]: MultisampleQuality.md
[9]: IntSize.md
[10]: SurfaceType.md
