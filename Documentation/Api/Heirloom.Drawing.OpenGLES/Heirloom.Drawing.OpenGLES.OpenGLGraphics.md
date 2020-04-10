# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## OpenGLGraphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES</small>  
<small>**Inherits**: [Graphics](../Heirloom.Drawing/Heirloom.Drawing.Graphics.md)</small>  

| Properties                             | Summary |
|----------------------------------------|---------|
| [GlobalTransform](#GLO9D3F3F33)        |         |
| [InverseGlobalTransform](#INV9F065FB7) |         |
| [ViewportScreen](#VIE9EEFEE58)         |         |
| [Viewport](#VIE365B3434)               |         |
| [Blending](#BLEF02A3CD5)               |         |
| [Color](#COLD1229651)                  |         |
| [Surface](#SUR40785EE9)                |         |
| [Shader](#SHA5D122CB9)                 |         |

| Methods                             | Summary                                                                 |
|-------------------------------------|-------------------------------------------------------------------------|
| [MakeCurrent](#MAK22754DC6)         |                                                                         |
| [ComputeViewportRect](#COMCF9BB544) |                                                                         |
| [SetBufferUniform<T>](#SET365DDD71) |                                                                         |
| [SetBufferUniform](#SET4B0F835B)    |                                                                         |
| [SetBufferUniform](#SETD1D1FF1A)    | Writes a matrix to the given address with each row aligned to 16 bytes. |
| [GrabPixels](#GRA28C2314)           |                                                                         |
| [Clear](#CLEC8B242F1)               |                                                                         |
| [DrawMesh](#DRA846D072B)            |                                                                         |
| [Flush](#FLU2F0EB18F)               |                                                                         |
| [GetDrawCounts](#GETF0B9C7EF)       |                                                                         |
| [EndFrame](#ENDE20271D1)            |                                                                         |
| [Dispose](#DISFDE72264)             |                                                                         |

### Constructors

#### OpenGLGraphics([MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md) multisample)

### Properties

#### <a name="GLO9D3F3F33"></a>GlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)


#### <a name="INV9F065FB7"></a>InverseGlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

#### <a name="VIE9EEFEE58"></a>ViewportScreen : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


#### <a name="VIE365B3434"></a>Viewport : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)


#### <a name="BLEF02A3CD5"></a>Blending : [Blending](../Heirloom.Drawing/Heirloom.Drawing.Blending.md)


#### <a name="COLD1229651"></a>Color : [Color](../Heirloom.Drawing/Heirloom.Drawing.Color.md)


#### <a name="SUR40785EE9"></a>Surface : [Surface](../Heirloom.Drawing/Heirloom.Drawing.Surface.md)


#### <a name="SHA5D122CB9"></a>Shader : [Shader](../Heirloom.Drawing/Heirloom.Drawing.Shader.md)


### Methods

#### <a name="MAK22754DC6"></a>MakeCurrent() : void
<small>`Abstract`, `Protected`</small>

#### <a name="COMCF9BB544"></a>ComputeViewportRect() : void
<small>`Protected`</small>

#### <a name="SET365DDD71"></a>SetBufferUniform<T>(string name, T data) : void


#### <a name="SET4B0F835B"></a>SetBufferUniform(string name, void data, int offset, int size) : void


#### <a name="SETD1D1FF1A"></a>SetBufferUniform(string name, [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) matrix) : void

Writes a matrix to the given address with each row aligned to 16 bytes.


#### <a name="GRA28C2314"></a>GrabPixels([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region) : [Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md)
<small>`Virtual`</small>


#### <a name="CLEC8B242F1"></a>Clear([Color](../Heirloom.Drawing/Heirloom.Drawing.Color.md) color) : void
<small>`Virtual`</small>


#### <a name="DRA846D072B"></a>DrawMesh([ImageSource](../Heirloom.Drawing/Heirloom.Drawing.ImageSource.md) source, [Mesh](../Heirloom.Drawing/Heirloom.Drawing.Mesh.md) mesh, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void
<small>`Virtual`</small>


#### <a name="FLU2F0EB18F"></a>Flush() : void
<small>`Virtual`</small>

#### <a name="GETF0B9C7EF"></a>GetDrawCounts() : [Graphics.DrawCounts](../Heirloom.Drawing/Heirloom.Drawing.Graphics.DrawCounts.md)
<small>`Virtual`, `Protected`</small>

#### <a name="ENDE20271D1"></a>EndFrame() : void
<small>`Virtual`, `Protected`</small>

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


