# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsAdapter (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</small>  
<small>**Interfaces**: IDisposable</small>  

| Properties                     | Summary                                                                         |
|--------------------------------|---------------------------------------------------------------------------------|
| [Instance](#INS4FAA4721)       |                                                                                 |
| [Capabilities](#CAPE7660DA4)   | Gets the capabilities of the graphics adapter associated with this application. |
| [ShaderFactory](#SHADBDB3BCD)  | Implementation of shader resources.                                             |
| [SurfaceFactory](#SUR5A900AE5) | Implementation of shader resources.                                             |

| Methods                              | Summary |
|--------------------------------------|---------|
| [QueryCapabilities](#QUE7FAB208E)    |         |
| [CreateSurfaceFactory](#CREF26AA94D) |         |
| [CreateShaderFactory](#CRE73D93191)  |         |
| [Dispose](#DISFDE72264)              |         |
| [Dispose](#DIS4E62D250)              |         |

### Constructors

#### GraphicsAdapter()

### Properties

#### <a name="INS4FAA4721"></a>Instance : [GraphicsAdapter](Heirloom.Drawing.GraphicsAdapter.md)

<small>`Static`, `Read Only`</small>

#### <a name="CAPE7660DA4"></a>Capabilities : [GraphicsCapabilities](Heirloom.Drawing.GraphicsCapabilities.md)

<small>`Static`, `Read Only`</small>

Gets the capabilities of the graphics adapter associated with this application.

#### <a name="SHADBDB3BCD"></a>ShaderFactory : [GraphicsAdapter.IShaderFactory](Heirloom.Drawing.GraphicsAdapter.IShaderFactory.md)

<small>`Static`</small>

Implementation of shader resources.

#### <a name="SUR5A900AE5"></a>SurfaceFactory : [GraphicsAdapter.ISurfaceFactory](Heirloom.Drawing.GraphicsAdapter.ISurfaceFactory.md)

<small>`Static`</small>

Implementation of shader resources.

### Methods

#### <a name="QUE7FAB208E"></a>QueryCapabilities() : [GraphicsCapabilities](Heirloom.Drawing.GraphicsCapabilities.md)
<small>`Abstract`, `Protected`</small>

#### <a name="CREF26AA94D"></a>CreateSurfaceFactory() : [GraphicsAdapter.ISurfaceFactory](Heirloom.Drawing.GraphicsAdapter.ISurfaceFactory.md)
<small>`Abstract`, `Protected`</small>

#### <a name="CRE73D93191"></a>CreateShaderFactory() : [GraphicsAdapter.IShaderFactory](Heirloom.Drawing.GraphicsAdapter.IShaderFactory.md)
<small>`Abstract`, `Protected`</small>

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void
<small>`Virtual`</small>

