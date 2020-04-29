# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  

## Monitor Class

> **Namespace**: [Heirloom.Desktop][0]  

Represents a physical display on the current device.

```cs
public class Monitor
```

#### Properties

[Name][1], [Width][2], [Height][3], [RefreshRate][4], [Position][5], [Workarea][6], [CurrentMode][7]

#### Methods

[GetVideoModes][8]

## Properties

| Name             | Summary                                                                                                             |
|------------------|---------------------------------------------------------------------------------------------------------------------|
| [Name][1]        | Gets the human-readable name of the monitor.                                                                        |
| [Width][2]       | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [Height][3]      | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [RefreshRate][4] | Gets the refresh rate of the monitor (in the current video mode).                                                   |
| [Position][5]    | Gets the virtual position of the monitor (in screen units).                                                         |
| [Workarea][6]    | Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars. |
| [CurrentMode][7] | Gets the current video mode on this monitor.                                                                        |

## Methods

| Name               | Summary                                     |
|--------------------|---------------------------------------------|
| [GetVideoModes][8] | Gets all known video modes on this monitor. |

[0]: ../../Heirloom.Platforms.Desktop.md
[1]: Monitor/Name.md
[2]: Monitor/Width.md
[3]: Monitor/Height.md
[4]: Monitor/RefreshRate.md
[5]: Monitor/Position.md
[6]: Monitor/Workarea.md
[7]: Monitor/CurrentMode.md
[8]: Monitor/GetVideoModes.md
