# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Image (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [ImageSource](Heirloom.Drawing.ImageSource.md)</small>  

| Properties          | Summary                              |
|---------------------|--------------------------------------|
| [Width](#WIDT6892)  | The width of the image (in pixels).  |
| [Height](#HEIGE098) | The height of the image (in pixels). |
| [Size](#SIZE9C93)   |                                      |

| Methods                                | Summary                                              |
|----------------------------------------|------------------------------------------------------|
| [GetPixel](#GETPE29B)                  |                                                      |
| [GetPixel](#GETPE29B)                  |                                                      |
| [GetPixels](#GETP6329)                 |                                                      |
| [SetPixel](#SETPE2A5)                  |                                                      |
| [SetPixel](#SETPE2A5)                  |                                                      |
| [SetPixels](#SETP647C)                 |                                                      |
| [Clear](#CLEA3BB2)                     | Sets all pixels in the image to the specified color. |
| [Flip](#FLIP1AC5)                      | Flips the image on the specified axis.               |
| [CopyTo](#COPY3AB2)                    |                                                      |
| [CopyTo](#COPY3AB2)                    |                                                      |
| [Clone](#CLONDE59)                     | Creates a clone of this image.                       |
| [CreateCheckerboardPattern](#CREAA37B) | Create an image with checkerboard pattern.           |
| [CreateGridPattern](#CREAF785)         | Create an image with a grid pattern.                 |
| [CreateColor](#CREAF1DE)               |                                                      |
| [CreateNoise](#CREA7C7C)               |                                                      |
| [CreateNoise](#CREA7C7C)               |                                                      |
| [Copy](#COPY982B)                      |                                                      |
| [Copy](#COPY982B)                      |                                                      |
| [Copy](#COPY982B)                      |                                                      |
| [Copy](#COPY982B)                      |                                                      |
| [WriteAsPng](#WRIT8540)                |                                                      |
| [WriteAsJpg](#WRITFA39)                |                                                      |
| [Load](#LOADC571)                      |                                                      |
| [Load](#LOADC571)                      |                                                      |

### Constructors

#### Image(string path)

Loads an image by a file path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Image(Stream stream)

Loads an image from a stream.

#### Image(byte file)

Loads an image directly from a block of bytes.

#### Image([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size)

#### Image(int width, int height)

### Properties

#### <a name="WIDT6892"></a> Width : int

<small>`Read Only`</small>

The width of the image (in pixels).

#### <a name="HEIGE098"></a> Height : int

<small>`Read Only`</small>

The height of the image (in pixels).

#### <a name="SIZE9C93"></a> Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


### Methods

#### <a name="GETP6D6C"></a> GetPixel(int x, int y) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)


#### <a name="GETPB152"></a> GetPixel([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)


#### <a name="GETP33DB"></a> GetPixels() : [ColorBytes[]](Heirloom.Drawing.ColorBytes.md)

#### <a name="SETP65AC"></a> SetPixel(int x, int y, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) color) : void


#### <a name="SETP695E"></a> SetPixel([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) color) : void


#### <a name="SETP839E"></a> SetPixels([ColorBytes[]](Heirloom.Drawing.ColorBytes.md) pixels) : void


#### <a name="CLEAE206"></a> Clear([ColorBytes](Heirloom.Drawing.ColorBytes.md) pixel) : void

Sets all pixels in the image to the specified color.


#### <a name="FLIP52E4"></a> Flip([Axis](Heirloom.Drawing.Axis.md) axis) : void

Flips the image on the specified axis.

<small>**axis**: <param name="axis"></param></small>  

#### <a name="COPY6964"></a> CopyTo(in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void


#### <a name="COPYBC09"></a> CopyTo([Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void


#### <a name="CLONA49B"></a> Clone() : [Image](Heirloom.Drawing.Image.md)

Creates a clone of this image.

#### <a name="CREA6BF3"></a> CreateCheckerboardPattern(int width, int height, [Color](Heirloom.Drawing.Color.md) color, int cellSize = 16) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>

Create an image with checkerboard pattern.

<small>**width**: <param name="width">Width of the image in pixels.</param></small>  
<small>**height**: <param name="height">Height of the image in pixels.</param></small>  
<small>**color**: <param name="color">Color to base the checkerboard pattern on.</param></small>  
<small>**cellSize**: <param name="cellSize">Size of each "checker" in the checkerboard.</param></small>  

#### <a name="CREA5416"></a> CreateGridPattern(int width, int height, [Color](Heirloom.Drawing.Color.md) color, int cellSize, int borderWidth = 1) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>

Create an image with a grid pattern.

<small>**width**: <param name="width">Width of the image in pixels.</param></small>  
<small>**height**: <param name="height">Height of the image in pixels.</param></small>  
<small>**color**: <param name="color">Color to base the grid pattern on.</param></small>  
<small>**borderWidth**: <param name="borderWidth">Size of the line between each cell.</param></small>  

#### <a name="CREA9126"></a> CreateColor(int width, int height, [Color](Heirloom.Drawing.Color.md) color) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>


#### <a name="CREABAD6"></a> CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>


#### <a name="CREA9704"></a> CreateNoise(int width, int height, [INoise2D](../Heirloom.Math/Heirloom.Math.INoise2D.md) noise, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>


#### <a name="COPYA37F"></a> Copy([Image](Heirloom.Drawing.Image.md) source, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void
<small>`Static`</small>


#### <a name="COPYDA10"></a> Copy([Image](Heirloom.Drawing.Image.md) source, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [ColorBytes*](Heirloom.Drawing.ColorBytes.md) target, int targetWidth, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void
<small>`Static`</small>


#### <a name="COPYABF7"></a> Copy([ColorBytes*](Heirloom.Drawing.ColorBytes.md) sourcePtr, int sourceWidth, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void
<small>`Static`</small>


#### <a name="COPYE53B"></a> Copy([ColorBytes*](Heirloom.Drawing.ColorBytes.md) source, int sourceWidth, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [ColorBytes*](Heirloom.Drawing.ColorBytes.md) target, int targetWidth, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void
<small>`Static`</small>


#### <a name="WRIT2154"></a> WriteAsPng([Image](Heirloom.Drawing.Image.md) image, Stream stream) : void
<small>`Static`</small>


#### <a name="WRITD1C9"></a> WriteAsJpg([Image](Heirloom.Drawing.Image.md) image, Stream stream, int quality = 85) : void
<small>`Static`</small>


#### <a name="LOAD93FA"></a> Load(Stream stream) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>


#### <a name="LOAD8A00"></a> Load(byte file) : [Image](Heirloom.Drawing.Image.md)
<small>`Static`</small>


