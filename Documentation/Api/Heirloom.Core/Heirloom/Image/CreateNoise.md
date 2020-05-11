# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Image.CreateNoise (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Image][1]

### CreateNoise(IntSize, float, int, float, Vector)

Creates an image filled with noise.

```cs
public static Image CreateNoise(IntSize size, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

| Name        | Type         | Summary                                 |
|-------------|--------------|-----------------------------------------|
| size        | [IntSize][2] | Size of the image in pixels.            |
| scale       | `float`      | The approximate size of a 'noise blob'. |
| octaves     | `int`        | Number of noise layers.                 |
| persistence | `float`      | How persistent each noise layer is.     |
| offset      | [Vector][3]  |                                         |

> **Returns** - [Image][1] - A noisy image with noise generated on all four components.

### CreateNoise(int, int, float, int, float, Vector)

Creates an image filled with noise.

```cs
public static Image CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

| Name        | Type        | Summary                                 |
|-------------|-------------|-----------------------------------------|
| width       | `int`       | Width of the image in pixels.           |
| height      | `int`       | Height of the image in pixels.          |
| scale       | `float`     | The approximate size of a 'noise blob'. |
| octaves     | `int`       | Number of noise layers.                 |
| persistence | `float`     | How persistent each noise layer is.     |
| offset      | [Vector][3] |                                         |

> **Returns** - [Image][1] - A noisy image with noise generated on all four components.

### CreateNoise(IntSize, INoise2D, float, int, float, Vector)

Creates an image filled with noise, provided with an instance of [INoise2D][4] .

```cs
public static Image CreateNoise(IntSize size, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

| Name        | Type          | Summary                                 |
|-------------|---------------|-----------------------------------------|
| size        | [IntSize][2]  | Size of the image in pixels.            |
| noise       | [INoise2D][4] | A 2D noise generator.                   |
| scale       | `float`       | The approximate size of a 'noise blob'. |
| octaves     | `int`         | Number of noise layers.                 |
| persistence | `float`       | How persistent each noise layer is.     |
| offset      | [Vector][3]   |                                         |

> **Returns** - [Image][1] - A noisy image with noise generated on all four components.

### CreateNoise(int, int, INoise2D, float, int, float, Vector)

Creates an image filled with noise, provided with an instance of [INoise2D][4] .

```cs
public static Image CreateNoise(int width, int height, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

| Name        | Type          | Summary                                 |
|-------------|---------------|-----------------------------------------|
| width       | `int`         | Width of the image in pixels.           |
| height      | `int`         | Height of the image in pixels.          |
| noise       | [INoise2D][4] | A 2D noise generator.                   |
| scale       | `float`       | The approximate size of a 'noise blob'. |
| octaves     | `int`         | Number of noise layers.                 |
| persistence | `float`       | How persistent each noise layer is.     |
| offset      | [Vector][3]   |                                         |

> **Returns** - [Image][1] - A noisy image with noise generated on all four components.

[0]: ../../../Heirloom.Core.md
[1]: ../Image.md
[2]: ../IntSize.md
[3]: ../Vector.md
[4]: ../INoise2D.md
