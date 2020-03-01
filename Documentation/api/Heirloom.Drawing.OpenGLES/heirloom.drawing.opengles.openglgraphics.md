# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../heirloom.drawing.opengles/heirloom.drawing.opengles.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## OpenGLGraphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES</sub></small>  
<small>**Inherits**: [Graphics](../heirloom.drawing/heirloom.drawing.graphics.md)</small>  

| Properties | Summary |
|------------|---------|
| [GlobalTransform](#GLO9D3F3F33) |  |
| [InverseGlobalTransform](#INV9F065FB7) |  |
| [ViewportScreen](#VIE9EEFEE58) |  |
| [Viewport](#VIE365B3434) |  |
| [Blending](#BLEF02A3CD5) |  |
| [Color](#COLD1229651) |  |
| [Surface](#SUR40785EE9) |  |
| [Shader](#SHA5D122CB9) |  |

| Methods | Summary |
|---------|---------|
| [MakeCurrent](#MAK22754DC6) |  |
| [ComputeViewportRect](#COMCF9BB544) |  |
| [SetBufferUniform<T>](#SET365DDD71) |  |
| [SetBufferUniform](#SET4B0F835B) |  |
| [SetBufferUniform](#SET87B239BA) | Writes a matrix to the given address with each row aligned to 16 bytes. |
| [GrabPixels](#GRA9953F74) |  |
| [Clear](#CLEDF014F91) |  |
| [DrawMesh](#DRA4623266B) |  |
| [Flush](#FLU2F0EB18F) |  |
| [GetDrawCounts](#GETB898FD0F) |  |
| [EndFrame](#ENDE20271D1) |  |
| [Dispose](#DISFDE72264) |  |

### Constructors

#### OpenGLGraphics([MultisampleQuality](../heirloom.drawing/heirloom.drawing.multisamplequality.md) multisample)

### Properties

#### <a name="GLO9D3F3F33"></a>GlobalTransform : [Matrix](../heirloom.math/heirloom.math.matrix.md)


#### <a name="INV9F065FB7"></a>InverseGlobalTransform : [Matrix](../heirloom.math/heirloom.math.matrix.md)

<small>`Read Only`</small>

#### <a name="VIE9EEFEE58"></a>ViewportScreen : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)


#### <a name="VIE365B3434"></a>Viewport : [Rectangle](../heirloom.math/heirloom.math.rectangle.md)


#### <a name="BLEF02A3CD5"></a>Blending : [Blending](../heirloom.drawing/heirloom.drawing.blending.md)


#### <a name="COLD1229651"></a>Color : [Color](../heirloom.drawing/heirloom.drawing.color.md)


#### <a name="SUR40785EE9"></a>Surface : [Surface](../heirloom.drawing/heirloom.drawing.surface.md)


#### <a name="SHA5D122CB9"></a>Shader : [Shader](../heirloom.drawing/heirloom.drawing.shader.md)


### Methods

#### <a name="MAK22754DC6"></a>MakeCurrent() : void

<small>`Abstract`, `Protected`</small>

#### <a name="COMCF9BB544"></a>ComputeViewportRect() : void

<small>`Protected`</small>

#### <a name="SET365DDD71"></a>SetBufferUniform<T>(string name, T data) : void



#### <a name="SET4B0F835B"></a>SetBufferUniform(string name, void data, int offset, int size) : void



#### <a name="SET87B239BA"></a>SetBufferUniform(string name, [Matrix](../heirloom.math/heirloom.math.matrix.md) matrix) : void


Writes a matrix to the given address with each row aligned to 16 bytes.


#### <a name="GRA9953F74"></a>GrabPixels([IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) region) : [Image](../heirloom.drawing/heirloom.drawing.image.md)

<small>`Virtual`</small>


#### <a name="CLEDF014F91"></a>Clear([Color](../heirloom.drawing/heirloom.drawing.color.md) color) : void

<small>`Virtual`</small>


#### <a name="DRA4623266B"></a>DrawMesh([ImageSource](../heirloom.drawing/heirloom.drawing.imagesource.md) source, [Mesh](../heirloom.drawing/heirloom.drawing.mesh.md) mesh, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void

<small>`Virtual`</small>


#### <a name="FLU2F0EB18F"></a>Flush() : void

<small>`Virtual`</small>

#### <a name="GETB898FD0F"></a>GetDrawCounts() : [Graphics.DrawCounts](../heirloom.drawing/heirloom.drawing.graphics.drawcounts.md)

<small>`Virtual`, `Protected`</small>

#### <a name="ENDE20271D1"></a>EndFrame() : void

<small>`Virtual`, `Protected`</small>

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void

<small>`Virtual`, `Protected`</small>


