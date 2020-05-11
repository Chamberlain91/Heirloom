# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## Application (Class)

> **Namespace**: [Heirloom.Desktop][0]

Controls a desktop application. Use this class to initialize window system and process events.

```cs
public static class Application
```

### Static Properties

[CpuInfo][1], [DefaultMonitor][2], [GpuInfo][3], [IsInitialized][4], [Monitors][5], [SupportsTransparentFramebuffer][6], [Windows][7]

### Static Methods

[Run][8], [Run\<TGameLoop>][9]

## Properties

| Name                                | Type                    | Summary                                                                |
|-------------------------------------|-------------------------|------------------------------------------------------------------------|
| [CpuInfo][1]                        | [CpuInfo][10]           | Gets detected information about the CPU.                               |
| [DefaultMonitor][2]                 | [Monitor][11]           | The default (primary) monitor.                                         |
| [GpuInfo][3]                        | [GpuInfo][12]           | Gets detected information about the GPU.                               |
| [IsInitialized][4]                  | `bool`                  | Gets a value determining if the application has been initialized.      |
| [Monitors][5]                       | `IEnumerable<Monitor>`  | Gets all currently connected monitors.                                 |
| [SupportsTransparentFramebuffer][6] | `bool`                  | Gets a value that determines if transparent window framebuffers are... |
| [Windows][7]                        | `IReadOnlyList<Window>` | Gets a read-only list of currently opened windows.                     |

## Methods

| Name                  | Return Type | Summary                                                                |
|-----------------------|-------------|------------------------------------------------------------------------|
| [Run(Action)][8]      | `void`      | Initializes windowing utilities, executes `startup` and then contin... |
| [Run<TGameLoop>()][9] | `void`      | Creates an instance of `TGameLoop` runs GameLoop.Start .               |

[0]: ../../Heirloom.Desktop.md
[1]: Application/CpuInfo.md
[2]: Application/DefaultMonitor.md
[3]: Application/GpuInfo.md
[4]: Application/IsInitialized.md
[5]: Application/Monitors.md
[6]: Application/SupportsTransparentFramebuffer.md
[7]: Application/Windows.md
[8]: Application/Run.md
[9]: Application/Run[TGameLoop].md
[10]: ../Heirloom.Desktop.Hardware/CpuInfo.md
[11]: Monitor.md
[12]: ../Heirloom.Desktop.Hardware/GpuInfo.md
