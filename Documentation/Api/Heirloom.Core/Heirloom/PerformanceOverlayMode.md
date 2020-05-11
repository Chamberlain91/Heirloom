# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## PerformanceOverlayMode (Enum)

> **Namespace**: [Heirloom][0]

Controls showing the performance overlay on a [GraphicsContext][1] object.

```cs
public enum PerformanceOverlayMode : IComparable, IFormattable, IConvertible
```

| Name     | Summary                                                      |
|----------|--------------------------------------------------------------|
| Disabled | Hides the performance overlay.                               |
| Full     | Displays FPS, batch count and draw count with std deviation. |
| Simple   | Displays FPS as a short string.                              |
| Standard | Displays FPS, batch count and draw count.                    |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext.md
