# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Monitor (Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

Represents a physical display on the current device.

| Properties               | Summary                                                                                                             |
|--------------------------|---------------------------------------------------------------------------------------------------------------------|
| [Name](#NAME5943)        | Gets the human-readable name of the monitor.                                                                        |
| [Width](#WIDT6892)       | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [Height](#HEIGE098)      | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [RefreshRate](#REFRCFA5) | Gets the refresh rate of the monitor (in the current video mode).                                                   |
| [Position](#POSIF46C)    | Gets the virtual position of the monitor (in screen units).                                                         |
| [Workarea](#WORK837E)    | Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars. |
| [CurrentMode](#CURR65B9) | Gets the current video mode on this monitor.                                                                        |

| Methods                    | Summary                                     |
|----------------------------|---------------------------------------------|
| [GetVideoModes](#GETV38C5) | Gets all known video modes on this monitor. |

### Constructors

#### Monitor(string name, [MonitorHandle](Heirloom.Desktop.MonitorHandle.md) monitor)

### Properties

#### <a name="NAME5943"></a> Name : string

<small>`Read Only`</small>

Gets the human-readable name of the monitor.

#### <a name="WIDT6892"></a> Width : int

<small>`Read Only`</small>

Gets the width (in pixels) of the monitor (in the current video mode).

#### <a name="HEIGE098"></a> Height : int

<small>`Read Only`</small>

Gets the width (in pixels) of the monitor (in the current video mode).

#### <a name="REFRCFA5"></a> RefreshRate : int

<small>`Read Only`</small>

Gets the refresh rate of the monitor (in the current video mode).

#### <a name="POSIF46C"></a> Position : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the virtual position of the monitor (in screen units).

#### <a name="WORK837E"></a> Workarea : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

<small>`Read Only`</small>

Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars.

#### <a name="CURR65B9"></a> CurrentMode : [VideoMode](Heirloom.Desktop.VideoMode.md)

<small>`Read Only`</small>

Gets the current video mode on this monitor.

### Methods

#### <a name="GETV218A"></a> GetVideoModes() : [VideoMode[]](Heirloom.Desktop.VideoMode.md)

Gets all known video modes on this monitor.

