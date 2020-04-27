# Image.CreateNoise

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Image][1]  

--------------------------------------------------------------------------------

### CreateNoise(IntSize, float, int, float, Vector)

Creates an image filled with noise.

```cs
public Image CreateNoise(IntSize size, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

### CreateNoise(int, int, float, int, float, Vector)

Creates an image filled with noise.

```cs
public Image CreateNoise(int width, int height, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

### CreateNoise(IntSize, INoise2D, float, int, float, Vector)

Creates an image filled with noise, provided with an instance of [INoise2D][2] .

```cs
public Image CreateNoise(IntSize size, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

### CreateNoise(int, int, INoise2D, float, int, float, Vector)

Creates an image filled with noise, provided with an instance of [INoise2D][2] .

```cs
public Image CreateNoise(int width, int height, INoise2D noise, float scale = 1, int octaves = 4, float persistence = 0.5, Vector offset = null)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Image.md
[2]: Heirloom.INoise2D.md
