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

[Size][3]

### Methods

[Clear][4], [Clone][5], [CopyTo][6], [Flip][7], [GetPixel][8], [GetPixels][9], [SetPixel][10], [SetPixels][11]

### Static Methods

[Copy][12], [CreateCheckerboardPattern][13], [CreateColor][14], [CreateGridPattern][15], [CreateNoise][16], [Load][17], [WriteAsJpg][18], [WriteAsPng][19]

## Fields

| Name                   | Type  | Summary |
|------------------------|-------|---------|
| [MaxImageDimension][2] | `int` |         |

## Properties

#### Instance

| Name      | Type          | Summary |
|-----------|---------------|---------|
| [Size][3] | [IntSize][20] |         |

## Methods

#### Instance

| Name                            | Return Type        | Summary                                              |
|---------------------------------|--------------------|------------------------------------------------------|
| [Clear(ColorBytes)][4]          | `void`             | Sets all pixels in the image to the specified color. |
| [Clone()][5]                    | [Image][21]        | Creates a clone of this image.                       |
| [CopyTo(in IntRectangle...][6]  | `void`             |                                                      |
| [CopyTo(Image, in IntVe...][6]  | `void`             |                                                      |
| [Flip(Axis)][7]                 | `void`             | Flips the image on the specified axis.               |
| [GetPixel(int, int)][8]         | [ColorBytes][22]   |                                                      |
| [GetPixel(IntVector)][8]        | [ColorBytes][22]   |                                                      |
| [GetPixels()][9]                | [ColorBytes[]][22] |                                                      |
| [SetPixel(int, int, in ...][10] | `void`             |                                                      |
| [SetPixel(IntVector, in...][10] | `void`             |                                                      |
| [SetPixels(ColorBytes[])][11]   | `void`             |                                                      |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Copy(Image, in IntRect...][12] | `void`      |                                                                        |
| [Copy(Image, in IntRect...][12] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][12] | `void`      |                                                                        |
| [Copy(ColorBytes*, int,...][12] | `void`      |                                                                        |
| [CreateCheckerboardPatt...][13] | [Image][21] | Create an image with checkerboard pattern.                             |
| [CreateCheckerboardPatt...][13] | [Image][21] | Create an image with checkerboard pattern.                             |
| [CreateColor(IntSize, C...][14] | [Image][21] | Creates an image filled with a solid color.                            |
| [CreateColor(int, int, ...][14] | [Image][21] | Creates an image filled with a solid color.                            |
| [CreateGridPattern(IntS...][15] | [Image][21] | Create an image with a grid pattern.                                   |
| [CreateGridPattern(int,...][15] | [Image][21] | Create an image with a grid pattern.                                   |
| [CreateNoise(IntSize, f...][16] | [Image][21] | Creates an image filled with noise.                                    |
| [CreateNoise(int, int, ...][16] | [Image][21] | Creates an image filled with noise.                                    |
| [CreateNoise(IntSize, I...][16] | [Image][21] | Creates an image filled with noise, provided with an instance of IN... |
| [CreateNoise(int, int, ...][16] | [Image][21] | Creates an image filled with noise, provided with an instance of IN... |
| [Load(Stream)][17]              | [Image][21] |                                                                        |
| [Load(byte[])][17]              | [Image][21] |                                                                        |
| [WriteAsJpg(Image, Stre...][18] | `void`      |                                                                        |
| [WriteAsPng(Image, Stream)][19] | `void`      |                                                                        |

[0]: ../../Heirloom.Core.md
[1]: ImageSource.md
[2]: Image/MaxImageDimension.md
[3]: Image/Size.md
[4]: Image/Clear.md
[5]: Image/Clone.md
[6]: Image/CopyTo.md
[7]: Image/Flip.md
[8]: Image/GetPixel.md
[9]: Image/GetPixels.md
[10]: Image/SetPixel.md
[11]: Image/SetPixels.md
[12]: Image/Copy.md
[13]: Image/CreateCheckerboardPattern.md
[14]: Image/CreateColor.md
[15]: Image/CreateGridPattern.md
[16]: Image/CreateNoise.md
[17]: Image/Load.md
[18]: Image/WriteAsJpg.md
[19]: Image/WriteAsPng.md
[20]: IntSize.md
[21]: Image.md
[22]: ColorBytes.md
