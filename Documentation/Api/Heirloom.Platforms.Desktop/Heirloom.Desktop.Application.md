# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  

## Application

> **Namespace**: [Heirloom.Desktop][0]  

```cs
public static class Application
```

#### Static Properties

[SupportsTransparentFramebuffer][1], [IsInitialized][2], [Windows][3], [DefaultMonitor][4], [Monitors][5], [CpuInfo][6], [GpuInfo][7]

#### Static Methods

[Run][8]

## Properties

| Name                                | Summary                                                                                                |
|-------------------------------------|--------------------------------------------------------------------------------------------------------|
| [SupportsTransparentFramebuffer][1] | Gets a value that determines if transparent window framebuffers are supported on this device/platform. |
| [IsInitialized][2]                  | Gets a value determining if the application has been initialized.                                      |
| [Windows][3]                        | Gets a read-only list of currently opened windows.                                                     |
| [DefaultMonitor][4]                 | The default (primary) monitor.                                                                         |
| [Monitors][5]                       | Gets all currently connected monitors.                                                                 |
| [CpuInfo][6]                        | Gets detected information about the CPU.                                                               |
| [GpuInfo][7]                        | Gets detected information about the GPU.                                                               |

## Methods

| Name     | Summary                                                                                                                                                      |
|----------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Run][8] | Initializes windowing utilities, executes `startup` and then continuously processes window events until all windows are closed. This is a blocking function. |

[0]: ../Heirloom.Platforms.Desktop.md
[1]: Heirloom.Desktop.Application.SupportsTransparentFramebuffer.md
[2]: Heirloom.Desktop.Application.IsInitialized.md
[3]: Heirloom.Desktop.Application.Windows.md
[4]: Heirloom.Desktop.Application.DefaultMonitor.md
[5]: Heirloom.Desktop.Application.Monitors.md
[6]: Heirloom.Desktop.Application.CpuInfo.md
[7]: Heirloom.Desktop.Application.GpuInfo.md
[8]: Heirloom.Desktop.Application.Run.md
