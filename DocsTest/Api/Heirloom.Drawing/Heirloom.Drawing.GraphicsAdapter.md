# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## GraphicsAdapter (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

| Properties                  | Summary                                                                         |
|-----------------------------|---------------------------------------------------------------------------------|
| [Instance](#INST4FAA)       |                                                                                 |
| [Capabilities](#CAPAE766)   | Gets the capabilities of the graphics adapter associated with this application. |
| [ShaderFactory](#SHADDBDB)  | Implementation of shader resources.                                             |
| [SurfaceFactory](#SURF5A90) | Implementation of shader resources.                                             |

| Methods                           | Summary |
|-----------------------------------|---------|
| [QueryCapabilities](#QUER2836)    |         |
| [CreateSurfaceFactory](#CREA3B6F) |         |
| [CreateShaderFactory](#CREA430C)  |         |
| [Dispose](#DISP8A0D)              |         |
| [Dispose](#DISP8A0D)              |         |

### Constructors

#### GraphicsAdapter()

### Properties

#### <a name="INST4FAA"></a> Instance : [GraphicsAdapter](Heirloom.Drawing.GraphicsAdapter.md)

<small>`Static`, `Read Only`</small>

#### <a name="CAPAE766"></a> Capabilities : [GraphicsCapabilities](Heirloom.Drawing.GraphicsCapabilities.md)

<small>`Static`, `Read Only`</small>

Gets the capabilities of the graphics adapter associated with this application.

#### <a name="SHADDBDB"></a> ShaderFactory : [GraphicsAdapter.IShaderFactory](Heirloom.Drawing.GraphicsAdapter.IShaderFactory.md)

<small>`Static`</small>

Implementation of shader resources.

#### <a name="SURF5A90"></a> SurfaceFactory : [GraphicsAdapter.ISurfaceFactory](Heirloom.Drawing.GraphicsAdapter.ISurfaceFactory.md)

<small>`Static`</small>

Implementation of shader resources.

### Methods

#### <a name="QUER7FAB"></a> QueryCapabilities() : [GraphicsCapabilities](Heirloom.Drawing.GraphicsCapabilities.md)
<small>`Abstract`, `Protected`</small>

#### <a name="CREAF26A"></a> CreateSurfaceFactory() : [GraphicsAdapter.ISurfaceFactory](Heirloom.Drawing.GraphicsAdapter.ISurfaceFactory.md)
<small>`Abstract`, `Protected`</small>

#### <a name="CREA73D9"></a> CreateShaderFactory() : [GraphicsAdapter.IShaderFactory](Heirloom.Drawing.GraphicsAdapter.IShaderFactory.md)
<small>`Abstract`, `Protected`</small>

#### <a name="DISPFDE7"></a> Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DISP4E62"></a> Dispose() : void
<small>`Virtual`</small>

