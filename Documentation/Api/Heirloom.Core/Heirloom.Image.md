# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Image

> **Namespace**: [Heirloom][0]  

```cs
public sealed class Image : ImageSource
```

### Inherits

[ImageSource][1]

#### Properties

[Size][2]

#### Methods

[GetPixel][3], [GetPixels][4], [SetPixel][5], [SetPixels][6], [Clear][7], [Flip][8], [CopyTo][9], [Clone][10]

#### Static Fields

[MaxImageDimension][11]

#### Static Methods

[CreateCheckerboardPattern][12], [CreateGridPattern][13], [CreateColor][14], [CreateNoise][15], [Copy][16], [WriteAsPng][17], [WriteAsJpg][18], [Load][19]

## Fields

| Name                    | Summary |
|-------------------------|---------|
| [MaxImageDimension][11] |         |

## Properties

| Name      | Summary |
|-----------|---------|
| [Size][2] |         |

## Methods

| Name                            | Summary                                                                           |
|---------------------------------|-----------------------------------------------------------------------------------|
| [GetPixel][3]                   |                                                                                   |
| [GetPixel][3]                   |                                                                                   |
| [GetPixels][4]                  |                                                                                   |
| [SetPixel][5]                   |                                                                                   |
| [SetPixel][5]                   |                                                                                   |
| [SetPixels][6]                  |                                                                                   |
| [Clear][7]                      | Sets all pixels in the image to the specified color.                              |
| [Flip][8]                       | Flips the image on the specified axis.                                            |
| [CopyTo][9]                     |                                                                                   |
| [CopyTo][9]                     |                                                                                   |
| [Clone][10]                     | Creates a clone of this image.                                                    |
| [CreateCheckerboardPattern][12] | Create an image with checkerboard pattern.                                        |
| [CreateCheckerboardPattern][12] | Create an image with checkerboard pattern.                                        |
| [CreateGridPattern][13]         | Create an image with a grid pattern.                                              |
| [CreateGridPattern][13]         | Create an image with a grid pattern.                                              |
| [CreateColor][14]               | Creates an image filled with a solid color.                                       |
| [CreateColor][14]               | Creates an image filled with a solid color.                                       |
| [CreateNoise][15]               | Creates an image filled with noise.                                               |
| [CreateNoise][15]               | Creates an image filled with noise.                                               |
| [CreateNoise][15]               | Creates an image filled with noise, provided with an instance of [INoise2D][20] . |
| [CreateNoise][15]               | Creates an image filled with noise, provided with an instance of [INoise2D][20] . |
| [Copy][16]                      |                                                                                   |
| [Copy][16]                      |                                                                                   |
| [Copy][16]                      |                                                                                   |
| [Copy][16]                      |                                                                                   |
| [WriteAsPng][17]                |                                                                                   |
| [WriteAsJpg][18]                |                                                                                   |
| [Load][19]                      |                                                                                   |
| [Load][19]                      |                                                                                   |

[0]: ../Heirloom.Core.md
[1]: Heirloom.ImageSource.md
[2]: Heirloom.Image.Size.md
[3]: Heirloom.Image.GetPixel.md
[4]: Heirloom.Image.GetPixels.md
[5]: Heirloom.Image.SetPixel.md
[6]: Heirloom.Image.SetPixels.md
[7]: Heirloom.Image.Clear.md
[8]: Heirloom.Image.Flip.md
[9]: Heirloom.Image.CopyTo.md
[10]: Heirloom.Image.Clone.md
[11]: Heirloom.Image.MaxImageDimension.md
[12]: Heirloom.Image.CreateCheckerboardPattern.md
[13]: Heirloom.Image.CreateGridPattern.md
[14]: Heirloom.Image.CreateColor.md
[15]: Heirloom.Image.CreateNoise.md
[16]: Heirloom.Image.Copy.md
[17]: Heirloom.Image.WriteAsPng.md
[18]: Heirloom.Image.WriteAsJpg.md
[19]: Heirloom.Image.Load.md
[20]: Heirloom.INoise2D.md
