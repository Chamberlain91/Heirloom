# Application

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  

```cs
public static class Application
```

--------------------------------------------------------------------------------

**Static Properties**: [SupportsTransparentFramebuffer][4], [IsInitialized][5], [Windows][6], [DefaultMonitor][7], [Monitors][8], [CpuInfo][9], [GpuInfo][10]

**Static Methods**: [Run][11]

--------------------------------------------------------------------------------

## Properties

| Name                                | Summary                                                                                                |
|-------------------------------------|--------------------------------------------------------------------------------------------------------|
| [SupportsTransparentFramebuffer][4] | Gets a value that determines if transparent window framebuffers are supported on this device/platform. |
| [IsInitialized][5]                  | Gets a value determining if the application has been initialized.                                      |
| [Windows][6]                        | Gets a read-only list of currently opened windows.                                                     |
| [DefaultMonitor][7]                 | The default (primary) monitor.                                                                         |
| [Monitors][8]                       | Gets all currently connected monitors.                                                                 |
| [CpuInfo][9]                        | Gets detected information about the CPU.                                                               |
| [GpuInfo][10]                       | Gets detected information about the GPU.                                                               |

## Methods

| Name      | Summary                                                                                                                                                      |
|-----------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Run][11] | Initializes windowing utilities, executes `startup` and then continuously processes window events until all windows are closed. This is a blocking function. |

[0]: ..\Heirloom.Platforms.Desktop.md
[1]: ..\Heirloom.Core.md
[2]: ..\Heirloom.OpenGLES.md
[3]: ..\Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Application.SupportsTransparentFramebuffer.md
[5]: Heirloom.Desktop.Application.IsInitialized.md
[6]: Heirloom.Desktop.Application.Windows.md
[7]: Heirloom.Desktop.Application.DefaultMonitor.md
[8]: Heirloom.Desktop.Application.Monitors.md
[9]: Heirloom.Desktop.Application.CpuInfo.md
[10]: Heirloom.Desktop.Application.GpuInfo.md
[11]: Heirloom.Desktop.Application.Run.md
