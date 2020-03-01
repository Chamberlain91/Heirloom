# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Image (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [ImageSource](heirloom.drawing.imagesource.md)</small>  

| Properties | Summary |
|------------|---------|
| [Width](#WID68924896) | The width of the image (in pixels). |
| [Height](#HEIE098AAEB) | The height of the image (in pixels). |
| [Size](#SIZ9C9392F9) |  |

| Methods | Summary |
|---------|---------|
| [GetPixel](#GET24B99BDB) |  |
| [GetPixel](#GETE14FEBA2) |  |
| [GetPixels](#GET81B60ED3) |  |
| [SetPixel](#SET60290649) |  |
| [SetPixel](#SET19A741DE) |  |
| [SetPixels](#SET59581E2E) |  |
| [Clear](#CLEAE4B1499) | Sets all pixels in the image to the specified color. |
| [Flip](#FLIE11FB6E6) | Flips the image on the specified axis. |
| [CopyTo](#COPD1AE9E0F) |  |
| [CopyTo](#COP86E59634) |  |
| [Clone](#CLOC7EE886B) | Creates a clone of this image. |
| [CreateCheckerboardPattern](#CRE355EAFEE) | Create an image with checkerboard pattern. |
| [CreateGridPattern](#CRE8D536A86) | Create an image with a grid pattern. |
| [CreateColor](#CRE76E4A194) |  |
| [CreateNoise](#CRE28B548D9) |  |
| [CreateNoise](#CREE1C285E1) |  |
| [Copy](#COP8F1E392B) |  |
| [Copy](#COPD3C208E5) |  |
| [Copy](#COPEA61BBE3) |  |
| [Copy](#COP273D63C7) |  |
| [WriteAsPng](#WRIBC4185A9) |  |
| [WriteAsJpg](#WRIE704748F) |  |
| [Load](#LOA6691CB2A) |  |
| [Load](#LOA6A22E5E) |  |

### Constructors

#### Image(string path)

Loads an image by a file path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Image(Stream stream)

Loads an image from a stream.

#### Image(byte file)

Loads an image directly from a block of bytes.

#### Image([IntSize](../heirloom.math/heirloom.math.intsize.md) size)

#### Image(int width, int height)

### Properties

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

The width of the image (in pixels).

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

The height of the image (in pixels).

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../heirloom.math/heirloom.math.intsize.md)


### Methods

#### <a name="GET24B99BDB"></a>GetPixel(int x, int y) : [ColorBytes](heirloom.drawing.colorbytes.md)



#### <a name="GETE14FEBA2"></a>GetPixel([IntVector](../heirloom.math/heirloom.math.intvector.md) co) : [ColorBytes](heirloom.drawing.colorbytes.md)



#### <a name="GET81B60ED3"></a>GetPixels() : [ColorBytes[]](heirloom.drawing.colorbytes.md)


#### <a name="SET60290649"></a>SetPixel(int x, int y, in [ColorBytes](heirloom.drawing.colorbytes.md) color) : void



#### <a name="SET19A741DE"></a>SetPixel([IntVector](../heirloom.math/heirloom.math.intvector.md) co, in [ColorBytes](heirloom.drawing.colorbytes.md) color) : void



#### <a name="SET59581E2E"></a>SetPixels([ColorBytes[]](heirloom.drawing.colorbytes.md) pixels) : void



#### <a name="CLEAE4B1499"></a>Clear([ColorBytes](heirloom.drawing.colorbytes.md) pixel) : void


Sets all pixels in the image to the specified color.


#### <a name="FLIE11FB6E6"></a>Flip([Axis](heirloom.drawing.axis.md) axis) : void


Flips the image on the specified axis.

<small>**axis**: <param name="axis"></param>  
</small>

#### <a name="COPD1AE9E0F"></a>CopyTo(in [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) region, [Image](heirloom.drawing.image.md) target, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void



#### <a name="COP86E59634"></a>CopyTo([Image](heirloom.drawing.image.md) target, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void



#### <a name="CLOC7EE886B"></a>Clone() : [Image](heirloom.drawing.image.md)


Creates a clone of this image.

#### <a name="CRE355EAFEE"></a>CreateCheckerboardPattern(int width, int height, [Color](heirloom.drawing.color.md) color, int cellSize = 16) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>

Create an image with checkerboard pattern.

<small>**width**: <param name="width">Width of the image in pixels.</param>  
</small>
<small>**height**: <param name="height">Height of the image in pixels.</param>  
</small>
<small>**color**: <param name="color">Color to base the checkerboard pattern on.</param>  
</small>
<small>**cellSize**: <param name="cellSize">Size of each "checker" in the checkerboard.</param>  
</small>

#### <a name="CRE8D536A86"></a>CreateGridPattern(int width, int height, [Color](heirloom.drawing.color.md) color, int cellSize, int borderWidth = 1) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>

Create an image with a grid pattern.

<small>**width**: <param name="width">Width of the image in pixels.</param>  
</small>
<small>**height**: <param name="height">Height of the image in pixels.</param>  
</small>
<small>**color**: <param name="color">Color to base the grid pattern on.</param>  
</small>
<small>**borderWidth**: <param name="borderWidth">Size of the line between each cell.</param>  
</small>

#### <a name="CRE76E4A194"></a>CreateColor(int width, int height, [Color](heirloom.drawing.color.md) color) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>


#### <a name="CRE28B548D9"></a>CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>


#### <a name="CREE1C285E1"></a>CreateNoise(int width, int height, [INoise2D](../heirloom.math/heirloom.math.inoise2d.md) noise, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>


#### <a name="COP8F1E392B"></a>Copy([Image](heirloom.drawing.image.md) source, in [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) sourceRegion, [Image](heirloom.drawing.image.md) target, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COPD3C208E5"></a>Copy([Image](heirloom.drawing.image.md) source, in [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) sourceRegion, [ColorBytes*](heirloom.drawing.colorbytes.md) target, int targetWidth, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COPEA61BBE3"></a>Copy([ColorBytes*](heirloom.drawing.colorbytes.md) sourcePtr, int sourceWidth, in [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) sourceRegion, [Image](heirloom.drawing.image.md) target, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COP273D63C7"></a>Copy([ColorBytes*](heirloom.drawing.colorbytes.md) source, int sourceWidth, in [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) sourceRegion, [ColorBytes*](heirloom.drawing.colorbytes.md) target, int targetWidth, in [IntVector](../heirloom.math/heirloom.math.intvector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="WRIBC4185A9"></a>WriteAsPng([Image](heirloom.drawing.image.md) image, Stream stream) : void

<small>`Static`</small>


#### <a name="WRIE704748F"></a>WriteAsJpg([Image](heirloom.drawing.image.md) image, Stream stream, int quality = 85) : void

<small>`Static`</small>


#### <a name="LOA6691CB2A"></a>Load(Stream stream) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>


#### <a name="LOA6A22E5E"></a>Load(byte file) : [Image](heirloom.drawing.image.md)

<small>`Static`</small>


