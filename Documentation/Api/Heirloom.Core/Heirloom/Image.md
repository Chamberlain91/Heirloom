# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image (Class)

> **Namespace**: [Heirloom][0]

```cs
public sealed class Image : ImageSource
```

### Inherits

[ImageSource][1]

### Constants

[MaxImageDimension][2]

### Properties

[Indexer][3], [Size][4]

### Methods

[Clear][5], [Clone][6], [CopyTo][7], [Flip][8], [GetPixel][9], [GetPixels][10], [SetPixel][11], [SetPixels][12]

### Static Methods

[Copy][13], [CreateCheckerboardPattern][14], [CreateColor][15], [CreateGridPattern][16], [CreateNoise][17], [Load][18], [WriteAsJpg][19], [WriteAsPng][20]

## Fields

| Name                   | Type  | Summary |
|------------------------|-------|---------|
| [MaxImageDimension][2] | `int` |         |

## Properties

#### Instance

| Name         | Type             | Summary |
|--------------|------------------|---------|
| [Indexer][3] | [ColorBytes][21] |         |
| [Indexer][3] | [ColorBytes][21] |         |
| [Size][4]    | [IntSize][22]    |         |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                              |
|---------------------------------|--------------------|------------------------------------------------------|
| [Clear(ColorBytes)][5]          | `void`             | Sets all pixels in the image to the specified color. |
| [Clone()][6]                    | [Image][23]        | Creates a clone of this image.                       |
| [CopyTo(in IntRectangle...][7]  | `void`             |                                                      |
| [CopyTo(Image, in IntVe...][7]  | `void`             |                                                      |
| [Flip(Axis)][8]                 | `void`             | Flips the image on the specified axis.               |
| [GetPixel(int, int)][9]         | [ColorBytes][21]   |                                                      |
| [GetPixel(IntVector)][9]        | [ColorBytes][21]   |                                                      |
| [GetPixels()][10]               | [ColorBytes[]][21] |                                                      |
| [SetPixel(int, int, in ...][11] | `void`             |                                                      |
| [SetPixel(IntVector, in...][11] | `void`             |                                                      |
| [SetPixels(ColorBytes[])][12]   | `void`             |                                                      |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Copy(Image, in IntRect...][13] | `void`      |                                                                        |
| [Copy(Image, in IntRect...][13] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][13] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][13] | `void`      |                                                                        |
| [CreateCheckerboardPatt...][14] | [Image][23] | Create an image with checkerboard pattern.                             |
| [CreateCheckerboardPatt...][14] | [Image][23] | Create an image with checkerboard pattern.                             |
| [CreateColor(IntSize, C...][15] | [Image][23] | Creates an image filled with a solid color.                            |
| [CreateColor(int, int, ...][15] | [Image][23] | Creates an image filled with a solid color.                            |
| [CreateGridPattern(IntS...][16] | [Image][23] | Create an image with a grid pattern.                                   |
| [CreateGridPattern(int,...][16] | [Image][23] | Create an image with a grid pattern.                                   |
| [CreateNoise(IntSize, f...][17] | [Image][23] | Creates an image filled with noise.                                    |
| [CreateNoise(int, int, ...][17] | [Image][23] | Creates an image filled with noise.                                    |
| [CreateNoise(IntSize, I...][17] | [Image][23] | Creates an image filled with noise, provided with an instance of IN... |
| [CreateNoise(int, int, ...][17] | [Image][23] | Creates an image filled with noise, provided with an instance of IN... |
| [Load(Stream)][18]              | [Image][23] |                                                                        |
| [Load(byte[])][18]              | [Image][23] |                                                                        |
| [WriteAsJpg(Image, Stre...][19] | `void`      |                                                                        |
| [WriteAsPng(Image, Stream)][20] | `void`      |                                                                        |

[0]: ../../Heirloom.Core.md
[1]: ImageSource.md
[2]: Image/MaxImageDimension.md
[3]: Image/Indexer.md
[4]: Image/Size.md
[5]: Image/Clear.md
[6]: Image/Clone.md
[7]: Image/CopyTo.md
[8]: Image/Flip.md
[9]: Image/GetPixel.md
[10]: Image/GetPixels.md
[11]: Image/SetPixel.md
[12]: Image/SetPixels.md
[13]: Image/Copy.md
[14]: Image/CreateCheckerboardPattern.md
[15]: Image/CreateColor.md
[16]: Image/CreateGridPattern.md
[17]: Image/CreateNoise.md
[18]: Image/Load.md
[19]: Image/WriteAsJpg.md
[20]: Image/WriteAsPng.md
[21]: ColorBytes.md
[22]: IntSize.md
[23]: Image.md
