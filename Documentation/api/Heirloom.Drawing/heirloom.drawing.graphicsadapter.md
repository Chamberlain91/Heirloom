# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsAdapter (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

| Properties | Summary |
|------------|---------|
| [Instance](#INS4FAA4721) |  |
| [Capabilities](#CAPE7660DA4) | Gets the capabilities of the graphics adapter associated with this application. |
| [ShaderFactory](#SHADBDB3BCD) | Implementation of shader resources. |
| [SurfaceFactory](#SUR5A900AE5) | Implementation of shader resources. |

| Methods | Summary |
|---------|---------|
| [QueryCapabilities](#QUEC95EDF0E) |  |
| [CreateSurfaceFactory](#CRE70C954AD) |  |
| [CreateShaderFactory](#CRE50EE0731) |  |
| [Dispose](#DISFDE72264) |  |
| [Dispose](#DIS4E62D250) |  |

### Constructors

#### GraphicsAdapter()

### Properties

#### <a name="INS4FAA4721"></a>Instance : [GraphicsAdapter](heirloom.drawing.graphicsadapter.md)

<small>`Static`, `Read Only`</small>

#### <a name="CAPE7660DA4"></a>Capabilities : [GraphicsCapabilities](heirloom.drawing.graphicscapabilities.md)

<small>`Static`, `Read Only`</small>

Gets the capabilities of the graphics adapter associated with this application.

#### <a name="SHADBDB3BCD"></a>ShaderFactory : [GraphicsAdapter.IShaderFactory](heirloom.drawing.graphicsadapter.ishaderfactory.md)

<small>`Static`</small>

Implementation of shader resources.

#### <a name="SUR5A900AE5"></a>SurfaceFactory : [GraphicsAdapter.ISurfaceFactory](heirloom.drawing.graphicsadapter.isurfacefactory.md)

<small>`Static`</small>

Implementation of shader resources.

### Methods

#### <a name="QUEC95EDF0E"></a>QueryCapabilities() : [GraphicsCapabilities](heirloom.drawing.graphicscapabilities.md)

<small>`Abstract`, `Protected`</small>

#### <a name="CRE70C954AD"></a>CreateSurfaceFactory() : [GraphicsAdapter.ISurfaceFactory](heirloom.drawing.graphicsadapter.isurfacefactory.md)

<small>`Abstract`, `Protected`</small>

#### <a name="CRE50EE0731"></a>CreateShaderFactory() : [GraphicsAdapter.IShaderFactory](heirloom.drawing.graphicsadapter.ishaderfactory.md)

<small>`Abstract`, `Protected`</small>

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void

<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void

<small>`Virtual`</small>

