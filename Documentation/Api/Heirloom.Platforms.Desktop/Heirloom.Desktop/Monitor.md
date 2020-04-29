# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]

## Monitor (Class)

> **Namespace**: [Heirloom.Desktop][0]

Represents a physical display on the current device.

```cs
public class Monitor
```

### Properties

[CurrentMode][1], [Height][2], [Name][3], [Position][4], [RefreshRate][5], [Width][6], [Workarea][7]

### Methods

[GetVideoModes][8]

## Properties

#### Instance

| Name             | Type               | Summary                                                                |
|------------------|--------------------|------------------------------------------------------------------------|
| [CurrentMode][1] | [VideoMode][9]     | Gets the current video mode on this monitor.                           |
| [Height][2]      | `int`              | Gets the width (in pixels) of the monitor (in the current video mode). |
| [Name][3]        | `string`           | Gets the human-readable name of the monitor.                           |
| [Position][4]    | [IntVector][10]    | Gets the virtual position of the monitor (in screen units).            |
| [RefreshRate][5] | `int`              | Gets the refresh rate of the monitor (in the current video mode).      |
| [Width][6]       | `int`              | Gets the width (in pixels) of the monitor (in the current video mode). |
| [Workarea][7]    | [IntRectangle][11] | Gets the work area (in screen units) of the monitor. This is the mo... |

## Methods

#### Instance

| Name                 | Return Type      | Summary                                     |
|----------------------|------------------|---------------------------------------------|
| [GetVideoModes()][8] | [VideoMode[]][9] | Gets all known video modes on this monitor. |

[0]: ../../Heirloom.Platforms.Desktop.md
[1]: Monitor/CurrentMode.md
[2]: Monitor/Height.md
[3]: Monitor/Name.md
[4]: Monitor/Position.md
[5]: Monitor/RefreshRate.md
[6]: Monitor/Width.md
[7]: Monitor/Workarea.md
[8]: Monitor/GetVideoModes.md
[9]: VideoMode.md
[10]: ../../Heirloom.Core/Heirloom/IntVector.md
[11]: ../../Heirloom.Core/Heirloom/IntRectangle.md
