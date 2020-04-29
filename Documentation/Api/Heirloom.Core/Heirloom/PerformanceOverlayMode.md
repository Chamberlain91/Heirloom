# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## PerformanceOverlayMode Enum

> **Namespace**: [Heirloom][0]  

Controls showing the performance overlay on a [Graphics][1] object.

```cs
public enum PerformanceOverlayMode : IComparable, IFormattable, IConvertible
```

| Name     | Summary                                                      |
|----------|--------------------------------------------------------------|
| Disabled | Hides the performance overlay.                               |
| Simple   | Displays FPS as a short string.                              |
| Standard | Displays FPS, batch count and draw count.                    |
| Full     | Displays FPS, batch count and draw count with std deviation. |

[0]: ../../Heirloom.Core.md
[1]: Graphics.md
