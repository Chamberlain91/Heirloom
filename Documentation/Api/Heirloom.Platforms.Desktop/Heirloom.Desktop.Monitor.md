# Monitor

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  

Represents a physical display on the current device.

```cs
public class Monitor
```

--------------------------------------------------------------------------------

**Properties**: [Name][4], [Width][5], [Height][6], [RefreshRate][7], [Position][8], [Workarea][9], [CurrentMode][10]

**Methods**: [GetVideoModes][11]

--------------------------------------------------------------------------------

## Properties

| Name              | Summary                                                                                                             |
|-------------------|---------------------------------------------------------------------------------------------------------------------|
| [Name][4]         | Gets the human-readable name of the monitor.                                                                        |
| [Width][5]        | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [Height][6]       | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [RefreshRate][7]  | Gets the refresh rate of the monitor (in the current video mode).                                                   |
| [Position][8]     | Gets the virtual position of the monitor (in screen units).                                                         |
| [Workarea][9]     | Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars. |
| [CurrentMode][10] | Gets the current video mode on this monitor.                                                                        |

## Methods

| Name                | Summary                                     |
|---------------------|---------------------------------------------|
| [GetVideoModes][11] | Gets all known video modes on this monitor. |

[0]: ../Heirloom.Platforms.Desktop.md
[1]: ../Heirloom.Core.md
[2]: ../Heirloom.OpenGLES.md
[3]: ../Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Monitor.Name.md
[5]: Heirloom.Desktop.Monitor.Width.md
[6]: Heirloom.Desktop.Monitor.Height.md
[7]: Heirloom.Desktop.Monitor.RefreshRate.md
[8]: Heirloom.Desktop.Monitor.Position.md
[9]: Heirloom.Desktop.Monitor.Workarea.md
[10]: Heirloom.Desktop.Monitor.CurrentMode.md
[11]: Heirloom.Desktop.Monitor.GetVideoModes.md
