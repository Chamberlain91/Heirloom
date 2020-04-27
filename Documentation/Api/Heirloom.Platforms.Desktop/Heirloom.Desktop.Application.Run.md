# Application.Run

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  
> **Type**: [Application][4]  

--------------------------------------------------------------------------------

### Run(Action)

Initializes windowing utilities, executes `startup` and then continuously processes window events until all windows are closed. This is a blocking function.

```cs
public void Run(Action startup)
```

[0]: ..\Heirloom.Platforms.Desktop.md
[1]: ..\Heirloom.Core.md
[2]: ..\Heirloom.OpenGLES.md
[3]: ..\Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Application.md
