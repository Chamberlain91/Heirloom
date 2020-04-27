# WindowState

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  

Describes the size state a window can be in.

```cs
public enum WindowState : IComparable, IFormattable, IConvertible
```

--------------------------------------------------------------------------------

| Name       | Summary                                   |
|------------|-------------------------------------------|
| Normal     | Window is neither minimized or maximized. |
| Minimized  | Window has been minimized.                |
| Maximized  | Window has been maximized.                |
| Fullscreen | Window is fullscreen.                     |

[0]: ..\Heirloom.Platforms.Desktop.md
[1]: ..\Heirloom.Core.md
[2]: ..\Heirloom.OpenGLES.md
[3]: ..\Heirloom.MiniAudio.md