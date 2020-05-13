# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image (Class)

> **Namespace**: [Heirloom][0]

Represents an image as a grid of [ColorBytes][1] .

```cs
public sealed class Image : ImageSource
```

### Inherits

[ImageSource][2]

### Constants

[MaxImageDimension][3]

### Properties

[Indexer][4], [Size][5]

### Methods

[Clear][6], [Clone][7], [CopyTo][8], [Flip][9], [GetPixel][10], [GetPixels][11], [SetPixel][12], [SetPixels][13], [WriteJPG][14], [WritePNG][15]

### Static Methods

[Copy][16], [CreateCheckerboardPattern][17], [CreateColor][18], [CreateGridPattern][19], [CreateNoise][20]

## Fields

| Name                   | Type  | Summary                                         |
|------------------------|-------|-------------------------------------------------|
| [MaxImageDimension][3] | `int` | The max allowable image size for any dimension. |

## Properties

#### Instance

| Name         | Type            | Summary                 |
|--------------|-----------------|-------------------------|
| [Indexer][4] | [ColorBytes][1] |                         |
| [Indexer][4] | [ColorBytes][1] |                         |
| [Size][5]    | [IntSize][21]   | The size of this image. |

## Methods

#### Instance

| Name                            | Return Type       | Summary                                                   |
|---------------------------------|-------------------|-----------------------------------------------------------|
| [Clear(ColorBytes)][6]          | `void`            | Sets all pixels in the image to the specified color.      |
| [Clone()][7]                    | [Image][22]       | Creates a clone of this image.                            |
| [CopyTo(in IntRectangle...][8]  | `void`            | Copies a region of this image to another.                 |
| [CopyTo(Image, in IntVe...][8]  | `void`            | Copies this image to another image.                       |
| [Flip(Axis)][9]                 | `void`            | Flips the image on the specified axis.                    |
| [GetPixel(int, int)][10]        | [ColorBytes][1]   | Gets the pixel at the specified coordinates.              |
| [GetPixel(IntVector)][10]       | [ColorBytes][1]   | Gets the pixel at the specified coordinates.              |
| [GetPixels()][11]               | [ColorBytes[]][1] | Gets a copy of all the pixels in this image.              |
| [GetPixels(Span<ColorBy...][11] | `void`            | Copies the image pixels into an already allocated buffer. |
| [GetPixels(ColorBytes[]...][11] | `void`            |                                                           |
| [SetPixel(int, int, in ...][12] | `void`            | Sets the color of a pixel at the specified coordinate.    |
| [SetPixel(IntVector, in...][12] | `void`            | Sets the color of a pixel at the specified coordinate.    |
| [SetPixels(ColorBytes[])][13]   | `void`            |                                                           |
| [WriteJPG(Stream, int)][14]     | `void`            | Writes the image to the stream as a PNG file format.      |
| [WritePNG(Stream)][15]          | `void`            | Writes the image to the stream as a PNG file format.      |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Copy(Image, in IntRect...][16] | `void`      | Copies a region of the `source` image into the `target` image at th... |
| [Copy(Image, in IntRect...][16] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][16] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][16] | `void`      |                                                                        |
| [CreateCheckerboardPatt...][17] | [Image][22] | Create an image with checkerboard pattern.                             |
| [CreateCheckerboardPatt...][17] | [Image][22] | Create an image with checkerboard pattern.                             |
| [CreateColor(IntSize, C...][18] | [Image][22] | Creates an image filled with a solid color.                            |
| [CreateColor(int, int, ...][18] | [Image][22] | Creates an image filled with a solid color.                            |
| [CreateGridPattern(IntS...][19] | [Image][22] | Create an image with a grid pattern.                                   |
| [CreateGridPattern(int,...][19] | [Image][22] | Create an image with a grid pattern.                                   |
| [CreateNoise(IntSize, f...][20] | [Image][22] | Creates an image filled with noise.                                    |
| [CreateNoise(int, int, ...][20] | [Image][22] | Creates an image filled with noise.                                    |
| [CreateNoise(IntSize, I...][20] | [Image][22] | Creates an image filled with noise, provided with an instance of IN... |
| [CreateNoise(int, int, ...][20] | [Image][22] | Creates an image filled with noise, provided with an instance of IN... |

[0]: ../../Heirloom.Core.md
[1]: ColorBytes.md
[2]: ImageSource.md
[3]: Image/MaxImageDimension.md
[4]: Image/Indexer.md
[5]: Image/Size.md
[6]: Image/Clear.md
[7]: Image/Clone.md
[8]: Image/CopyTo.md
[9]: Image/Flip.md
[10]: Image/GetPixel.md
[11]: Image/GetPixels.md
[12]: Image/SetPixel.md
[13]: Image/SetPixels.md
[14]: Image/WriteJPG.md
[15]: Image/WritePNG.md
[16]: Image/Copy.md
[17]: Image/CreateCheckerboardPattern.md
[18]: Image/CreateColor.md
[19]: Image/CreateGridPattern.md
[20]: Image/CreateNoise.md
[21]: IntSize.md
[22]: Image.md
