# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Image (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Inherits**: [ImageSource](Heirloom.Drawing.ImageSource.md)</small>  

| Properties | Summary |
|------------|---------|
| [Width](#WID68924896) | The width of the image (in pixels). |
| [Height](#HEIE098AAEB) | The height of the image (in pixels). |
| [Size](#SIZ9C9392F9) |  |

| Methods | Summary |
|---------|---------|
| [GetPixel](#GET6D6C5BDB) |  |
| [GetPixel](#GETB152AD22) |  |
| [GetPixels](#GET33DBF793) |  |
| [SetPixel](#SET65ACED49) |  |
| [SetPixel](#SET695E975E) |  |
| [SetPixels](#SET839EE26E) |  |
| [Clear](#CLEE2064C99) | Sets all pixels in the image to the specified color. |
| [Flip](#FLI52E46586) | Flips the image on the specified axis. |
| [CopyTo](#COP6964DB6F) |  |
| [CopyTo](#COPBC09C414) |  |
| [Clone](#CLOA49B4FCB) | Creates a clone of this image. |
| [CreateCheckerboardPattern](#CRE6BF39FAE) | Create an image with checkerboard pattern. |
| [CreateGridPattern](#CRE54163986) | Create an image with a grid pattern. |
| [CreateColor](#CRE9126DF94) |  |
| [CreateNoise](#CREBAD66DF9) |  |
| [CreateNoise](#CRE97042461) |  |
| [Copy](#COPA37F06B) |  |
| [Copy](#COPDA107945) |  |
| [Copy](#COPABF7383) |  |
| [Copy](#COPE53B3007) |  |
| [WriteAsPng](#WRI21543049) |  |
| [WriteAsJpg](#WRID1C95C6F) |  |
| [Load](#LOA93FA19CA) |  |
| [Load](#LOA8A003EFE) |  |

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

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

The width of the image (in pixels).

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

The height of the image (in pixels).

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


### Methods

#### <a name="GET6D6C5BDB"></a>GetPixel(int x, int y) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)



#### <a name="GETB152AD22"></a>GetPixel([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)



#### <a name="GET33DBF793"></a>GetPixels() : [ColorBytes[]](Heirloom.Drawing.ColorBytes.md)


#### <a name="SET65ACED49"></a>SetPixel(int x, int y, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) color) : void



#### <a name="SET695E975E"></a>SetPixel([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) color) : void



#### <a name="SET839EE26E"></a>SetPixels([ColorBytes[]](Heirloom.Drawing.ColorBytes.md) pixels) : void



#### <a name="CLEE2064C99"></a>Clear([ColorBytes](Heirloom.Drawing.ColorBytes.md) pixel) : void


Sets all pixels in the image to the specified color.


#### <a name="FLI52E46586"></a>Flip([Axis](Heirloom.Drawing.Axis.md) axis) : void


Flips the image on the specified axis.

<small>**axis**: <param name="axis"></param>  
</small>

#### <a name="COP6964DB6F"></a>CopyTo(in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void



#### <a name="COPBC09C414"></a>CopyTo([Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void



#### <a name="CLOA49B4FCB"></a>Clone() : [Image](Heirloom.Drawing.Image.md)


Creates a clone of this image.

#### <a name="CRE6BF39FAE"></a>CreateCheckerboardPattern(int width, int height, [Color](Heirloom.Drawing.Color.md) color, int cellSize = 16) : [Image](Heirloom.Drawing.Image.md)

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

#### <a name="CRE54163986"></a>CreateGridPattern(int width, int height, [Color](Heirloom.Drawing.Color.md) color, int cellSize, int borderWidth = 1) : [Image](Heirloom.Drawing.Image.md)

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

#### <a name="CRE9126DF94"></a>CreateColor(int width, int height, [Color](Heirloom.Drawing.Color.md) color) : [Image](Heirloom.Drawing.Image.md)

<small>`Static`</small>


#### <a name="CREBAD66DF9"></a>CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](Heirloom.Drawing.Image.md)

<small>`Static`</small>


#### <a name="CRE97042461"></a>CreateNoise(int width, int height, [INoise2D](../Heirloom.Math/Heirloom.Math.INoise2D.md) noise, float scale = 1, int octaves = 4, float persistence = 0.5) : [Image](Heirloom.Drawing.Image.md)

<small>`Static`</small>


#### <a name="COPA37F06B"></a>Copy([Image](Heirloom.Drawing.Image.md) source, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COPDA107945"></a>Copy([Image](Heirloom.Drawing.Image.md) source, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [ColorBytes*](Heirloom.Drawing.ColorBytes.md) target, int targetWidth, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COPABF7383"></a>Copy([ColorBytes*](Heirloom.Drawing.ColorBytes.md) sourcePtr, int sourceWidth, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [Image](Heirloom.Drawing.Image.md) target, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="COPE53B3007"></a>Copy([ColorBytes*](Heirloom.Drawing.ColorBytes.md) source, int sourceWidth, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) sourceRegion, [ColorBytes*](Heirloom.Drawing.ColorBytes.md) target, int targetWidth, in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) targetOffset) : void

<small>`Static`</small>


#### <a name="WRI21543049"></a>WriteAsPng([Image](Heirloom.Drawing.Image.md) image, Stream stream) : void

<small>`Static`</small>


#### <a name="WRID1C95C6F"></a>WriteAsJpg([Image](Heirloom.Drawing.Image.md) image, Stream stream, int quality = 85) : void

<small>`Static`</small>


#### <a name="LOA93FA19CA"></a>Load(Stream stream) : [Image](Heirloom.Drawing.Image.md)

<small>`Static`</small>


#### <a name="LOA8A003EFE"></a>Load(byte file) : [Image](Heirloom.Drawing.Image.md)

<small>`Static`</small>


