# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Monitor (Class)
<small>**Namespace**: Heirloom.Desktop</small>  

Represents a physical display on the current device.

| Properties                  | Summary                                                                                                             |
|-----------------------------|---------------------------------------------------------------------------------------------------------------------|
| [Name](#NAM5943D12B)        | Gets the human-readable name of the monitor.                                                                        |
| [Width](#WID68924896)       | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [Height](#HEIE098AAEB)      | Gets the width (in pixels) of the monitor (in the current video mode).                                              |
| [RefreshRate](#REFCFA57A9B) | Gets the refresh rate of the monitor (in the current video mode).                                                   |
| [Position](#POSF46C3C91)    | Gets the virtual position of the monitor (in screen units).                                                         |
| [Workarea](#WOR837EBBDE)    | Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars. |
| [CurrentMode](#CUR65B9D688) | Gets the current video mode on this monitor.                                                                        |

| Methods                       | Summary                                     |
|-------------------------------|---------------------------------------------|
| [GetVideoModes](#GET218AAA2F) | Gets all known video modes on this monitor. |

### Constructors

#### Monitor(string name, [MonitorHandle](Heirloom.Desktop.MonitorHandle.md) monitor)

### Properties

#### <a name="NAM5943D12B"></a>Name : string

<small>`Read Only`</small>

Gets the human-readable name of the monitor.

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

Gets the width (in pixels) of the monitor (in the current video mode).

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

Gets the width (in pixels) of the monitor (in the current video mode).

#### <a name="REFCFA57A9B"></a>RefreshRate : int

<small>`Read Only`</small>

Gets the refresh rate of the monitor (in the current video mode).

#### <a name="POSF46C3C91"></a>Position : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets the virtual position of the monitor (in screen units).

#### <a name="WOR837EBBDE"></a>Workarea : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

<small>`Read Only`</small>

Gets the work area (in screen units) of the monitor. This is the monitor bounds minus any global task or menu bars.

#### <a name="CUR65B9D688"></a>CurrentMode : [VideoMode](Heirloom.Desktop.VideoMode.md)

<small>`Read Only`</small>

Gets the current video mode on this monitor.

### Methods

#### <a name="GET218AAA2F"></a>GetVideoModes() : [VideoMode[]](Heirloom.Desktop.VideoMode.md)

Gets all known video modes on this monitor.

