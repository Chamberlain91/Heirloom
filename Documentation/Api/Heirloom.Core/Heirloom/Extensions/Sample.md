# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Extensions.Sample

> **Namespace**: [Heirloom][0]  
> **Type**: [Extensions][1]  

### Sample(INoise1D, float, int, float)

Sample one-dimensional octave noise.

```cs
public static float Sample(INoise1D noise, float x, int octaves, float persistence = 0.5)
```

### Sample(INoise2D, in Vector)

Sample two-dimensional noise.

```cs
public static float Sample(INoise2D noise, in Vector position)
```

### Sample(INoise2D, in Vector, int, float)

Sample two-dimensional octave noise.

```cs
public static float Sample(INoise2D noise, in Vector position, int octaves, float persistence = 0.5)
```

### Sample(INoise2D, float, float, int, float)

Sample two-dimensional octave noise.

```cs
public static float Sample(INoise2D noise, float x, float y, int octaves, float persistence = 0.5)
```

### Sample(INoise3D, float, float, float, int, float)

Sample three-dimensional octave noise.

```cs
public static float Sample(INoise3D noise, float x, float y, float z, int octaves, float persistence = 0.5)
```

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
