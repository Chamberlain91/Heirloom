# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## OpenGLGraphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES</sub></small>  
<small>**Inherits**: [Graphics](../Heirloom.Drawing/Heirloom.Drawing.Graphics.md)</small>  

| Properties                          | Summary |
|-------------------------------------|---------|
| [GlobalTransform](#GLOB9D3F)        |         |
| [InverseGlobalTransform](#INVE9F06) |         |
| [ViewportScreen](#VIEW9EEF)         |         |
| [Viewport](#VIEW365B)               |         |
| [Blending](#BLENF02A)               |         |
| [Color](#COLOD122)                  |         |
| [Surface](#SURF4078)                |         |
| [Shader](#SHAD5D12)                 |         |

| Methods                          | Summary                                                                 |
|----------------------------------|-------------------------------------------------------------------------|
| [MakeCurrent](#MAKEB77D)         |                                                                         |
| [ComputeViewportRect](#COMP2FD0) |                                                                         |
| [SetBufferUniform<T>](#SETBE4FE) |                                                                         |
| [SetBufferUniform](#SETB46AC)    |                                                                         |
| [SetBufferUniform](#SETB46AC)    | Writes a matrix to the given address with each row aligned to 16 bytes. |
| [GrabPixels](#GRAB1D23)          |                                                                         |
| [Clear](#CLEA3BB2)               |                                                                         |
| [DrawMesh](#DRAWDBE2)            |                                                                         |
| [Flush](#FLUSCBEB)               |                                                                         |
| [GetDrawCounts](#GETDC2CC)       |                                                                         |
| [EndFrame](#ENDFD6AD)            |                                                                         |
| [Dispose](#DISP8A0D)             |                                                                         |

### Constructors

#### OpenGLGraphics([MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md) multisample)

### Properties

#### <a name="GLOB9D3F"></a> GlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)


#### <a name="INVE9F06"></a> InverseGlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

#### <a name="VIEW9EEF"></a> ViewportScreen : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


#### <a name="VIEW365B"></a> Viewport : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)


#### <a name="BLENF02A"></a> Blending : [Blending](../Heirloom.Drawing/Heirloom.Drawing.Blending.md)


#### <a name="COLOD122"></a> Color : [Color](../Heirloom.Drawing/Heirloom.Drawing.Color.md)


#### <a name="SURF4078"></a> Surface : [Surface](../Heirloom.Drawing/Heirloom.Drawing.Surface.md)


#### <a name="SHAD5D12"></a> Shader : [Shader](../Heirloom.Drawing/Heirloom.Drawing.Shader.md)


### Methods

#### <a name="MAKE2275"></a> MakeCurrent() : void
<small>`Abstract`, `Protected`</small>

#### <a name="COMPCF9B"></a> ComputeViewportRect() : void
<small>`Protected`</small>

#### <a name="SETB365D"></a> SetBufferUniform<T>(string name, T data) : void


#### <a name="SETB4B0F"></a> SetBufferUniform(string name, void data, int offset, int size) : void


#### <a name="SETBD1D1"></a> SetBufferUniform(string name, [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) matrix) : void

Writes a matrix to the given address with each row aligned to 16 bytes.


#### <a name="GRAB28C2"></a> GrabPixels([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region) : [Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md)
<small>`Virtual`</small>


#### <a name="CLEAC8B2"></a> Clear([Color](../Heirloom.Drawing/Heirloom.Drawing.Color.md) color) : void
<small>`Virtual`</small>


#### <a name="DRAW846D"></a> DrawMesh([ImageSource](../Heirloom.Drawing/Heirloom.Drawing.ImageSource.md) source, [Mesh](../Heirloom.Drawing/Heirloom.Drawing.Mesh.md) mesh, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void
<small>`Virtual`</small>


#### <a name="FLUS2F0E"></a> Flush() : void
<small>`Virtual`</small>

#### <a name="GETDF0B9"></a> GetDrawCounts() : [Graphics.DrawCounts](../Heirloom.Drawing/Heirloom.Drawing.Graphics.DrawCounts.md)
<small>`Virtual`, `Protected`</small>

#### <a name="ENDFE202"></a> EndFrame() : void
<small>`Virtual`, `Protected`</small>

#### <a name="DISPFDE7"></a> Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


