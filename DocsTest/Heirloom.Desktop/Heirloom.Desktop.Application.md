# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Application (Static Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

| Properties | Summary |
|------------|---------|
| [GraphicsAdapter](#GRA3DF95FEA) | Gets the graphics adapter. |
| [GraphicsFactory](#GRA86043A3D) | Gest the graphics factory. |
| [SupportsTransparentFramebuffer](#SUPE5CC44EF) | Gets a value that determines if transparent window framebuffers are supported on this device/platform. |
| [IsInitialized](#ISIBEED663C) | Gets a value determining if the application has been initialized. |
| [Windows](#WIN241D4DB) | Gets a read-only list of currently opened windows. |
| [DefaultMonitor](#DEFADFFB257) | The default (primary) monitor. |
| [Monitors](#MONCF21587B) | Gets all currently connected monitors. |
| [CpuInfo](#CPUF6D51F94) | Gets detected information about the CPU. |
| [GpuInfo](#GPUF6D30F18) | Gets detected information about the GPU. |

| Methods | Summary |
|---------|---------|
| [Run](#RUN2A80AB85) | Initializes windowing utilities, executes `initialize` and then continuously processes window events until all windows are closed. This is a blocking function. |

### Properties

#### <a name="GRA3DF95FEA"></a>GraphicsAdapter : [GraphicsAdapter](../Heirloom.Drawing/Heirloom.Drawing.GraphicsAdapter.md)

<small>`Static`</small>

Gets the graphics adapter.

#### <a name="GRA86043A3D"></a>GraphicsFactory : [IWindowGraphicsFactory](Heirloom.Desktop.IWindowGraphicsFactory.md)

<small>`Static`</small>

Gest the graphics factory.

#### <a name="SUPE5CC44EF"></a>SupportsTransparentFramebuffer : bool

<small>`Static`, `Read Only`</small>

Gets a value that determines if transparent window framebuffers are supported on this device/platform.

#### <a name="ISIBEED663C"></a>IsInitialized : bool

<small>`Static`, `Read Only`</small>

Gets a value determining if the application has been initialized.

#### <a name="WIN241D4DB"></a>Windows : IReadOnlyList\<Window>

<small>`Static`, `Read Only`</small>

Gets a read-only list of currently opened windows.

#### <a name="DEFADFFB257"></a>DefaultMonitor : [Monitor](Heirloom.Desktop.Monitor.md)

<small>`Static`, `Read Only`</small>

The default (primary) monitor.

#### <a name="MONCF21587B"></a>Monitors : IEnumerable\<Monitor>

<small>`Static`, `Read Only`</small>

Gets all currently connected monitors.

#### <a name="CPUF6D51F94"></a>CpuInfo : [CpuInfo](Heirloom.Desktop.Hardware.CpuInfo.md)

<small>`Static`, `Read Only`</small>

Gets detected information about the CPU.

#### <a name="GPUF6D30F18"></a>GpuInfo : [GpuInfo](Heirloom.Desktop.Hardware.GpuInfo.md)

<small>`Static`, `Read Only`</small>

Gets detected information about the GPU.

### Methods

#### <a name="RUN2A80AB85"></a>Run(Action initialize, [GraphicsBackend](Heirloom.Desktop.GraphicsBackend.md) graphicsBackend = 0) : void

<small>`Static`</small>

Initializes windowing utilities, executes `initialize` and then continuously processes window events until all windows are closed. This is a blocking function.


