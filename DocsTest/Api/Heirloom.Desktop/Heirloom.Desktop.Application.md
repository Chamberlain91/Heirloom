# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Application (Static Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

| Properties                                  | Summary                                                                                                |
|---------------------------------------------|--------------------------------------------------------------------------------------------------------|
| [GraphicsAdapter](#GRAP3DF9)                | Gets the graphics adapter.                                                                             |
| [GraphicsFactory](#GRAP8604)                | Gest the graphics factory.                                                                             |
| [SupportsTransparentFramebuffer](#SUPPE5CC) | Gets a value that determines if transparent window framebuffers are supported on this device/platform. |
| [IsInitialized](#ISINBEED)                  | Gets a value determining if the application has been initialized.                                      |
| [Windows](#WIND241D)                        | Gets a read-only list of currently opened windows.                                                     |
| [DefaultMonitor](#DEFAADFF)                 | The default (primary) monitor.                                                                         |
| [Monitors](#MONICF21)                       | Gets all currently connected monitors.                                                                 |
| [CpuInfo](#CPUIF6D5)                        | Gets detected information about the CPU.                                                               |
| [GpuInfo](#GPUIF6D3)                        | Gets detected information about the GPU.                                                               |

| Methods         | Summary                                                                                                                                                         |
|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Run](#RUN3501) | Initializes windowing utilities, executes `initialize` and then continuously processes window events until all windows are closed. This is a blocking function. |

### Properties

#### <a name="GRAP3DF9"></a> GraphicsAdapter : [GraphicsAdapter](../Heirloom.Drawing/Heirloom.Drawing.GraphicsAdapter.md)

<small>`Static`</small>

Gets the graphics adapter.

#### <a name="GRAP8604"></a> GraphicsFactory : [IWindowGraphicsFactory](Heirloom.Desktop.IWindowGraphicsFactory.md)

<small>`Static`</small>

Gest the graphics factory.

#### <a name="SUPPE5CC"></a> SupportsTransparentFramebuffer : bool

<small>`Static`, `Read Only`</small>

Gets a value that determines if transparent window framebuffers are supported on this device/platform.

#### <a name="ISINBEED"></a> IsInitialized : bool

<small>`Static`, `Read Only`</small>

Gets a value determining if the application has been initialized.

#### <a name="WIND241D"></a> Windows : IReadOnlyList\<Window>

<small>`Static`, `Read Only`</small>

Gets a read-only list of currently opened windows.

#### <a name="DEFAADFF"></a> DefaultMonitor : [Monitor](Heirloom.Desktop.Monitor.md)

<small>`Static`, `Read Only`</small>

The default (primary) monitor.

#### <a name="MONICF21"></a> Monitors : IEnumerable\<Monitor>

<small>`Static`, `Read Only`</small>

Gets all currently connected monitors.

#### <a name="CPUIF6D5"></a> CpuInfo : [CpuInfo](Heirloom.Desktop.Hardware.CpuInfo.md)

<small>`Static`, `Read Only`</small>

Gets detected information about the CPU.

#### <a name="GPUIF6D3"></a> GpuInfo : [GpuInfo](Heirloom.Desktop.Hardware.GpuInfo.md)

<small>`Static`, `Read Only`</small>

Gets detected information about the GPU.

### Methods

#### <a name="RUN(FF6A"></a> Run(Action initialize, [GraphicsBackend](Heirloom.Desktop.GraphicsBackend.md) graphicsBackend = OpenGLES) : void
<small>`Static`</small>

Initializes windowing utilities, executes `initialize` and then continuously processes window events until all windows are closed. This is a blocking function.


